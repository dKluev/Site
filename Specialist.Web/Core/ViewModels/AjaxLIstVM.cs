using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleUtils.Utils;

namespace Specialist.Web.Pages {
	public class AjaxGridVM {

		public class Column {
			public string name { get; set; }
			public string index { get; set; }
			public bool sortable { get; set; }
			public Column(string name) {
				this.name = name;
				this.index = name;
				sortable = false;
			}
		}

		public bool OpenDialogsInPage { get; set; }

		public string Postfix {get { return StringUtils.UrlToHtmlId(GetListUrl); }}
		public string GetListUrl { get; set; }
		public string EditUrl { get; set; }
		public string DeleteUrl { get; set; }
		public string ViewUrl { get; set; }

		public List<string> ColumnTitles { get; set; }

		public List<Column> Columns { get; set; }

		public string Caption { get; set; }

		public bool DenyEdit { get; set; }

		public bool DenyAdd { get; set; }

		public AjaxGridVM() {
			Columns = new List<Column>();
			ColumnTitles = new List<string>();
		}
	}
}