using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Context.ViewModel;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Order;
using System.Linq;
using Specialist.Services.Utils;
using Specialist.Web.Common.Utils;


namespace Specialist.Services.Order
{
    public class ContractVMService: IContractVMService
    {
        [Dependency]
        public ICartService CartService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        [Dependency]
        public IRepository<StudyReason> StudyReasonService { get; set; }

        [Dependency]
        public IRepository<SimplePage> SimplePageService { get; set; }

		public List<string> GetStudyReasons() {
			return CacheUtils.Get(MethodBase.GetCurrentMethod(), 
				() => StudyReasonService.GetAll(x => x.IsVisible).OrderBy(x => x.SortOrder)
					.Select(x => x.Description).ToList(), 24);
		}
/*
		public Dictionary<string,string> GetOffers() {
			return CacheUtils.Get(MethodBase.GetCurrentMethod(), 
				() => {
					var offers = new[] {
						SimplePages.Offers.Universal,
						SimplePages.Offers.Webinar,
						SimplePages.Offers.WebinarRu,
					};
					var result = SimplePageService.GetAll(x => offers.Contains(x.SysName))
						.ToDictionary(x => x.SysName, x=> x.Description);
					if(result.Count != offers.Length)
						Logger.Exception(new Exception("offer.Length != result.Count"), "offer error");
					return result;
				}, 24);
		}
*/
        
        public ContractVM GetContractVM ()
        {
            CartService.UpdateDiscount(true);
            return 
                new ContractVM
                {
                    Cart = CartService.GetCart(),
					StudyReasons = GetStudyReasons(),
                };
        }

    }
}