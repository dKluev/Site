using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Common.Extension;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using System.Linq;
using SimpleUtils.Common.Extensions;

namespace Specialist.Services
{
    public class VendorVMService:IVendorVMService
    {
        
        [Dependency]
        public IVendorService VendorService { get; set; }

        [Dependency]
        public IEntityCommonService EntityCommonService { get; set; }

        public VendorVM GetBy(string urlName, int? vendorId)
        {
            var vendor = urlName.IsEmpty()
			? VendorService.GetByPK(vendorId.Value)
			: VendorService.GetAll().ByUrlName(urlName);
			if(vendor == null)
				return null;
            return new VendorVM(vendor) {
                EntityWithTags = EntityCommonService.GetEntityWithTags(vendor) 
            };
     
        }
    }
}