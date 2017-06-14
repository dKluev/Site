using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class OrgQuestionnaireVM: IViewModel {
		public Questionnaire Questionnaire { get; set; }

		public string Title {
			get { return "Анкета"; }
		}
	}
}