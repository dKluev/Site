using System.Xml.Linq;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Common.Html {
	public static class HtmlTagExtension {
		public static T AddClass<T>(this T Tag, string name) where T:IHtmlTag {
			var xElement = Tag.As<XElement>();
			var cls = xElement.Attribute("class").GetOrDefault(x => x.Value);
			if(!cls.IsEmpty())
				cls += " ";
			xElement.SetAttributeValue("class", cls + name );
			return Tag;
		}
		public static T Data<T>(this T Tag, string name, string value) where T:IHtmlTag {
			return Tag.SetAttr("data-" + name, value);
		}
		public static T Hide<T>(this T Tag) where T:IHtmlTag {
			return Tag.SetAttr("style", "display:none;");
		}

		public static T SetDisabled<T>(this T Tag, bool disabled) where T:IHtmlTag {
			if (disabled) {
				Tag.SetAttr("disabled", "disabled");
			}
			return Tag;
		}
		public static T SetAttr<T>(this T Tag, string name, string value) where T:IHtmlTag {
			var xElement = Tag.As<XElement>();
			xElement.SetAttributeValue(name, value);
			return Tag;
		}


	}
}