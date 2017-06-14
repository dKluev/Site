using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class SimplePageRelationMD : CmsMetaData<SimplePageRelation>
    {
        public override void Init()
        {
            this.Display("Связь страниц");
            this.Deletable();
            For(x => x.ParentPageID).Display("Родитель");
            For(x => x.SimplePageID).Display("Страница");
            For(x => x.IsMainParent).Display("Главный родитель");
        }
    }
}