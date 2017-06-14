using System.Collections.Generic;

namespace Specialist.Web.Pages {
	using System.Collections;

	public class GridData
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Records { get; set; }
        public static bool RepeatItems { get { return false; } }
        public IEnumerable<object> Rows { get; set; }
        public object UserData { get; set; }

		public GridData(int total, int page, int records, IEnumerable<object> rows, object userData = null) {
			Total = total;
			Page = page;
			Records = records;
			Rows = rows;
			UserData = userData;
		}
    }
}