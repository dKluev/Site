using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;

namespace Specialist.Entities.Profile.ViewModel
{
    public class UserImageVM: UploadFile
    {

        [DisplayName("����")]
        [Example("����������: ���� � ������� .jpg, �������� �� ����� 200 kb")]
        [UIHint(Controls.File)]
        public string File { get; set; }

        public bool HasFile { get; set; }
    }
}