using System;
using System.Threading;
using System.Web;
using System.Linq;
using NLog;

namespace MigrationUtils.Web.Handler {
	public class RedirectOrlUrlsModule: IHttpModule {
		public void Init(HttpApplication context) {
			context.BeginRequest += ContextOnBeginRequest;
			 context.PreSendRequestHeaders += OnPreSendRequestHeaders;

		}

		void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
			if(HttpContext.Current != null && !HttpContext.Current.Request.IsLocal)
				HttpContext.Current.Response.Headers.Set("Server", "");
        }


		private static void ContextOnBeginRequest(object sender, EventArgs eventArgs) {
			var context = HttpContext.Current;

			var path = context.Request.Url.PathAndQuery;
			var lowerPath = path.ToLowerInvariant();
			if(path == "/" || lowerPath.StartsWith("/content/") 
				|| lowerPath.StartsWith("/scripts/")
				|| lowerPath.Contains("favicon") || lowerPath.Contains("robots.txt"))
				return;
			
			try {
				var url = RedirectUrlCreator.GetUrl(path);
                if (url != null)
                {
                    context.Response.RedirectPermanent(url, true);
                }
			}
			catch(ThreadAbortException){}
			catch(Exception e) {
				LogManager.GetCurrentClassLogger().ErrorException(
					"redirect " + path + " " + e.Message, e);
			}
		}

		public void Dispose() {
		}


	}
}