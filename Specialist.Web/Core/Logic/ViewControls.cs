using System;
using Specialist.Web.Common.Html;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Core.Logic {
	public class ViewControls {
		public object Text(string name, string value) {
			return H.InputText(name, value);
		}
		public object DatePicker(string name, DateTime? value) {
			return H.InputText(name, value.DefaultString()).Class("date-picker");
		}
		public object TextArea(string name, string value) {
			return H.textarea.Name(name)[value];
		}
		public object BigTextArea(string name, string value) {
			return H.textarea.Name(name)[value].Rows(10);
		}
		public object CheckBox(string name, bool? value) {
			return H.input.Type("checkbox").Name(name).SetChecked(value.GetValueOrDefault()).Value("true");
		}
		public object Number(string name, object value) {
			return H.InputText(name, value.NotNullString());
		}

		public object Html(string name, object value) {
			return BigTextArea(name, value.NotNullString());
		}
	}
}