using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Passport;
using SimpleUtils.FluentAttributes.Const;

namespace Specialist.Entities.Profile.ViewModel
{
    public class ChangeStatusVM: IViewModel
    {
        [DisplayName("�����")]
        public string WebLogin { get; set; }

        [DisplayName("������")]
        [UIHint(Controls.Password)]
        public string WebKeyword { get; set; }

        public bool IsStudent { get; set; }
        public string Title
        {
            get { return "��������� ������� �� �����"; }
        }
    }
}