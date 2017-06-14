using Specialist.Entities.ViewModel;

namespace Specialist.Services.Interface
{
    public interface IVendorVMService
    {
        VendorVM GetBy(string urlName, int? sectionId);
    }
}