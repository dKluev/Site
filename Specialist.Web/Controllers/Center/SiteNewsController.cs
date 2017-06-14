using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcContrib.ActionResults;
using SimpleUtils.Collections.Paging;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Util;
using Specialist.Entities.Catalog;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Core.Logic;
using Specialist.Web.Pages;
using Specialist.Web.Util;
using System.Linq.Dynamic;
using System.Linq;
using Specialist.Services.Common.Extension;
using SimpleUtils.Collections.Extensions;
using Specialist.Services.Catalog.Extension;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;
using Specialist.Services.Interface.Center;
using Specialist.Web.Helpers;
using Specialist.Web.Common.Utils;

namespace Specialist.Web.Controllers.Center
{
    public class SiteNewsController : ViewController
    {
        [Dependency]
        public INewsService NewsService { get; set; }

        [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

        [Dependency]
        public IComplexService ComplexService { get; set; }

        [Dependency]
        public IRepository2<Group> GroupRepository { get; set; }


        public ActionResult List(string newsTypeUrl, int? pageIndex)
        {
            if (!pageIndex.HasValue)
                return RedirectToAction(() => List(newsTypeUrl, 1));
            var newsTypeID = (byte)1;
	        if (newsTypeUrl != null) {
		        var newsType = NewsType.All.First(n => !n.HideFromTabs && n.UrlName == newsTypeUrl);
		        if (newsType == null)
			        return NotFound();
		        newsTypeID = newsType.NewsTypeID;
	        }
	        else {
		        return NotFound();
	        }

            
            var news = NewsService.GetAll().IsActive();
			if(CommonConst.IsMobile) {
				news = news.Where(n => n.ForMainPage);
			}
			else if (newsTypeID == NewsType.Publish)
				news = news.Where(n => NewsType.Publishes.Contains(n.Type));
			else if (newsTypeUrl != NewsType.Main)
				news = news.Where(n => n.Type == newsTypeID);
			else {
				news = news.Where(n => n.Type != NewsType.TrainerPublish);
			}
        	var count = 20;
            return MView(Views.SiteNews.List,
                         new NewsListVM{
							 News = news.OrderByDescending(n => n.PublishDate)
								.ToPagedList(pageIndex.GetValueOrDefault(1) - 1, count), 
							 Type = NewsType.AllById[newsTypeID]
            });
        }

		[HandleNotFound]
        public ActionResult Search(string typeName, int id, int? pageIndex)
        {
            if (!pageIndex.HasValue)
                return RedirectToAction(() => Search(typeName, id, 1));
        	var type = SiteObject.GetType(typeName);
			if(type == null)
				return null;
            var news = SiteObjectService.GetByRelationObject<News>(type, id)
				.IsActive().OrderByDescending(n => n.PublishDate)
                .ToPagedList(pageIndex.GetValueOrDefault(1) - 1, 5);

			return View(
				new RelationNewsVM {
					News = news,
					SiteObject = SiteObjectService.GetBy(
						LinqToSqlUtils.GetTableName(type), id)
				});
	
        }

		[HandleNotFound]
        public ActionResult Details(int? newsID, string title)
        {
			if(newsID == null)
				return null;
            var news = NewsService.GetByPK(newsID);
			if(news == null || !news.IsActive)
				return null;
        	var currentTitle = Linguistics.UrlTranslite(news.Title);
			if(title != currentTitle)
				return  new RedirectResult(Url
					.Action<SiteNewsController>(
					c => c.Details(newsID, currentTitle)), true);
              
			GroupRepository.LoadWith(x => x.GroupCalc);
			var seminar = GroupRepository.GetAll()
				.SeminarsFilter()
				.OrderByDescending(x => x.DateBeg)
				.FirstOrDefault(x => x.UrlName == "/news/" + newsID);
			if (seminar != null) {
				seminar.Complex = seminar.Complex_TC.GetOrDefault(x => ComplexService.List()[x]);
			}
            var model =
                new NewsVM
                {
                    News = news,
					Seminar = seminar
                };
            return MView(Views.SiteNews.Details, model);
        }

		public ActionResult All(int? year) {
			var yearOrCurrent = year ?? DateTime.Today.Year;
			var html = MethodBase.GetCurrentMethod().CacheWith(() => {
				var yearsBlock = H.h2[Enumerable.Range(2001, DateTime.Today.Year - 2001 + 1).Reverse()
					.Select(x => x == yearOrCurrent ? (IHtmlTag)H.b[x] : Url.SiteNews().All(x, x.ToString()))];
				var news = NewsService.GetAll().Where(x => x.IsActive
					&& x.PublishDate.Year == yearOrCurrent).OrderByDescending(n => n.PublishDate)
					.Select(x => new {
						x.NewsID, x.Title, x.PublishDate
					}).ToList()
					.Select(x => new News {
						NewsID = x.NewsID, Title = x.Title, PublishDate = x.PublishDate
					});

				return H.div[yearsBlock, news.GroupBy(x => x.PublishDate.Month)
					.Select(x => H.div[H.h3[MonthUtil.GetName(x.Key)],
					H.Ul(x.Select(y => y.PublishDate.DefaultString() + " " + Html.NewsLink(y)))])].ToString();
			}, yearOrCurrent, 24);

			var model = new AllNewsVM {
				Year = yearOrCurrent
			};
			var view = InlineBaseView.New(model, x => html);
			return BaseViewWithModel(view, model);

		}

	}
}