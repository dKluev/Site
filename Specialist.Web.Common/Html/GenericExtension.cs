using System;
using System.Linq.Expressions;
using Microsoft.Web.Mvc.Internal;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Common.Html
{
    public static class GenericExtension
    {
        public static string For<TModel, TProperty>(this TModel model,
			Expression<Func<TModel, TProperty>> expression, int? index = null) {
	        var inputName = ExpressionHelper.GetInputName(expression);
	        if (index.HasValue) {
		        var parts = inputName.Split('.');
		        inputName = "{0}[{1}].{2}".FormatWith(parts[0], index, parts[1]);
	        }
	        return inputName;
        }

	    public static TResult Get<T, TResult>(this T obj, Func<T, TResult> func)
           where T : class
        {
            if (obj == null)
                return default(TResult);
            try
            {
                return func(obj);
            }
            catch (NullReferenceException)
            {
                return default(TResult);
            }
        }
    }
}