using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;

namespace Specialist.Web.Cms.Repository
{
    public class PaymentTypeRepository: Repository<PaymentType>
    {
        List<string> _types = new List<string> {
            PaymentTypes.Cash,
            PaymentTypes.SberBank,
            PaymentTypes.Terminal,
            PaymentTypes.CyberPlat,
            PaymentTypes.YandexMoney,
            PaymentTypes.WebMoney,
            PaymentTypes.Qiwi,
            PaymentTypes.Invoice,
            PaymentTypes.RbkMoney,
            PaymentTypes.SberMerchant,
			PaymentTypes.NoPayment,
			PaymentTypes.ExpressOrder
    };


	    public PaymentTypeRepository(IContextProvider contextProvider) : base(contextProvider) {}

	    [Cached]
        public override IQueryable<PaymentType> GetAll() {
            var paymentTypes = base.GetAll().Where(x => _types.Contains(x.PaymentType_TC))
                .ToList();
            paymentTypes = paymentTypes.OrderBy(x => _types.IndexOf(x.PaymentType_TC))
                .ToList();
            foreach (var paymentType in paymentTypes) {
                paymentType.Name = new Regex( @"\(.*").Replace(paymentType.Name, "");
            }
            return paymentTypes.AsQueryable();
        }
    }
}
