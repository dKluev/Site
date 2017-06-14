using System.Text.RegularExpressions;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Web.Common.Validators.Core;
using FluentValidation; using Microsoft.Practices.Unity;
using SimpleUtils;

namespace Specialist.Web.Common.Validators.Profile
{
    public class UserContactValidator: ExValidator<UserContact>
    {
        protected override void Init()
        {
            RuleFor(x => x.Contact).Must((contact, x) =>
                 {
                     if(x.IsEmpty())
                         return true;
                     var regexp = new Regex(ContactTypes.GetRegExp(contact.ContactTypeID));
                     return regexp.IsMatch(x);

                 }
                ).WithMessage("Не верный формат");
        }
    }
}