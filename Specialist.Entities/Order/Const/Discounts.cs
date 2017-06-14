using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Order.Const {
	public class Discounts {
		public const int GroupId = 1;
		public const int RegId = 11111;
		public const int RealSpecId = 22222;
		public const int FriendId = 33333;
		public const int CityId = 44444;
		public const int SocialUrlId = 60000;
		public const int RegMoney = 500;
		public const int FriendMoney = 1000;
		public const int RegMaxDiscount = 20;
		public const int RegMinPrice = 5000;
		public const int CityMaxDiscount = 40;
		public const int PromocodeId = 55555;
		public const int SecondCourseId = 66666;

		public const int SocialUrlDiscount = 50;

		public static Dictionary<string,byte> RealSpec = new Dictionary<string, byte> {
			{ClabCardColors.Blue, 10},
			{ClabCardColors.Silver, 15},
			{ClabCardColors.Gold, 20},
			{ClabCardColors.Platinum, 25},
			{ClabCardColors.Diamond, 30}
		};

		public static Dictionary<string, byte> MaxAuth =
			_.Dict(AuthorizationTypes.Cisco, (byte)5)
				.AddFluent(AuthorizationTypes.Microsoft, (byte)10);

		public static List<string> WeekendCards = _.List(ClabCardColors.Gold,
			ClabCardColors.Platinum,
			ClabCardColors.Diamond);

		public const byte SenDiscount = 10;
		public static List<string> SenCards = _.List(
			ClabCardColors.Platinum,
			ClabCardColors.Diamond);
	}
}