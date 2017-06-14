//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using FluentValidation; using Microsoft.Practices.Unity;
//using FluentValidation.Internal;
//using FluentValidation.Results;
//using FluentValidation.Validators;
//using SimpleUtils.Util;
//
//namespace Specialist.Web.Validators.Core
//{
//    public class LinqToSqlAttributesRule<T> : IValidationRule<T>
//    {
//        readonly List<IDynamicRule<T>> _rules = new List<IDynamicRule<T>>();
//
//        public LinqToSqlAttributesRule(IMetaDataService metaDataService)
//        {
//            foreach (var propertyInfo in typeof(T).GetProperties())
//            {
//                Type propertyValidatorType = null;
//                var canBeNull = LinqToSqlUtil.CanBeNull(propertyInfo);
//                if(!canBeNull)
//                {
//                    if(propertyInfo.PropertyType == typeof(string))
//                    {
//                        propertyValidatorType = typeof(NotEmptyValidator<,>);
//                    }
//                    else
//                    {
//                        propertyValidatorType = typeof(NotNullValidator<,>);
//                    }
//                }
//                if(propertyValidatorType != null)
//                {
//                    var validatorType = propertyValidatorType
//                        .MakeGenericType(typeof(T), propertyInfo.PropertyType);
//                    var validator = Activator.CreateInstance(validatorType);
//                    var ruleType = typeof(DynamicRule<,>)
//                        .MakeGenericType(typeof(T), propertyInfo.PropertyType);
//                    var rule = (IDynamicRule<T>)Activator
//                        .CreateInstance(ruleType, validator, propertyInfo);
//                    _rules.Add(rule);
//                }
//            }
//        }
//
//
//
//        public IEnumerable<ValidationFailure> Validate(ValidationContext<T> context)
//        {
//            foreach (var rule in _rules)
//            {
//                var result = rule.Validate(context.InstanceToValidate);
//                if (result != null) yield return result;
//            }
//        }
//
//    }
//
//    public interface IDynamicRule<T>
//    {
//        ValidationFailure Validate(T instanceToValidate);
//    }
//
//    public class DynamicRule<T, TProperty> : IDynamicRule<T>
//    {
//        public DynamicRule(IPropertyValidator<T, TProperty> validator, PropertyInfo property)
//        {
//            Validator = validator;
//            Property = property;
//        }
//
//        public IPropertyValidator<T, TProperty> Validator { get; private set; }
//        public PropertyInfo Property { get; private set; }
//
//        public ValidationFailure Validate(T instanceToValidate)
//        {
//            Func<T, TProperty> invoker = x => (TProperty)Property.GetValue(x, null);
//            var context = new PropertyValidatorContext<T, TProperty>(Property.Name, instanceToValidate, invoker, null, null);
//            var result = Validator.Validate(context);
//
//            if (result.IsValid)
//                return null;
//
//            return new ValidationFailure(Property.Name, result.Error, context.PropertyValue);
//        }
//    }
//
//}