using System;
using FluentValidation; using Microsoft.Practices.Unity;
using FluentValidation.Results;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Passport;
using Specialist.Web.Common.Validators.Core;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Validators;

namespace Specialist.Web.Common.Site.Validators.Order {
    public class SberbankInfoValidator: ExValidator<SberbankInfoVM> {

        public class AddressValidator : ExValidator<UserAddress>
        {
            protected override void Init()
            {
                RuleFor(x => x.City).Length(1,50);
            	RuleFor(x => x.Address).Length(1, 300);
                RuleFor(x => x.Index).NotEmpty();
            }
        }

        protected override void Init() {
            RuleFor(x => x.UserAddress).SetValidator(
               UnityContainer.Resolve<AddressValidator>());
            Custom(OnePhone);
        }

        private static ValidationFailure OnePhone(SberbankInfoVM model) {
            if(model.Contacts.Mobile.IsEmpty() 
                && model.Contacts.Phone.IsEmpty()
                && model.Contacts.WorkPhone.IsEmpty())
                return new ValidationFailure("", "Необходимо указать хотя бы один телефон");
            return null;
        }
    }
}