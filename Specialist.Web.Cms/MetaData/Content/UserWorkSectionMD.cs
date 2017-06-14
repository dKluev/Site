using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class UserWorkSectionMD : CmsMetaData<UserWorkSection>
    {
        public override void Init()
        {
            this.Display("Раздел работ");
            this.DisplayByName();
            this.AddName();
          
        }
    }
}