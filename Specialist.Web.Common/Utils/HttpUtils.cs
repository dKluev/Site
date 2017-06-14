using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Specialist.Entities.Utils {
	public static class HttpUtils {

        public static string ToQueryString(this NameValueCollection collection)
        {
            var result = string.Empty;
            for (int i = 0; i < collection.Keys.Count; i++)
            {
                var key = collection.Keys[i];
                result += key + "=" + HttpUtility.UrlEncode(collection[key]);
                if (i != collection.Keys.Count - 1)
                    result += "&";
            }
            return result;
        }
		public static NameValueCollection Convert(object x) {

			NameValueCollection formFields = new NameValueCollection();

			x.GetType().GetProperties()
				.ToList()
				.ForEach(pi => formFields.Add(pi.Name, pi.GetValue(x, null).ToString()));
			return formFields;
		}


		public static string Get(string url, NameValueCollection param = null) {
			using (var wb = new WebClient()) {
				if (param != null) {
					url = url + "?" + ToQueryString(param);
				}
				var result = wb.DownloadString(url);
				return result;
			}
		}


		public static byte[] FileToBytes(HttpPostedFileBase file) {
			using (var ms = new BinaryReader(file.InputStream)) {
				file.InputStream.Position = 0;
				return ms.ReadBytes(file.ContentLength);
			}
		}

		public static string Post(string url, NameValueCollection param) {
			using (var wb = new WebClient()) {
				var result = Encoding.UTF8.GetString(wb.UploadValues(url, "POST", param));
				return result;
			}
		}

		public static string Post(string url, object param) {
			using (var wb = new WebClient()) {
				var result = Encoding.UTF8.GetString(wb.UploadValues(url, "POST", Convert(param)));
				return result;
			}
		}
	}
}