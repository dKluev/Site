using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class PollMD:CmsMetaData<Poll>
    {
        public override void Init()
        {
            this.Display("Опрос");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
        }
    }
}