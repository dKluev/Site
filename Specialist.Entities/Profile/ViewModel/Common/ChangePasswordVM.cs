using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Validation.Attribute;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;

namespace Specialist.Entities.Profile.ViewModel
{
    public class ChangePasswordVM: IViewModel
    {

        [DisplayName("������� ������")]
        [UIHint(Controls.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("����� ������")]
        [UIHint(Controls.Password)]
        public string NewPassword { get; set; }

        [DisplayName("������������� ������ ������")]
        [UIHint(Controls.Password)]
        public string ConfirmPassword { get; set; }

//        public string CorrectCurrentPassword { get; set; }

        [DisplayName("E-mail")]
        public string NewEmail { get; set; }

        public string Title
        {
            get { return "����� ������ ��� E-mail (������)"; }
        }
    }
}