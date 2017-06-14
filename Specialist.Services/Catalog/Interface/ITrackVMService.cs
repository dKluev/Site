using Specialist.Entities.ViewModel;

namespace Specialist.Services.Interface
{
    public interface ITrackVMService
    {
        TrackVM GetByUrlName(string urlName);
    }
}