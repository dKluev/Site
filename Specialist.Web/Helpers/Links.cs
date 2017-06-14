using System.Text.RegularExpressions;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Entities.Filter;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Tests;
using Specialist.Entities.ViewModel;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using System.Linq;
using Specialist.Web.Controllers.Center;
using Specialist.Web.Controllers.Common;
using Specialist.Web.Controllers.Message;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Group=Specialist.Entities.Context.Group;
using Specialist.Entities.Context.Const;
using Specialist.Web.Const;

namespace Specialist.Web.Helpers
{
    public static class Links
    {
        public const string CoursesPrefix = "Курсы ";

        public static string DefaultEntity(this HtmlHelper helper, 
            DefaultEntityCommonInfo entityCommonInfo)
        {
            return entityCommonInfo.Name;
        }
        public static string VacancyLink(this HtmlHelper helper, Vacancy vacancy) {
            return helper.ActionLink<CenterController>(
                x => x.Vacancy(vacancy.VacancyID), vacancy.Name).ToString();
        }

		public static TagA MarketingActionLink(this HtmlHelper helper, 
			MarketingAction action)
		{
			return helper.Url().Link<CenterController>(c =>
				c.MarketingAction(action.UrlName), action.Name);
		}

        public static string ExamLink(this HtmlHelper helper, Exam exam)
        {
            return helper.ExamLink(exam, exam.Exam_TC);
        }

        public static string ExamLinkName(this HtmlHelper helper, Exam exam)
        {
            return helper.ExamLink(exam, exam.ExamName);
        }

        public static string ExamLinkNumberWithName(this HtmlHelper helper, Exam exam)
        {
            return helper.ExamLink(exam, exam.Exam_TC + " - " + exam.ExamName);
        }


        public static string ExamLink(this HtmlHelper helper, Exam exam, string title)
        {
            return helper.ActionLink<ExamController>(
                x => x.Details(exam.Exam_TC), title).ToString();
        }

        public static string EmployeeEntityLink(this HtmlHelper helper, Employee employee) {
            return helper.EmployeeLink(employee);
        }

        public static string EmployeeLink(this HtmlHelper helper, IEmployeeLink employee)
        {
            if(employee == null)
                return null;
            if (employee.IsTrainer)
                return helper.TrainerLink(employee).NotNullString();
            return helper.ManagerLink(employee);
        }

        public static TagA TrainerLink(this HtmlHelper helper, IEmployeeLink employee,
			object content = null)
        {
            if(employee == null || !employee.FinalSiteVisible)
                return null;
        	var url = new UrlHelper(helper.ViewContext.RequestContext);
            return url.Link<EmployeeController>(
                x => x.AboutTrainer(employee.Employee_TC.ToLowerInvariant(),
					SimplePages.Urls.AboutTrainer, null), content ?? employee.FullName);
        }

        public static string ManagerLink(this HtmlHelper helper, IEmployeeLink employee)
        {
            if (employee == null || !employee.SiteVisible)
                return null;
            return helper.ManagerLink(employee.Employee_TC.ToLower(), employee.FullName);
        }

        public static string ManagerLink(this HtmlHelper helper, 
            string employeeTC, string text)
        {
            return helper.ActionLink<EmployeeController>(
                x => x.Manager(employeeTC.ToLowerInvariant()), text).ToString();
        }

        public static string PublicProfileLink(this HtmlHelper helper, User user) {
            if (user == null || user.IsCompany)
                return user.FullName;

            if (user.Employee != null) {
				if(user.Employee.SiteVisible)
					return helper.EmployeeLink(user.Employee);
            	return helper.ManagerLink(user.Employee.Employee_TC, user.Employee.FullName);
            }

            return helper.ActionLink<ProfileController>(
                x => x.Public(user.UserID), user.FullName).ToString();
        }

        public static string SiteTermLink(this HtmlHelper helper, SiteTerm term)
        {
            return helper.ActionLink<DictionaryController>(
                x => x.Definition(term.UrlName), 
				StringUtils.CoursesPrefix( term.Name)).ToString();
        }

        public static TagBuilder SimplePageLink(this HtmlHelper helper, SimplePage page)
        {
            if(page.WithoutLink) {
                return new TagBuilder("span") {InnerHtml = page.LinkTitle};
            }
            return HtmlControls.Anchor(page.Url, page.LinkTitle);
        }

        public static string ComplexLink(this HtmlHelper helper, Complex complex) {
        	return helper.Url().ComplexLinkAnchor(complex).ToString();
        }

        public static TagA ComplexLinkAnchor(this UrlHelper helper, Complex complex)
        {
            if(complex == null)
                return new NullTagA();
/*
            if (!complex.IsPublished)
                return complex.Name;
*/
            return helper.Link<LocationsController>(
                x => x.Complex(complex.UrlName), complex.Name);
        }

        public static string CityLink(this HtmlHelper helper, City city)
        {
            return helper.ActionLink<LocationsController>(
				x => x.City(city.UrlName), city.CustomName).ToString();
        }
        public static string CityInfoLink(this HtmlHelper helper, CityInfo cityInfo)
        {
            return helper.ActionLink<InfoController>(
				x => x.CityInfoBlock(cityInfo.Id), cityInfo.Name).ToString();
        }

		public static string CityLinkBig(this HtmlHelper helper, City city)
		{
			return helper.ActionLink<LocationsController>(
				x => x.City(city.UrlName), city.CityName,
				new { @class="citysize" }).ToString();
		}
        public static string VendorLink(this HtmlHelper helper,
          Vendor vendor)
        {
            if(vendor == null) return null;
            return H.Anchor(SimplePages.FullUrls.Vendor + vendor.UrlName , 
				StringUtils.CoursesPrefix(vendor.Name)).ToString();
        }

        public static string VendorExamLink(this HtmlHelper helper,
          Vendor vendor)
        {
            return
                helper.ActionLink<VendorController>(c =>
                    c.Details(vendor.UrlName, VendorVM.Tab.Testing, 1),
                    "Все экзамены " + vendor.Name)
                    .ToString();
        }

        public static string VendorExamLink(this HtmlHelper helper,
           Vendor Vendor, string linkText)
        {
            return helper.ActionLink<VendorController>(
                ec => ec.Details(Vendor.UrlName, VendorVM.Tab.Testing, 1), linkText).ToString();
        }

        public static string GroupLink(this HtmlHelper helper,
          Group group)
        {
            return helper.GroupLink(group.Group_ID, "Страница группы");
        }

        public static string GroupLink(this HtmlHelper helper,
         decimal groupID, string linkText)
        {
            return helper.ActionLink<GroupController>(
                c => c.Details(groupID), linkText).ToString();
        }

	    public static string CourseLinkByGroup(this HtmlHelper helper, Group gr) {
		    var name = gr.Title;
		    return gr.Course.IsActive ? 
				CourseLinkAnchor(helper, gr.Course.UrlName, name).ToString()
				: name;
	    }

        public static string CertificationLink(this HtmlHelper helper, Certification certification) {
	        if (certification.IsStatus)
		        return certification.Name;
            return helper.ActionLink<CertificationController>(
                cc => cc.Details(certification.UrlName), certification.Name).ToString();
        }

        public static string CourseEntityLink(this HtmlHelper helper, Course course) {
            return helper.CourseLinkShortName(course);
        }

        public static string CourseLinkShortName(this HtmlHelper helper, ICourseLink course)
        {
            if (course == null)
                return null;
            if (course.WebShortName.IsEmpty())
                return helper.CourseLink(course);
            if (course.IsTrack.GetValueOrDefault())
                return helper.ActionLink<TrackController>(
                    tc => tc.Details(course.UrlName), course.WebShortName).ToString();

        	return helper.CourseLink(course.UrlName, course.WebShortName);
        }



        public static string CourseLink(this HtmlHelper helper, ICourseLink course)
        {
            if (course == null)
                return null;
            if (course.IsTrack.GetValueOrDefault())
				return HtmlControls.Anchor("/track/" + course.UrlName, 
					course.GetName()).ToString();

        	return helper.CourseLink(course.UrlName, course.GetName());
        }

        public static string CourseLink(this HtmlHelper helper, string urlName, string name) {
        	return helper.CourseLinkAnchor(urlName, name).ToString();
        }

        public static TagA CourseLinkAnchor(this HtmlHelper helper, string urlName, string name) {
        	return H.Anchor("/course/" + urlName.GetOrDefault(x => x.ToLower()), name);
        }
        public static TagA CourseLinkAnchor(this HtmlHelper helper, Course course) {
	        var prefix = course.IsTrackBool ? "/track/" : "/course/";
        	return H.Anchor(prefix + course.UrlName.GetOrDefault(x => x.ToLower()),
				course.WebName);
        }
        public static string CourseFiles(this HtmlHelper helper, string courseTC, 
            string linkText)
        {
            return helper.ActionLink<CourseController>(
                cc => cc.Files(courseTC), linkText).ToString();
        }


        public static string GroupsLinkForCourse(this HtmlHelper helper,
        string courseTC)
        {
            return helper.ActionLinkImageEx<GroupController>(
                 gc => gc.List(new GroupFilter { CourseTC = courseTC }, 1),
				 Urls.Main("ico_timetable.gif"), "Расписание всех групп", 
				 new {title = "Расписание всех групп"});
        }

        public static string GroupsLinkForCourseText(this HtmlHelper helper,
       string courseTC)
        {
            return helper.ActionLinkEx<GroupController>(
                 gc => gc.List(new GroupFilter { CourseTC = courseTC }, 1),
                    "Полное расписание курса");
        }

        public static string GroupsLinkForComplex(this HtmlHelper helper,
           string complexTC)
        {
            return helper.ActionLinkEx<GroupController>(
                 gc => gc.List(new GroupFilter { ComplexTC = complexTC }, 1),
                 "Расписание");
        }

        public static string ProductLink(this HtmlHelper helper, Product product)
        {
            return helper.ActionLink<ProductController>(
                cc => cc.Details(product.UrlName + UrlName.ProductPostfix), 
                StringUtils.CoursesPrefix(product.Name)).ToString();
        }

		public static string VideoLink(this HtmlHelper helper, Video video) {
			return helper.Url().VideoLink(video).ToString();
		}

		public static string GuideLink(this HtmlHelper helper, Guide guide) {
			return helper.Url().Course().Guide(guide.GuideID, guide.Name).ToString();
		}

        public static string ProfessionLink(this HtmlHelper helper, Profession profession)
        {
            if(profession == null) return null;
            return helper.ActionLink<ProfessionController>(
                cc => cc.Details(profession.UrlName), profession.Name).ToString();
        }

        public static string SectionLink(this HtmlHelper helper, Section section)
        {
            if(section == null)
                return null;
            return helper.SectionLink(section, section.Name);
        }

        public static string SectionLink(this HtmlHelper helper, Section section, 
            string linkText)
        {
            return helper.ActionLink<SectionController>(
                sc => sc.Details(section.UrlName), linkText).ToString();
        }


        public static TagA SectionAnchor(this HtmlHelper helper, Section section)
        {
            if(section == null)
                return new NullTagA();
	        return helper.Url().Section().Details(section.UrlName, section.Name);
        }

        public static string UserWorks(this HtmlHelper helper, Section section) {
            return helper.ActionLink<ClientController>(
                sc => sc.UserWorks(section.Section_ID, 0, 1), section.Name).ToString();
        }

        public static string UserWorks(this HtmlHelper helper, Section section,
            UserWorkSection workSection) {
            return helper.ActionLink<ClientController>(
                sc => sc.UserWorks(section.Section_ID, workSection.UserWorkSectionID, 1), 
                workSection.Name).ToString();
        }

        public static string MessageLink(this HtmlHelper helper, UserMessage userMessage) {
            var text = userMessage.Title;
            return helper.MessageLink(userMessage, text);
        }

        public static string MessageLink(this HtmlHelper helper, UserMessage userMessage, string text) {
	        return helper.Url().Message().Details(userMessage.UserMessageID, 1,
		        helper.Encode(text)).ToString();
/*
            return helper.ActionLink<MessageController>(
                sc => sc.Details(userMessage.UserMessageID, 1), 
				helper.Encode(text)).ToString();
*/
        }

        public static string MessagePrivateList(this HtmlHelper helper, int receiverID)
        {
            return MessagePrivateList(helper, receiverID, "История переписки");
        }

        public static string MessagePrivateList(this HtmlHelper helper, int receiverID, 
            string text)
        {
            return helper.ActionLink<MessageController>(
                sc => sc.PrivateList(receiverID, 1), text).ToString();
        }

        public static string MessageSectionLink(this HtmlHelper helper, 
            MessageSection messageSection)
        {
            if(!messageSection.IsGraduateClub || helper.InRole(Role.GraduateClubAccess))
			{
				return helper.ActionLink<MessageController>(
                    sc => sc.Section(messageSection.MessageSectionID, 1), 
                    messageSection.Name).ToString();
			}
            return messageSection.Name;
        }

        public static string TagLink<T>(this HtmlHelper helper, TagWithEntity<T> tagsWithEntity)
        {
            var link = helper.GetLinkFor(tagsWithEntity.Entity);
            return link;
        }

        public static string NewsLink(this HtmlHelper helper, News news)
        {
            return helper.NewsLink(news, news.Title);
        }

        public static string NewsLink(this HtmlHelper helper, News news, string title) {
        	return new UrlHelper(helper.ViewContext.RequestContext)
        		.NewsAnchor(news, title).ToString();
        }

		public static TagA NewsAnchor(this UrlHelper url, News news, 
			string title = null) {
            if (title.IsEmpty()) title = news.Title ?? "Читать новость";
			return url.Link<SiteNewsController>(
				sc => sc.Details(news.NewsID, Linguistics.UrlTranslite(news.Title)),
				title);
		}

        public static string AdviceLink(this HtmlHelper helper, Advice advice) {
            return helper.ActionLink<CenterController>(
                sc => sc.Advice(advice.AdviceID, 
                    Linguistics.UrlTranslite(advice.Name)), 
                advice.GetShortDescription()).ToString();
        }

		public static string AdviceLink2(this HtmlHelper helper, Advice advice) {
			return helper.ActionLink<CenterController>(
				sc => sc.Advice(advice.AdviceID,
					Linguistics.UrlTranslite(advice.Name)),
				advice.Name).ToString();
		}


        public static string NewsLinkRead(this HtmlHelper helper, News news)
        {
            return helper.NewsLink(news, news.IsVideo ? "Смотреть видеоновость" :
                "Читать дальше");
        }

        public static string UserFileLink(this HtmlHelper helper, UserFile userFile) {
            return HtmlControls.Anchor(
                UserFiles.GetUserFileUrl(userFile), userFile.Name).ToString();
        }

		public static TagA UserWorkLink(this HtmlHelper helper, UserWork w) {
			
			return new UrlHelper(helper.ViewContext.RequestContext).Link<ClientController>(
				sc => sc.UserWorks(w.Section_ID, w.WorkSectionID.GetValueOrDefault() , 1),
				Images.Image("UserWork/Small/" + w.SmallImage).Title("Перейти на страницу работ слушателей").Width(170));
		}

        public static string TestLink(this HtmlHelper helper, Test test) {
        	return helper.Url().TestLink(test).ToString();
        }


     /*   public static string TagLink(this HtmlHelper helper, Tag tag)
        {
            
              if (obj == null)
                return null;
            foreach (var methodInfo in typeof(Links).GetMethods())
            {
                if (methodInfo.GetParameters().Count() == 2
                    && methodInfo.GetParameters()[1].ParameterType == obj.GetType())
                    return (string) methodInfo.Invoke(null, new[] { helper, obj });
            }
            return "link none";
        }*/

        public static string GetLinkFor(this HtmlHelper helper, object obj)
        {
            if (obj == null)
                return null;
            foreach (var methodInfo in typeof(Links).GetMethods())
            {
                if (methodInfo.GetParameters().Count() == 2
                    && methodInfo.GetParameters()[1].ParameterType == obj.GetType())
                    return methodInfo.Invoke(null, new[] { helper, obj })
                        .NotNullString();
            }
            return "link none";
        }

        public static string GetLinkWithoutCoursesPrefixFor(
            this HtmlHelper helper, object obj)
        {
            if(obj == null)
                return null;
        	var link = helper.GetLinkFor(obj);
			if(obj is Section)
				return link;
        	return link.Replace(CoursesPrefix, string.Empty);
        }


        public static string GetUrlFor(this HtmlHelper helper, object obj)
        {

            var link = GetLinkFor(helper, obj) ?? string.Empty;
            const string urlRegExp = @"<A[^>]*?HREF\s*=\s*[""']?([^'"" >]+?)[ '""]?>";
            var matches = Regex.Matches(link, urlRegExp,
                             RegexOptions.Singleline
                             | RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;

        }


    }
}