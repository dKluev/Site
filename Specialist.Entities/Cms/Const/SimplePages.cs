using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Context.Const
{
    public static class SimplePages
    {
        public const string Center = "Center";

        public const string MainPage = "MainPage";

        public const string Courses = "Courses";

        public const string Trainers = "Trainers";

        public const string SpecialistTV = "SpecialistTV";

        public const string Managers = "Managers";

        public const string SpecialOffers = "SpecialOffers";
        
        public const string Locations = "Locations";

        public const string HostingInfo = "HostingInfo";

        public const string HotGroups = "HotGroups";

        public const string Career = "Career";

        public const string Job = "Job";


        public const string AboutTrainer = "AboutTrainer";

        public const string CorporateClients = "CorporateClients";

        public const string PersonalManager = "PersonalManager";
        public const string OflManager = "OflManager";

        public const string UsefulInformation = "UsefulInformation";

        public const string MarketingActions = "MarketingActions";
        public const string SpecialActions = "SpecialActions";

        public const string Discounts = "Discounts";

        public const string Reserve = "Reserve";

        public const string Webinar = "Webinar";

        public const string OpenClasses = "OpenClasses";

        public const string WebinarResponses = "WebinarResponses";


    	public const string MainBenefits = "MainBenefits";

    	public const string EducationTypes = "EducationTypes";

    	public const string WeekendCourses = "WeekendCourses";

    	public const string Graduates = "Graduates";

    	public const string NewYearCourses = "NewYearCourses";

    	public const string ClassIpCamera = "ClassIpCamera";

    	public const string WebinarStudent2011 = "WebinarStudent2011";
		public const string BestStudent2011 = "BestStudent2011";
    	public const string TestingCenter = "TestingCenter";
    	public const string OnlineTesting = "OnlineTesting";

    	public const string Recruiter2011 = "Recruiter2011";

    	public const string JubileeGroups = "JubileeGroups";

    	public const string CollectionMcts = "CollectionMcts";
    	public const string SignUpWebinar = "SignUpWebinar";
    	public const string Skype = "Skype";
    	public const string MicrosoftSeminars = "MicrosoftSeminars";
	    public const string IntramuralExtramural = "IntramuralExtramural";
	    public const string IntraExtraResponses = "IntraExtraResponses";
	    public const string DiplomResponses = "DiplomResponses";

	    public const string AboutDiplom = "AboutDiplom";

	    public const string TrainingPrograms = "TrainingPrograms";

	    public const string CorpOffers = "CorpOffers";

	    public const string PartnerVacancy = "PartnerVacancy";
	    public const string Probation = "Probation";
	    public const string Unlimit = "Unlimit";
	    public const string UnlimitEng = "UnlimitEng";
	    public const string UnlimitWithCharge = "UnlimitWithCharge";
	    public const string UnlimitWithoutCharge = "UnlimitWithoutCharge";
	    public const string UnlimitNoChargeEng = "UnlimitNoChargeEng";
	    public const string WebinarInv = "WebinarInv";

	    public const int NotFound = 362;

		public static class Offers {
	    	public const string Universal = "OfferUniversal";
	    	public const string Webinar = "OfferWebinar";
	    	public const string WebinarRu = "OfferWebinarRu";
	    	public const string Bt = "OfferBt";
	    	public const string Cs = "OfferCs";
			public static Dictionary<string,string> Orgs = new Dictionary<string, string> {
				{Universal,  OurOrgs.Spec},
				{Webinar, OurOrgs.Cos},
				{WebinarRu, OurOrgs.Ru},
				{Bt, OurOrgs.Bt},
				{Cs, OurOrgs.CS},
			};
			public static List<string> All = _.List(Universal, Webinar, WebinarRu, Bt, Cs);
		}

        public static class Urls
        {
            public const string SuccessStories = "success-stories";


            public const string Responses = "responses";

            public const string Works = "works";

            public const string Projects = "projects";

            public const string AboutTrainer = "about-trainer";

            public const string TrainerAdvices = "advices";

            public const string TrainerVideos = "videos";

            public const string TrainerWorks = "works";
            public const string TrainerPortfolio = "portfolio";

            public const string TrainerContacts = "contacts";
            public const string TrainerTests = "tests";

            public const string TrainerResponses = "trainer-responses";

            public const string TrainerOrgResponses = "trainer-org-responses";

            public const string SearchParamsVacancy = "search-params-vacancy";

            public const string SearchResultsVacancy = "search-results-vacancy";

            public const string SearchParamsResume = "search-params-resume";

            public const string SearchResultsResume = "search-results-resume";

        }

        public static class FullUrls {

	        public const string Job = "/job";
	        public const string SoglasiePersDannye = "/soglasie_pers_dannye";
	        public const string FirstPaymentNews = "/news/2229";
	        public const string ForCompanies = "/center/corpeducation/corporate";
	        public const string Discount = "/special-offers/discount";
	        public const string Diplom = "/center/educationtypes/diplomn_progr";
	        public const string CorpManagers = "/center/corpeducation/personal-manager";
	        public const string Seminars = "/course/seminars";
	        public const string OpenBadgeIssuer = "/badge/issuer";
	        public const string RealSpecialist = "/special-offers/real-specialist";
        	public const string MicrosoftLabs = "/microsoft-labs-online";
        	public const string MicrosoftExamHome = "/exam-home-microsoft";
	        public const string PlannigCourse = "/section/planning-courses";
        	public const string WebinarSiteTerm = "/dictionary/definition/webinar";
        	public const string CareerDay = "/news/1843";
        	public const string OnlineTesting = "/online-testing";
        	public const string BetterService = "/better-service";
        	public const string Price = "/center/about-center/price";
        	public const string SpecialistTV = "/videos";
        	public const string Licences = "/center/about-center/licences";

        	public const string PartnerWebinar = "/partners-webinar";

        	public const string TestCertOrder = "/zakaz-certificate";
        	public const string Partners = "/partners-courses";
        	public const string Offert = "/oferta-univer-spec";
        	public const string OpenClasses = "/center/educationtypes/open-classes";
        	public const string IntraExtramural = "/center/educationtypes/intramural-extramural-classes";
        	public const string Tracks = "/center/educationtypes/tracks";
        	public const string BestGraduate = "/center/references/bestgraduate2011";
        	public const string ExcelMaster = "/profession/excel-master";

			

            public const string GroupDiscounts = 
                "/special-offers/groups-discounts";
            public const string Activities = 
                "/special-offers/activities";
            public const string Responses =
                "/center/references/private-persons/responses";
            public const string DocumentsOff =
                "/center/about-center/documents-off";
            public const string CatalogInfo = "/center/news/cataloginfo";
            public const string CorpActions = "/center/corpeducation/corp-offers";
            public const string Reserve = 
                "/special-offers/reserve";
            public const string Individual =
                "/center/educationtypes/individual";
            public const string Elearning =
                "/elearning";
            public const string Webinar =
                "/center/educationtypes/webinar";
            public const string Unlimited = "/special-offers/activities/unlimited-education";
            public const string TrainingPrograms = "/center/educationtypes/refresher_course";
            public const string ProfileInstruction = "/instruction_lk";

            public const string Fulltime =
                "/center/educationtypes/fulltime";
        	public const string Testing =
        		"/center/about-center/testingcenter";

        	public const string Awards =
        		"/center/about-center/awards";

        	public const string Authorizations =
        		"/center/about-center/authorizations";

        	public const string Payment =
        		"/center/about-center/payments";

        	public const string Vendor = "/vendor/";

        	public const string Trainers = "/center/comand/trainers";

        }


            public static List<SimplePage> GetAboutTrainer(bool hasOrgResponses, bool hasAdvices,
				bool hasWorks, bool hasResponses, bool hasContacts, bool hasTests, bool hasPortfolio, bool hasVideos) {
            	var result = new List<SimplePage> {
            		new SimplePage{UrlName = Urls.AboutTrainer, Title = "О преподавателе"},
            	};
				if(hasResponses)
					result.Add(
						new SimplePage{UrlName = Urls.TrainerResponses, Title = "Отзывы"});
				if(hasOrgResponses)
					result.Add(
						new SimplePage {
							UrlName = Urls.TrainerOrgResponses,
							Title = "Корпоративные отзывы"
						});
				if(hasAdvices)
					result.Add(
						new SimplePage {
							UrlName = Urls.TrainerAdvices,
							Title = "Советы"
						});
				if(hasPortfolio)
					result.Add(
						new SimplePage {
							UrlName = Urls.TrainerPortfolio,
							Title = "Работы преподавателя"
						});
				if(hasWorks)
					result.Add(
						new SimplePage {
							UrlName = Urls.TrainerWorks,
							Title = "Работы выпускников"
						});
				if(hasContacts)
					result.Add(
						new SimplePage {
							UrlName = Urls.TrainerContacts,
							Title = "Контакты"
						});
				if(hasTests)
					result.Add(
						new SimplePage {
							UrlName = Urls.TrainerTests,
							Title = "Тесты"
						});
				if(hasVideos)
					result.Add(
						new SimplePage {
							UrlName = Urls.TrainerVideos,
							Title = "Видео"
						});
            	return result;
            }

    	public static List<SimplePage> GetPrivatePerson() {
			/*var relation = 
				new SimplePageRelation{IsMainParent = true, SimplePage = 
				new SimplePage{UrlName = Urls.PrivatePerson, }};*/

    		var privatePerson = new List<SimplePage> {
    			new SimplePage{UrlName = Urls.SuccessStories, Title = "Истории успеха" },
    			new SimplePage{UrlName = Urls.Responses, Title = "Отзывы слушателей о центре компьютерного обучения " +
					StringUtils.AngleBrackets("Специалист") },
    			new SimplePage{UrlName = Urls.Works, Title = "Работы выпускников"},
    		};
    		/*foreach (var simplePage in privatePerson) {
    			simplePage.SimplePageRelations.Add(relation);
    		}*/
    		return privatePerson;
        }

        public static List<SimplePage> GetCorporateClients() {
            return new List<SimplePage> {
                new SimplePage{UrlName = Urls.Responses, Title = "Отзывы"},
                new SimplePage{UrlName = Urls.Projects, Title = "Проекты"},
            };
        }

        public static List<SimplePage> GetSearchVacancy()
        {
            return new List<SimplePage> {
                new SimplePage{UrlName = Urls.SearchParamsVacancy, Title = "Задать параметры поиска"},
                new SimplePage{UrlName = Urls.SearchResultsVacancy, Title = "Результаты поиска"},
            };
        }

        public static List<SimplePage> GetSearchResume()
        {
            return new List<SimplePage> {
                new SimplePage{UrlName = Urls.SearchParamsResume, Title = "Задать параметры поиска"},
                new SimplePage{UrlName = Urls.SearchResultsResume, Title = "Результаты поиска"},
            };
        }
    }
}
