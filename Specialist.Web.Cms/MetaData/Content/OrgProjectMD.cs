using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class OrgProjectMD : CmsMetaData<OrgProject>
    {
        public override void Init()
        {
            this.Display("Проект");
            this.AddName();
            this.TryAddStandartProperties();

        }
    }
}