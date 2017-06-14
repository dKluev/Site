using System.Collections.Generic;
using System.Linq;
using SimpleUtils.FluentHtml.Tags;
using SpecialistTest.Web.Core.Mvc;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Common.Html {
	public class H: SimpleUtils.FluentHtml.Tags.Htmls {
		public static TagList l(params object[] tags)
		{
			return new TagList(tags);
		}

		public static RawXml Raw(string text) {
			return new RawXml(text);
		}
		public static RawXml CleanRaw(string text) {
			return Raw(text.Replace("\u001F",""));
		}

		public static TagList l(IEnumerable<object> tags)
		{
			return new TagList(tags);
		}

		public static TagTr Row(params object[] tags) {
			return tr[tags.Select(x => x is TagTd ? x : td[x])];
		}
		public static TagTr Row2(params object[] tags) {
			return tr[tags.Where(x => x != null).Select(x => x is TagTd ? x : td[x])];
		}

		public static TagTr Head(params object[] tags) {
			return tr[tags.Where(x => x != null).Select(x => th[x])];
		}

		public static TagInput InputImage(string src) {
			return input.Type("image").Src(src);
		}

		public static TagInput InputText(string name, object value) {
			return input.Type("text").Name(name).Value(value);
		}
		public static TagInput InputFile(string name) {
			return input.Type("file").Name(name);
		}
		public static TagInput InputNumber(string name, object value) {
			return input.Type("number").Name(name).Value(value);
		}
		public static TagInput InputRadio(string name, string value) {
			return input.Type("radio").Name(name).Value(value);
		}

		public static TagInput InputCheckbox(string name, string value) {
			return input.Type("checkbox").Name(name).Value(value);
		}

		public static TagA Anchor(string href, object content = null) {
			content = content ?? href;
			return a.Href(href)[content];
		}

		public static TagImg Img(string src, string alt = "") {
			if(src.IsEmpty())
				return new NullTagImg();
			return img.Src(src).Alt(alt);
		}

		public static TagForm Form(string action)
		{
			return form.Action(action).Method("post");
		}

		public static TagInput Hidden(string name, object value)
		{
			return input.Type("hidden").Name(name).Value(value.NotNullString());
		}

		public static TagInput Submit(string value)
		{
			return input.Type("submit").Value(value);
		}
		public static TagOl Ol(IEnumerable<object> list)
		{
			return ol[list.Select(x => li[x])];
		}

		public static TagUl Ul(IEnumerable<object> list)
		{
			return ul[list.Select(x => li[x])];
		}

		public static TagDiv Div(string @class) {
			return div.Class(@class);
		}

		public static TagScript JavaScript()
		{
			return script.Type("text/javascript");
		}
		public static TagScript JQuery(string javaScript)
		{
			return JavaScript()[new RawXml("$(function () {" + javaScript + " });")];
		}


		public static TagA Email(string email) {
			if (email.IsEmpty()) {
				return new NullTagA();
			}
			return Anchor("mailto:" + email, email);
		}

		public static TagA Tel(string tel) {
			if (tel.IsEmpty()) {
				return new NullTagA();
			}
			return Anchor("tel:" + tel, tel);
		}

	}
}