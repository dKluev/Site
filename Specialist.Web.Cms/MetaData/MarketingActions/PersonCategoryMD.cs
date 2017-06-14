using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.MarketingAction
{
    public class PersonCategoryMD : CmsMetaData<PersonCategory>
    {
        public override void Init()
        {
            this.Display("Категория человека");
            this.DisplayByName();
            this.AddName();
            For(x => x.DocumentName).Display("Документ");
            For(x => x.DocumentInfo).Display("Информация из документа "); 
        }
    }
}