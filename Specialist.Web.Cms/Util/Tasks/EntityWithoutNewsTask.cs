using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Services.Catalog;
using Specialist.Services.Common;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Web.Cms.Services;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Tasks;
using SimpleUtils.Collections.Extensions;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity;
namespace Specialist.Web.WinService.Tasks {
	public class EntityWithoutNewsTask : TaskWithTimer {
		public override double Minutes {
			get { return 60*24; }
		}

		public override void TimerTick() {
			var objectRelationService = Cms.MvcApplication.Container.Resolve<ISiteObjectRelationService>();
			var entityService = Cms.MvcApplication.Container.Resolve<EntityService>();
			var newsService = Cms.MvcApplication.Container.Resolve<IRepository<News>>();
			var sections = Cms.MvcApplication.Container.Resolve<IRepository<Section>>()
				.GetAll(x => x.IsActive && !x.IsMain);
			var vendors = Cms.MvcApplication.Container.Resolve<IRepository<Vendor>>().GetAll(x => x.IsActive);
			var products = Cms.MvcApplication.Container.Resolve<IRepository<Product>>().GetAll(x => x.IsActive);
			var professions = Cms.MvcApplication.Container.Resolve<IRepository<Profession>>().GetAll(x => x.IsActive);
			var siteTerms = Cms.MvcApplication.Container.Resolve<IRepository<SiteTerm>>().GetAll(x => x.IsActive);
			var entities = new List<object>();
			var entitiesWithoutNews = new List<object>();
			entities.AddRange(sections);
			entities.AddRange(products);
			entities.AddRange(vendors);
			entities.AddRange(siteTerms);
			entities.AddRange(professions);
			var dateTime = DateTime.Today.AddDays(-14);
			var newIds = newsService.GetAll(x => x.PublishDate >= dateTime && x.IsActive).Select(x => x.NewsID)
				.Cast<object>()
				.ToList();

			AddEntitiesWithoutNews(entitiesWithoutNews, vendors.ToList(), objectRelationService, newIds);
			AddEntitiesWithoutNews(entitiesWithoutNews, products.ToList(), objectRelationService, newIds);
			AddEntitiesWithoutNews(entitiesWithoutNews, sections.ToList(), objectRelationService, newIds);
			AddEntitiesWithoutNews(entitiesWithoutNews, siteTerms.ToList(), objectRelationService, newIds);
			AddEntitiesWithoutNews(entitiesWithoutNews, professions.ToList(), objectRelationService, newIds);
			//    		AddEntitiesWithoutNews(entitiesWithoutNews, simplePages.ToList(), objectRelationService, newIds);
			const string br = "<br/>";
			var mailBody = entitiesWithoutNews.Cast<IEntityCommonInfo>()
				.GroupBy(entityService.GetRank)
				.OrderBy(x => x.Key)
				.Select(x => H.strong[x.Key] + br + x.Select(y => y.Name).JoinWith(br)).JoinWith(br);
			var mailService = Cms.MvcApplication.Container.Resolve<IMailService>();
			mailService.Send(new MailAddress("info@specialist.ru"), MailService.motorina,
				mailBody, "Без новостей " + entitiesWithoutNews.Count);
		}

		private void AddEntitiesWithoutNews(List<object> entitiesWithoutNews,
			IEnumerable<object> entities, ISiteObjectRelationService objectRelationService,
			IEnumerable<object> newIds) {
			var type = entities.First().GetType();
			var relationIds = objectRelationService.GetRelation(typeof (News), newIds, type)
				.Select(x => x.RelationObject_ID).Cast<int>().ToList();
			entitiesWithoutNews.AddRange(
				entities.Where(x => !relationIds.Contains((int) LinqToSqlUtils.GetPK(x))));
		}

	}
}