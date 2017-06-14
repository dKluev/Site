using Microsoft.Practices.Unity;
using Specialist.Entities.Context.ViewModel;
using Specialist.Services.Interface.Order;

namespace Specialist.Services.Order
{
    public class EditTrackVMService: IEditTrackVMService
    {
        [Dependency]
        public IOrderDetailService OrderDetailService { get; set; }

        public EditTrackVM GetTrackForEdit (int OrderDetailID)
        {
          /*  var Track = OrderDetailService.GetOrderTrack(OrderDetailID);
            var TrackVM = new EditTrackVM
                              {
                                  Track = Track
                              };
            return TrackVM;*/
            return null;
        }
    }
}