using System;
using System.Web;

namespace Specialist.Web.Common.Mvc.Extensions {
	public static class AjaxRequestExtension {
		 public static bool IsPost(this HttpRequest request) {
		 	return request.HttpMethod.ToLower() == "post";
		 }
	
		 public static bool IsAjaxRequest(this HttpRequest request) {
            if (request == null) { 
                throw new ArgumentNullException("request"); 
            }
 
            return (request["X-Requested-With"] == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest"));
        } 
	}
}