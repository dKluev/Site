using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;

namespace Specialist.Entities.Order.ViewModel {
    public class RegisterVM: StepsVM {
        public RegisterVM() : base(Context.OrderStep.Register) {}

        public Context.Order Order { get; set; }

    }
}