using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SimpleUtils.Collections;
using SimpleUtils.Utils;
using Specialist.Entities.Passport;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Services;

namespace Specialist.Web.Cms.Root.Socials {
	public class VkontakteService {
	    public const string Token =
	"3c9d190ed71b71fc1d62954706641eebf3c8e35225e52d15f2968b8badddc8b64be4161aedf8676c89b87";
	    public const string GroupsToken =
	"7a686f0acbfce3a45699c718377d8065d832f2de1a0ff42d2499c0e487db97f0beac507a3f2feddfa301c";

	    public const string TestToken =
	"991c124620b5346dbdd1d79dce23df92d6bd96bf4eff21387885a3b8ba4581d9e2653a6d9be353d3d3817";

		private string token = null;

		public VkontakteService(string token) {
			this.token = token;
		}


		private const string root = "https://api.vk.com/method/";

		public void PostStatusUpdate(string message) {
			if (token.IsEmpty()) return;

			var reqStr = string.Format(root + "wall.post?access_token={0}&message={1}",
				token,message);

			PostToApi(reqStr);
		}

		private const int SpecGroupId = 2190892;

		public string PostPhoto(string path) {
			var url = GetUploadUrl();
			var uri = new Uri(url);
			var client = new RestClient("http://" + uri.Host);
			var request = new RestRequest(uri.PathAndQuery, Method.POST);
			request.AddFile("photo", path);
			var c = client.Execute(request).Content;
			var p = JObject.Parse(c).As<dynamic>();
			var result = PostToApi2("photos.saveWallPhoto", 
				new {gid=SpecGroupId, p.hash, p.photo, p.server});
			return result.response[0].id;
		}

		string GetValue(JToken o, string name) {
			var v = o[name];
			return v == null ? null : v.Value<string>();
		}

		public List<List<string>> UsersSearch() {
			var usersSearch = GetJObject("users.search", new {city=1, age_from=18, count=1000,
				fields="interests,company,position,religion"})["response"].Children().Skip(1)
				.Select(x => new List<string>{
					GetValue(x, "uid"), 
					GetValue(x, "last_name"), 
					GetValue(x, "first_name"), 
					GetValue(x, "interests"), 
					GetValue(x, "religion"), 
					GetValue(x, "company"), 
					GetValue(x, "position"), 
					}).ToList();
			return usersSearch;
		}


		public long CreateGroup(string title, decimal captchaId, string captchaKey) {
			var param = new object();
			if (captchaId > 0) {
				param = new {title = title, type = "group", captcha_sid = captchaId, captcha_key = captchaKey};
			} else {
				param = new {title = title, type = "group"};
				
			}
			var result = PostToApi2("groups.create", param);
			return result.response.gid;
		}

		private string GetUploadUrl() {
			var result = PostToApi2("photos.getWallUploadServer", new {gid=SpecGroupId});
			string url = result.response.upload_url;
			return url;
		}

		public void PostSpecUpdate(string message, string picture) {
			if (token.IsEmpty()) return;
			var path = Urls.WebToSys(picture);
			var photoId = File.Exists(path) ? PostPhoto(path) : null;
			PostToApi2("wall.post", new {message, owner_id = -SpecGroupId, from_group = 1, attachments = photoId});
		}

		private dynamic PostToApi2(string methodName, object prms) {
			var jObject = GetJObject(methodName, prms);
			return jObject.As<dynamic>();
		}

		private JObject GetJObject(string methodName, object prms) {
			var type = prms.GetType();
			var props = type.GetProperties();
			var result = props.Select(x => x.Name + "=" + x.GetValue(prms, null)).JoinWith("&");
			var reqStr = methodName + "?access_token={0}&".FormatWith(token) + result;

			var jObject = JObject.Parse(PostToApi(root + reqStr));
			return jObject;
		}

		private static string PostToApi(string reqStr) {
			using (var webClient = new WebClient()) {
				webClient.Proxy = null;
				webClient.Encoding = Encoding.UTF8;
				var result = webClient.DownloadString(reqStr);
				if (result.Contains("captcha_sid")) {
					var data = (JObject.Parse(result) as dynamic).error;
					var id = (decimal) data.captcha_sid;
					var img = data.captcha_img.ToString();
					var ex = new CaptchaException(id, img);
					throw ex;
				}
				if (result.Contains("error")) {
					throw new Exception(result);
				}
				return result;
			}
		}
	}
}