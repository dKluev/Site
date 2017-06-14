using System.Linq;
using Specialist.Entities.Context;
using Specialist.Web.Cms.Core;
using Specialist.Services.Common.Extension;

namespace Specialist.Web.Cms.Controllers
{
    public class PollEntityController: BaseController<Poll>
    {

//        protected override void BeforeEditSubmit(Poll entity)
//        {
//            if(entity.IsActive)
//                foreach (var poll in Repository.GetAll()
//                    .Where(x => x.PollID != entity.PollID).IsActive())
//                {
//                    poll.IsActive = false;
//                }
//        }
    }
}