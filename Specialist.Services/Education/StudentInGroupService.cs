using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Context.Logic;
using Specialist.Entities.Lms;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Order;
using Specialist.Web.Common.Utils;
using Specialist.Entities.Profile.ViewModel.Common;
using Specialist.Services.Utils;

namespace Specialist.Services.Education
{
    public class StudentInGroupService:Repository2<StudentInGroup>
    {

        public StudentInGroupService(IContextProvider contextProvider) :
            base(contextProvider) {}
		[Dependency]
		public IRepository2<GroupTest> GroupTestService { get; set; }
		[Dependency]
		public IRepository2<Group> GroupService { get; set; }

		[Dependency]
		public IAuthService AuthService { get; set; }

	    public StudyTypeStatsVM GetStats(decimal? studentId, decimal? orgId) {
		    var openTC = "OPEN";
		    var sigs = studentId.HasValue 
				? this.GetAll(x => x.Student_ID == studentId)
				: this.GetAll(x => x.Org_ID == orgId);
		    var hoursData = sigs.Where(x => BerthTypes.AllPaidForCourses.Contains(x.BerthType_TC)).Select(x => new {
			    type = x.Group.MegaGroup_ID.HasValue ? openTC : x.PriceType_TC,
			    hours = x.Group.Hours + x.Group.HoursAdditional
		    }).GroupBy(x => x.type, x => x.hours).Select(x => new {type = x.Key, hours = x.Sum()}).ToList();
//		    var open = sigs.Where(x => x.Group.MegaGroup_ID.HasValue)
//			    .Sum(x => (decimal?) (x.Group.Hours + x.Group.HoursAdditional));
		    var hours = hoursData.Where(x => x.type != openTC).Select(x =>
			    new {type = PriceTypes.GetBaseType(x.type), x.hours})
			    .GroupBy(x => x.type, x => x.hours).ToDictionary(x => x.Key, x => x.Sum());
		    var openHours = hoursData.Where(x => x.type == openTC).Select(x => (decimal?) x.hours).Sum();
		    return new StudyTypeStatsVM {
			   Intra = (int) hours.GetValueOrDefault(PriceTypes.Main),
			   IntraExtra = (int) hours.GetValueOrDefault(PriceTypes.IntraExtra),
			   Open = (int) openHours.GetValueOrDefault(),
			   Webinar = (int) hours.GetValueOrDefault(PriceTypes.Webinar),
			   Unlimit = (int) hours.GetValueOrDefault(PriceTypes.Unlimited),
		    };
	    }

		public IQueryable<GroupTest> GetGroupTests(decimal studentId) {
			var groupIds = this.GetAll(x => x.Group.Course_TC == CourseTC.Test
				&& x.Student_ID == studentId).Select(x => x.Group_ID).ToList();
			if(!groupIds.Any())
				return Enumerable.Empty<GroupTest>().AsQueryable();
			GroupTestService.LoadWith(x => x.Test);
			GroupTestService.LoadWith(x => x.TestPassRule);
			var today = DateTime.Today;
			var groupTests = GroupTestService.GetAll(x => groupIds.Contains(x.GroupInfo.Group_ID)
				&& x.DateBegin <= today && x.DateEnd >= today);
			return groupTests;
		}


		public GroupTest GetUserGroupTest(int testId) {
			var user = AuthService.CurrentUser;
			if(user == null || !user.Student_ID.HasValue)
				return null;
			return GetGroupTests(user.Student_ID.Value)
				.FirstOrDefault(x => x.TestId == testId);
		}


		public bool CalcTestIsActive(Test test) {
			if (test.IsActive) {
				var user = AuthService.CurrentUser;
				if (test.CompanyId.HasValue) {
					return user != null && (user.CompanyID == test.CompanyId
											|| user.EmployeeCompanyID == test.CompanyId);
				}

				if (!test.IsGraduateOnly) {
					return true;
				} 
				var testForCourses = test.CourseTCSplitList;
				if (user != null && user.IsStudent &&
					this.GetAll(x => x.Student_ID == user.Student_ID &&
						BerthTypes.AllPaid.Contains(x.BerthType_TC) &&
						(testForCourses.Contains(x.Group.Course_TC) 
						  || (x.Group.Course_TC == CourseTC.DpCons 
						        && testForCourses.Contains(x.Track_TC) 
								&& x.Group.DateBeg.Value.Add(x.Group.DateBeg.Value.TimeOfDay) <= DateTime.Now))).Any()) {
					return true;
				}
				return false;

			}
			if (test.Status == TestStatus.Archive) {
				if (GetUserGroupTest(test.Id) == null) {
					return false;
				}
				return true;

			}
			return false;
		}

	    public virtual Dictionary<string, int> CompleteCountForTracks() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => 
				this.GetAll(x => x.BerthType_TC == BerthTypes.Paid && !x.Track_TC.Equals(null))
			    .GroupJoin(context.GetTable<Course>(), x => x.Track_TC, x => x.Course_TC, 
				    (sig,cs) => new {sig, cs = cs.DefaultIfEmpty()})
			    .SelectMany(x => x.cs.Select(z => z.ParentCourse_TC)).GroupBy(x => x)
			    .Select(x => new {x.Key, Count = x.Count()}).ToDictionary(x => x.Key, x => x.Count));
	    }

	    public Entities.Context.Order GetOrder(decimal sigId) {
			LoadWith(b => b.Load(x => x.Student, x => x.StudentsInGroupsCalc).And<Student>(s => s.StudentEmails));
		    return ConvertToOrder(GetByPK(sigId));
	    }

	    Entities.Context.Order ConvertToOrder(StudentInGroup sig) {
			if (sig == null) return null;
			var email = sig.Student.StudentEmails.OrderByDescending(x => x.StudentEmail_ID).FirstOrDefault()
				.GetOrDefault(x => x.Email.Trim());
			if(email.IsEmpty() || email.Contains(" ") || !email.Contains("@"))
				email = null;
		    var orderDetails = new EntitySet<OrderDetail>();
		    var orderExams = new EntitySet<OrderExam>();
			GroupService.LoadWith(x => x.Course);
		    var group = GroupService.GetByPK(sig.Group_ID);
		    if (sig.Exam_ID > 0) {
			   orderExams.Add(new OrderExam{Exam_ID = sig.Exam_ID.Value}); 
		    } else {
			orderDetails.Add(new OrderDetail{Group = group, Course_TC = group.Course_TC, Course = group.Course, Track_TC = sig.Track_TC});
			    
		    }
		    var order = new Entities.Context.Order {
			    User = new User {
				    FirstName = sig.Student.FirstName,
				    LastName = sig.Student.LastName,
				    SecondName = sig.Student.MiddleName,
				    Email = email
			    },
				IsSig = true,
			    OrderID = sig.StudentInGroup_ID,
			    IsSigPaid = sig.BerthType_TC == BerthTypes.Paid,
			    TotalPriceWithDescount = sig.PartialPayment > 0 
					? sig.PartialPayment.Value 
					: sig.Charge.GetValueOrDefault(),
			    OrderDetails = orderDetails,
				OrderExams = orderExams,
				OurOrg_TC =  null,
			    Description = "Оплата спец заказа №" + sig.StudentInGroup_ID
		    };
		    var context = new PioneerDataContext();
//		    if (PriceService.DopUslCourses().Contains(group.Course_TC)) {
//			    order.OurOrg_TC = OurOrgs.Bt;
//		    }

		    order.OurOrg_TC = context.fnGetDefaultOurOrgTC(sig.StudentInGroup_ID, null);
		    return order;
		}

	    public bool HasEducationDocument(decimal? studentId) {
		    if (studentId.HasValue) {
			    try {
				    var ctx = new PioneerDataContext();
				    return ctx.fnIsProEducation(studentId).GetValueOrDefault();
			    } catch (Exception e) {
				  Logger.Exception(e, "HasEducationDocument");  
			    }
		    }
		    return false;
	    }

    }
}