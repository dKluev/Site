using Specialist.Entities.Const;
using Specialist.Entities.Context.Const;

namespace Specialist.Entities.Context {
	public partial class StudentInGroup {

		public bool IsUnlimit {
			get {
				return IsUnlimitSig(PriceType_TC, BerthType_TC);
			}
		}

		public static bool IsUnlimitSig(string priceTypeTC, string berthTypeTC) {
			return PriceTypes.Unlimited == priceTypeTC || berthTypeTC == BerthTypes.Unlimit;
		}

	}
}