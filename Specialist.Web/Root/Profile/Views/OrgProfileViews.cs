using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;
using Specialist.Entities.Context;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Web.Helpers;

namespace Specialist.Web.Root.Profile.Views {
	public class OrgProfileViews:H {
		public OrgProfileViews(UrlHelper url) {
			Url = url;
		}



		public UrlHelper Url { get; set; }
		public object OrgGroups(List<Group> groups, bool hideHead = false) {
			var head = hideHead ? null : H.Head("Группа", "Курс", "Дата", " ");
			return H.table.Class("table")[head,
				groups.Select(x => H.Row(
					Url.OrgGroupLink(x),
					x.Course.WebName,
					x.DateInterval,
					Url.OrgGroupLink(x, "Сотрудники").OpenInUiDialog()
					))];
		}

	}
}