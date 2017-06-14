using SimpleUtils.FluentHtml.Tags;

namespace Specialist.Web.Common.Html {
	public class NullTagImg:TagImg {
		public override string ToString() {
			return string.Empty;
		}

		public override void WriteTo(System.Xml.XmlWriter writer)
		{
		}

	}
	public class NullTagA:TagA {
		public override string ToString() {
			return string.Empty;
		}

		public override void WriteTo(System.Xml.XmlWriter writer)
		{
		}

	}
}