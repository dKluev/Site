using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Web.Controllers;

namespace Specialist.Web.Pages.Interfaces
{
    public interface ISimplePageVMService {
        EntityCommonVM GetByUrl(string url, PageController controller);
        SimplePage GetHostingInfo();
    }
}