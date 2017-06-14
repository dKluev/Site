using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Utils;

namespace Specialist.Web.Common.Html {
	public class HtmlUtils {
		public static string RandomColor() {
			return String.Format("#{0:X6}", new Random().Next(0x1000000));
		} 
		public static Dictionary<int,string> Colors = 
		     _.List("4caf50", "3f51b5", "ff9800", "9c27b0", "f44336", 
				"cddc39", "607d8b", "00bcd4", "795548", "2196f3", "009688", "e91e63", "673ab7")
				.Select((x,i) => new {x,i}).ToDictionary(x => x.i, x => x.x);

		public static string ReplaceSpaceWithNbsp(string str) {
			return str.Replace(" ", "&nbsp;");
		}
	}

}