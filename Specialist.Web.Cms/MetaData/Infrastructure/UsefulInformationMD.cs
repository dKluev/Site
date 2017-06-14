using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class UsefulInformationMD : CmsMetaData<UsefulInformation>
    {
        public override void Init()
        {
            this.Display("Полезная информация");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
        }
    }
}
