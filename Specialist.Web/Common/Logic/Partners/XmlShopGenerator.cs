using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog;
using Specialist.Services.Interface;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Common {
	public class XmlShopGenerator {

		[Dependency]
    	public ICourseListVMService CourseListVMService { get; set; }
		

		[Dependency]
    	public ISectionService SectionService { get; set; }

		[Dependency]
    	public ICourseService CourseService { get; set; }

		[Dependency]
		public ISiteObjectRelationService SiteObjectService { get; set; }
		 
		protected static XElement X(string name, params object[] content) {
			return new XElement(name, content);
		}
		protected static XElement X(XName name, params object[] content) {
			return new XElement(name, content);
		}

		protected static XAttribute A(string name , object value) {
			value = value ?? string.Empty;
			return new XAttribute(name, value);
		}
		protected XAttribute A(XName name , object value) {
			value = value ?? string.Empty;
			return new XAttribute(name, value);
		}


		protected Dictionary<string, string> GetCourseDescs() {
		    var courseDescs = CourseService.GetAll(c => c.IsActive)
			    .Select(c => new {c.Course_TC, c.Description})
			    .ToDictionary(c => c.Course_TC, c => c.Description);
		    return courseDescs;
	    }
		protected Dictionary<string, string> GetCourseShortDescs() {
		    var courseDescs = CourseService.GetAll(c => c.IsActive)
			    .Select(c => new {c.Course_TC, c.AnnounceDescription})
			    .ToDictionary(c => c.Course_TC, c => c.AnnounceDescription);
		    return courseDescs;
	    }

		protected Dictionary<string, List<int>> GetCourseSections() {
		    return  SiteObjectService.GetRelation(typeof (Course),
			    new object[0], typeof (Section)).Select(x => new {
				    x.Object_ID,
				    x.RelationObject_ID
			    }).ToList().GroupBy(x => x.Object_ID.ToString())
			    .ToDictionary(x => x.Key,
				    x => x.Select(y => (int) y.RelationObject_ID).ToList());
	    }

		protected IEnumerable<CommonCourseListItemVM> GetCourses() {
		    var courses = CourseListVMService.GetAll(null)
			    .Common.Items.Where(x => x.Prices.Any(z =>
				    !PriceTypes.IsIndividual(z.PriceType_TC))
				    && !CourseTC.HalfTrackCourses.Contains(x.Course.Course_TC));
		    return courses;
	    }
	}
}