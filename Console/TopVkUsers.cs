using System.IO;
using System.Linq;
using Specialist.Entities.Utils;
using Specialist.Web.Cms.Root.Socials;
using Specialist.Web.Const;

namespace Console {
	public class TopVkUsers {
		public void CreateFile() {
			var users = new VkontakteService(VkontakteService.Token).UsersSearch();
			var data = CsvUtil.Render(users);
			File.WriteAllText("topusers.csv", data);
		} 
	}
}