using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Common.Mvc.Binders {
	public class DateTimeBinder : DefaultModelBinder {
		protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
			PropertyDescriptor propertyDescriptor) {
			var propertyType = propertyDescriptor.PropertyType;
			if (propertyType.In(typeof (DateTime), typeof (DateTime?))) {
				var value = GetValue<string>(bindingContext, propertyDescriptor.Name);
				DateTime result; 
				if(DateTime.TryParse(value, CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out result)) {
					SetProperty(controllerContext, bindingContext, propertyDescriptor, result);
					return;
				}
			}
			base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
		}

		private static T GetValue<T>(ModelBindingContext
				bindingContext,
				string key) {
			if (bindingContext.ValueProvider.ContainsPrefix(key)) {
				ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(key);
				if (valueResult != null) {
					//    bindingContext.ModelState.SetModelValue(key, valueResult);
					return (T) valueResult.ConvertTo(typeof (T));
				}
			}
			return default(T);
		}
	}
}