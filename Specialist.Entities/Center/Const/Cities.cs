using System;
using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Context.Logic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class Cities {
		public const string Moscow = "М";
		public const string Piter = "П";
		public const string Kazan = "К";
		public const string Rostov = "Р";
		public const string Omsk = "О";
//		public const string Distance = "Distance";

		public static string GetByCityName(string sityName) {
			switch (sityName) {
				case "Москва":
					return Moscow;
				case "Ростов-на-Дону":
					return Rostov;
				case "Санкт-Петербург":
					return Piter;
				case "Казань":
					return Kazan;
				default:
					return null;
			}
		}

		public static readonly List<string> Disable = _.List(Omsk, Piter, Rostov, Kazan,
			"В");
		public static class Terrains {
			public const decimal Moscow = 3862;
		}

		public static class ClasseRooms {
//			public static List<string> OrderSpec = _.List(
//				"БС9", "БС10", "БС11", "БС12", "БС13", "БС14", "БС15", "БС16", "БС17", "БС18");

			public static List<string> OrderRu = _.List("БС3");
			public static List<string> Bm891011 = _.List("БМ8", "БМ9", "БМ10", "БМ11");
//			public static List<string> OrderCos = _.List(
//				"ТГ2","ТГ3","ТГ11","ТГ13","ТГ14","ТГ15","ТГ16",
//				"БМ1","БМ2","БМ5","БМ6","БМ8","БМ9","БМ10","БМТ2","БМТ3","БМТ1",
//				"РА1","РА4","РА5","РА6","РА7","РА11","РА12","РА13");
		}

		public class ComplexAddress {



			public string Street { get; set; }
            public string House { get; set; }

			public string FullHouse {
				get {
					if (Building.IsEmpty()) {
						return House;
					}
					else {
						return House + "с" + Building;

					}
				}
			}

			public string Building { get; set; }
			public string Floor { get; set; }
			public string ZipCode { get;set; }
			public ComplexAddress(string street, string house, string building, string floor, string zipCode) {
				Street = street;
				House = house;
				Building = building;
				Floor = floor;
				ZipCode = zipCode;
			}
		}
		public static class Complexes {
			public const string Partners = "ПАРТНЕРЫ";
			public const string Viezd = "ВЫЕЗД";

			public const string Tulskii = "ТЛ";
			public const string Radio = "Р";
			public const string B = "Б";
			public const string Stilobat = "СТИЛОБАТ";
			public const string NoMoscow = "НЕ_МОСКВА";
			public const string Park = "ПАРК";
			public const string BS = "БС";
			public const string TAG = "ТАГ";
			public const string PL = "ПЛ";

			public static Dictionary<string, ComplexAddress> Addresses = new  Dictionary<string, ComplexAddress> {
				
{Stilobat ,new ComplexAddress("Госпитальный переулок" ,"4/6","","2","198328")},
{B ,new ComplexAddress("Бауманская" ,"6","2","4","105005")},
{Radio ,new ComplexAddress("Радио" ,"24","1","2","105005")},
{PL ,new ComplexAddress("4-я Магистральная" ,"11","","6","123007")},
{BS ,new ComplexAddress("3-я улица Ямского Поля" ,"32","","5","125040")},
{TAG ,new ComplexAddress("Воронцовская" ,"35Б","2","5","109147")},
{Park ,new ComplexAddress("Барклая" ,"6","9","3","121309")},
{Tulskii ,new ComplexAddress("Варшавское шоссе" ,"1","1-2","6","108852")},
			};



			public static HashSet<string> OrderSpec = _.HashSet(Stilobat, Tulskii);
			public static HashSet<string> OrderCos = _.HashSet(B, Radio);
			public static HashSet<string> OrderRu = _.HashSet(Park, BS,PL);
			public static HashSet<string> OrderTag = _.HashSet(TAG);
			public static HashSet<string> PlPark = _.HashSet(PL,Park);
			public static Dictionary<string,int> Blocks = new Dictionary<string, int> {
				{B, HtmlBlocks.ComplexPath},
				{Radio, HtmlBlocks.RadioPath},
				{Stilobat, HtmlBlocks.StilobatPath},
			}; 

			public static readonly Dictionary<string, string> GeoLocations = new Dictionary<string, string> {
				{"БК", "37.691152,55.77981"},
				{"Б", "37.675289,55.776011"},
				{"БС", "37.578907,55.783905"},
				{"ФИН", "30.354635,59.955554"},
				{"ПАРК", "37.508684,55.740081"},
				{"ПЛ", "37.517581,55.770094"},
				{"ПУШ", "39.717914,47.22553"},
				{"Р", "37.683599,55.761902"},
				{"СТИЛОБАТ", "37.687687,55.770401"},
				{"ТАГ", "37.663311,55.7339"},
				{"ВЕР", "37.516465,55.681693"},
				{"ТЛ", "37.625891,55.704025"},
			};
		}

	}
}