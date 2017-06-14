using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using Specialist.Entities.Context;
using SimpleUtils.Extension;
using SimpleUtils;

namespace Specialist.Web.Common.Html
{
    public static class JavaScripts {
	    public static readonly string StyleNewPath = GetPathWithPosfix("/Content/stylenew.min.css");
	    public static readonly string PersonalStyle = GetPathWithPosfix("/Content/Views/personal-style.css");
	    public static readonly string ScriptPath = GetPathWithPosfix("/Scripts/all.min.js");
		public static string GetPathWithPosfix(string path) {
			var fullPath = HostingEnvironment.MapPath("~" + path);
			return path + "?m=" + System.IO.File.GetLastWriteTime(fullPath).GetHashCode();
				
		}
        public static string Cdn(string file) {
            return Common(Urls.JavaScript(file));
        }

        public static string Local(string file) {
            return Common("/Scripts/" + file);
        }

        public static string Common(string file) {
            var tagBuilder = new TagBuilder("script");
            tagBuilder.Attributes.Add("src", file);
            tagBuilder.Attributes.Add("type", "text/javascript");
            return tagBuilder.ToString();
        }

        public static string TinyMce() {
            return Local("Views/Message/tiny_mce/tiny_mce.js");
        }

    }
}