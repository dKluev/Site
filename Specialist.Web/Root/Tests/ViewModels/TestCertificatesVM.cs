using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class TestCertificatesVM {
		public List<OrderDetail> OrderDetails { get; set; }
		public TestCertificatesVM(List<OrderDetail> orderDetails) {
			OrderDetails = orderDetails;
		}
	}
}