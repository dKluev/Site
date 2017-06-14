using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class GroupAttendanceVM:IViewModel {
		public decimal GroupId { get; set; }
		public List<StudentInGroupLecture> Lectures { get; set; }
		public string Title { get { return "Обучение в группе " + GroupId; }}
		public bool HasGroupCert { get; set; }
		public decimal SigId { get; set; }
	}
}