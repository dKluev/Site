using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class PresentMD : CmsMetaData<Present>
    {
        public override void Init()
        {
            this.Display("Подарок");
            this.DisplayByName();
            this.AddName();
            For(x => x.Image).Display("Картинка");
        }
    }
}