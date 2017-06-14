using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;
using NLog;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Passport;
using Specialist.Entities.Utils;
using Specialist.Services.Interface.Passport;
using SimpleUtils;

namespace Specialist.Services.Utils
{
    public static class Logger
    {
	    public static Exception GetFromAggregate(AggregateException ex) {
		    var inner = ex.InnerException as AggregateException;
		    return inner != null 
				? GetFromAggregate(inner) 
				: ex.InnerException;
	    }

	    public static void Run(Action action, string name) {
		    try {
			    action();
		    } catch (Exception e) {
			    Exception(e,name);
		    }
	    }

	    public static void Error(string sysMessage) {

            GDC.Set("title", sysMessage);
            LogManager.GetCurrentClassLogger().Error(sysMessage);
	    }

	    public static void Exception(Exception ex, string sysMessage)
        {
            GDC.Set("title", sysMessage);
            var builder = new StringBuilder();
            builder.AppendLine();
            if(!sysMessage.IsEmpty())
                builder.AppendLine("sysmessage: " + sysMessage);
            builder.AppendLine("type: " + ex.GetType());
            builder.AppendLine("message: " + ex.Message);
            builder.AppendLine("source: " + ex.Source);
            builder.AppendLine("targetSite: " + ex.TargetSite);
            builder.AppendLine("stackTrace: " + ex.StackTrace);
        	var httpContext = HttpContext.Current;
        	var request = httpContext.GetOrDefault(x => x.Request);
			if(request != null) {
				builder.AppendLine("user-agent: " + request.UserAgent);
			}
			/*var trace = new System.Diagnostics.StackTrace(ex, true);

			if(trace.FrameCount > 0) {
	        	var stackFrame = trace.GetFrame(0);
				builder.AppendLine("Line: " + stackFrame.GetFileLineNumber());
				builder.AppendLine("Column: " + stackFrame.GetFileColumnNumber());
				
			}*/
            LogManager.GetCurrentClassLogger().Error(builder.ToString());
        }

        public static void Exception(Exception ex, User user) {
        	var httpContext = HttpContext.Current;
        	var request = httpContext.GetOrDefault(x => x.Request);
        	var statusCode = httpContext.Response.StatusCode;
	        if (statusCode == (int) HttpStatusCode.NotFound) {
		        SpecLogger.NotFound(request.Url.ToString());
		        return;
	        }
            GDC.Set("title", "[" + statusCode + "] Custom " + request.Url);
            var builder = new StringBuilder();
            builder.AppendLine();
        	builder.AppendLine("type: " + ex.GetType());
            builder.AppendLine("message: " + ex.Message);
            builder.AppendLine("source: " + ex.Source);
            if (user != null)
                builder.AppendLine("user: " + user.Email + " " + user.FullName);
			if(httpContext.User != null)
                builder.AppendLine("identity: " + 
					httpContext.User.Identity.GetOrDefault(x => x.Name));
            builder.AppendLine("userhost: " + request.UserHostAddress + " " +
                request.UserHostName);
			try {
	            builder.AppendLine("form: " + request.Form);
			}catch{}
            builder.AppendLine("url: " + request.Url);
            builder.AppendLine("referrer-url: " + request.UrlReferrer);
        	builder.AppendLine("user-agent: " + request.UserAgent);
            builder.AppendLine("target-site: " + ex.TargetSite);
            builder.AppendLine("stack-trace: " + ex.StackTrace);
            LogManager.GetCurrentClassLogger().Error(builder.ToString());
        }

    }
}