using System.Xml.Linq;

namespace Specialist.Web.Common.Html {
	public	class RawXml : XText
	{
		private readonly string _text;

		public RawXml(string text)
			: base(string.Empty)
		{
			_text = text;
		}

		public override void WriteTo(System.Xml.XmlWriter writer)
		{
			writer.WriteRaw(_text); ;
		}
	}

}