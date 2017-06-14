using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using DynamicForm.Mvc;
using SimpleUtils;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Html;
using System.Web.Mvc.Html;
using Specialist.Web.Common.Mvc;


namespace Specialist.Web.Common.Html
{
	public static class HtmlControls
	{
		public static TagBuilder Anchor(string url)
		{
			return Anchor(url, url);
		}

		public static TagBuilder Anchor(string url, string content)
		{
			return Anchor(url, content, null);
		}

		public static TagBuilder Anchor(string url, string content, string fragment)
		{
			var tagBuilder = new ExTagBuilder("a");
			var href = fragment.IsEmpty() 
				? url
				: url + "#" + fragment;
			tagBuilder.Attributes.Add("href", href);
			tagBuilder.InnerHtml = content;
			return tagBuilder;
		}

		public static TagBuilder MailTo(string mail)
		{
			if (mail.IsEmpty())
				return new NullTagBuilder();
			mail = mail.ToLower();
			return Anchor("mailto:" + mail, mail);
		}

		public static string AddContentPrefix(this string html, string prefix)
		{
			var index = html.IndexOf(">") + 1;
			return html.Insert(index, prefix);
		}

		public static string AddAttribute(this string html, string attribute, object value)
		{
			if(html.IsEmpty())
				return html;
			var index = html.IndexOf(">");
			if(index < 0)
				return html;
			return html.Insert(index, " " + attribute + "='" + value + "' ");
		}

	   

	  

		public static string Script(string script)
		{
			return string.Format("<script type=\"text/javascript\">{0}</script>",
				script);
		}


		public static string AnchorName(string content, string name)
		{
			return string.Format("<a name={1}>{0}</a>", content,
				name.Quote());
		}

		public static string ImgAnchor(string url, string imgUrl)
		{
			return ImgAnchor(url, imgUrl, null);
		}

		public static string ImgAnchor(string url, string imgUrl, string title)
		{
			return string.Format("<a href={0} title={2}>{1}</a>",
				url.Quote(), Image(imgUrl), title.Quote());
		}

		public static TagBuilder Image(string url)
		{
			return Image(url, "");
		}

	 /*   public static TagBuilder Image(string url, object htmlAttribute)
		{
			if (url == null)
				return new NullTagBuilder();

			var builder = new ExTagBuilder("img");
			builder.RenderMode = TagRenderMode.SelfClosing;
			builder.Attributes.Add("src", url);
			builder.MergeAttributes(new RouteValueDictionary(htmlAttribute));
			return builder;
		}*/

		public static TagBuilder Image(string url, string alt)
		{
			return Image(url, alt, null);
		}

		public static TagBuilder Image(string url, string alt, object htmlAttribute)
		{
			if(url == null)
				return new NullTagBuilder();
				
			var builder = new ExTagBuilder("img");
			builder.RenderMode = TagRenderMode.SelfClosing;
			builder.Attributes.Add("src", url);
			builder.Attributes.Add("alt", alt);
			builder.MergeAttributes(new RouteValueDictionary(htmlAttribute));
			return builder;
		}
		public static string Text(string name, object value, string @class)
		{
			var attributes = "class=" + @class.Quote();
			return string.Format("<input {0} name={1} id={1} value={2} type=\"text\"/>", 
								 attributes, name.Quote(), value.NotNullString().Quote());
		}

		public static string Hidden(string name, object value)
		{
			return string.Format("<input name={0} id={0} value={1} type=\"hidden\"/>",
				name.Quote(), value.NotNullString().Quote());
		}

		public static TagBuilder Submit(string value)
		{
			return Submit(value, "");
		}

		public static TagBuilder Submit(string value, string id)
		{
			var builder = Input();
			builder.Attributes.Add("type", "submit");
			builder.Attr(new {value, id});
			return builder;
		}

		public static TagBuilder File(string name) {
			var builder = Input();
			builder.Attributes.Add("type", "file");
			builder.Attr(new { name });
			return builder;
		}

		private static TagBuilder Input() {
			var builder = new ExTagBuilder("input");
			builder.RenderMode = TagRenderMode.SelfClosing;
			return builder;
		}

		public static TagBuilder ImgSubmit(string src)
		{
			return ImgSubmit(src, src);
		}

		public static TagBuilder ImgSubmit(string src, string value)
		{
			var builder = Input();
			builder.Attributes.Add("type", "image");
			builder.Attr(new { value, src });
			return builder;
		}


		public static string CheckBox(string name, bool check)
		{
			/*var @checked = check ? "checked" : string.Empty;
			return "<input name=\"" + name + "\" value=\"True\" type=\"checkbox\" " 
				   + @checked +">";*/
			return CheckBox(name, check, true);
		}

		public static string CheckBox(string name, bool check, object value)
		{
			var @checked = check ? "checked" : string.Empty;
			return string.Format("<input name=\"{0}\" value=\"{2}\" type=\"checkbox\" {1}/>",
				name, @checked, value);
		}

		public static string TriCheckBox(this HtmlHelper helper, string name, bool? value)
		{
			var source = 
				new List<SelectListItem>
				{
					new SelectListItem{Selected = !value.HasValue, Text = "Безотносительно",
						Value = string.Empty},
					new SelectListItem{
						Selected = value.HasValue ? !value.Value : false , Text = "Нет",
						Value = "false"},
					new SelectListItem{
						Selected = value.HasValue ? value.Value : false , Text = "Да",
						Value = "true"},
				};

			return helper.DropDownList(name, source).ToString();
		}

		public static string TextArea(string name, object value, string @class)
		{
			var attributes = "class=" + @class.Quote();
			return string.Format("<textarea {0} name={1}>{2}</textarea>", 
								 attributes, name.Quote(), value);
		}

		public static string TextArea(string name, object value, string @class, string id) {
			var attributes = "class=" + @class.Quote();
			return string.Format("<textarea {0} name={1} id={3}>{2}</textarea>",
								 attributes, name.Quote(), value, id.Quote());
		}
	}
}