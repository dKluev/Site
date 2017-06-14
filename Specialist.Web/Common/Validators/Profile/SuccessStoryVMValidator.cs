using FluentValidation.Results;
using Specialist.Entities.Context;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Validators.Core;
using FluentValidation; using Microsoft.Practices.Unity;

namespace Specialist.Web.Common.Validators.Profile
{
    public class SuccessStoryVMValidator: ExValidator<EditSuccessStoryVM>
    {
        public class SuccessStoryValidator: ExValidator<SuccessStory>
        {
            protected override void Init()
            {
                RuleFor(x => x.Description).Length(1, 5000).NotEmpty();
            }
        }

        public class UserImageVMValidator: ExValidator<UserImageVM>
        {
            protected override void Init()
            {
                RuleFor(x => x.Name).Must(x => x.ToLower().EndsWith(Urls.PhotoExt))
                    .Configure(x => x.CustomPropertyName = "File")
                    .WithMessage("Картинка только в формате .jpg");
             /*  Custom(file =>
               {
                   if (file.ContentLength == 0)
                       return null;

                   if (!file.Name.ToLower().EndsWith(Urls.PhotoExt))
                       return new ValidationFailure("File", 
                           "Картинка только в формате .jpg");
                   if (file.ContentLength > UserImages.MaxImageSize)
                       return new ValidationFailure("File", "Слишком большой файл");
                   return null;
               });*/
            }
        }

        protected override void Init()
        {
            RuleFor(x => x.SuccessStory).SetValidator(
                UnityContainer.Resolve<SuccessStoryValidator>());
//            RuleFor(x => x.Images).SetValidator(
//                UnityContainer.Resolve<UserImageVMValidator>());
        }
    }
}