using System;
using System.Collections.Generic;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Web.Const;

namespace Specialist.Entities.Catalog.ViewModel {
	public class TrackListVM: IViewModel{

		public bool IsDiplomPage { get; set; }

		public bool IsTrainingProgramsPage { get; set; }

		public List<string> Courses { get; set; } 

		public Course Course { get; set; }

		public string Title {
			get { return CommonTexts.TracksName + " содержащие курс " + StringUtils.AngleBrackets(Course.GetName())  ; }
		}
	}
}