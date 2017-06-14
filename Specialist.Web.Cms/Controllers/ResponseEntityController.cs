using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Web.Cms.Const;
using Specialist.Web.Cms.Core;
using System.Linq;
using Specialist.Services.Common.Extension;
using Specialist.Web.Cms.Core.ViewModel;
using Specialist.Web.Cms.Logic;
using Specialist.Web.Cms.Logic.Responses;
using Specialist.Web.Cms.Services;
using Specialist.Web.Cms.ViewModel.Responses;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Cms.Controllers
{
    public class ResponseEntityController : BaseController<Response>
    {
        [Dependency]
         public IRepository<RawQuestionnaire> RawQuestionnaireService { get; set; }

        [Dependency]
        public IResponseLogicService ResponseLogicService { get; set; }

        [Dependency]
        public IRepository<StudentInGroup> StudentInGroupService { get; set; }

        [Dependency]
        public IRepository<Questionnaire> QuestionnaireService { get; set; }

        public ActionResult ExportFromSpecialist()
        {
            ResponseLogicService.ExportToRaws();
            return null;
        }

       

        public ActionResult Export(DateTime? date)
        {
            if(date.HasValue)
            {
                var list = ResponseLogicService.GetRawQuestionnaires(date);
                if(list.Any())
                    return View(ViewNames.RawQuestionnaireList, list);
                return RedirectToAction("Export");
            }
            var model =
                from rq in RawQuestionnaireService.GetAll().IsActive()
                select rq.Questionnaire.InputDate;

            return View(ViewNames.ExportDate, model.OrderByDescending(x => x));

        }

       

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Export(List<RawQuestionnaire> model, DateTime? date)
        {
            ResponseLogicService.ExportToResponse(model);

            return RedirectToAction("Export", new {date});
//            return View(ViewNames.RawQuestionnaireList, model);
        }


        public ActionResult ExportSearch(string trainerTC, string courseTCList,
            byte? responseType, decimal? group_ID, bool isWebinar)
        {
            var splitList = StringUtils.SafeSplit(courseTCList);
            var model = new ResponseSearchVM 
                {CourseTCList = courseTCList, TrainerTC = trainerTC, Group_ID = group_ID};
            model.RawQuestionnaires = new List<RawQuestionnaire>();
	       
            if(!trainerTC.IsEmpty() || splitList.Any() || responseType.HasValue 
				|| group_ID != null || isWebinar) {
                IQueryable<RawQuestionnaire> rawQuestionnaires = 
                    RawQuestionnaireService.GetAll().IsActive();
				 if ( trainerTC.IsEmpty() && !splitList.Any() && !group_ID.HasValue) {
				ShowMessage("¬ведите код курса, код преподавател€ или номер группы");
		        return RedirectBack();
	        }
                if(!trainerTC.IsEmpty())
//                    rawQuestionnaires = rawQuestionnaires
//                    .Where(rq => rq.QuestionnaireTeachersMark.Teacher_TC == trainerTC);
                    rawQuestionnaires = rawQuestionnaires
                            .Where(r => r.Questionnaire.StudentInGroup.Group.Teacher_TC == trainerTC);
                if (responseType.HasValue)
                {
                    if (responseType.Value >= 0)
                        rawQuestionnaires = rawQuestionnaires
                        .Where(rq => rq.Type == responseType.Value);
                }

                if (group_ID != null)
                {
                    rawQuestionnaires = rawQuestionnaires
                          .Where(r => r.Questionnaire.StudentInGroup.Group_ID == group_ID);
                }
                if (splitList.Any())
                    rawQuestionnaires = rawQuestionnaires.Where(rq =>/* rq.Type
                        == RawQuestionnaireType.CourseComment
                        &&*/ splitList
                            .Contains( rq.Questionnaire.StudentInGroup.Group.Course_TC));
				if(isWebinar) {
					rawQuestionnaires = rawQuestionnaires
						  .Where(r => r.Questionnaire.StudentInGroup
						  .PriceType_TC == PriceTypes.Webinar);
				}

                model.RawQuestionnaires = rawQuestionnaires
					.OrderByDescending(x => x.Questionnaire_ID ?? x.TeacherMark_ID).Take(20).ToList();
            }
            return View(model);
        }

        public ActionResult SearchPost(ResponseSearchVM model) {
            return RedirectToAction("ExportSearch",
                new { model.TrainerTC, model.CourseTCList, model.ResponseType, model.Group_ID, model.IsWebinar });
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ExportSearch(List<RawQuestionnaire> model,
            string trainerTC, string courseTCList, byte? responseType,
			bool isWebinar)
        {
            ResponseLogicService.ExportToResponse(model);
            return RedirectToAction("ExportSearch",
                new { trainerTC, courseTCList, responseType, isWebinar });
        }


    }
}