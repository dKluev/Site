using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.ViewModel;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Html;
using SimpleUtils.Collections.Extensions;
using System.Linq;
using Specialist.Web.Const;
using Specialist.Web.Controllers;
using Specialist.Web.Helpers;
using System.Web.Mvc.Html;
using SpecialistTest.Web.Core.Mvc.Extensions;

namespace Specialist.Web.Extension {
    public static class VendorVMHelper {

        public const string Certifications = "[Certifications]";
        public const string Tests = "[Tests]";
        public const string TestsLink = "[TestsLink]";
        public static string GetCertificationDescription(
            this VendorVM model, HtmlHelper helper) {
            return GetText(() => model.Vendor.Certifications.Where(c => c.IsActive)
                .Cast<object>(), 
                model.Vendor.CertificationDescription, helper,
                Certifications);
        }

		
		public static List<string> GetTabs(UrlHelper url, Vendor vendor) {
			var urlName = vendor.UrlName;
			var tabs = new List<string> {
				H.Anchor("/vendor/" + urlName, "Обучение").ToString(), 
				vendor.CertificationDescription.IsEmpty() ? null : url.Link<VendorController>(c => c.Details(urlName, VendorVM.Tab.Certifications, null), "Сертификации").ToString(), vendor.TestingDescription.IsEmpty() ? null : url.Link<VendorController>(c => c.Details(urlName, VendorVM.Tab.Testing, 1), "Тестирование").ToString()
			};
			if (vendor.Vendor_ID == Vendor.Microsoft) {
				tabs.Add(H.Anchor(SimplePages.FullUrls.MicrosoftLabs, 
					"Виртуальные лабораторные работы Labs Online").ToString());
				tabs.Add(H.Anchor(SimplePages.FullUrls.MicrosoftExamHome, 
					"Экзамены онлайн").ToString());
				tabs.Add(H.Anchor(SimplePages.FullUrls.PlannigCourse, 
					"Курсы в разработке").ToString());
			}
			return tabs;
		}

        public static string GetTestingDescription(
            this VendorVM model, HtmlHelper helper) {

            var result = GetTestText(() => model.Vendor.Exams
                .Where(e => e.Available && e.ExamPrice > 0).Take(30), 
                model.Vendor.TestingDescription, helper, 
                Tests);

            result = GetNewText(result,
                TestsLink,
                () => helper.VendorExamLink(model.Vendor));
            return result;
        }

        private static string GetTestText(Func<IEnumerable<Exam>> entities,
          string text, HtmlHelper helper, string tag)
        {
			
            return GetNewText(text,
                tag,
                () => helper.Partial(PartialViewNames.CommonExamList, 
					entities()).ToString());
        }

        private static string GetText(Func<IEnumerable<object>> entities,
           string text, HtmlHelper helper, string tag) {
            return GetNewText(text,
                tag,
                () => Htmls.DefaultList(entities().Select(
                    c => helper.GetLinkFor(c))));
        }

        private static string GetNewText( 
            string text, string tag, Func<string> generatedText) {
            if (text.IsEmpty())
                return null;
            if (text.Contains(tag)) {
                text = text.Replace(tag, generatedText());
            }
            return text;
        }
    }
}