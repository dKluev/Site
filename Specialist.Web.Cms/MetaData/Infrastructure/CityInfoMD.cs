using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class CityInfoMD:BaseMetaData<CityInfo>
    {
        public override void Init()
        {
            this.Display("Блок города");
            this.DisplayByName();
			this.Deletable();
            For(x => x.Name).Display("Город").UIHint(Controls.Text);
            For(x => x.Description).Display("Описание").UIHint(Controls.TextArea);
        }
    }
}