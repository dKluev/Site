using System;
using System.Linq;
using System.Web.Mvc;

namespace Specialist.Web.Common.Mvc.Binders {
	public class FlagEnumModelBinder: DefaultModelBinder
{
		protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor) {
			var propertyType = propertyDescriptor.PropertyType;
			if(propertyType.IsEnum && propertyType.IsDefined(
				typeof(FlagsAttribute), false)) {
				var values = GetValue<string[]>(bindingContext, propertyDescriptor.Name);
				if(values == null)
					return;
				if (values.Length >= 1) {
                long byteValue = 0;
                foreach (var value in values.Where(v => Enum.IsDefined(propertyType, v))) {
                    byteValue |= (int)Enum.Parse(propertyType, value);
                }
            	var x = Enum.Parse(propertyType, byteValue.ToString());
				SetProperty(controllerContext, bindingContext, propertyDescriptor, x);
                return ;
            }
			}
			base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
		}
  /*  public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
        if (bindingContext == null) throw new ArgumentNullException("bindingContext");
 
        if (bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName)) { 
            var values = GetValue<string[]>(bindingContext, bindingContext.ModelName);
 
            if (values.Length > 1 && (bindingContext.ModelType.IsEnum && bindingContext.ModelType.IsDefined(typeof(FlagsAttribute), false))) {
                long byteValue = 0;
                foreach (var value in values.Where(v => Enum.IsDefined(bindingContext.ModelType, v))) {
                    byteValue |= (int)Enum.Parse(bindingContext.ModelType, value);
                }
 
                return Enum.Parse(bindingContext.ModelType, byteValue.ToString());
            }
        }
 
        return base.BindModel(controllerContext, bindingContext);
    }*/
 
    private static T GetValue<T>(ModelBindingContext bindingContext, string key) {
        if (bindingContext.ValueProvider.ContainsPrefix(key)) {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(key);
            if (valueResult != null) {
            //    bindingContext.ModelState.SetModelValue(key, valueResult);
                return (T)valueResult.ConvertTo(typeof(T));
            }
        }
        return default(T);
    }
}
}