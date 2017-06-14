using FluentValidation; using Microsoft.Practices.Unity;
using Specialist.Entities.Passport;
using Specialist.Web.Common.Validators.Core;

namespace Specialist.Web.Validators
{
    public class UserExamQuestionnaireValidator:
        ExValidator<UserExamQuestionnaire>
    {
        protected override void Init() {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.MiddleInitial).Length(1, 10).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.House).NotEmpty();
            RuleFor(x => x.Flat).Length(1,10).NotEmpty();
        }

   
    }
}