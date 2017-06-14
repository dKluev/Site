using System;
using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Context.Logic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class Cities {
		public const string Moscow = "�";
		public const string Piter = "�";
		public const string Kazan = "�";
		public const string Rostov = "�";
		public const string Omsk = "�";
//		public const string Distance = "Distance";

		public static string GetByCityName(string sityName) {
			switch (sityName) {
				case "������":
					return Moscow;
				case "������-��-����":
					return Rostov;
				case "�����-���������":
					return Piter;
				case "������":
					return Kazan;
				default:
					return null;
			}
		}

		public static readonly List<string> Disable = _.List(Omsk, Piter, Rostov, Kazan,
			"�");
		public static class Terrains {
			public const decimal Moscow = 3862;
		}

		public static class ClasseRooms {
//			public static List<string> OrderSpec = _.List(
//				"��9", "��10", "��11", "��12", "��13", "��14", "��15", "��16", "��17", "��18");

			public static List<string> OrderRu = _.List("��3");
			public static List<string> Bm891011 = _.List("��8", "��9", "��10", "��11");
//			public static List<string> OrderCos = _.List(
//				"��2","��3","��11","��13","��14","��15","��16",
//				"��1","��2","��5","��6","��8","��9","��10","���2","���3","���1",
//				"��1","��4","��5","��6","��7","��11","��12","��13");
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
						return House + "�" + Building;

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
			public const string Partners = "��������";
			public const string Viezd = "�����";

			public const string Tulskii = "��";
			public const string Radio = "�";
			public const string B = "�";
			public const string Stilobat = "��������";
			public const string NoMoscow = "��_������";
			public const string Park = "����";
			public const string BS = "��";
			public const string TAG = "���";
			public const string PL = "��";

			public static Dictionary<string, ComplexAddress> Addresses = new  Dictionary<string, ComplexAddress> {
				
{Stilobat ,new ComplexAddress("������������ ��������" ,"4/6","","2","198328")},
{B ,new ComplexAddress("����������" ,"6","2","4","105005")},
{Radio ,new ComplexAddress("�����" ,"24","1","2","105005")},
{PL ,new ComplexAddress("4-� �������������" ,"11","","6","123007")},
{BS ,new ComplexAddress("3-� ����� ������� ����" ,"32","","5","125040")},
{TAG ,new ComplexAddress("������������" ,"35�","2","5","109147")},
{Park ,new ComplexAddress("�������" ,"6","9","3","121309")},
{Tulskii ,new ComplexAddress("���������� �����" ,"1","1-2","6","108852")},
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
				{"��", "37.691152,55.77981"},
				{"�", "37.675289,55.776011"},
				{"��", "37.578907,55.783905"},
				{"���", "30.354635,59.955554"},
				{"����", "37.508684,55.740081"},
				{"��", "37.517581,55.770094"},
				{"���", "39.717914,47.22553"},
				{"�", "37.683599,55.761902"},
				{"��������", "37.687687,55.770401"},
				{"���", "37.663311,55.7339"},
				{"���", "37.516465,55.681693"},
				{"��", "37.625891,55.704025"},
			};
		}

	}
}