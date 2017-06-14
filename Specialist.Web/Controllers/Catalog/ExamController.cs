using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using Specialist.Entities.Examination.ViewModel;
using Specialist.Entities.Profile;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using SimpleUtils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Const;
using System.Linq.Dynamic;
using System.Linq;
using Specialist.Web.Root.Exams.ViewModels;
using Specialist.Web.Util;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Controllers
{
    public partial class ExamController : ExtendedController
    {
        [Dependency]
        public IVendorService VendorService { get; set; }

        [Dependency]
        public IExamService ExamService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        [Dependency]
        public IRepository<Provider> ProviderService { get; set; }


       

		[HandleNotFound]
        public ActionResult Provider(int providerID) {
			var provider = ProviderService.GetByPK(providerID);
			if(provider == null)
				return null;
			return View(new ProviderVM {
                Provider = provider,
				Vendors = ExamService.GetAll(x =>x.Available && x.ExamProviders.Any(e => e.Provider_ID == providerID))
					.Select(e => e.Vendor).IsActive().Distinct().ToList()
            });
		}

    	[HandleNotFound]
        public virtual ActionResult Details(string urlName)
        {
            var exam = ExamService.GetByNumber(urlName);
			if(exam == null)
				return null;
            var cityTC = UserSettingsService.CityTC;
            var groups = 
              exam.Courses.SelectMany(c => GroupService.GetGroupsForCourse(c.Course_TC)
                  .ByCity(cityTC)).Distinct().OrderBy(g => g.DateBeg)
                  .Take(CommonConst.AnnounceCount);
            var model = new ExamVM {
                Exam = exam,
                Groups = groups.ToList()
            };
            return View(model);
        }

		public ActionResult Search(int vendorId, string number) {
			if (vendorId == 0) {
				return NotFound();
			}
			var vendor = VendorService.GetByPK(vendorId);
			if (vendor == null) {
				return NotFound();
			}
			var model = new ExamSearchVM {
				Vendor = vendor,
				Number = number,
				Exams = number.IsEmpty()
					? new List<Exam>()
					: ExamService.GetAll(x => x.Vendor_ID == vendorId && x.Exam_TC.Contains(number) && x.Available).Take(20).ToList()
			};
		    return View(model);
		}
    }
}