using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using DynamicForm.Core;
using SimpleUtils.FluentAttributes.Attributes;
using DynamicForm.Mvc.Extensions;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.ComponentModel;
using SimpleUtils.ComponentModel.Extensions;

namespace DynamicForm
{
    public class PropertyVM
    {
        public PropertyDescriptor Descriptor { get; set; }

        public string Name { get; set; }

        private string _partialName = null;

        public string PartialName { 
            get
            {
                return Config.ControlFolder +
                    "/" + (_partialName ?? UIHint);
            }
            set
            {
                _partialName = value;
            }

        }

        public ModelStateDictionary ModelState { get; set; }

        public bool HasErrors { get
        {
            return ModelState.HasError(Name);
        }}

        public string UIHint
        {
            get { return Descriptor.Get<UIHintAttribute>().UIHint; }
        }

        public bool Required { get
        {
            return Descriptor.Get<RequiredAttribute>() != null;
        } }

        public string Example
        {
            get
            {
                return Descriptor.Get<ExampleAttribute>().GetOrDefault(x => x.Example);
            }
        }

    }

    public class PropertyVM<Te>:PropertyVM
    {
        public Te Value { get; set; }

        public string For<Tp>(Expression<Func<Te, Tp>> selector)
        {
            var propertyName = ExpressionUtils.GetPropertyName(selector);
            return Name + "." + propertyName;
        }

        public bool HasErrorFor<Tp>(Expression<Func<Te, Tp>> selector)
        {
            var propertyName = For(selector);
            return ModelState.ContainsKey(propertyName) &&
                   ModelState[propertyName].Errors.Count > 0;
        }
    }
}