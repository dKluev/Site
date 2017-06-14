using FluentValidation; using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Services.Interface.Passport;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Validators.Core;
using SimpleUtils;
using SimpleUtils.Extension;

namespace Specialist.Web.Common.Validators.Profile
{
    public class SubscribesVMValidator: ExValidator<SubscribesVM>
    {
        public class UserValidator : ExValidator<User> {
        [Dependency]
        public IAuthService AuthService { get; set; }

            protected override void Init() {
                var message = "Чтобы подписаться на бумажные издания"
                    + " необходимо в "
                    + HtmlControls.Anchor("/profile/editprofile", "профиле") 
                    + " заполнить почтовый адрес";
                var user = AuthService.CurrentUser;
                RuleFor(x => x.Subscribes)
                    .Must(x =>x == 0 || user.HasFullAddress)
                    .WithMessage(message);
              /*  RuleFor(x => x.NewspaperSubscribed)
                    .Must(x => user.CatalogSubscribed || !x || user.HasFullAddress)
                        .WithMessage(message);*/
            }
        }

        protected override void Init()
        {
            RuleFor(x => x.User).SetValidator(
                UnityContainer.Resolve<UserValidator>());
        }

    }
}