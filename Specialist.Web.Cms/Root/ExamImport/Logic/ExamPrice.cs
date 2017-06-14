using Specialist.Entities.Context;

namespace Specialist.Web.Cms.Root.ExamImport.Logic {
	public class ExamPrice {
		public Exam Exam { get; set; }

		public int Old { get; set; }

		public int New { get; set; }
	}
}