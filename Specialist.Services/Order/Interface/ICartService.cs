using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;

namespace Specialist.Services.Interface.Order
{
    public interface ICartService
    {
        void DeleteCourse(decimal orderDetailID);
        void Clear();
        void DeleteTrack (string trackTC);
        void AddCourse (string courseTC, string priceTypeTC, string socialUrl = null, string secondCourseTC = null);
        void AddTrack(string trackTC, string priceTypeTC);
        void AddGroup(decimal groupID);
        CartVM GetCart(decimal? orderId = null, bool addTrackDiscounts = false);
        bool AddExam(decimal examID);
        void DeleteExam(decimal examID);
        void UpdateOrder(string customerType);
        string SetPaymentType(string paymentType, decimal @decimal);
        void ToggleExam(decimal examID);
        void ToggleCourse(decimal orderDetailID);
        void ToggleTrack(string trackTC);
        void UpdateDiscount(bool force);
    	bool? AddTestCert(int testId);
    	bool AddForeignDelivery(OrderDetail orderDetail);
    	string GetOrderManagerTC(decimal orderId);
	    Dictionary<decimal, List<short>> GetReservedSeatNumbers(List<decimal> groupIds);
    }
}