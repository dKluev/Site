using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class UserContactTypeMD : CmsMetaData<UserContactType>
    {
        public override void Init()
        {
            this.Display("Тип контакта");
            this.DisplayByName();
            this.ReadOnly();
            this.AddName().ReadOnly();
        }
    }
}