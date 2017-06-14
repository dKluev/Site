using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Web.Mvc.Internal;
using System.Web.Mvc.Html;
using Microsoft.Web.Mvc;
using SimpleUtils;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;
using Specialist.Web.Util;
using ExpressionHelper = Microsoft.Web.Mvc.Internal.ExpressionHelper;

namespace Specialist.Web.Common.Html
{
    public static class ExpressionInputExtension
    {
      /*  public static MvcHtmlString CheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
                                                 Expression<Func<TModel, bool>> expression) 
            where TModel : class
        {
            return CheckBoxFor(htmlHelper, expression, null);
        }
*/
        public static MvcHtmlString CheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, IDictionary<string, object> htmlAttributes) 
            where TModel : class
        {
            var inputName = ExpressionHelper.GetInputName(expression);
            var local = GetValue(htmlHelper, expression);
            return htmlHelper.CheckBox(inputName, local, htmlAttributes);
        }

        public static MvcHtmlString DropDownListFor<Tm, Tp, Ti>(this HtmlHelper<Tm> htmlHelper,
            Expression<Func<Tm, Tp >> expression, IEnumerable<Ti> source, 
            Func<Ti, object> textSelector, Func<Ti, object> valueSelector)
            where Tm : class
        {
            return DropDownListFor(htmlHelper, expression, source, textSelector, valueSelector, null);
        }

        public static MvcHtmlString DropDownListFor<Tm, Tp, Ti>(this HtmlHelper<Tm> htmlHelper,
            Expression<Func<Tm, Tp >> expression, IEnumerable<Ti> source, 
            Func<Ti, object> textSelector, Func<Ti, object> valueSelector,
            IDictionary<string, object> htmlAttributes)
            where Tm : class
        {
            var inputName = ExpressionHelper.GetInputName(expression);
//            var local = GetValue(htmlHelper, expression);
            var selectItemList = new List<SelectListItem>();
            var propertyInfo =
                ((MemberExpression)(expression.Body)).Member as PropertyInfo;
            if(LinqToSqlUtils.CanBeNull(propertyInfo))
                selectItemList.Add(
                    new SelectListItem
                    {
                        Text = "Нет",
                        Value = string.Empty,
                    });
            selectItemList.AddRange(SelectListUtil.GetSelectItemList(source, textSelector, valueSelector));
            return htmlHelper.DropDownList(inputName, selectItemList, htmlAttributes);
        }


      /*  public static string RadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object value)
            where TModel : class
        {
            return RadioButtonFor(htmlHelper, expression, value, null);
        }
*/
        public static string RadioButtonFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
         Expression<Func<TModel, TProperty>> expression, object value, object htmlAttributes)
         where TModel : class
        {
            var inputName = ExpressionHelper.GetInputName(expression);
            object local = GetValue(htmlHelper, expression);
            if (local == null)
                local = string.Empty;
            return htmlHelper.RadioButton(inputName, value, local.NotNullString() 
                == value.NotNullString(), htmlAttributes).ToString();
        }

     /*   public static string ValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                            Expression<Func<TModel, TProperty>> expression)
            where TModel : class
        {
            return PasswordFor(htmlHelper, expression, null);
        }*/

       /* public static string PasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                         Expression<Func<TModel, TProperty>> expression)
         where TModel : class
        {
            var inputName = ExpressionHelper.GetInputName(expression);
            return htmlHelper.Password(inputName).ToString();
        }
*/

        public static MvcHtmlString[] RadioButtonListFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList)
         where TModel : class
        {
            var inputName = ExpressionHelper.GetInputName(expression);
            return htmlHelper.RadioButtonList(inputName, selectList);
        }

/*

        public static string PasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
            where TModel : class
        {
            var inputName = ExpressionHelper.GetInputName(expression);
            var local = GetValue(htmlHelper, expression);
            return htmlHelper.Password(inputName, local, htmlAttributes).ToString();
        }
*/


        static TProperty GetValue<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) where TModel : class
        {
            TModel model = htmlHelper.ViewData.Model;
            if (model == null)
            {
                return default(TProperty);
            }
            return expression.Compile()(model);
        }

 

 

 

 

    }
}