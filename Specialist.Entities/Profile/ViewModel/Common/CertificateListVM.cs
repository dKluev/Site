using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Profile.ViewModel.Common {
	public class CertificateListVM : IViewModel {


		public List<StudentInGroup> List { get; set; }
		public string Title {
			get { return "Сертификаты обучения"; }
		}
	}
}