using System;
using System.ComponentModel;
using SimpleUtils.FluentAttributes.Attributes;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Profile.MetaData
{
    public class RestorePasswordVM: IViewModel
    {
        [DisplayName("Email")]
        [Example("������� ��� E-mail, ������� �� ����������� �� ����� �����")]
        public string Email { get; set; }

        public string Message { get; set; }
        public string Title {
            get { return "�������������� ������"; }
        }
    }
}