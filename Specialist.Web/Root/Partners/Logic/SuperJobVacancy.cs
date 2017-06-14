using System;
using System.Collections.Generic;
using Specialist.Entities.Const;

namespace Specialist.Services.Common {
	[Serializable]
	public class SuperJobVacancy {
		public string Name { get; set; }

		public string Company { get; set; }

		public string Url { get; set; }

		public string PaymentFrom { get; set; }

		public string Town { get; set; }

		public string CityTC { get; private set; }

		public List<int> CategoryIds { get; set; }

		public SuperJobVacancy(string name, string company, string url, string payment, string town, List<int> categoryIds) {
			Name = name;
			Company = company;
			Url = url;
			PaymentFrom = payment;
			Town = town;
			CategoryIds = categoryIds;
			CityTC = Cities.GetByCityName(Town);

		}
	}
}