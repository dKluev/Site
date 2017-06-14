using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Examination.Const;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.Cms.Repository;
using Specialist.Services.Common.Extension;
using System.Linq;
using Specialist.Services.Core.Interface;
using Specialist.Entities.Catalog.Links.Interfaces;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Specialist.Web.Common.Extension;
using SimpleUtils.Extension;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Common.Extensions;

namespace Specialist.Services.Common {
    public class YmlGenerator: XmlShopGenerator {



		[Dependency]
    	public IRepository2<Exam> ExamService { get; set; }


		private List<Exam> _exams;
    	private UrlHelper _url;
    	private List<Section> _allSections;
    	private int _otherCategoryId = 1;


    

		
        public XDocument Get(UrlHelper urlHelper) {
        	_url = urlHelper;
        	_exams = ExamService.GetAll(x => x.ExamPrice > 0 && x.Available).ToList();
            var yaml =
             new XDocument(
             X("yml_catalog", A("date", DateTime.Now.ToString(
				 "yyyy-MM-dd HH:mm")),
				 X("shop",
					 X("name","Специалист при МГТУ"),
					 X("company",
					 "Центр компьютерного обучения &quot;Специалист&quot; при МГТУ им.Н.Э.Баумана"),
					 X("url","http://www.specialist.ru/"),
					 X("currencies", 
						X("currency", A("id", "RUR"), A("rate", 1))),

					
					 Categories(),
					 Offers()
				)
				 )
             );
            return yaml;
        }

		public XElement Categories() {
			_allSections = SectionService.AllActiveSections().ToList();
			var tree = SectionService.GetSectionsTree();
			var vendors = ExamService.GetAll(e => e.Available && e.ExamPrice > 0)
				.Select(e => e.Vendor).Distinct().ToList();
			
			
			return X("categories",
				GetCategory(_otherCategoryId, null, "Другое"),
				_allSections.Select(s => GetCategory(s.Section_ID, null, s.Name)),
				GetCategory(VendorOffset, null, "Вендоры"),
				vendors.Select(v => GetCategory(VendorOffset + v.Vendor_ID, VendorOffset, v.Name))
				);
		}
		const int VendorOffset = 10000;


		int GetCategoryId(Course c) {
			var sectionIds = _courseSections.GetValueOrDefault(c.Course_TC)
				?? new List<int>() ;
			var section = _allSections.FirstOrDefault(s => sectionIds
				.Contains(s.Section_ID));
			if(section == null)
				return _otherCategoryId;
			return section.Section_ID;
		}
    	Dictionary<string, List<int>> _courseSections;

    	public XElement Offers() {
    		_courseSections = GetCourseSections();
    		var courseDescs = GetCourseDescs();
			var courses = GetCourses();
			return X("offers",
				courses.Select(x => {
					var priceName = "(Очно)";
					var price = x.Prices.FirstOrDefault(z =>
						z.PriceType_TC == PriceTypes.PrivatePersonWeekend);
					if(price == null) {
						priceName = "(Дистанционно)";
					}
					return GetOffer((int) x.Course.Course_ID,
						x.Course.IsTrackBool
							? _url.Action<TrackController>(c => c.Details(x.Course.UrlName))
							: _url.Action<CourseController>(c => c.Details(x.Course.UrlName)),
							(int) (price != null 
								? price.Price : x.Prices.Select(y => y.Price).Min()),
						GetCategoryId(x.Course),
						CourseVM.WithCoursePrefix(x.Course.GetName()) + " " + priceName,
						StringUtils.RemoveTags(courseDescs
							.GetValueOrDefault(x.Course.Course_TC)) +
								(price != null ? "" : " Дистанционный курс."));
				}),

				_exams.Select(s => GetOffer(VendorOffset + (int)s.Exam_ID, 
					_url.Action<ExamController>(c => c.Details(s.Exam_TC.Trim())),
					(int) s.ExamPrice, VendorOffset + s.Vendor_ID,
					"Экзамен " + s.ExamName, "Языки: " + s.Languages + ". Продолжительность: " + 
					s.Minutes + " мин.", GetProviderPicture(s.ExamType)))
				);
		}


	    private string GetProviderPicture(string examType) {
			var provider = new Provider {Provider_ID = Providers.GetByType(examType)};
			return Images.Entity(provider).Attribute("src").Value;
		}


    	private XElement GetCategory(int id, int? parentId, string name) {
    		return X("category", A("id",id), 
    			parentId.HasValue ? A("parentId", parentId.Value) : null, name);
    	}

		private XElement GetOffer(int id, string url,
			int price, int categoryId, string name, string description,
			string picture = null) {
    		return X("offer", A("available", true), A("id",id), 
				X("url", CommonConst.SiteRoot + url + "?utm_source=yandex&utm_medium=cpc&utm_campaign=market"),
				X("price", price),
				X("currencyId", "RUR"),
				X("categoryId", categoryId),
				picture == null ? null : X("picture", picture),
				X("name", name),
				X("description", description),
				X("sales_notes", "Бронирование места по предоплате не менее 50%")
    			);
    	}
    }
}