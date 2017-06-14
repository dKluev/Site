using System.ComponentModel;
using SimpleUtils.Common.Enum;
using SimpleUtils.Extension;

namespace Specialist.Entities.Context
{
    public enum OrderStep
    {
        [EnumDisplayName("Ваша корзина")]
        Cart,
        [EnumDisplayName("Вход/Регистрация")]
        Register,
//        [EnumDisplayName("Договор")]
//        Contract,
        [EnumDisplayName("Выбор формы оплаты")]
        PaymentTypeChoice
    }
}