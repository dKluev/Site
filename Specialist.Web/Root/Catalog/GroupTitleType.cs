using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Web.Root.Catalog {
	public class GroupTitleType {
		public const int Webinar = 1;
		public const int Vacancy = 2;
		public const int GroupCert = 3;

		public static Dictionary<int,string> Text = new Dictionary<int, string> {
			{Webinar, "Если Вас заинтересовал вебинар, но Вы не можете на нем присутствовать, обратите внимание на следующие курсы"},
			{Vacancy, "Для повышения профессиональной квалификации в данной области рекомендуем пройти обучение по следующим курсам"},
			{GroupCert, "Чтобы стать профессионалом, мы рекомендуем Вам изучить следующие курсы"},
		}; 

		public static string Get(int type) {
			return Text.GetValueOrDefault(type) ?? "Ближайшие группы";
		}
	}
}