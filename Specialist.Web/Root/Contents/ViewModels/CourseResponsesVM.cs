using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Utils;
using Specialist.Entities.Catalog.Links.Interfaces;

namespace Specialist.Web.Root.Contents.ViewModels {
	public class CourseResponsesVM: IViewModel {
		public Course Course { get; set; }

		public PagedList<Response> Responses { get; set; }

		public List<OrgResponse> OrgResponses { get; set; }

		public List<string> Tabs() {
			var result = _.List("������� ����");
			if(OrgResponses.Any())
				result.Add("������������� �������");
			return result;

		}

		public string Title {
			get { return "������ �� ����� " + StringUtils.AngleBrackets(Course.GetName()); }
		}
	}
}