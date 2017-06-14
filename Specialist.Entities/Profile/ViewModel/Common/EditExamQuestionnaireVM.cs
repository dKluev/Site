using System;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Profile.ViewModel
{
    public class EditExamQuestionnaireVM: IViewModel
    {
        public UserExamQuestionnaire ExamQuestionnaire { get; set; }

        public EditExamQuestionnaireVM(UserExamQuestionnaire examQuestionnaire)
        {
            ExamQuestionnaire = examQuestionnaire;
        }

        public string Title
        {
            get { return "Анкета для сдачи экзаменов и " + 
                "оформления международных сертификатов"; }
        }
    }
}