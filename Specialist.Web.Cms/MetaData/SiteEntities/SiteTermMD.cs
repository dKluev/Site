using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class SiteTermMD : CmsMetaData<SiteTerm>
    {
        public override void Init()
        {
            this.Display("Технология");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
			For(x => x.ForMainPage).Display("На главную");
        }
    }
}
