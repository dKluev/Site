using System.Linq;

namespace Specialist.Entities.Context.ViewModel
{
    public class PaymentTypeChoiceVM: StepsVM
    {
        public PaymentTypeChoiceVM() : base(OrderStep.PaymentTypeChoice) {}
        public Order Order { get; set; }

        public CartVM Cart { get; set; }


    }
}