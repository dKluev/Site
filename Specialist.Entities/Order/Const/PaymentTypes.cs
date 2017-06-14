namespace Specialist.Entities.Context.Const
{
    public static class PaymentTypes
    {
        public const string Cash = "НАЛ";//Наличный расчет
//        public const string name = "ПК";//Кредитная карта (Оплата в приемной)
//        public const string name = "БАРТ";//Бартер

	    public const string SberMerchant = "ШСБ";
        public const string Invoice = "БНАЛ";//Безналиный расчет
        public const string SberBank = "СБ";//Оплата через сберкассу
        public const string CyberPlat = "КИБ";//Кредитная карта (Оплата через Киберплат)
        public const string WebMoney = "ВМ";//Web money
        public const string RbkMoney = "РБК";//Web money
        public const string YandexMoney = "ЯНД";
        public const string Terminal = "ПТ";//Платежные терминалы (Оплата через Киберплат)
        public const string Qiwi = "КИВИ";//Платежные терминалы (Оплата через Киберплат)
        public const string NoPayment = "ЭЮЯ";//Нет оплаты
        public const string ExpressOrder = "БЗ";
        public const string AlphaCredit = "КРАЛ";

       
    }
}