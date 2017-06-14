//using System.Web.Mvc;
//using SimpleUtils.Reflection;
//using SimpleUtils.Util;
//using FluentMetaData.Core;
//
//namespace Specialist.Web.Cms.Core
//{
//    public class Validator
//    {
//        public static void Validate(ModelStateDictionary modelState, object obj, 
//            MetaData metaData)
//        {
//            foreach (var property in metaData.Properties)
//            {
//                var value = property.Info.GetValue(obj);
//                var canBeNull = LinqToSqlUtil.CanBeNull(property.Info);
//                if(!canBeNull && (value == null || Equals(value, string.Empty)))
//                    modelState.AddModelError(property.Info.Name, "Поле <" + 
//                        property.DisplayName + "> обязательно к заполнению");
//
//                if(property.Info.PropertyType == typeof(string))
//                {
//                    var strValue = (string)value;
//                    if (strValue != null)
//                    {
//                        var maxLength = LinqToSqlUtil.GetMaxLength(
//                            property.Info);
//                        if (maxLength > 0 && strValue.Length > maxLength)
//                            modelState.AddModelError(property.Info.Name, "Поле <" +
//                                property.DisplayName + "> не должно быть длиннее " +
//                                maxLength);
//                    }
//
//                }
//            }
//        }
//    }
//}