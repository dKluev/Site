using System;
using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using SimpleUtils.FluentAttributes.Core.Interfaces;

namespace Specialist.Entities.Passport.MetaData
{
    public class UserMD: BaseMetaData<User>
    {
        public UserMD() {
	        For(x => x.LastName).Display("�������");
        	For(x => x.EngFullName)
        		.Display("������ ��� �� ����������")
				.Example("������ ���������� ��� ��������� ����������� �������������� �������");
	        For(x => x.FirstName).Display("���");
	        For(x => x.SecondName).Display("��������");
	        For(x => x.BirthDate).Display("���� ��������").Example("30.06.1980");
            For(x => x.Password)
                .Display("������")
                .UIHint(Controls.Password)
                .Example("������ ������ ���� ������ �� 6 ������");
            For(x => x.Source_ID).Display("������ �� � ��� ������");
            For(x => x.WorkBranch_ID).Display("���������������� �������");
            For(x => x.Metier_ID).Display("���������������� ���������");
            For(x => x.Student_ID).UIHint(Controls.Hidden);
            For(x => x.Sex).Display("���").UIHint("Sex");
            For(x => x.HideCourses).Display("������ ���������� ����� � �����");
            For(x => x.HideContacts).Display("�� ���������� �� �������� ��������������");
            For(x => x.Email)
                .Display("E-mail").Example("��� ����� ����������� ����� ����������� �����, ����� ������� ��� � ����� ������ � ����������� � ����� ���������").Requerd();
            For(x => x.MailListSubscribed).Display("��������� ��������");
        }
    }
}