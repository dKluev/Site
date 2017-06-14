using System;
using System.Collections.Generic;
using Specialist.Services.Interface;

namespace Specialist.Web.Cms.Repository
{
    public class CmsUserSettingsService: IUserSettingsService
    {
        public string CityTC
        {
            get { return "Москва"; }
            set { throw new NotImplementedException(); }
        }

        public Guid SessionID
        {
            get { throw new NotImplementedException(); }
        }

        public int? VotedPollID
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string CaptchaText {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

    	public bool CommonSite {
    		get { throw new NotImplementedException(); }
    		set { throw new NotImplementedException(); }
    	}

	    public List<string> VisitedCourses { get; set; }
	    public string CityName { get; set; }
	    public bool HideCityCoupon { get; set; }
	    public bool NewDesign { get; set; }
	    public string AdmitadId { get; set; }
    }
}