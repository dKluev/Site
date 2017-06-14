using System;
using System.Globalization;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Common {
	public class SimpleValueService: Repository2<SimpleValue> {
		public SimpleValueService(IContextProvider contextProvider) : base(contextProvider) {}

		public int CityCouponCount {
			get { return int.Parse(GetByPK(SimpleValues.GeoCouponCountId).Value); }
		}

		public void IncreaseCouponCount() {
			EnableTracking();
			var x = GetByPK(SimpleValues.GeoCouponCountId);
			x.Value = (int.Parse(x.Value) + 1).ToString();
			SubmitChanges();
		}

		public string OkRefreshToken {
			get { return GetByPK(SimpleValues.OkRefreshToken).Value; }
			set {
				GetByPK(SimpleValues.OkRefreshToken).Value = value;
			}
		}

		public int LastPostedNewsId {
			get { return int.Parse(GetByPK(SimpleValues.LastPostedNewsId).Value); }
			set {
				GetByPK(SimpleValues.LastPostedNewsId).Value = value.ToString();
			}
		}

		private const string format = "dd.MM.yyyy";
		public DateTime LastPostDiscountsDate {
			get { return DateTime.ParseExact(
				GetByPK(SimpleValues.LastPostDiscountsDate).Value,format, CultureInfo.InvariantCulture); }
			set {
				GetByPK(SimpleValues.LastPostDiscountsDate).Value = value.ToString(format);
			}
		}
	}
}