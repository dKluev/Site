using System;
using System.Collections.Generic;

namespace Specialist.Services.Interface
{
    public interface IUserSettingsService
    {
        string CityTC { get; set; }
        Guid SessionID { get; }
        int? VotedPollID { get; set; }
        string CaptchaText { get; set; }
    	bool CommonSite { get; set; }
	    List<string> VisitedCourses { get; set; }
	    string CityName { get; set; }
	    bool HideCityCoupon { get; set; }
	    bool NewDesign { get; set; }
	    string AdmitadId { get; set; }
    }
}