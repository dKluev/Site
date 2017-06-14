using Specialist.Entities.ViewModel;

namespace Specialist.Services.Interface
{
    public interface ICertificationcVMService
    {
        CertificationVM GetByUrlName(string urlName);
    }
}