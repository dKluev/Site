using SimpleUtils.FluentAttributes.Core;
using FluentValidation; using Microsoft.Practices.Unity;
using FluentValidation.Internal;
using Microsoft.Practices.Unity;
using SimpleUtils.ComponentModel;
using SimpleUtils.ComponentModel.Extensions;

namespace Specialist.Web.Common.Validators.Core
{
    public class ExValidator<T> : AbstractValidator<T>
    {
        public IUnityContainer UnityContainer { get; set; }

        [InjectionMethod]
        public void SetContainer(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
            Init();
            AddNames();
        }

        virtual protected void Init()
        {
            
        }

        protected void AddNames()
        {

            foreach (var validator in this)
            {
                var propertyRule = validator as IPropertyRule<T>;
                if (propertyRule != null)
                    propertyRule.CustomPropertyName = 
                        Config.DescriptionProvider.GetProperty(propertyRule.Member)
                            .DisplayName;

            }
        }
    }
}