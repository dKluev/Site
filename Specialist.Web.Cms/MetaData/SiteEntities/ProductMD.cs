using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class ProductMD : CmsMetaData<Product>
    {
        public override void Init()
        {
            this.Display("Продукт");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
			For(x => x.ForMainPage).Display("На главную");
			For(x => x.IsNew).Display("Новый");
			For(x => x.IsHit).Display("Хит");
        }
    }
}