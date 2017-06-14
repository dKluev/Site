namespace Specialist.Entities.Lms {
	public partial class PiStudentInGroupLecture {

		public string FinalExamMark_TC { get; set; }

		public bool IsPresent {
			get {
				return !Truancy.GetValueOrDefault();
			}
			set {
				Truancy = !value;
			}
		}
		 
	}
}