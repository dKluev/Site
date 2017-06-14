using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Passport;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class UserMD : CmsMetaData<User>
    {
        public override void Init()
        {
            this.Display("Пользователь");
            this.DisplayBy(x => x.Email);
            For(x => x.Email).Display("E-mail").ReadOnly();
            For(x => x.LastName).Display("Фамилия").ReadOnly();
            For(x => x.FirstName).Display("Имя").ReadOnly();
            For(x => x.SecondName).Display("Отчество").ReadOnly();
            For(x => x.Employee_TC).Display("Код сотрудника");
            For(x => x.IsActive).Display("Активность");
        }
    }
}