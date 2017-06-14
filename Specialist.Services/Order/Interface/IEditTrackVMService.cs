using Specialist.Entities.Context.ViewModel;

namespace Specialist.Services.Interface.Order
{
    public interface IEditTrackVMService
    {
        EditTrackVM GetTrackForEdit (int OrderDetailID);
    }
}