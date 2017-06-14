using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Xml.Linq;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Passport;
using Specialist.Services.Interface.Passport;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Common.Html;
using SpecialistTest.Web.Common;
using Microsoft.Practices.Unity;

namespace Specialist.Web.Core.Logic {
	public abstract class BaseView:H {
		public static object Tabs(IEnumerable<string> names, params object[] contents) {
			var ids = names.Select(s => "tab-" + Linguistics.UrlTranslite(s) + Guid.NewGuid().ToString("N")).ToList();
			return div.Class("ui-tab-control")[
				ul[names.Select((x, i) => li[a.Href("#" + ids[i])[x]])],
				contents.Select((x, i) => ids.Count > i ? div.Id(ids[i])[x] : null)];
		}
		
		private User _user;

		protected User User {
			get { return _user ?? (_user = MvcApplication.Container.Resolve<IAuthService>().CurrentUser); }
		}

		public abstract object Get();

		public static IEnumerable<XElement> GetScript(
			string scr, string name, params object[] args) {
			var argsPart = GetArgsPart(args);
			var javaScript = name + "(" + argsPart + ");";
			return new[] {
				JavaScript()
					.Src(scr)[" "],
				JQuery(javaScript)
			};
		}

		protected static string GetArgsPart(IEnumerable<object> args) {
			return args.Select(o => {
				if (o is string)
					return "\"" + o + "\"";
				var list = o as IEnumerable;
				if (list != null)
					return "[" + GetArgsPart(list.Cast<object>()) + "]";
				return o;
			})
				.Select(x => x ?? "null")
				.Select(o => o.ToString()).JoinWith(", ");
		}
	}

	public class InlineBaseView<T>:BaseView<T> {
		private Func<BaseView<T>, object> getFunc;
		public InlineBaseView(Func<BaseView<T>, object> getFunc) {
			this.getFunc = getFunc;
		}

		public override object Get() {
			return getFunc(this);
		}
	}

	public static class InlineBaseView {
		public static InlineBaseView<T> New<T>(T model,
			Func<BaseView<T>, object> getFunc) {
			var view = new InlineBaseView<T>(getFunc);
			view.Model = model;
			return view;
		} 
		
	}

	public abstract class BaseView<T> : BaseView {

		public BaseView<T> Init(T model, UrlHelper url) {
			Model = model;
			Url = url;
			return this;
		}

		public TagForm AjaxForm(string url) {
			return Form(url).Class("ajax-form");

		}

		protected BaseView() {}

		public object Title(string title = null) {
			if (title != null)
				return h1[title];
			return h1[new TitleCreator().Get(Model)];
		}

		public T Model { get; set; }

		public UrlHelper Url { get; set; }

		public HtmlHelper HtmlHelper { get; set; }

		

		public object ControlFor(Expression<Func<T, object>> selector) {
			return l(LabelFor(selector), div.Class("editor-div")[EditorFor(selector)]);
		}
		public object LabeledTextInput(string label,Expression<Func<T, object>> selector) {
			return l(Label(label), 
				div.Class("editor-div")[InputText(
					ExpressionUtils.GetPropertyName(selector), selector.Compile()(Model).NotNullString())]);
		}

		public object TextFor(Expression<Func<T, object>> selector) {
			return div[strong[GetDisplayName(selector),": "], selector.Compile()(Model)];
		}

		public TagDiv LabelFor(Expression<Func<T, object>> selector) {
			var displayName = GetDisplayName(selector);
			return Label(displayName);
		}

		public static TagDiv Label(string displayName) {
			return div.Class("label-div")[label[displayName]];
		}

		private static string GetDisplayName(Expression<Func<T, object>> selector) {
			var propertyInfo = ExpressionUtils.GetPropertyInfo(selector);
			var propertyMetaData =  MvcApplication.MetaDataProvider.Get(propertyInfo.DeclaringType)
				.GetProperties().First(x => x.Info == propertyInfo);
			var displayName = propertyMetaData.DisplayName();
			return displayName;
		}

		public TagInput HiddenFor(Expression<Func<T, object>> selector) {
			return Hidden(ExpressionUtils.GetPropertyName(selector), selector.Compile()(Model));
		}

		public object EditorFor(Expression<Func<T, object>> selector) {
			var propertyMetaData = GetMetaData(selector);
			var uiHint = propertyMetaData.Control();
			var propertyName = ExpressionUtils.GetPropertyName(selector);
			var value = selector.Compile()(Model);
			return new ViewControls().InvokeMethod(uiHint, propertyName, value);
		}

		private PropertyMetaData GetMetaData<Tp>(Expression<Func<T, Tp>> selector) {
			var propertyInfo = ExpressionUtils.GetPropertyInfo(selector);
			return MvcApplication.MetaDataProvider.Get(propertyInfo.DeclaringType)
				.GetProperties().First(x => x.Info == propertyInfo);
		}

		protected TagButton SaveButton(string title = "Сохранить") {
			return button[title].Style("margin:5px;");
		}
		protected TagForm AjaxButton(string url, string title) {
			return AjaxForm(url)[SaveButton(title)];
		}


		public object SelectFor<K>(Expression<Func<T, object>> selector,
			IEnumerable<K> source, Func<K, object> textSelector,
			Func<K, object> valueSelector, bool withEmpty = false, string emptyText = "Нет") {
			var value = selector.Compile()(Model);
			var propertyName = ExpressionUtils.GetPropertyName(selector);
			return Select(propertyName, value, source, textSelector, valueSelector, withEmpty, emptyText);
		}

		public static TagSelect Select<K>(string propertyName, object value, IEnumerable<K> source, Func<K, object> textSelector, Func<K, object> valueSelector, bool withEmpty = false, string emptyText = "Нет") {
			var result = new List<SelectListItem>();
			if (withEmpty) {
				result.Add(new SelectListItem {Text = emptyText, Value = string.Empty});
			}
			foreach (var item in source) {
				var itemValue = valueSelector(item);
				result.Add(new SelectListItem {
					Text = textSelector(item).ToString(),
					Value = itemValue.ToString(),
					Selected = value == itemValue,
				});
			}
			return @select.Name(propertyName)[result.Select(x =>
				option.Value(x.Value).SetSelected(x.Value.NotNullString() == value.NotNullString())[x.Text])]
				.Class("form-control");
		}

		public object AutocompleteFor(object currentName,
			Expression<Func<T, object>> idSelector, string dataUrl) {
			var inputName = "auto-for" + StringUtils.UrlToHtmlId(dataUrl);
			var idPropertyName = ExpressionUtils.GetPropertyName(idSelector);
			return new[] {
				LabelFor(idSelector), div[InputText(inputName,
					currentName.NotNullString()),
					HiddenFor(idSelector), JQuery("controls.initAutocomplete('{0}','{1}','{2}')"
						.FormatWith(idPropertyName, inputName, dataUrl))]
			};
		}

		public object Select2For(object currentName,
			Expression<Func<T, object>> idSelector, string dataUrl) {
			return new[] {
				LabelFor(idSelector), Select2WithoutLabel(currentName, idSelector, dataUrl)
			};
		}

		public TagDiv Select2WithoutLabel(object currentName, Expression<Func<T, object>> idSelector, string dataUrl) {
			var idPropertyName = ExpressionUtils.GetPropertyName(idSelector);
			return div[
				HiddenFor(idSelector).Class("select2"),
				JQuery("controls.initSelect2('{0}','{1}','{2}')"
					.FormatWith(idPropertyName, currentName, dataUrl))];
		}
	}
}