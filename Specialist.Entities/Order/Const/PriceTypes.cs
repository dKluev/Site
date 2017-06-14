using System;
using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Reflection;
using System.Linq;
using SimpleUtils.Extension;
using Specialist.Entities.Context;
using SimpleUtils;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const
{
    public static class PriceTypes
    {
        public const string PPPrefix = "ЧЛ";

        public const string PrivatePersonWeekend = "ЧЛВ";
	    public const string Main = PrivatePersonWeekend;

//        public const string PrivatePersonBusinessClass = "ЧЛБ";

        public const string DistanceOptimal = "ДООПТ";

        public const string Distance = "ДО";

        public const string DistanceEconomical = "ДОЭК";

        public const string DistanceOffline = "ДООФФ";

        public const string DistanceProf = "ДОПРО";

        public const string DistanceReg = "ДОРЕГ";

        public const string DistanceOnline = "ДООНЛ";

        public const string DistanceLab = "ДОЛАБ";

        public const string Corporate = "ОРГ";

        public const string CorporateBusinessClass = "ОРГБ";

        public const string Individual = "ЧЛИ";

        public const string IndividualOrg = "ОРГИ";

        public const string Business = "Б";

        public const string Weekend = "В";

        public const string Webinar = "ВЕБЧЛ"; // shortName

        public const string WebinarOrg = "ВЕБОР";

	    public const string IntraExtra = "ЧЛВОЗ";
	    public const string IntraExtraWebinar = "ВЧЛОЗ";
	    public const string IntraExtraOrg = "ОРГОЗ";
	    public const string Unlimited = "ДПБО";
	    public const string DopUsl = "ДПУСЛ";


	    public static List<string> Current = _.List(
		    WebinarOrg,
		    Webinar,
		    DistanceOffline,
		    Corporate,
		    CorporateBusinessClass,
		    IndividualOrg,
		    IntraExtraOrg,
			Unlimited,
		    "ЧЛБВ",
		    PrivatePersonWeekend,
		    IntraExtra,
		    Individual,
			DopUsl
		    );

	    public static bool IsCorp(string tc) {
		    return tc.Contains(Corporate);
	    }
	    public static string GetBaseType(string tc) {
		    if (tc == Unlimited) {
			    return Unlimited;
		    }
		    if (IsIntraExtra(tc)) {
			    return IntraExtra;
		    }
		    if (IsWebinar(tc)) {
			    return Webinar;
		    }
		    return Main;
	    }

		public static readonly HashSet<string> Webinars = new HashSet<string>(
			_.List(Webinar,WebinarOrg));

        public const string ElearningS = "ВАУЧС";

        public const string ElearningP = "ВАУЧП";


		public static string GetWebinar(bool isOrg) {
			return isOrg ? WebinarOrg : Webinar;
		}
		public static string GetIndividual(bool isOrg) {
			return isOrg ? IndividualOrg : Individual;
		}

        public static string GetForGroup(Group group, 
            bool isBusiness, string customerType)
        {
            var customerTypePart =
                customerType == OrderCustomerType.Organization ?
                PriceTypes.Corporate : PriceTypes.PPPrefix;
            var businessPart = isBusiness
                ? PriceTypes.Business : string.Empty;
            var dayShiftPart = string.Empty;
            if (customerType == OrderCustomerType.PrivatePerson)
                dayShiftPart = PriceTypes.Weekend;

            return customerTypePart + businessPart + dayShiftPart;
        }

        public static string GetShortName(string priceType)
        {
            switch (priceType)
            {
                case DistanceOffline:
                    return "Off- line";
                case Webinar:
                    return "Вебинар";
                case WebinarOrg:
                    return "Вебинар";
                case DistanceEconomical:
                    return "Эконом";
                case DistanceOptimal:
                    return "Оптима";
                case DistanceLab:
                    return "Эконом";
                case DistanceReg:
                    return "Регион";
                case DistanceOnline:
                    return "online";
                case Corporate:
                    return "Органи- зации";
                case Individual:
                    return "Индиви- дуально";
                case IndividualOrg:
                    return "Индиви- дуально";
                case IntraExtra:
                    return "Очно-заочно";
                case IntraExtraOrg:
                    return "Очно-заочно";
                case PrivatePersonWeekend:
                    return "Стандарт";
                case ElearningS:
                    return "E-learning самостоят.";
                case ElearningP:
                    return "E-learning c преподават.";
            }

            if (IsBusiness(priceType))
                return GetShortName(priceType.RemoveLast());

            return priceType;
        }


        public static string GetFullName(string priceType) {
            switch (priceType) {
                case DistanceOffline:
                    return "Offline";
                case Webinar:
                    return "Вебинар";
                case WebinarOrg:
                    return "Вебинар";
                case DistanceEconomical:
                    return "Эконом";
                case DistanceOptimal:
                    return "Оптима";
                case DistanceLab:
                    return "Эконом";
                case DistanceReg:
                    return "Регион";
                case DistanceOnline:
                    return "online";
                case Corporate:
                    return "Организации";
                case Individual:
                    return "Индивидуально";
                case IndividualOrg:
                    return "Индивидуально";
                case PrivatePersonWeekend:
                    return "Стандарт";
                case ElearningS:
                    return "E-learning самостоятельно";
                case ElearningP:
                    return "E-learning с преподавателем";
                case IntraExtra:
                    return "Очно-заочно";
                case IntraExtraOrg:
                    return "Очно-заочно";
            }

            if (IsBusiness(priceType))
                return GetFullName(priceType.Remove(Business));

            return priceType;
        }

        public static string GetPrefix(char cityPrefix) {
            if(cityPrefix == ' ')
                return string.Empty;
            return cityPrefix.ToString();
        }
      

        public static bool IsDistance(string priceTypeTC)
        {
            return priceTypeTC != null && (priceTypeTC.StartsWith(Distance)
                || GetElearning().Contains(priceTypeTC));
        }

        public static bool IsDistanceOrWebinar(string priceTypeTC)
        {
            return  priceTypeTC != null &&(IsDistance(priceTypeTC)
                || IsWebinar(priceTypeTC));
        }
		public static bool IsIntraExtra(string priceTypeTC) {
			return priceTypeTC == IntraExtra || priceTypeTC == IntraExtraOrg;
		}

		public static bool IsWebinar(string priceTypeTC) {
			return Webinars.Contains(priceTypeTC);
		}

		public static bool IsMain(string priceTypeTC) {
			return priceTypeTC == PrivatePersonWeekend;
		}
		public static bool IsPP(string priceTypeTC) {
			if(priceTypeTC.IsEmpty())
				return false;
			return priceTypeTC.Contains(PPPrefix);
		}

        public static bool IsDay(string priceTypeTC)
        {
            return IsBusiness(priceTypeTC)
                || priceTypeTC.EndsWith(PPPrefix)
                || priceTypeTC.EndsWith(Corporate);

        }

        public static bool IsIndividual(string priceTypeTC)
        {
            return priceTypeTC.Contains(Individual) 
				|| priceTypeTC.Contains(IndividualOrg);
        }

      /*  public static string GetCityPrefix(string cityTC)
        {
            var priceListTypeTC = PriceListTypes.GetPriceListTypeTC(cityTC);
            if (priceListTypeTC.In(PriceListTypes.Common, PriceListTypes.Distance))
                return string.Empty;
            return priceListTypeTC.ToString();
        }*/

        public static List<string> GetDistance()
        {
            return new List<string>{DistanceOptimal, DistanceEconomical, DistanceOffline, DistanceReg, DistanceOnline, ElearningS, ElearningP};
        }

        public static List<string> GetCustomerTypes()
        {
            return new List<string> { PrivatePersonWeekend, Distance, Individual, Corporate };
        }

        public static List<string> GetFulltime()
        {
            return new List<string>
            {
                Corporate, Individual, PrivatePersonWeekend
            };
        }

        public static List<string> FulltimePerson = 
	        new List<string> { Individual, PrivatePersonWeekend };
        public static List<string> CourseTable = 
	        new List<string> { PrivatePersonWeekend, Webinar, Corporate, WebinarOrg};

        public static List<string> GetFulltimeOrg()
        {
            return new List<string>
            {
                IndividualOrg, Corporate
            };
        }

        public static List<string> GetPerson()
        {
            return new List<string> { PrivatePersonWeekend };
        }

        public static List<string> GetElearning() {
            return new List<string> { ElearningP, ElearningS };
        }

     /*   public static string GetCityTCByPriceType(string priceTypeTC)
        {
            if (priceTypeTC.StartsWith(PriceListTypes.Piter.ToString()))
                return Cities.Piter;
            if (priceTypeTC.StartsWith(PriceListTypes.Rostov.ToString()))
                return Cities.Rostov;
            return Cities.Moscow;
        }*/

        public static string GetCustomerType(string priceTypeTC)
        {
            if (priceTypeTC.Contains(PriceTypes.Corporate))
            {
                return OrderCustomerType.Organization;
            }
            if (priceTypeTC.Contains(PriceTypes.PPPrefix))
            {
                return OrderCustomerType.PrivatePerson;
            }
            return null;
        }

        public static string GetByCustomerType(string customerType)
        {
            if(customerType == OrderCustomerType.Organization)
                return Corporate;
            if (customerType == OrderCustomerType.PrivatePerson)
                return PPPrefix;
            throw new Exception("Not CustomerType");
        }

        public static bool IsBusiness(string priceTypeTC)
        {
			if(priceTypeTC == null)
				return false;
            return priceTypeTC.EndsWith(Business) 
                || priceTypeTC[priceTypeTC.Length - 2].ToString() == Business;
        }


        public static bool IsWeekend(string priceTypeTC)
        {
            return priceTypeTC.EndsWith(Weekend);
        }

       
     /*   private static List<string> GetAll()
        {
            return new List<string>
                   {
                       PrivatePerson, PrivatePersonBusinessClass, PrivatePersonWeekend,
                       Corporate, CorporateBusinessClass
                   };
        }*/

       /* public static string AddPriceListTypeTC(string priceTypeTC, string cityTC)
        {
            if (cityTC != Cities.Moscow)
            {
                var priceListType = GetPriceListTypeTC(cityTC);
                return priceTypeTC + priceListType;
            }
            else
            {
                return priceTypeTC;
            }
        }*/

     /*   public static string RemovePriceListTypeTC(string priceTypeTC, 
            char priceListTypeTC)
        {
            if (priceListTypeTC != 'О')
            {
                
                return priceTypeTC.Remove(0, 1);
            }
            else
            {
                return priceTypeTC;
            }
        }*/

     /*   public static List<string> GetAll(string cityTC)
        {
            var result = GetAll();
            result = result.Select(pt => AddPriceListTypeTC(pt, cityTC)).ToList();
            result.AddRange(GetDistance());
            return result;
        }*/




    }
}