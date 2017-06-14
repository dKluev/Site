using Specialist.Entities.Education.ViewModel;
using Specialist.Web.Common.Validators.Core;
using FluentValidation; using Microsoft.Practices.Unity;

namespace Specialist.Web.Common.Validators.Profile
{
    public class ForumGroupVMValidator: ExValidator<GroupForumVM>
    {
        protected override void Init()
        {
            RuleFor(x => x.NewMessage).NotEmpty();
        }
    }
}