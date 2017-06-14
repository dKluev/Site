using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace Specialist.Entities.Context
{
    public partial class RawQuestionnaire
    {
        public bool? IsGood { get; set; }

		public bool ResponseIsActive { get; set; }

        public RawQuestionnaireType TypeEntity
        {
            get { return RawQuestionnaireType.GetAll().First(rqt => rqt.Type == Type); }
        }

        public Employee Teacher
        {
            get
            {
                return QuestionnaireTeachersMark != null
                    ? QuestionnaireTeachersMark.Teacher
                    : Questionnaire.StudentInGroup.Group.Teacher;
            }
        }

        private EntityRef<Questionnaire> _Questionnaire = default(EntityRef<Questionnaire>);
        [Association(Storage = "_Questionnaire", ThisKey = "Questionnaire_ID",
            OtherKey = "Questionnaire_ID", IsForeignKey = true)]
        public Questionnaire Questionnaire
        {
            get { return _Questionnaire.Entity; }
            set { _Questionnaire.Entity = value; }
        }

        private EntityRef<QuestionnaireTeachersMark> _QuestionnaireTeachersMark = 
            default(EntityRef<QuestionnaireTeachersMark>);
        [Association(Storage = "_QuestionnaireTeachersMark", 
            ThisKey = "TeacherMark_ID",
            OtherKey = "QuestionnaireTeacherMark_ID", IsForeignKey = true)]
        public QuestionnaireTeachersMark QuestionnaireTeachersMark
        {
            get { return _QuestionnaireTeachersMark.Entity; }
            set { _QuestionnaireTeachersMark.Entity = value; }
        }

    }
}