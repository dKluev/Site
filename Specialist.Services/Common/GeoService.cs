using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Util;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Utils;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Utils.Logic;

namespace Specialist.Services.Common {
    public class GeoService {

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        [Dependency]
        public SimpleValueService SimpleValueService { get; set; }



	    public const string None = "none";
	    public static bool IsLogSended;

		public const string Rostov = "ростов";

	    public bool IsCityCoupon {
		    get { return CheckCityCoupon(UserSettingsService.CityName); }
		    
	    }

	    public bool CheckCityCoupon() {
		    var cityName = GetCityName();
		    return CheckCityCoupon(cityName);
	    }

	    private bool CheckCityCoupon(string cityName) {
		    return cityName != null && cityName.ToLower() == Rostov && CouponUtils.CityIsActive(SimpleValueService.CityCouponCount);
	    }

	    public string GetCityName() {
		    var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
			var cityName = UserSettingsService.CityName;
			if (cityName.IsEmpty()) {
				try {
					cityName = GetIpGeoBaseCityName(ip);
					UserSettingsService.CityName = cityName ?? None;
				}
				catch (Exception e) {
					if (!IsLogSended) {
						Logger.Exception(e, "geo service");
					}
					IsLogSended = true;
				}
			}
			return cityName;
		}



		public static string GetIpGeoBaseCityName(string ip) {
			using (var webClient = new WebClient()) {
				webClient.Proxy = null;
				var result = webClient.DownloadString("http://ipgeobase.ru:7020/geo?ip=" + ip);
				var xml = XDocument.Parse(result);
				var city = xml.Descendants("city").FirstOrDefault();
				return city == null ? null : city.Value;
			}
		}
	}
}