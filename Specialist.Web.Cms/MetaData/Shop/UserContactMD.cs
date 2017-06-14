using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class UserContactMD : CmsMetaData<UserContact>
    {
        public override void Init()
        {
            this.Display("Контакт");
            this.DisplayBy(x => x.Contact);
            this.ReadOnly();
            For(x => x.Contact).Display("Контакт");
            For(x => x.ContactTypeID).Display("Тип").ReadOnly();
            For(x => x.UserID).Display("Пользователь").ReadOnly();
        }
    }
}