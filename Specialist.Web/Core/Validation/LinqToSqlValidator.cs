using System;
using System.Web.Mvc;
using SimpleUtils.ComponentModel.Extensions;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Reflection.Extensions;
using SpecialistTest.Web.Core.Utils;
using System.Linq;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;


namespace Specialist.Web.Cms.Core
{
    public class LinqToSqlValidator
    {
        public static bool Validate(ModelStateDictionary modelState, object obj)
        {
        	var type = obj.GetType();
        	foreach (var propertyInfo in type.GetProperties()) {
            	var metaData = MvcApplication.MetaDataProvider.Get(type);
                var value = propertyInfo.GetValue(obj);
                var canBeNull = LinqToSqlUtils.CanBeNull(propertyInfo);
            	var isDefault = false;
        		var isForeign = LinqToSqlUtils.GetForeignProperty(type, propertyInfo.Name) != null;
				if(value != null) {
					isDefault = Equals(value.GetType().Default(),value);
				}
                if(!canBeNull && 
					(value == null || Equals(value, string.Empty) || (isForeign && isDefault))) {
                	var displayName = metaData.GetProperties().First(x => x.Info == propertyInfo).DisplayName();
                	modelState.AddModelError(propertyInfo.Name,
                		ValidationMessages.notempty_error.Replace(
                			ValidationMessages.PropertyName,

                			displayName));
                }

            	/* if(propertyInfo.Info.PropertyType == typeof(string))
                {
                    var strValue = (string)value;
                    if (strValue != null)
                    {
                        var maxLength = LinqToSqlUtil.GetMaxLength(
                            propertyInfo.Info);
                        if (maxLength > 0 && strValue.Length > maxLength)
                            modelState.AddModelError(propertyInfo.Info.Name, "Поле <" +
                                propertyInfo.DisplayName + "> не должно быть длиннее " +
                                maxLength);
                    }

                }*/
            }
        	return modelState.IsValid;
        }
    }
}