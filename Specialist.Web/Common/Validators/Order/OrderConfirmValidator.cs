using Specialist.Entities.Order.ViewModel;
using Specialist.Web.Common.Validators.Core;
using FluentValidation; using Microsoft.Practices.Unity;

namespace Specialist.Web.Common.Validators.Order {
    public class OrderConfirmValidator: ExValidator<OrderConfirmVM> {
        protected override void Init() {
            RuleFor(x => x.ConfirmInfo).NotEmpty();
        }
    }
}