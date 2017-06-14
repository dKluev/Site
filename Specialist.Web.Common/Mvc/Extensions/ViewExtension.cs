using System;
using System.IO;
using System.Web.Mvc;

namespace Specialist.Web.Common.Mvc.Extensions {
	public static class ViewExtension {
		 public static string GetWebFormViewName(this IView view) {
                if (view is WebFormView) {
                        string viewUrl = ((WebFormView)view).ViewPath;
                        string viewFileName = viewUrl.Substring(viewUrl.LastIndexOf('/'));
                        string viewFileNameWithoutExtension = Path.GetFileNameWithoutExtension(viewFileName);
                        return (viewFileNameWithoutExtension);
                } else {
                        throw (new InvalidOperationException("This view is not a WebFormView"));
                }
        }

	}
}