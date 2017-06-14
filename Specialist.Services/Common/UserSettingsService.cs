using System;
using System.Collections.Generic;
using System.Web;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Services.Interface;
using SimpleUtils;
using SimpleUtils.Extension;
using System.Linq;

namespace Specialist.Services
{
    public class UserSettingsService: IUserSettingsService
    {

//        public const string UserSettingsCookieName = "UserSettings";
        public const string CityTCCookieName = "CityTC";
        public const string SessionIDCookieName = "SessionID";
        public const string CaptchaTextSessionName = "CaptchaText";
        public const string PollCookieName = "PollID";
        public const string CommonSiteName = "CommonSite";
        public const string VisitedCoursesName = "VisitedCourses";
        public const string CityNameKey = "CityName";
	    public const string HideCityCouponKey = "HideCityCoupon";
	    public const string NewDesignKey = "NewDesign";
	    public const string AdmitadKey = "admit";

        public static Random Random = new Random();


        public string AdmitadId
        {
            get
            {
                return GetCookie(AdmitadKey);
            }
            set
            {
                SetCookie(AdmitadKey, value, expires: DateTime.Today.AddMonths(1));
            }
        }


        public Guid SessionID
        {
            get
            {
                var sessionIdString = GetCookie(SessionIDCookieName);
            	Guid guid;
				if(!Guid.TryParse(sessionIdString, out guid)) {
	            	guid = Guid.NewGuid();
	            	SessionID = guid;
				}
				return guid;
            }
            private set
            {
                SetCookie(SessionIDCookieName, value.ToString());
            }
        }


        public string CaptchaText { get {
            var number = HttpContext.Current.Session[CaptchaTextSessionName];
            if (number == null)
                CaptchaText = Random.Next(100000, 999999).ToString();
            return HttpContext.Current.Session[CaptchaTextSessionName].ToString();

        } 
        set {
            HttpContext.Current.Session[CaptchaTextSessionName] = value;
        }
        }

        private void SetCookie(string name, string value, bool isSession = false, DateTime? expires = null)
        {
            var cookie = HttpContext.Current.Response.Cookies[name];
            if (cookie == null)
            {
                cookie = new HttpCookie(value);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
	        if (!isSession) {
		        cookie.Expires = expires ?? DateTime.Today.AddYears(1);
	        }
            cookie.Value = value;
        }

        private string GetCookie(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie == null)
                return null;
            return cookie.Value;
        }

        public bool HasCityCookie {
            get {
                return GetCookie(CityTCCookieName) != null;
            }
        }
        
        
        public string CityTC
        {
            get
            {
               
/*
                var cityTCEncode = GetCookie(CityTCCookieName);

                var cityTC = Decode(cityTCEncode);
				if(Cities.Disable.Contains(cityTC))
					return null;
                return cityTC == string.Empty ? null : cityTC;*/
            	return Cities.Moscow;
            }
            set
            {
//                SetCookie(CityTCCookieName, StringUtils.EncodeBase64(value));

            }
        }
        public bool CommonSite
        {
            get
            {
                var commonSite = GetCookie(CommonSiteName);
                return commonSite != null;
            }
            set
            {
				if(value)
	                SetCookie(CommonSiteName, 1.ToString(), true);

            }
        }
        public bool HideCityCoupon
        {
            get
            {
                return GetCookie(HideCityCouponKey) != null;
            }
            set
            {
				if(value) SetCookie(HideCityCouponKey, 1.ToString());
            }
        }

        public bool NewDesign
        {
            get
            {
                return GetCookie(NewDesignKey) != null;
            }
            set
            {
				if(value) SetCookie(NewDesignKey, 1.ToString());
            }
        }


        public List<string> VisitedCourses
        {
            get
            {
                return StringUtils.SafeSplit(StringUtils.DecodeBase64(GetCookie(VisitedCoursesName)));
            }
            set
            {
                SetCookie(VisitedCoursesName, 
					StringUtils.EncodeBase64(value.Distinct().Take(15).JoinWith(",")));
            }
        }

        public string CityName
        {
            get
            {
                return StringUtils.DecodeBase64(GetCookie(CityNameKey));
            }
            set
            {
                SetCookie(CityNameKey, StringUtils.EncodeBase64(value));
            }
        }

        public int? VotedPollID
        {
            get
            {
                var poll = GetCookie(PollCookieName);
                var pollID = 0;
                if (int.TryParse(poll, out pollID))
                    return pollID;
                return null;
            }
            set
            {
                SetCookie(PollCookieName, value.ToString());

            }
        }
    }
}