using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Utils;

namespace Console {
	public class ResponsePhotoService {

		public void Update() {
			var context = new PassportDataContext();
			var photos = context.UserContacts.Where(x => x.ContactTypeID == ContactTypes.VKontakte 
				&& x.User.Student_ID > 0)
				.Select(x => new {x.User.Student_ID, x.Contact})
				.ToDictionary(x => x.Student_ID.Value, x => GetVkUrl(x.Contact));
			var students = photos.Keys.ToList();
			var specContext = new SpecialistDataContext();
			var responses = specContext.GetTable<Response>().Select(x => new {x.RawQuestionnaire.Questionnaire.Student_ID, Response = x})
				.Where(x => students.Contains(x.Student_ID)).ToList();
			foreach (var response in responses) {
				var vkUrl = photos[response.Student_ID];
				if (vkUrl != null) {
					response.Response.PhotoUrl = vkUrl.Item1;
					response.Response.SocialUrl = vkUrl.Item2;
				}
			}
			specContext.SubmitChanges();
		}
		public string GetVkUserId(string vkUrl) {
			return vkUrl.Remove("vk.com/").Remove("#/");
		}
		public Tuple<string,string> GetVkUrl(string vkUrl) {
			var userId = GetVkUserId(vkUrl);
			if (userId.IsEmpty()) {
				return null;
			}
			var url = "http://api.vkontakte.ru/method/users.get?uids={0}&fields=photo_50".FormatWith(userId);
			var json = JsonConvert.DeserializeObject(HttpUtils.Get(url)).As<JObject>();
			if (json["error"] != null) {
				return null;
			}
			var data = json["response"][0];
			if (data["deactivated"] != null) {
				return null;
			}

			var photoUrl = data["photo_50"].Value<string>();
			var socialUrl = "http://" + vkUrl;
			return Tuple.Create(photoUrl, socialUrl);
		} 
		public string GetFbUserId(string url) {
			if (url.Contains("?id=")) {
				return StringUtils.GetRegGroupValue(url, "id=(\\d+)");
			}
			return url.Remove("facebook.com/");
		}
	}
}