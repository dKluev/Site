using System;
using FluentValidation.Results;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Validators.Core;
using FluentValidation; using Microsoft.Practices.Unity;
using System.Linq;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Common.Validators.Profile
{
    public class UserFileValidator: ExValidator<UserFileVM>
    {
      
        protected override void Init()
        {
            RuleFor(x => x.UserFile.Name).NotEmpty();
            Custom(model => {

                var file = model.UploadFile;
                if (file.IsEmpty)
                    return model.IsNew
                        ? new ValidationFailure("File", "Прикрепите файл")
                        : null;

                if (!UserFiles.UserFileExt.Any(x => file.Name.ToLower().EndsWith(x)) )
                    return new ValidationFailure("File",
                        "Только {0} файлы".FormatWith(UserFiles.UserFileExt.JoinWith(", ")));
                if (file.ContentLength >
                    UserFiles.MaxFileSize) 
                        return new ValidationFailure("File", "Слишком большой файл");
                return null;
            });
            
        }
    }
}