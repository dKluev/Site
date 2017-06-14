using System;
using Microsoft.Practices.Unity;
using NLog;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Web.Cms;
using Specialist.Web.Cms.Services;
using Specialist.Web.Common.Utils.Tasks;
using Logger = Specialist.Services.Utils.Logger;
using System.Linq;
using Specialist.Services.Catalog.Extension;

namespace Specialist.Web.WinService.Tasks
{
    public class EntityActivationTask : TaskWithTimer
    {
        public override double Minutes
        {
            get { return 60 * 4; }
        }

        public override void TimerTick() {
        	BannerActivation();
			MarketingActionActivation();
			NewsMainPageDisactivation();
        }

		private void MarketingActionActivation() {
    		var marketingActionService = Cms.MvcApplication.Container.Resolve<IRepository<MarketingAction>>();
    		var actionForActivation = marketingActionService.GetAll()
				.IsActiveByDate().Where(x => !x.IsActive).ToList();
    		foreach (var action in actionForActivation) {
    			action.IsActive = true;
				ChangeUpdate(action);
    		}
    		var actionForDisactivation = marketingActionService.GetAll()
				.IsNotActiveByDate().Where(x => x.IsActive).ToList();
    		foreach (var action in actionForDisactivation) {
    			action.IsActive = false;
				ChangeUpdate(action);
    		}

    		marketingActionService.SubmitChanges();
    	}


		private void NewsMainPageDisactivation() {
    		var newsService = Cms.MvcApplication.Container.Resolve<IRepository<News>>();
    		var newsForDisactivation = newsService.GetAll()
				.Where(x => x.MainPageDateEnd != null 
					&& x.MainPageDateEnd < DateTime.Now && x.ForMainPage).ToList();
    		foreach (var action in newsForDisactivation) {
    			action.ForMainPage = false;
				ChangeUpdate(action);
    		}

    		newsService.SubmitChanges();
    	}


		void ChangeUpdate(dynamic entity) {
			entity.LastChanger_TC = Employees.Specweb;
			entity.UpdateDate = DateTime.Now;
		}

    	private void BannerActivation() {
    		var bannerService = Cms.MvcApplication.Container.Resolve<IRepository<Banner>>();
    		var bannersForActivation = bannerService.GetAll().IsActiveByDate().Where(x => !x.IsActive).ToList();
    		foreach (var banner in bannersForActivation) {
    			banner.IsActive = true;
				ChangeUpdate(banner);
    		}
    		var bannersForDisactivation = bannerService.GetAll().IsNotActiveByDate().Where(x => x.IsActive).ToList();
    		foreach (var banner in bannersForDisactivation) {
    			banner.IsActive = false;
				ChangeUpdate(banner);
    		}

    		bannerService.SubmitChanges();
    	}
    }
}