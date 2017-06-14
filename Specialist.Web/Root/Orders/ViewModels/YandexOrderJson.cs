using System.Collections.Generic;

namespace Specialist.Web.ViewModel.Orders {
	public class YandexOrderJson {

		public class Good {
			public string id;
			public string name;
			public int price;
			public int quantity = 1;
		}
		public string order_id;
		public int order_price;
		public string currency = "RUR";
		public int exchange_rate = 1;
		public List<Good> goods;

	}
}