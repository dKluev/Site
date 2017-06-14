using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;
using Json = System.Collections.Generic.Dictionary<string, object>;

namespace Console {

	public class UserTable {
		public List<string> Captions { get; set; }
		public List<SendsayUser> Users { get; set; }
		public UserTable(List<string> captions, List<SendsayUser> users) {
			Captions = captions;
			Users = users;
		}
	}

	public class SendsayUser {
		public string Email { get; set; }

		public string FullName { get; set; }

		public List<string> Custom { get; set; }

		public SendsayUser(string email, string fullName, List<string> custom) {
			Email = email;
			FullName = fullName;
			Custom = custom;
		}
	}
	public class SendIssueParams {
		public string Name { get; set; }
		public string Subject { get; set; }
		public string FromName { get; set; }
		public string FromEmail { get; set; }
		public int DraftId { get; set; }
		public List<string> EmailList { get; set; }

		public SendIssueParams(
			string name, string subject, string fromName,
			string fromEmail, int draftId, List<string> emailList) {
			Name = name;
			Subject = subject;
			FromName = fromName;
			FromEmail = fromEmail;
			EmailList = emailList;
			DraftId = draftId;
		}
	}
	public class SendsayApi {
		const string root = "https://api.sendsay.ru";

		private string session;

		const string loginAction = "login";

		JObject Request(string action, object prm) {
			var jObject = JObject.FromObject(prm);
			jObject.Add("action", action);
			if (action != loginAction) {
				session = Login();
				jObject.Add("session", session);
			}
			var r = RequestJson(jObject.ToString());
			if (action != loginAction) {
				session = null;
			}
			return r;
		}

		private static JObject RequestJson(string requestJson) {
			var param = new NameValueCollection();
			param["apiversion"] = "100";
			param["json"] = "1";
			param["request"] = requestJson;
			var json = PostWithErrorHandler(root, param);
			var redirect = json["REDIRECT"];
			if (redirect != null) {
				json = PostWithErrorHandler(root + redirect, param);
			}

			return json;
		}


		public static string Post(string url, NameValueCollection param) {
			using (var wb = new WebClient()) {
				var result = Encoding.UTF8.GetString(wb.UploadValues(url, "POST", param));
				return result;
			}
		}

		private static JObject PostWithErrorHandler(string url, NameValueCollection param) {
			var jsonTxt = Post(url, param);
			var json = JsonConvert.DeserializeObject(jsonTxt) as JObject;
			if (json["errors"] != null) {
				throw new Exception(json["errors"].ToString());
			}
			return json;
		}

		string Login() {
			var x = Request(loginAction, new {
				login = "specialist",
				sublogin = "specialist",
				passwd = "he4Jeech"
			});
			return x["session"].ToString();
		}
 
		public string ImportUsers(UserTable table) {
			var x = Request("member.import", new Json {
				{"users.list", CreateUsersListObject(table)},
				{"auto_group", new {id = "p168"}},
				{"if_exists", "overwrite" }
			});
			return x["track.id"].ToString();
		}


		public string SendIssue(SendIssueParams param) {
			var emailList = param.EmailList;
//			var emails = CreateUsersList(emailList);
			var emails = param.EmailList.JoinWith(",");
			var r = Request("issue.send", new Json {
				{"name", param.Name},
				{"letter", new Json {
					{"subject", param.Subject},
					{"from.name", param.FromName},
					{"from.email", param.FromEmail},
					{"draft.id", param.DraftId}
				}},
				{"group", "masssending"},
				{"sendwhen", "now"},
				{"users.list", emails}
			});
			return r["track.id"].ToString();

		}

		private string GroupList() {
			return Request("group.list", new {}).ToString();
		}

		private static object CreateUsersListObject(UserTable table) {
			var emails = new {
				caption = _.List(
					new { anketa = "member", quest = "email" },
					new { anketa = "a867", quest = "q960" }
					).Concat(table.Captions.Select(x => new { anketa = "a867", quest = x })).ToList(),
				rows = table.Users.Select(x => _.List(x.Email, x.FullName).Concat(x.Custom).ToList())
			};
			return emails;
		}


		private static object CreateUsersList(List<string> emailList) {
			var emails = emailList.Select(e => new {
				member = new {
					email = e
//					addr_type = "email"
				}
			});
			return emails;
		}

		public string TrackStatus(string id) {
			var r = Request("track.get", new {
				id = id
			});

			return r["obj"]["status"].ToString();
		}

		public void Test() {
			//			var x = IssueDraftList();
			var emailList = new List<string> {"test6@mailinator.com"};
			var table = new UserTable(_.List("q400"), 
				_.List(new SendsayUser("test6@mailinator.com", "Иван Иванович", _.List("KABC"))));
			var param = new SendIssueParams(
				"Тест 2",
				"Тестовая рассылка",
				"ИНФО",
				"info@specialist.ru",
				302,
				emailList
				);
			var x = SendIssue(param);
//			var x = TrackStatus("9619");
//			var x = GroupList();
//			var x = ImportUsers(table);
			System.Console.WriteLine(x);
		}
	}
}