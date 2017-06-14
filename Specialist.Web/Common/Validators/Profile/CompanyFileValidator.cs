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
    public class CompanyFileValidator: ExValidator<CompanyFileVM>
    {
      
        protected override void Init()
        {
            Custom(model => {

                var file = model.UploadFile;
                if (file.IsEmpty)
                    return model.IsNew
                        ? new ValidationFailure("File", "Прикрепите файл")
                        : null;

                if (file.ContentLength >
                    CompanyFiles.MaxFileSize) 
                        return new ValidationFailure("File", "Слишком большой файл");
                return null;
            });
            
        }
    }
}