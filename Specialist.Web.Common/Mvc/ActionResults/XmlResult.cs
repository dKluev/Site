using System;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Specialist.Web.Common.Mvc.ActionResults {
	public class XmlResult : ActionResult {
		private string Xml {
			get;
			set;
		}

		public static string ToStringWithDeclaration(XDocument doc) {
			if (doc == null) {
				throw new ArgumentNullException("doc");
			}
			var builder = new StringBuilder();
			using (var writer = new StringWriterWithEncoding(builder, new UTF8Encoding())) {
				doc.Save(writer);
			}
			return builder.ToString();
		}

		public XmlResult(XDocument doc) {
			Xml = ToStringWithDeclaration(doc);
		}

		public XmlResult(string xml) {
			Xml = xml;
		}

		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.ContentType = "text/xml";

			context.HttpContext.Response.Write(Xml);
		}
	}
}