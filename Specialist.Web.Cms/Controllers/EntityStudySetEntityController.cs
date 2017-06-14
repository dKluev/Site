using Specialist.Entities.Context;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Core;
using System.Linq;
using System.Linq.Dynamic;

namespace Specialist.Web.Cms.Controllers
{
	[Auth(Emails = "ptolochko,lovkov,dinzis,aharkevich")]
    public class EntityStudySetEntityController: BaseController<EntityStudySet>
    {
    }
}
