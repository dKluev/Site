using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;

namespace Specialist.Web.Root.Profile.Logic {
	public class ProfileMenu {
		public ProfileMenu(string name, string icon) {
			Name = name;
			Icon = icon;
			Links = new List<string>();
		}

		public string Name { get; set; }
		
		public string Icon { get; set; }
		public bool IsLearning {get { return Icon == "rasp"; }}
		public List<string> Links { get; set; }

		public ProfileMenu Add(params object[] link) {
			Links.AddRange(link.Select(x => x.ToString()));
			return this;
		}
	}
}