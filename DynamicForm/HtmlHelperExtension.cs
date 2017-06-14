using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using DynamicForm.Mvc;
using DynamicForm.Utils;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.ComponentModel;
using SimpleUtils.ComponentModel.Extensions;

namespace DynamicForm
{
    public static class HtmlHelperExtension
    {
        public static ControlBuilder ControlFor<Te, Tp>(this HtmlHelper<Te> htmlHelper, Expression<Func<Te, Tp>> selector) where Te : class
        {
            var builder = CreateControlBuilder(selector, htmlHelper);
            return builder;
        }

     /*   public static FormBuilder Form<Te, Tc>(this HtmlHelper<Te> htmlHelper, 
            Expression<Action<Tc>> action) where Te : class
        {
            var metaData
            var builder = new FormBuilder(htmlHelper, );
            builder.
            return builder;
        }
*/
        private static ControlBuilder CreateControlBuilder<Te, Tp>(
            Expression<Func<Te, Tp>> selector, HtmlHelper<Te> htmlHelper) 
            where Te : class
        {
            var instance = ExpressionUtils.GetPropertyOwner(selector,
                htmlHelper.ViewData.Model);
            var propertyVM = new PropertyVMCreator(htmlHelper).Create(selector, instance);
            return new ControlBuilder
            {
                HtmlHelper = htmlHelper,
                Model = propertyVM
            };
        }

      /*  public static ControlBuilder SelectFor<Te, Tp, Ts>(
            this HtmlHelper<Te> htmlHelper, 
            Expression<Func<Te, Tp>> expr, 
            IEnumerable<Ts> source, 
            Func<Ts, object> textSelector, 
            Func<Ts, object> valueSelector) where Te : class
        {
            var selectItemList = SelectListUtil.GetSelectItemList(source,
                textSelector, valueSelector);
            var specification = GetSelect(htmlHelper, expr, selectItemList);
            return specification;
        }
*/
        private static ControlBuilder GetSelect<Te, Tp>(HtmlHelper<Te> htmlHelper,
            Expression<Func<Te, Tp>> selector, IEnumerable<SelectListItem> selectItemList) 
            where Te : class
        {
            var controlBuilder = CreateControlBuilder(selector, htmlHelper);
            var currentValue = selector.Compile().Invoke(htmlHelper.ViewData.Model);
            foreach (var listItem in selectItemList)
            {
                if(listItem.Value == currentValue.NotNullString())
                {
                    listItem.Selected = true;
                    break;
                }
            }
            SetNewModel(controlBuilder,  selectItemList);

            controlBuilder.UIHint(Controls.Select);
            return controlBuilder;
        }

        private static PropertyVM<T> SetNewModel<T>(
            ControlBuilder controlBuilder, T value) {
            var modelProperty = new PropertyVM<T>();
            modelProperty.Descriptor = controlBuilder.Model.Descriptor;
            modelProperty.Name = controlBuilder.Model.Name;
            modelProperty.Value = value;
            controlBuilder.Model = modelProperty;
            return modelProperty;
        }

        public static ControlBuilder SelectFor<Te, Tp, Ts>(this HtmlHelper<Te> htmlHelper, Expression<Func<Te, Tp>> selector, IEnumerable<Ts> source) where Te : class
        {
            return SelectFor<Te, Tp, Ts>(htmlHelper, selector, source, null);
        }

        public static ControlBuilder SelectFor<Te, Tp, Ts>(this HtmlHelper<Te> htmlHelper, Expression<Func<Te, Tp>> selector, IEnumerable<Ts> source, string nullText) where Te : class
        {
            var selectItemList = SelectListUtil.GetSelectItemList(source);
            if(!nullText.IsEmpty())
            {
                selectItemList.Insert(0, SelectListUtil.GetEmptyItem(nullText));
            }

            return GetSelect(htmlHelper, selector, selectItemList);
        }

		 public static ControlBuilder SelectForWithList<Te, Tp>(this HtmlHelper<Te> htmlHelper, Expression<Func<Te, Tp>> selector, List<SelectListItem> selectItemList,
			 string nullText = null) where Te : class
        {
            if(!nullText.IsEmpty())
            {
                selectItemList.Insert(0, SelectListUtil.GetEmptyItem(nullText));
            }

            return GetSelect(htmlHelper, selector, selectItemList);
        }

        public static TagBuilder LabelFor<Te, Tp>(this HtmlHelper<Te> htmlHelper, 
            Expression<Func<Te, Tp>> selector) where Te : class
        {
            var propertyInfo = ExpressionUtils.GetPropertyInfo(selector);
            var builder = new ExTagBuilder("label");
            builder.Attributes.Add("for", ExpressionUtils.GetPropertyName(selector));
            builder.InnerHtml =
                Config.DescriptionProvider.GetProperty(propertyInfo, 
                htmlHelper.ViewData.Model).DisplayName;
            return builder;
        }


    }
}