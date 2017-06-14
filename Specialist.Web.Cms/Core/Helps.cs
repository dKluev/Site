using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SubSonic.Extensions;
using System.Linq;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Web.Cms.Core {
	public class Helps {
		public static Dictionary<string, Dictionary<string, string>> Texts;
		public static Dictionary<string, string> GetHelp(Type type) {
			return Texts.GetValueOrDefault(type.Name) ?? new Dictionary<string, string>();
		} 
		static Helps() {
			var xml = XDocument.Parse(Properties.Resources.helps);
			Texts = xml.Root.Elements().ToDictionary(x => x.Name.LocalName,
				x => x.Elements().ToDictionary(y => y.Name.LocalName, y => y.Value.Trim()));

		}
	}
}