using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Context;
using Specialist.Services.Interface;
using Specialist.Web.Cms.Core;
using System.Linq;
using System.Linq.Dynamic;

namespace Specialist.Web.Cms.Controllers
{
    public class UserWorkEntityController: BaseController<UserWork>
    {
		[Dependency]
	    public ISectionService SectionService { get; set; }

        protected override string GetComboBoxFilter(string propertyName, object id) {
	        var sectionIds = SectionService.GetSectionsTree()
				.SelectMany(x => x.SubSections.Select(s => s.Section_ID))
		        .ToList();
            if(propertyName == "Section_ID")
                return  sectionIds.Select(sId =>
				string.Format(" Section_ID == {0} ", sId)).JoinWith(" || ");;
            return null;
        }
    }
}