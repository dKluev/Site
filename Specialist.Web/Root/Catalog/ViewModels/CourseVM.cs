using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;
using Specialist.Services.Common;

namespace Specialist.Entities.ViewModel {
	public class CourseVM : CourseBaseVM {
		public bool HasPaperBook { get; set; }

		public int? MaxDiscount { get; set; }
		public bool HasPrerequisites {
			get { return Course.CoursePrerequisites.Any(); }
		}


		public List<Test> PrerequisiteTests { get; set; }


		public List<Response> Responses { get; set; }

		public List<SuccessStory> SuccessStories { get; set; }

		public int ResponseTotalCount { get; set; }

		public List<CourseLink> NextCourses { get; set; }

		public NearestGroupSet NearestGroups { get; set; }

		public List<MarketingAction> Actions { get; set; }

		public List<Employee> Trainers { get; set; }

		public List<Test> Tests { get; set; } 



		public bool ShowRss {
			get { return NearestGroups.All.Any() && NearestGroups.All.First().DateBeg > DateTime.Now.AddMonths(1); }
		}



		public List<Section> Sections { get; set; }


		public Vendor AuthorizationVendor {
			get {
				if (Course.AuthorizationType == null)
					return null;
				return Course.AuthorizationType.Vendors.FirstOrDefault();
			}
		}

		public Product Product { get; set; }

		public List<SuperJobVacancy> Vacancies { get; set; }

		public List<CourseLink> VisitedCourses { get; set; }
		public List<CourseContent> CourseContents { get; set; }

		public bool ShowCiscoBlock {
			get { return CourseUrls.CiscoBlock.Contains(Course.UrlName); }
		}

		public bool Is3dPrint {
			get { return CourseUrls.Print3dUrls.Contains(Course.UrlName); }
		}

	}
}