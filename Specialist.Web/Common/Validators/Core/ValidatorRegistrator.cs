using FluentValidation; using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity;
using Specialist.Entities.Passport;
using System.Linq;

namespace Specialist.Web.Validators.Core
{
    public class ValidatorRegistrator
    {
        public static void Register(IUnityContainer container)
        {
            var validatorType = typeof(IValidator<>);
            foreach (var type in typeof(ValidatorRegistrator).Assembly.GetTypes())
            {
                if(typeof(IValidator).IsAssignableFrom(type))
                {
                    var interfaces = type.GetInterfaces().Where(i => i.IsGenericType);
                    foreach (var inter in interfaces)
                    {
                        if (inter.GetGenericTypeDefinition() == validatorType)
                            container.RegisterType(inter, type);
                    }
                    
                }
            }
          /*  container.RegisterType<IValidator<UserExamQuestionnaire>,
                UserExamQuestionnaireValidator>();*/
        }
    }
}