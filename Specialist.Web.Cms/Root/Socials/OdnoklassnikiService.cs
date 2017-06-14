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
	public class OdnoklassnikiService {

		private const string appId = OdnoklassnikiUtl.appId;
		private const string publicKey = "CBALMJOBEBABABABA";
		private const string secretKey = OdnoklassnikiUtl.secretKey;
		private const string root = "http://api.odnoklassniki.ru/fb.do?";

		static string GetRefreshToken() {
//			return "20618f93f0fdb67416c1f06bb744113de6e_567889641645_146470852";
	        var unity = Cms.MvcApplication.Container;
    		var simpleValueService = unity.Resolve<SimpleValueService>();
			return simpleValueService.OkRefreshToken;
		} 

		private const string code = "1kge2vFCUpeIx7OHy3OFr2HVv2uViXKve8DAink0HGp4hp2IUxByCYLyuKJz6jQREc2mLgvbwu5e3E39Jl6u1P4V7WLsxGP8LRuCB1qaaaTqkxKCLBTFYGk7vUMdUtEkNpZeYvJk9CTSuW4Du6O73J6uGuAxOAKoCyrcsesw4yE7Wve";

		public void PostNews(string url) {
			var token = GetNewToken();
			var attachment = JsonConvert.SerializeObject(new {
				media = _.List<object>(new {type="link", url=url}) });
			var args = _.List(
				Tuple.Create("application_key", publicKey),
				Tuple.Create("attachment", attachment),
				Tuple.Create("gid", "44188494790769"),
				Tuple.Create("method", "mediatopic.post"),
				Tuple.Create("type", "GROUP_THEME")
				);
			var param = args.Select(x => x.Item1 + "=" + x.Item2).JoinWith("");
			var sigSecret = StringUtils.GetMd5(token + secretKey);
			var sig = StringUtils.GetMd5(param + sigSecret);
			args.Add(Tuple.Create("access_token", token));
			args.Add(Tuple.Create("sig", sig));
			var request = args.Select(x => x.Item1 + "=" + x.Item2).JoinWith("&");
			GetApi(root + request);

		}
		 
		private static string GetApi(string reqStr) {
			using (var webClient = new WebClient()) {
				webClient.Proxy = null;
				webClient.Encoding = Encoding.UTF8;
				var result = webClient.DownloadString(reqStr);
				if (result.Contains("error")) {
					throw new Exception(result);
				}
				return result;
			}
		}

		public static string GetNewToken() {
			using (var wb = new WebClient())
			{
			    var data = new NameValueCollection();
			    data["refresh_token"] = GetRefreshToken();
			    data["grant_type"] = "refresh_token";
			    data["client_id"] = appId;
			    data["client_secret"] = secretKey;
			    var response = 
					Encoding.Default.GetString(wb.UploadValues("http://api.odnoklassniki.ru/oauth/token.do", "POST", data));
				var token = JsonConvert.DeserializeAnonymousType(response, new {access_token = ""}).access_token;
				return token;
			}
		}


		public string GetToken() {
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