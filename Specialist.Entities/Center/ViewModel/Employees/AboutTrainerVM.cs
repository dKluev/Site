using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Common.Logic;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Tests;


namespace Specialist.Entities.Center.ViewModel
{
    public class AboutTrainerVM : IViewModel
    {
        public EmployeeVM Employee { get; set; }


        public SimplePage Page { get; set; }

        public PagedList<Response> Responses { get; set; }

        public PagedList<OrgResponse> OrgResponses { get; set; }

        public PagedList<Advice> Advices { get; set; }
        public PagedList<Test> Tests { get; set; }

        public List<Video> Videos { get; set; }

		public List<Certification> Certifications { get; set; }

    	public bool HasOrgResponses { get; set; }

		public bool HasResponses { get; set; }

    	public bool HasAdvices { get; set; }

    	public bool HasVideos { get; set; }
    	public bool HasTests { get; set; }
    	public bool HasPortfolio { get; set; }
    	public bool HasWorks { get; set; }




        public bool ShowPrivateContacts { get; set; }

        public AboutTrainerVM()
        {
            Responses = new PagedList<Response>();
            Advices = new PagedList<Advice>();
            Videos = new PagedList<Video>();
            OrgResponses = new PagedList<OrgResponse>();
			Tests = new PagedList<Test>();
			UserWorks = new PagedList<UserWork>();
            ShowPrivateContacts = true;
        }

        public bool IsAboutTrainer
        {
            get
            {
                return UrlName == SimplePages.Urls.AboutTrainer;
            }
        }

        public bool IsTrainerResponses
        {
            get
            {
                return UrlName == SimplePages.Urls.TrainerResponses;
            }
        }
        public bool IsTrainerVideos
        {
            get
            {
                return UrlName == SimplePages.Urls.TrainerVideos;
            }
        }

        public bool IsTrainerOrgResponses
        {
            get
            {
                return UrlName == SimplePages.Urls.TrainerOrgResponses;
            }
        }

        public bool IsAdvices { get { return UrlName == SimplePages.Urls.TrainerAdvices; } }
        public bool IsTests { get { return UrlName == SimplePages.Urls.TrainerTests; } }
        public bool IsWorks { get { return UrlName == SimplePages.Urls.TrainerWorks; } }
        public bool IsPortfolio { get { return UrlName == SimplePages.Urls.TrainerPortfolio; } }
        public bool IsContacts { get { return UrlName == SimplePages.Urls.TrainerContacts; } }
        public bool IsVideos { get { return UrlName == SimplePages.Urls.TrainerVideos; } }

        public List<SimplePage> Pages { get { return 
			SimplePages.GetAboutTrainer(HasOrgResponses, HasAdvices, HasWorks, 
			HasResponses, Employee.Employee.PublicContacts.Any(), HasTests, HasPortfolio, HasVideos); } }

        public string UrlName { get; set; }

        public string Title
        {
            get { return Employee.Employee.FullName; }
        }

    	private TextWithInfoTags _description;
    	public TextWithInfoTags Description { get {
    		return _description = _description ?? new TextWithInfoTags(Employee.Employee.SiteDescription, "[Works]");
		} }

	    public PagedList<UserWork> UserWorks { get; set; }
    }
}