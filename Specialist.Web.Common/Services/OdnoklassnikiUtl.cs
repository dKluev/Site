using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Utils;
using Specialist.Services.Common;

namespace Specialist.Web.Cms.Root.Socials {
	public static class OdnoklassnikiUtl {

		public const string appId = "1088997888";
		public const string secretKey = "9F57CC3901B119975D4ED1AA";


		public const string AccessUrl =
			"http://www.odnoklassniki.ru/oauth/authorize?client_id=1088997888&scope=PUBLISH_TO_STREAM;GROUP_CONTENT&response_type=code&redirect_uri=http://www.specialist.ru";

		public static string GetRefreshToken(string code) {
			using (var wb = new WebClient())
			{
			    var data = new NameValueCollection();
			    data["code"] = code;
			    data["client_id"] = appId;
			    data["client_secret"] = secretKey;
			    data["redirect_uri"] = "http://www.specialist.ru";
			    data["grant_type"] = "authorization_code";
			    dynamic response = JsonConvert.DeserializeObject(
					Encoding.Default.GetString(wb.UploadValues("https://api.odnoklassniki.ru/oauth/token.do", "POST", data)));

				return response["refresh_token"];
			}
		}
	}
}