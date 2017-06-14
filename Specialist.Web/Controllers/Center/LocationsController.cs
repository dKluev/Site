using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Center;
using System.Linq;
using Specialist.Services.Common.Extension;
using Specialist.Services.Center.Extension;
using Specialist.Services.Catalog.Extension;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Core.Logic;
using Specialist.Web.Pages;
using Specialist.Web.Root.Center.ViewModels;
using Specialist.Web.Util;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Extension;
using Specialist.Web.Helpers;

namespace Specialist.Web.Controllers
{
    public partial class LocationsController : ViewController
    {
        [Dependency]
        public ICityService CityService { get; set; }

        [Dependency]
        public IRepository2<Complex> ComplexService { get; set; }

        [Dependency]
        public IResponseService ResponseService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

		[Dependency]
		public IRepository<SimplePage> SimplePageService { get; set; }


		[Dependency]
		public IRepository2<ClassRoom> ClassRoomService { get; set; }

        public virtual ActionResult City(string urlName)
        {
            City city;
            if (urlName == null)
            {
                var cityTC = UserSettingsService.CityTC;
				if (cityTC == null || cityTC == Cities.Moscow)
					return Redirect(MainMenu.Urls.Locations);
                city = CityService.GetByPK(cityTC);
            }
            else
                city = CityService.GetAll().ByUrlName(urlName);
			if(city == null)
				return Redirect(MainMenu.Urls.Locations);
        	var model =
        		new CityVM {
        			City = city,
        			Complexes = ComplexService.GetAll().ByCity(city.City_TC).ToList(),
        			Locations = SimplePageService.GetAll().BySysName(SimplePages.Locations),
        			Cities = CityService.GetAll()
						.Where(c => c.City_TC != city.City_TC).ToList(),
					Groups = GroupService.GetPlannedAndNotBegin().Where(x => x.WebinarExists).Take(20).ToList()
                };
            return View(model);
        }

		public ActionResult ClassRooms(string complexTC) {
			var rooms = ClassRoomService.GetAll(x => x.Complex_TC == complexTC && x.IsInUse)
				.Select(x => x.ClassRoom_TC).ToList();
			var view = new InlineBaseView<ClassRoomsVM>(v =>
				H.Ul(v.Model.ClassRooms.Select(x => H.Anchor(Urls.ClassRoom(x), x)
					.Class("fancy-box"))));
			return BaseViewWithModel(view,new ClassRoomsVM{ClassRooms = rooms});

		}

		[HandleNotFound]
		[MobileCache]
        public virtual ActionResult Complex(string urlName)
        {
			ComplexService.LoadWith(b => b.Load(x => x.BranchOffice, x => x.Admin).And<BranchOffice>(x => x.City));
            var complex = ComplexService.GetAll().ByUrlName(urlName);
			if(complex == null)
				return null;
			if(complex.Complex_TC == Cities.Complexes.Partners)
				return Redirect(SimplePages.FullUrls.Partners);
			if(!complex.IsPublished)
				return RedirectToAction(() => City(complex.BranchOffice.City.UrlName));
			var responses = ResponseService.GetAll()
				.IsActive().Where(x => x.Complex_TC == complex.Complex_TC)
				.OrderByDescending(x => x.UpdateDate).ToList();
			var otherComplexes = ComplexService.GetAll(x => x.IsPublished &&
				x.UrlName != urlName).ToList();
            var model = new ComplexVM
            {
                Complex = complex,
				OtherComplexes = otherComplexes,
				Responses = responses,
                NearestGroupSet = GroupService.GetNearestGroups(complex),
				GeoLocation = Cities.Complexes.GeoLocations.GetValueOrDefault(complex.Complex_TC)
            };
            return MView(Views.Locations.Complex, model);
        }

		[MobileCache]
		public ActionResult Complexes() {
			var complexes = GetComplexes();
			var view = H.l(MHtmls.Back(Url.AboutCenter()),
				MHtmls.LongList(MHtmls.Title("О Центре:"), 
				Html.Site().MobileComplexes(complexes)));
			return BaseView(new PagePart( 
				view.ToString()), 
				new PagePart(Views.Shared.Education.NearestGroupMobile, null));
		}

	    private List<Complex> GetComplexes() {
		    return ComplexService.GetAll(x => x.IsPublished).ToList();
	    }


	    [MobileCache]
		[NotMobileRedirect]
		public ActionResult Contacts() {
			return BaseView(Views.Locations.Contacts, CityService.GetByPK(Cities.Moscow));
		}
		[OutputCache(Duration = 24*60*60, VaryByParam = "none")]
		public ActionResult MetroBlock() {
			var complexes = GetComplexes();
			return Content(Htmls2.Menu2("Комплексы у метро") + 
				Htmls2.MarkArrow(complexes.Select(x => Url.Locations()
					.Complex(x.UrlName, StringUtils.AngleBrackets(x.Name) + " " + x.Metro))));
		}
    }
}