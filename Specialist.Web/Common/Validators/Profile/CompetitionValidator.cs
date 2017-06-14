using FluentValidation.Results;
using Specialist.Entities.Context;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Validators.Core;
using FluentValidation; using Microsoft.Practices.Unity;

namespace Specialist.Web.Common.Validators.Profile {
    public class CompetitionVMValidator: ExValidator<CompetitionVM> {

        protected override void Init() {
            RuleFor(x => x.Request).NotEmpty();
            Custom(model => {

                var file = model.UploadFile;
                if (file.IsEmpty)
                    return null;

           
                if (file.ContentLength >
                    UserFiles.CompetitionMaxFileSize)
                    return new ValidationFailure("File", "Слишком большой файл");
                return null;
            });
        }
        
    }
}