using System;
using System.ComponentModel;
using SimpleUtils.Common.Enum;

namespace Specialist.Entities.Profile {
	[Flags]
	public enum MailListType {
		None = 0,
		[EnumDisplayName("IT-������������ � IT-����������")]
		It = 1,
		[EnumDisplayName("����������, ���������������, 3D")]
		Design = 2,
		[EnumDisplayName("���-�������������, ���-������������, ���-����������")]
		Web = 4,
		[EnumDisplayName("�����������, ����������, ���������")]
		Buh = 8,
		[EnumDisplayName("���������� � ����������������")]
		Manager = 16,
		[EnumDisplayName("������������ �� ��������� � ������� ��, ������������� PC � Apple")]
		Computer = 32,
	}
}