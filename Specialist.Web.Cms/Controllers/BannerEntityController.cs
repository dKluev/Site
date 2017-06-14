using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Context;
using Specialist.Web.Cms.Core;
using Specialist.Services.Common.Extension;

namespace Specialist.Web.Cms.Controllers
{
    public class BannerEntityController: BaseController<Banner>
    {

        protected override void BeforeEditSubmit(Banner entity) {
	        entity.Pages = entity.Pages.Remove(" ");
        }
    }
}