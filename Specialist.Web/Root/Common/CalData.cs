using System;

namespace Bing {
	public class CalData {
		public DateTime Begin { get; set; } 
		public DateTime End { get; set; } 
		public string Location { get; set; }
		public string Title { get; set; }
		public CalData(DateTime begin, DateTime end, string location, string title) {
			Begin = begin;
			End = end;
			Location = location;
			Title = title;
		}
	}
}