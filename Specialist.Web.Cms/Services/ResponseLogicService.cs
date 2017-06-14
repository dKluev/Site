using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using SimpleUtils;
using SimpleUtils.Reflection;
using Specialist.Services.Interface.Passport;

namespace Specialist.Web.Cms.Services
{
    public class ResponseLogicService : IResponseLogicService
    {
        private DateTime _exportDate = new DateTime(2009, 1, 1);

        [Dependency]
        public IRepository<QuestionnaireTeachersMark>
            QuestionnaireTeachersMarkService { get; set; }

        [Dependency]
        public IRepository<Questionnaire> QuestionnaireService { get; set; }


        [Dependency]
        public IRepository<RawQuestionnaire> RawQuestionnaireService { get; set; }

        [Dependency]
        public IRepository<Response> ResponseService { get; set; }

      /*  [Dependency]
        public IRepository<SiteObjectRelation> SiteObjectRelationService { get; set; }*/

        [Dependency]
        public IAuthService AuthService { get; set; }

        [Dependency]
        public IRepository<Group> GroupService { get; set; }

        public void ExportToRaws()
        {
            ExportTeachersToRaws();
            ExportQuestionnairesToRaws();
            RawQuestionnaireService.SubmitChanges();
        }

        private void ExportTeachersToRaws() {
            var teachersMarks = QuestionnaireTeachersMarkService.GetAll()
                .Where(qtm => qtm.InputDate >= _exportDate
                    && qtm.Notes != null);
            var existedTeacherMarkIDList = RawQuestionnaireService.GetAll()
                .Where(rq => rq.TeacherMark_ID != null)
                .Select(rq => rq.TeacherMark_ID).ToList();
            foreach (var teachersMark in teachersMarks)
            {
                if (existedTeacherMarkIDList.Contains(teachersMark
                    .QuestionnaireTeacherMark_ID))
                    continue;
                AddResponse(null,
                        RawQuestionnaireType.Teacher,
                        teachersMark.Notes,
                        teachersMark
                        );
            }
        }

        private void ExportQuestionnairesToRaws()
        {
            var questionnaires = QuestionnaireService.GetAll()
                .Where(qtm => qtm.InputDate >= _exportDate);
            var existedQuestionnaireIDList = RawQuestionnaireService.GetAll()
                .Where(rq => rq.TeacherMark_ID == null)
                .Select(rq => rq.Questionnaire_ID.Value)
                .ToList();
            foreach (var questionnaire in questionnaires)
            {
                if (existedQuestionnaireIDList.Contains(questionnaire
                    .Questionnaire_ID))
                    continue;
                foreach (var propertyName in RawQuestionnaireType.GetResponseProperties())
                {
                    AddResponse(questionnaire, 
                        RawQuestionnaireType.GetType(propertyName),
                        questionnaire.GetValue(propertyName) as string,
                        null
                        );
                }
            }
        }

        private void AddResponse(Questionnaire questionnaire, byte type,
            string responseText,
            QuestionnaireTeachersMark teachersMark) {
            if(TextNotGood(responseText))
                return;

            var questionnaireID = teachersMark == null
                ? questionnaire.Questionnaire_ID
                : teachersMark.Questionnaire_ID;
            var raw = new RawQuestionnaire
            {
                TeacherMark_ID = teachersMark == null 
                    ? (decimal?) null 
                    : teachersMark.QuestionnaireTeacherMark_ID,
                Questionnaire_ID = questionnaireID,
                IsActive = true,
                Notes = responseText,
                Type = type,
            };
            RawQuestionnaireService.Insert(raw);
        }

        private bool TextNotGood(string text)
        {
            return text.IsEmpty() || text.Count(c => c == ' ') < 5;
        }

        public IQueryable<RawQuestionnaire> GetRawQuestionnaires(DateTime? date)
        {
            var d = date.Value;
            return from rq in RawQuestionnaireService.GetAll().IsActive()
                   let dt = rq.Questionnaire.InputDate
                   where dt.Year == d.Year && dt.Month == d.Month && dt.Day == d.Day
                   select rq;
        }


        public void ExportToResponse(List<RawQuestionnaire> model)
        {
            var responses = new List<Response>();
            var rqList = new List<RawQuestionnaire>();
            foreach (var rawQuestionnaire in model.Where(rq => rq.IsGood.HasValue))
            {
                var rq =
                    RawQuestionnaireService.GetByPK(rawQuestionnaire.RawQuestionnaireID);
                rqList.Add(rq);
                if (rawQuestionnaire.IsGood.Value)
                {
                    var description = rq.QuestionnaireTeachersMark != null
                        ? rq.QuestionnaireTeachersMark.Notes
                        : rq.Notes;

                    var group = GroupService.GetAll().Where(g => g.Group_ID == rq.Questionnaire.StudentInGroup.Group.Group_ID).FirstOrDefault();

                    var teacher = group.Teacher;
                        
                    var response =
                        new Response
                        {
                            Authors = rq.Questionnaire.Student.FullName,
                            RawQuestionnaireID = rq.RawQuestionnaireID,
                            Course_TC = rq.Questionnaire.StudentInGroup.Group.Course_TC,
                            Description = description,
                            IsActive = rawQuestionnaire.ResponseIsActive,
                            LastChanger_TC = AuthService.CurrentUser.Employee_TC,
                            UpdateDate = DateTime.Now,
                            Type = rawQuestionnaire.Type,
                            Employee_TC = teacher.Employee_TC
                        };
                    responses.Add(response);
                }
                rq.IsActive = false;
            }

            foreach (var response in responses)
            {
                ResponseService.Insert(response);
            }
            RawQuestionnaireService.SubmitChanges();
            ResponseService.SubmitChanges();

//            AddRelations(rqList, responses);
        }

//        private void AddRelations(List<RawQuestionnaire> rqList, List<Response> responses) {
//            foreach (var response in responses)
//            {
//                var rq = rqList.FirstOrDefault(r => r.RawQuestionnaireID ==
//                    response.RawQuestionnaireID);
//                var responseType = LinqToSqlUtils.GetTableName(response);
//                var teacherType = LinqToSqlUtils.GetTableName(typeof(Employee));
//                string teacherTC;
//             /*   if (rq.Teacher != null)
//                {
//                    teacherTC = rq.Teacher.Employee_TC;
//                AddResponseRelation(response, responseType,
//                    teacherTC, teacherType);
//                }*/
//
//             /*   }
//                if(rq.Type == RawQuestionnaireType.CourseComment)
//                {*/
//               /* var courseType = LinqToSqlUtils.GetTableName(typeof(Course));
//                AddResponseRelation(response, responseType,
//                    rq.Questionnaire.StudentInGroup.Group.Course_TC, courseType);*/
//                }
//                
//            }
//            SiteObjectRelationService.SubmitChanges();
//        }

      /*  private void AddResponseRelation(Response response, 
            string responseType, object relationObjectID, string relationObjectType)
        {
            var sor = new SiteObjectRelation
            {
                Object_ID = response.ResponseID,
                ObjectType = responseType,
                RelationObject_ID = relationObjectID,
                RelationObjectType = relationObjectType,
            };
            SiteObjectRelationService.Insert(sor);
        }*/
    }
}