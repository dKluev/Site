using System;
using System.Collections.Generic;
using System.Web;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;

namespace MigrationUtils {
	public class RedirectUrlCreator {
        static readonly Dictionary<string, string> _newLinks 
			= new Dictionary<string, string>();
    	static RedirectUrlCreator() {
    		var lines = Resources.newlinks.ToLower()
				.Split(new []{'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries );
			foreach (var line in lines) {
                var parts = line.Split(new[] {';'},
                    StringSplitOptions.RemoveEmptyEntries);
				var url = parts[0].Trim();
				if(url.EndsWith("/"))
					url = url.RemoveLast();
				var url2 = parts[1].Trim();
				if(string.Equals(url, url2, StringComparison.InvariantCultureIgnoreCase))
					continue;
				if(!_newLinks.ContainsKey(url))
					_newLinks.Add(url, url2);
            }

    	}

		public static string GetUrl(string url) {
			var lowerUrl = HttpUtility.UrlDecode(url.ToLower());
			var pathWithoutQuery = RemoveAfter(lowerUrl, "?");
			return _newLinks.GetValueOrDefault(pathWithoutQuery);
		}

		private static string RemoveAfter(string url, string symbol) {
			var index = url.IndexOf(symbol);
			var pathWithoutQuery = url;
			if( index > -1)
				pathWithoutQuery = url.Substring(0, index);
			return pathWithoutQuery;
		}
	}
}