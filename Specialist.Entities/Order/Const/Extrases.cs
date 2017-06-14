using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Order.Const {
	public static class Extrases {
		public const decimal WebinarRecord = 6;
		public const decimal Courier = 8;
		public const decimal Depifr = 9;
		public const decimal BuhTrial = 7;
		public const decimal ForeignDelivery = 15;
		public const decimal ExpressDelivery = 17;
		public const decimal TestCertificteImage = 16;
		public const decimal TestCertificte = 14;
		public const decimal PaperBook = 116;

		public static bool IsTravel(decimal id) {
			return Travel.Contains(id);
		}


		public static readonly List<decimal> ForTestCert = _.List(ForeignDelivery, ExpressDelivery);
		public static readonly List<decimal> Travel = _.List<decimal>(
		48,49,50,34,36,37,38,39);
		/*
		public const decimal TestCertPrice = 300;
		public const decimal TestCertImagePrice = 200;
*/
	}
}