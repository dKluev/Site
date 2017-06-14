using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class GroupMD : CmsMetaData<Group>
    {
        public override void Init()
        {
            this.Display("Ãðóïïà");
            this.DisplayBy(x => x.Group_ID); 
            this.ReadOnly();
        }
    }
}