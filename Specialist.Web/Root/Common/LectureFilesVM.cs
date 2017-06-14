using System.Collections.Generic;
using System.ComponentModel;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Bing {
	public class LectureFilesVM: IViewModel {
		[DisplayName("Группа")]
		public decimal? GroupId { get; set; }

		public List<LectureFile> Files { get; set; }

		public List<Lecture> Lectures { get; set; }

		public string Title {
			get { return "Архивы проверки заданий группы"; }
		}
	}
}