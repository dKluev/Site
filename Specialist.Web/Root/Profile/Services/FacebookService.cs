using System;
using System.Globalization;
using System.IO;
using System.Net;
using Facebook;
using Specialist.Entities.Passport;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Const;

namespace Specialist.Web.Cms.Root.Socials {
	public class FacebookService {

		public static string ConnectPermission = "public_profile,email";
		public class FbUser {
			public string Email { get; set; }
			public string LastName { get; set; }
			public string FirstName { get; set; }
			public string MiddleName { get; set; }
			public string Id { get; set; }
			public bool Sex { get; set; }
			public DateTime? BirthDate { get; set; }
		}
		public FbUser GetFbUser() {
		    var client = new FacebookClient(token);
			dynamic me = client.Get("me?fields=first_name,last_name,middle_name,id,email,gender");
//			var birthDay = StringUtils.ParseDate((string)me.birthday, "MM/dd/yyyy");
			return new FbUser {
				Email = me.email,
				LastName = me.last_name,
				FirstName = me.first_name,
				MiddleName = me.middle_name,
				Id = me.id,
//				BirthDate = birthDay,
				Sex = me.gender != "female"
			};


		}

		public void PostCreateEvent(DateTime startDate, 
			string eventName, string message) {
			if (token.IsEmpty()) return;
		    var client = new FacebookClient(token);
		    var task = client.PostTaskAsync("/me/events",
			    new {
				    name = eventName,
				    description = message,
				    start_time = startDate.ToString("s"),
				    //end_time = g.DateEnd.Value.ToString("s"),
			    });
		    task.Wait();
	    }

		private string token = null;
		public FacebookService(string token) {
			this.token = token;
		}
		public void PostSpecUpdate(string message, string picture) {
			if (token.IsEmpty())
				return;
			var client = new FacebookClient(token);
			object prm = picture == null
				? (object)new {
					message = message
				}
				: new {
					message = message,
					picture = picture
				};
			var task = client.PostTaskAsync("/specialist.ru/feed", prm);
			task.Wait();
		}
		public void PostStatusUpdate(string message) {
			if (token.IsEmpty())
				return;
			var client = new FacebookClient(token);
			var task = client.PostTaskAsync("/me/feed", new {
				message = message,
			});
			task.Wait();
		}

	}
}