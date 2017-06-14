using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;

namespace Specialist.Entities.Profile.ViewModel
{
    public class UserImageVM: UploadFile
    {

        [DisplayName("Фото")]
        [Example("Примечание: файл в формате .jpg, размером не более 200 kb")]
        [UIHint(Controls.File)]
        public string File { get; set; }

        public bool HasFile { get; set; }
    }
}