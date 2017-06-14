using Specialist.Entities.Context;
using Specialist.Web.Cms.Core;
using System.Linq;
using System.Linq.Dynamic;

namespace Specialist.Web.Cms.Controllers
{
    public class SectionEntityController: BaseController<Section>
    {
        protected override string GetComboBoxFilter(string propertyName, object id) {
            if(propertyName == "ParentSection_ID")
                return "ParentSection_ID == null";
            return null;
        }
    }
}