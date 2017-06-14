using System.Collections.Generic;
using System.Web.Mvc;
using SimpleUtils;
using Specialist.Entities.Context.ViewModel;

namespace Specialist.Web.Extension
{
    public static class EditExamVMExtension
    {
        public static List<SelectListItem> GetAllGroups(this EditExamVM model)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem()
            {
                Text = "Уточнить дату позже",
                Value = ""
            });
            foreach (var group in model.Groups)
            {
                result.Add(new SelectListItem
                {
                    Selected = group.Group_ID == model.OrderExam.Group_ID,
                    Text =
                        (group.DateBeg)
                        .NotNullToString("dd.MM.yyyy") + " " +
                        group.TimeBeg.GetValueOrDefault().ToShortTimeString(),
                    Value = group.Group_ID.ToString(),
                   
                });
            }
            return result;
        }
    }
}