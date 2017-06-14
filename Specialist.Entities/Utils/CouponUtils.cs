using System;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Passport;

namespace Specialist.Web.Common.Utils.Logic {
	public class CouponUtils {
		public static bool MailListIsActive { get { return true; } }
		public static bool RegistrationIsActive = false;

		public static bool CityIsActive(int couponCount) {
			return false;
		}
		public static DateTime RegStartDate = new DateTime(2014,2,10);
		public static string PromoCode(int userId) {
			return userId.ToString("X");
		}

		private const string CityPrefix = "A-";

		public static bool IsCityPromocode(string code) {
			return code.StartsWith(CityPrefix);
		}

	    public static string CityPromocode(int userId) {
		    return CityPrefix + PromoCode(userId);
	    }

		public static bool CheckRegPromocode(User user, string code) {
			return user != null && user.RegCouponIsValid && code.GetOrDefault(x => x.ToUpper()) ==
					PromoCode(user.UserID);
		}

		public static bool IsFriendPromocode(string code) {
			if (code == null)
				return false;
			var upper = code.ToUpper();
			return upper.StartsWith("P-") || upper.StartsWith("F-");
		}

	}
}
