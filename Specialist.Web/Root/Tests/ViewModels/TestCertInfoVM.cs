using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class TestCertInfoVM {

		public List<Country> Countries { get; set; } 

		public UserAddress UserAddress { get; set; }

		public User User { get; set; }

		public bool IsEngCert { get; set; }

		public bool IsPaper { get; set; }
	}
}