using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.ViewModel;

namespace Specialist.Entities.Profile
{
    public class UploadStudentPhotoVM : IViewModel
    {

        [DisplayName("����")]
        [Example("����������: ���� � ������� .jpg, �������� 320 (������) �� 240 (������)")]
        [UIHint(Controls.File)]
        public string Photo { get; set; }

	    public bool PhotoExists { get; set; }

	    public string Title {
		    get { return "���������� ��������� ��������� ������������ ��������"; }
	    }
    }
}