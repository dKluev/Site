using Specialist.Entities.Message.ViewModel;
using Specialist.Web.Common.Validators.Core;
using FluentValidation; using Microsoft.Practices.Unity;

namespace Specialist.Web.Validators.Message
{
    public class AddMessageVMValidator: ExValidator<EditMessageVM>
    {
        protected override void Init()
        {
            RuleFor(x => x.MessageTitle).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}