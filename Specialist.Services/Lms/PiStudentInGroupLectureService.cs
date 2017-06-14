using SimpleUtils.Common.Extensions;
using Specialist.Entities.Lms;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Lms {
	public class PiStudentInGroupLectureService: Repository2<PiStudentInGroupLecture> {
		public PiStudentInGroupLectureService(IContextProvider contextProvider) : base(contextProvider) {}

		public void EditBegin(string employeeTC) {
			context.As<PioneerDataContext>().uspChangerSPIDSet(employeeTC);
		}
		public void EditFinish() {
			context.As<PioneerDataContext>().uspChangerSPIDDelete();
		}
	}
}