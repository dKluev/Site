using System.Linq;
using Specialist.Entities.Passport;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Utils.Logic;

namespace Specialist.Services.Passport {
	public class UserInfoService: Repository2<UserInfo> {
		public UserInfoService(IContextProvider contextProvider) : base(contextProvider) {}

		public bool HasCityCoupon(User user) {
			return this.GetAll(x => x.CityPromocode != null && x.UserId == user.UserID).Any();
		}

	}
}