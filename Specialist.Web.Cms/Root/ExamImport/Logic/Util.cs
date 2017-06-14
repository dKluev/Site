using System.IO;
using System.Net;
using System.Text;

namespace WebSpider {
	public static class Util {
		/*public static Image GetImage(string url)
        {
            Stream str = null;
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(url);
            wReq.Proxy = null;
            HttpWebResponse wRes = (HttpWebResponse)(wReq).GetResponse();
            str = wRes.GetResponseStream();

            return Image.FromStream(str);
        }

*/

		public static string GetHtml(string url, ref CookieCollection
			cookies, string postData = null) {
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.AllowAutoRedirect = false;
			request.Proxy = null;
			if (postData != null) {
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				var bytes = Encoding.UTF8.GetBytes(postData);
				request.ContentLength = bytes.Length;
				using(var stream = request.GetRequestStream())
					stream.Write(bytes,0, bytes.Length);
			}

			if (cookies != null) {
				request.CookieContainer = new CookieContainer();
				request.CookieContainer.Add(cookies);
			}

			using (var response = request.GetResponse()) {
				using (var stream = response.GetResponseStream()) {
					using (var reader = new StreamReader(stream)) {
						var htmlText = reader.ReadToEnd();
						var responseCookie = ((HttpWebResponse) response).Cookies;
						if(responseCookie.Count > 0)
							cookies = responseCookie;
						return htmlText;
					}
				}
			}
		}
	}
}