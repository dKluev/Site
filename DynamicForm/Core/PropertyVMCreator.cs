using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.ComponentModel;
using SimpleUtils.ComponentModel.Extensions;
using SimpleUtils.Reflection;
using SimpleUtils.Reflection.Extensions;

namespace DynamicForm
{
    public class PropertyVMCreator
    {
        private readonly HtmlHelper _htmlHelper;

        public PropertyVMCreator(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public PropertyVM Create<Te, Tp>(Expression<Func<Te, Tp>> selector,
            object instance)
        {
            var propertyInfo =
                ExpressionUtils.GetPropertyInfo(selector);
            var propertyName = ExpressionUtils.GetPropertyName(selector);
//            var value = selector.Compile().Invoke((Te) _htmlHelper.ViewData.Model);
            return Create<Tp>(propertyName, propertyInfo, instance);
        }

        public PropertyVM Create<Tp>(PropertyInfo propertyInfo, object instance)
        {
            return Create<Tp>(propertyInfo.Name, propertyInfo, instance);
        }

        private PropertyVM Create<Tp>(string propertyName, 
            PropertyInfo propertyInfo, object instance) {
            var value = (Tp)propertyInfo.GetValue(instance);
            
            var propertyDescriptor = 
                Config.DescriptionProvider.GetProperty(propertyInfo, instance);
            var uiHint = propertyDescriptor.Get<UIHintAttribute>();
          /*  var hasErrors = _htmlHelper.ViewData.ModelState.ContainsKey(propertyName) &&
                   _htmlHelper.ViewData.ModelState[propertyName].Errors.Count > 0;*/
            if(uiHint.UIHint.In(Controls.Hidden, Controls.Number))
            {
                return
                    new PropertyVM<object>
                    {
                        Descriptor = propertyDescriptor,
                        Name = propertyName,
                        Value = value,
                        ModelState = _htmlHelper.ViewData.ModelState,
                    };
            }
            return
                new PropertyVM<Tp>
                {
                    Descriptor = propertyDescriptor,
                    Name = propertyName,
                    Value = value,
                    ModelState = _htmlHelper.ViewData.ModelState,
                };
        }
    }
}
