using System;
using System.Web;
using System.Web.Mvc;

namespace Specialist.Web.Common.Mvc.ActionResults {
    public class DownloadResult : ActionResult
    {
        public DownloadResult(string fileDownloadName, string filePath) {
            FileDownloadName = fileDownloadName;
            FilePath = filePath;
        }

        public string FileDownloadName
        {
            get;
            set;
        }

        public string FilePath { get; set; }

	    public static void AddContentDisposition(HttpResponseBase response, string fileDownloadName) {
		    response.AddHeader("content-disposition",
			    "attachment; filename=" + fileDownloadName);
	    }
	    public void AddContentDisposition() {
	    }

        public override void ExecuteResult(ControllerContext context)
        {
            if (!String.IsNullOrEmpty(FileDownloadName))
            {
                AddContentDisposition(context.HttpContext.Response, FileDownloadName);
            }

            context.HttpContext.Response.TransmitFile(FilePath);
        }

    }
}