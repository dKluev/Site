using System;
using System.Collections.Generic;
using Specialist.Entities.Utils;
using System.Linq;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Entities.Const {
	public class TopSearch {
		public static IEnumerable<Tuple<string, int>> List = _.List(

			"excel",
			"java",
			"php",
			"sql",
			"html",
			"c",
			"linux",
			"cisco",
			"itil",
			"python",
			"javascript",
			"sharepoint",
			"android",
			"seo",
			"autocad",
			"sap",
			"PMI",
			"oracle",
			"joomla",
			"photoshop",
			"c#",
			"asterisk",
			"access",
			"ccna",
			"project",
			"битрикс",
			"vba",
			"ruby"
			).Select((x,i) => Tuple.Create(x, 5 - i/5)).Shuffle();
	}
}