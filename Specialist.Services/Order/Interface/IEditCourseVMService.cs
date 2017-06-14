using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;

namespace Specialist.Services.Interface.Order
{
    public interface IEditCourseVMService
    {
        EditCourseVM GetCourseForEdit(decimal orderDetailID);
        void Update(EditCourseVM model);
        EditCourseVM GetCourseForEdit(string trackTC);
        void UpdateTrack(List<OrderDetail> orderDetails);
    }
}