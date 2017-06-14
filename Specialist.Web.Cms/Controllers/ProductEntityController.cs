using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.Cms.Core;
using Specialist.Web.Cms.Core.ViewModel;

namespace Specialist.Web.Cms.Controllers
{
    public class ProductEntityController : BaseController<Product>
    {
        protected override void ListVMCreated(ListVM listVM)
        {
            listVM.AdditionalColumns.Add("Вендоры");
            base.ListVMCreated(listVM);
        }

        protected override List<object> GetValuesForRow(Product item)
        {
            var values = base.GetValuesForRow(item);
            var vendors = SiteObjectService.GetSingleRelation<Vendor>(item);
            var entityLinks = new ListVM.EntityLinkList();

            var metaData = MetaDataProvider.Get(typeof(Vendor));
            foreach (var vendor in vendors)
            {
                entityLinks.Add(new ListVM.EntityLink(vendor, metaData, Url));
            }
            values.Add(entityLinks);
            return values;
        }
    }
}