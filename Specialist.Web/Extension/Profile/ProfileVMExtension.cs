using System.Collections.Generic;
using System.Security.Policy;
using System.Web.Mvc;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Utils;
using Specialist.Entities.Profile;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Mvc;

namespace Specialist.Web.Extension
{
    public static class ProfileVMExtension
    {
        public static List<SelectListItem> GetCountriesList(
              this EditProfileVM registrationVM)
        {
            var result = new List<SelectListItem>();
            var countries = registrationVM.Countries;
            var checkd = false;
            foreach (var country in countries)
            {
                if (registrationVM.UserAddress.CountryID != 0)
                {
                    if (country.Country_ID == registrationVM.UserAddress.CountryID)
                    {
                        checkd = true;
                    }
                }
                else
                {
                    if (country.CountryName == "Российская Федерация") checkd = true;
                }
                result.Add(
                    new SelectListItem
                    {
                        Selected = checkd, 
                        Text = country.CountryName, 
                        Value = country.Country_ID.ToString()
                    });
                checkd = false;
            }
            return result;
        }

        public static string GetLink(this ProfileVM model, object anchor) {
            if (!model.User.IsStudent)
                return "<span class='blockedlinks'>" 
                    + StringUtils.GetInnerHtml(anchor.ToString()) + "</span>";
            return anchor.ToString();
        }

        public static List<string> MyCourses(
             this ProfileVM model, UrlHelper url)
        {

            var result = _.List(model.GetLink(HtmlControls.Anchor(
                url.Action<ProfileController>(c => c.Learning()),
                "Предстоящие", LearningListType.Coming).ToString()));

			result.Add(
			model.GetLink(HtmlControls.Anchor(
                url.Action<ProfileController>(c => c.Learning()),
                "Текущие", LearningListType.Current).ToString()));

            result.Add(model.GetLink(HtmlControls.Anchor(
                url.Action<ProfileController>(c => c.Learning()),
                "Законченные", LearningListType.Ended).ToString()));

            result.Add(model.GetLink(HtmlControls.Anchor(
                url.Action<ProfileController>(c => c.Learning()),
                "Экзамены", LearningListType.Exam).ToString()));

            return result;
        }
    }
}