using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Extension;

namespace SpecialistTest.Web.Core.Mvc.Extensions {
	public static class UrlHelperExtension {

		private const string SiteRoot = "http://www.specialist.ru";

		public static TagA Link<Tc>(this UrlHelper urls, Expression<Action<Tc>> selector,
			object content) where Tc : Controller {
			return H.Anchor(urls.Action(selector),content);
		}

		public static TagA AbsolutLink<Tc>(this UrlHelper urls, 
			Expression<Action<Tc>> selector,
			object content) where Tc : Controller {
			return H.Anchor(SiteRoot + urls.Action(selector),content);
		}

	/*	public static TagA LinkEx<Tc>(this UrlHelper urls, Expression<Action<Tc>> selector,
			object content) where Tc : Controller
		{
			return H.Anchor(urls.ActionEx(selector), content);
		}*/
	}
}