using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Specialist.Entities.Context;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Shop;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Mvc;

namespace Specialist.Web.Helpers.Shop
{
    public static class ShopLinks
    {
        public static string AddToCart(this HtmlHelper helper,
            Expression<Action<CartController>> action, bool button = false, bool withoutDialog = false) {
            return AddToCart(helper, action,
				button ? Urls.Button("register_red") : Urls.Main("ico_signup.gif"), withoutDialog);
        }

		   public static string AddToCartMobile(this HtmlHelper helper,
            Expression<Action<CartController>> action)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action);
            var split = url.Split('?');
            if(split.Length < 2)
                return null;
            var queryString = split[1];
            var parseResult = HttpUtility.ParseQueryString(queryString);
            var form = new TagBuilder("form");
            form.Attributes.Add("id", "rec");
            form.Attributes.Add("action", url);
            form.Attributes.Add("method", "post");
            var innerHtml = H.Submit("Записаться на курс").Class("submit").ToString();
            foreach (var key in parseResult.Keys)
            {
                innerHtml += HtmlControls.Hidden(key.ToString(), 
                    parseResult[key.ToString()]);
            }
                innerHtml += HtmlControls.Hidden(CartController.IsStay, 
                    "false");
            form.InnerHtml = innerHtml;
            return form.ToString();
        }


       /* public static string AddToCartButton(this HtmlHelper helper,
            Expression<Action<CartController>> action) {
            return AddToCart(helper, action, true);
        }*/

        public static string AddToCart(this HtmlHelper helper,
            Expression<Action<CartController>> action, string imgUrl, bool withoutDialog = false)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action);
            var split = url.Split('?');
            if(split.Length < 2)
                return null;
            var queryString = split[1];
            var parseResult = HttpUtility.ParseQueryString(queryString);
            var form = new TagBuilder("form");
            form.Attributes.Add("action", url);
            form.Attributes.Add("method", "post");
            var innerHtml = HtmlControls.ImgSubmit(imgUrl)
				.Class(withoutDialog ? "" : "add-cart-button").Attr(new {title="Добавить в корзину"})
                .ToString();
            foreach (var key in parseResult.Keys)
            {
                innerHtml += HtmlControls.Hidden(key.ToString(), 
                    parseResult[key.ToString()]);
	            if (withoutDialog) {
	                innerHtml += HtmlControls.Hidden("isStay", "true");
	            }
            }
            form.InnerHtml = innerHtml;
            return form.ToString();
        }

        public static string AddToCart(this HtmlHelper helper, Course course, bool withoutDialog = false,
			string priceTypeTC = null)
        {
            return helper.AddToCart(x => x.AddCourse(course.Course_TC, priceTypeTC), withoutDialog: withoutDialog);
        }

/*
        public static string AddWebinarRecordToCart(this HtmlHelper helper, Course course, bool withoutDialog = false)
        {
            return helper.AddToCart(x => x.AddWebinarRecord(course.Course_TC),
			withoutDialog: withoutDialog);
        }
*/
        public static string AddToCart(this HtmlHelper helper, Exam exam) {
            return AddToCart(helper, exam, false);
        }

        public static string AddToCart(this HtmlHelper helper, Exam exam, bool button) {
            if (exam.ExamPrice.GetValueOrDefault() == 0 || !exam.Available) {
                return string.Empty;
            }

            return helper.AddToCart(x => x.AddExam(exam.Exam_ID), button);
        }

    }
}