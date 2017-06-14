using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using SimpleUtils.Util;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Center;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using System.Linq;
using Specialist.Services.Interface.Passport;

namespace Specialist.Services.Center.ViewModel
{
    public class EmployeeVMService : IEmployeeVMService
    {
        [Dependency]
        public IEmployeeService EmployeeService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IAuthService AuthService { get; set; }

        [Dependency]
        public ICourseService CouserService { get; set; }

        [Dependency]
        public IRepository<OrgResponse> OrgResponseService { get; set; }

        [Dependency]
        public IRepository2<EmployeesCourse> EmployeeCourseService { get; set; }

        public EmployeeVM GetEmployee(string employeeTC)
        {
            
            var employee = EmployeeService.GetByPK(employeeTC);
			if(employee ==  null || !employee.FinalSiteVisible)
				return null;
            var groups = new List<Group>();
            if(employee.IsTrainer)
            {
                groups = GroupService.GetAllByTrainer(employeeTC)
                .OrderBy(x => x.DateBeg).Take(CommonConst.NearestGroupCount).ToList();
            }

        	var orgResponses = OrgResponseService.GetAll().IsActive()
        		.Where(r => r.Employee_TC.Contains(employeeTC))
        		.OrderByDescending(o => o.UpdateDate).ToList();
            var model = new EmployeeVM
            {
                Employee = employee,
				OrgResponses = orgResponses,
                Groups = groups,
            };
            return
                model;

        }

     /*   public ManagerVM GetManager(string employeeTC)
        {
            var manager = EmployeeService.GetByPK(employeeTC);
            var responses = ResponseService.GetAllFor(manager);
            return
                new ManagerVM
                {
                    Manager = manager,
                    Responses = responses.Take(5).ToList(),
                    Receiver = manager.User,
                };

        }*/

	    public IQueryable<Group> GetTrainerGroups(string employeeTC) {
		    return GroupService.GetAll().Where(x =>
			    x.Teacher_TC == employeeTC
				&& x.Color_TC != Colors.LightBlue);
	    }

        public TrainerGroupsVM GetGroups()
        {

            var user = AuthService.CurrentUser;
	        var count = 20;
            if (user.Employee_TC == null)
                return new TrainerGroupsVM { Groups = new List<Group>() };
	        var seminarGroups = GroupService.GetAll().Where(x =>
		        x.Teacher_TC == user.Employee_TC && x.DateBeg >= DateTime.Today
				&& x.Course_TC == CourseTC.Seminar)
		        .ToList().OrderBy(x => x.DateBeg).ToList();
	        var groupQuery = GetTrainerGroups(user.Employee_TC)
				.Where(x => !x.MegaGroup_ID.HasValue);
	        var groups = groupQuery.Where(x => x.DateBeg > DateTime.Today)
		        .OrderBy(x => x.DateBeg).Take(count).ToList();
	        var startedGroups = groupQuery.Where(x => 
		       x.DateEnd >= DateTime.Today && x.DateBeg <= DateTime.Today)
		        .OrderBy(x => x.DateBeg).ToList();
	        var endGroups = groupQuery.Where(x => 
				x.DateEnd < DateTime.Today)
				.OrderByDescending(x => x.DateBeg)
				.Take(count)
		        .ToList();
            return new TrainerGroupsVM{Groups = groups, 
				User = user, 
				SeminarGroups = seminarGroups,
				EndedGroups = endGroups,
				StartedGroups = startedGroups};
        }

		public List<Tuple<string,bool>> GetEmployeeCoursesHasVideo(string employeeTC) {
			return EmployeeCourseService.GetAll(x => x.IsActive && x.Employee_TC == employeeTC)
				.Select(ec => new {ec.Course_TC, HasVideo = !Equals(ec.BroadcastingURL, null)})
				.ToList().Select(x => Tuple.Create(x.Course_TC, x.HasVideo)).ToList();

		}

		public List<string> GetEmployeeCourses(string employeeTC) {
			return EmployeeCourseService.GetAll(x => x.IsActive && x.Employee_TC == employeeTC)
				.Select(ec => ec.Course_TC).ToList();

		}

		public TrainerCoursesVM GetCourses() {
			var user = AuthService.CurrentUser;
			if (user.Employee_TC == null)
				return new TrainerCoursesVM {
					CourseHasVideos = new List<Tuple<CourseLink,bool>>()
				};
			var courseLinks = CouserService.AllCourseLinks();
			var courseTCs = GetEmployeeCoursesHasVideo(user.Employee_TC);
			var courses = courseTCs.Select(x => 
			Tuple.Create(courseLinks[x.Item1], x.Item2)).ToList();

			return new TrainerCoursesVM {
				 CourseHasVideos = courses
			};
		}
	}
}