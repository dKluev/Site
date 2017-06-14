using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Mime;
using Microsoft.Practices.Unity;
using NLog;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Tests;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Order;
using System.Configuration;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Utils;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Lms;
using Specialist.Services.ViewModel;

namespace Specialist.Services.Order
{
    public class SpecialistExportService : ISpecialistExportService
    {

        [Dependency]
        public IOrderService OrderService { get; set; }

		[Dependency]
        public IRepository2<UserTest> UserTestService { get; set; }

		[Dependency]
        public IRepository2<Terrain> TerrainService { get; set; }

		[Dependency]
        public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }
        

		public void Export(Entities.Context.Order order, bool checkIfExists) {

			var alreadyInGroups = new List<decimal>();
			var groups = order.OrderDetails.Where(x => x.Group_ID.HasValue)
				.Select(x => new {Group_ID = x.Group_ID.Value, x.PriceType_TC}).ToList();
			if (checkIfExists && order.User.Student_ID.HasValue && groups.Any()) {
				alreadyInGroups = StudentInGroupService.GetAll(x =>
					x.Student_ID == order.User.Student_ID.Value
					&& groups.Contains(new {x.Group_ID, x.PriceType_TC})).Select(x => x.Group_ID).ToList();
			}
            var student = GetOrInsertStudentByUser(order.User, order.OrderID.ToString());
            var berthTypeTC = BerthTypes.NotPaid;
            var studentInGroupRepository = new Repository<StudentInGroup>(
             new SpecialistContextProvider());
        	var nextContactDate = DateTime.Today.AddDays(1);
			if (order.OrderDetails.Any(x =>
				x.Group.GetOrDefault(g => g.DateBeg) == DateTime.Today)) {
				nextContactDate = DateTime.Today;
			}
			var addEduDocs = false;
        	foreach (var orderDetail in order.OrderDetails.Where(x => 
				!alreadyInGroups.Contains(x.Group_ID.GetValueOrDefault()))) {
        		if (!orderDetail.IsTestCert) {
	        		addEduDocs = true;
        		}
                var discount = orderDetail.GetAllDiscountsInPercent();

                string presenceTypeTC;
                if (PriceTypes.IsDistance(orderDetail.PriceType_TC))
                    presenceTypeTC = PresenceTypes.Distance;
                else if(PriceTypes.Webinars.Contains(orderDetail.PriceType_TC))
                    presenceTypeTC = PresenceTypes.Webinar;
                else
                    presenceTypeTC = PresenceTypes.Intramural;

				var courseOrderMangerTC = orderDetail.IsTestCert
					? Employees.TestCert
					: Employees.Site;

        		var isFree = orderDetail.Price == 0;
        		var reason = orderDetail.ReasonForLearning;
        		if (orderDetail.IsTestCert) {
        			reason = LearningReasons.Testing;
        		}else if (!(orderDetail.IsTrack 
					|| orderDetail.Course.GetOrDefault(x => x.FullHours >= CommonConst.LongCourseHours))) {
        			reason = LearningReasons.Comprehensive;
        		}



            	var studentInGroup = 
                    new StudentInGroup
                    {
                        Student_ID = student.Student_ID,
                        Group_ID = orderDetail.CalcGroupId,
						InputSource_TC = GetInputSource(),
                        PriceType_TC = orderDetail.PriceType_TC,
                        Discount = Math.Round(discount),
                        BerthType_TC = berthTypeTC,
                        Track_TC = orderDetail.Track_TC,
						SeatNumber = orderDetail.SeatNumber,
                        Employee_TC = Employees.Site,
                        InputDate = DateTime.Now,
						FavoriteTeacher1 = orderDetail.IsCourseOrder ? order.FavoriteTeacher1 : null,
                        Debt = 100,
						NextContactDate = nextContactDate,
                        Consultant_TC = courseOrderMangerTC,
                        Router_TC = courseOrderMangerTC,
                        PresenceType_TC = presenceTypeTC,
						ReasonForLearning = reason,
						PromoCode = orderDetail.Order.PromoCode,
                        Charge = orderDetail.PriceWithDiscount,
                        IsHungry = PriceTypes.IsBusiness(
                        	orderDetail.PriceType_TC)
                    };
        		;
        		if (IsPreTestPass(orderDetail)) {
        			studentInGroup.IsEntryTestPassed = true;
        		}
				if(isFree) {
					studentInGroup.Debt = 0;
					studentInGroup.BerthType_TC = BerthTypes.Kons;
					studentInGroup.NextContactDate = null;
				}
                AddSource(order, studentInGroup);
            	foreach (var extras in orderDetail.OrderExtras) {
            		studentInGroup.StudentInGroupExtras.Add(
						new StudentInGroupExtras {
							Extras_ID = extras.Extras_ID,
							StudentInGroup = studentInGroup
						});	
            	}
                studentInGroupRepository.InsertAndSubmit(studentInGroup);
                orderDetail.StudentInGroup_ID = studentInGroup.StudentInGroup_ID;

            }


            foreach (var orderExam in order.OrderExams)
            {
                decimal groupID = Groups.NotChoiceGroupID;
                if (orderExam.Group_ID.HasValue)
                    groupID = orderExam.Group_ID.Value;
                var studentInGroup = 
                    new StudentInGroup
                    {
                        Student_ID = student.Student_ID,
                        Group_ID = groupID,
                        PriceType_TC = PriceTypes.PrivatePersonWeekend,
                        BerthType_TC = berthTypeTC,
                        Employee_TC = Employees.Site,
                        InputDate = DateTime.Now,
						InputSource_TC = GetInputSource(),
                        Debt = 100,
						ReasonForLearning = LearningReasons.Exam,
						Exam_ID = orderExam.Exam_ID,
						NextContactDate = nextContactDate,
                        Charge = orderExam.Price,
                        PresenceType_TC = PresenceTypes.Intramural,
                    };
                AddSource(order, studentInGroup);
                studentInGroupRepository.InsertAndSubmit(studentInGroup);
                orderExam.StudentInGroup_ID = studentInGroup.StudentInGroup_ID;

            }

			if (addEduDocs) {
				new PioneerDataContext().uspAutoEntryPersonalRecords(student.Student_ID,
					order.EduDocTypeTC);
			}

		}

        public void SetDVP(decimal sigId)
        {
            try
            {
                var studentInGroupRepository = new Repository<StudentInGroup>(
                new SpecialistContextProvider());
                var sig = studentInGroupRepository.GetByPK(sigId);
                sig.BerthType_TC = BerthTypes.Dvp;
                studentInGroupRepository.SubmitChanges();
            }
            catch (Exception ex)
            {
                Utils.Logger.Exception(ex, "SetDVP ERROR");
                //throw ex;
            }

        }

	    public bool IsPreTestPass(OrderDetail od) {
		    var result = UserTestService.GetAll(x => x.UserId == od.Order.UserID 
				&& x.Course_TC == od.Course_TC).Select(x => x.Status)
				.OrderByDescending(x => x).FirstOrDefault();
		    if (result == 0) {
			    return false;
		    }
		    return result > 1;
	    }

        public void Export(decimal orderID, bool checkIfExists, string eduDocTypeTC) {
            var order = OrderService.GetByPK(orderID);

            if (order.Exported || order.User.IsCompany)
                return;
//	        if (checkIfExists && !order.OrderExams.Any()) {
//				var courses = order.GetCourseOrderDetails().Select(x => x.Course_TC).ToList();
//				var needToExport = courses.Except(OrderDetailService.GetAll(x => x.Order.UserID == order.UserID &&
//					x.StudentInGroup_ID.HasValue).Select(x => x.Course_TC).Distinct().ToList()).Any();
//		        if (!needToExport) return;
//	        }
	        order.EduDocTypeTC = eduDocTypeTC;
			Export(order, checkIfExists);
            OrderService.SubmitChanges();
        }

        private void AddSource(Entities.Context.Order order, StudentInGroup studentInGroup) {
            if(order.User.Source_ID.HasValue)
                studentInGroup.StudentInGroupSources.Add(
                    new StudentInGroupSource {
                        Source_ID = order.User.Source_ID.Value,
                        InputDate = DateTime.Now,
                        Employee_TC = Employees.Specweb,
                        LastChangeDate = DateTime.Now,
                        LastChanger_TC = Employees.Specweb
                    });
        }

		private static char GetInputSource() {
			return CommonConst.IsMobile ? InputSources.Mobile : InputSources.Site;
		}

		public static decimal SaveExpressOrder(decimal studentId) {
			var nextContactDate = DateTime.Now;
			if(nextContactDate.Hour >= 18)
				nextContactDate = nextContactDate.Date.AddDays(1);
			var studentInGroup = 
                    new StudentInGroup
                    {
                        Student_ID = studentId,
						InputSource_TC = GetInputSource(),
                        Group_ID = Groups.NotChoiceGroupID,
                        BerthType_TC = BerthTypes.NotPaid,
                        Employee_TC = Employees.Specweb,
                        InputDate = DateTime.Now,
                        Debt = 100,
						NextContactDate = nextContactDate,
						PriceType_TC = PriceTypes.PrivatePersonWeekend,
                        PresenceType_TC = PresenceTypes.Intramural,
						IsExpressOrder = true,
                    };
			var studentRepository = new Repository<StudentInGroup>(
            new SpecialistContextProvider());
			studentRepository.InsertAndSubmit(studentInGroup);
			return studentInGroup.StudentInGroup_ID;
		}

		public static void SaveStudnet(Student student) {
			var studentRepository = new Repository<Student>(
             new SpecialistContextProvider());
			studentRepository.InsertAndSubmit(student);
		}
        public Student GetOrInsertStudentByUser(User user, string webkeyword)
        {
            var studentRepository = new Repository<Student>(
             new SpecialistContextProvider());
            var student = user.Student;
            if (student != null)
            {
                student = studentRepository.GetByPK(user.Student_ID);
            }
            else {
	            var address = user.GetAddress();
				var city = address.GetOrDefault(x => x.City);
				var terrainId = Cities.Terrains.Moscow;
	            var countryId = Countries.Russian;
	            if (!city.IsEmpty()) {
		            if (address.CountryID > 0) {
			            countryId = address.CountryID;	
		            }
		            var terrain = TerrainService.FirstOrDefault(x => 
						x.TerrainName == city.Trim() && x.Country_ID == countryId);
					if (terrain != null) {
						terrainId = terrain.Terrain_ID;
					}
				}
	            
                student = new Student {
                    FirstName = user.FirstName,
                    LastName = user.LastName ?? "Слушатель",
                    MiddleName = user.SecondName,
                    WebLogin = user.Email,
                    WebKeyword = webkeyword,
                    Sex = user.Sex ? Sex.M : Sex.W,
					Terrain_ID = terrainId,
					Country_ID = countryId,
					Branch_ID = user.WorkBranch_ID,
					Metier_ID = user.Metier_ID
                };
            }
            AddContacts(user, student);
            if(student.Student_ID == 0) {
                studentRepository.InsertAndSubmit(student);
                var userForChange = UserService.GetByPK(user.UserID);
                userForChange.Student_ID = student.Student_ID;
                UserService.SubmitChanges();
            }else {
                studentRepository.SubmitChanges();
            }
            return student;
        }

        public static void AddContacts(User user, Student student) {
        	var email = user.Email;
        	if (student.StudentEmails.All(se => se.Email != email)) {
                student.StudentEmails.Add(CreateStudentEmail(email));
            }

            var phones = user.UserContacts.ToList()
                .Where(uc => !uc.Contact.IsEmpty()
                    && ContactTypes.Phones().Contains(uc.ContactTypeID)
                    && student.StudentPhones.All(sp => sp.PhoneNumber != uc.Contact));
            foreach (var phone in phones) {
            	var phoneTypeTC = ContactTypes.GetPhoneTypeTC(phone.ContactTypeID);
            	var phoneNumber = phone.Contact;
            	student.StudentPhones.Add(CreateStudentPhone(phoneTypeTC, phoneNumber));
            }
        }

    	public static StudentPhone CreateStudentPhone(char? phoneTypeTC, string phoneNumber) {
    		return new StudentPhone {
    			PhoneNumber = phoneNumber,
    			InputDate = DateTime.Now,
    			Employee_TC = Employees.Specweb,
    			LastChangeDate = DateTime.Now,
    			PhoneType_TC = phoneTypeTC,
    			LastChanger_TC = Employees.Specweb
    		};
    	}

    	public static StudentEmail CreateStudentEmail(string email) {
    		return new StudentEmail {
    			Email = email,
    			InputDate = DateTime.Now,
    			Employee_TC = Employees.Specweb,
    			LastChangeDate = DateTime.Now,
    			LastChanger_TC = Employees.Specweb
    		};
    	}
    }
}