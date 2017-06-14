using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;

namespace Specialist.Entities.Passport.ViewModel
{
    public class PhoneVM
    {
      /*  [DisplayName("���")]
        [Required]
        [RegularExpression(RegExp.OnlyNumbers)]
        public string Code { get; set; }*/

        [DisplayName("�������")]
        [Example("(495)1234567<br/>���� ��������� � ������������� ���������������� ���, ��������� � ������� ������ � ������� � ������� ��� ��� ������. �� �� �������� ������ ������� �����")]
        public string Phone { get; set; }

        public int ContactType { get; set; }
    }
}