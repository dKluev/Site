using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SimpleUtils.FluentAttributes.Attributes;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Web.Common.Mvc.Binders;

namespace Specialist.Entities.Profile.ViewModel {
	public class RealSpecialistVM : IViewModel {
		public Student Student { get; set; }

		public User User { get; set; }

		public string Title {
			get { return "Настоящий специалист"; }
		}
	}
}