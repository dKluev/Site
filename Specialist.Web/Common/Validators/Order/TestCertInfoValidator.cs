using System;
using FluentValidation; using Microsoft.Practices.Unity;
using FluentValidation.Results;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Passport;
using Specialist.Web.Common.Validators.Core;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Validators;

namespace Specialist.Web.Common.Site.Validators.Order {
    public class TestCertInfoValidator: ExValidator<TestCertInfoVM> {

/*
        public class AddressValidator : ExValidator<UserAddress>
        {
            protected override void Init()
            {
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.Address).NotEmpty().Length(0,);
                RuleFor(x => x.Index).NotEmpty();
            }
        }
*/

        protected override void Init() {
            RuleFor(x => x.UserAddress).SetValidator(
               UnityContainer.Resolve<SberbankInfoValidator.AddressValidator>());
			Custom(EngFullName);
        }

        private static ValidationFailure EngFullName(TestCertInfoVM model) {
            if(model.IsEngCert && model.User.EngFullName.IsEmpty())
                return new ValidationFailure("User.EngFullName", "Необходимо указать полное имя на английском");
            return null;
        }
    }
}