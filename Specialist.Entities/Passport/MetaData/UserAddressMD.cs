using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;

namespace Specialist.Entities.Passport.MetaData
{
    public class UserAddressMD : BaseMetaData<UserAddress>
    {
        public UserAddressMD()
        {
            For(x => x.ForSberbank).UIHint(Controls.Hidden);
            For(x => x.CountryID).Display("Страна") ;
            For(x => x.City).Display("Город")
                .Example("Пример: Москва");
            For(x => x.Index)
                .Display("Почтовый индекс")
                .Example("Пример: 123456");
            For(x => x.Address)
                .Display("Почтовый адрес")
                .Example("Пример: ул. Молостовых, дом 16, корпус 2, кв. 11");

        }
    }
}