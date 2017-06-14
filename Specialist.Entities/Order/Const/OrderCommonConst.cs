using System;
using System.Collections.Generic;
using Specialist.Entities.Const;
using Specialist.Entities.Properties;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Context.Const
{
    public static class OrderCommonConst {
	    public const string prefixSpec = "spec";
	    public const string prefixWeb = "web";

        const string WebMoneySpec = "R261293596321";
        const string WebMoneyRu = "3e12fa2e-d6e3-4e86-b266-1e2a251007f7";
        const string WebMoneyCos = "d885c7f5-0adb-492b-904a-9b75cda03c5b";

/*
        const string QiwiSpec = "9474";
	    const string QiwiRu = "364706";
	    const string QiwiCos = "364707";
*/
	    private const string sberPassword = "FRUmBV9Qza";
        static Tuple<string,string> SberbankMerchantRuId = Tuple.Create("specialist-api", sberPassword) ;
        static Tuple<string,string> SberbankMerchantSpecId = Tuple.Create("specialist2-api", sberPassword) ;
        static Tuple<string,string> SberbankMerchantCosId = Tuple.Create("specialist3-api", sberPassword) ;
        static Tuple<string,string> SberbankMerchantCsId = Tuple.Create("specialist4-api", sberPassword) ;
        static Tuple<string,string> SberbankMerchantBtId = Tuple.Create("specialist5-api", sberPassword) ;


        static Tuple<string,string> YandexSpecId = Tuple.Create("2054", "611733") ;
        static Tuple<string,string> YandexRuId = Tuple.Create("35126", "101201") ;
        static Tuple<string,string> YandexCosId = Tuple.Create("35818", "102289") ;

    	const string RbkSpecShopId = "2009809";

    	const string RbkRuShopId = "2018124";

    	const string RbkCosShopId = "2035542";
    	const string RbkCSShopId = "2036213";

    	const string AlphaCreditSpec = "7701257303";

        public const decimal SberReceiptPercent = (decimal)0.0292;

		public static Dictionary<string,string> WebMoney = new Dictionary<string, string> {
			{OurOrgs.Spec, WebMoneySpec},
			{OurOrgs.Ru, WebMoneyRu},
			{OurOrgs.Cos, WebMoneyCos}
		};
/*
		public static Dictionary<string,string> Qiwi = new Dictionary<string, string> {
			{OurOrgs.Spec, QiwiSpec},
			{OurOrgs.Ru, QiwiRu},
			{OurOrgs.Cos, QiwiCos},
		};
*/
//		public static Dictionary<string,Tuple<string,string>> YandexMoney = new Dictionary<string, Tuple<string, string>> {
//			{OurOrgs.Spec, YandexSpecId},
//			{OurOrgs.Ru, YandexRuId},
//			{OurOrgs.Cos, YandexCosId}
//		};

		public static Dictionary<string,Tuple<string,string>> SberbankMerchant = new Dictionary<string, Tuple<string, string>> {
			{OurOrgs.Spec, SberbankMerchantSpecId},
			{OurOrgs.Ru, SberbankMerchantRuId},
			{OurOrgs.Cos, SberbankMerchantCosId},
			{OurOrgs.CS, SberbankMerchantCsId},
			{OurOrgs.Bt, SberbankMerchantBtId},
		};

		public static Dictionary<string,string> AlphaCredit = new Dictionary<string, string> {
			{OurOrgs.Spec, AlphaCreditSpec},
			{OurOrgs.Cos, AlphaCreditSpec},
			{OurOrgs.Ru, AlphaCreditSpec},
		};

		public static Dictionary<string,string> Rbk = new Dictionary<string, string> {
			{OurOrgs.Spec, RbkSpecShopId},
			{OurOrgs.Ru, RbkRuShopId},
			{OurOrgs.Cos, RbkCosShopId},
			{OurOrgs.CS, RbkCSShopId},
		};

    	public static readonly List<string> LearningReasons =
    		_.List("Найти работу",
    			"Повысить квалификацию",
    			"Открыть свое дело",
    			"Для себя, для души",
    			"Для ребёнка");

    }
}