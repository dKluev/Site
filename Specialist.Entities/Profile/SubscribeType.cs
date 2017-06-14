using System;
using System.ComponentModel;
using SimpleUtils.Common.Enum;

namespace Specialist.Entities.Profile {
	[Flags]
	public enum SubscribeType {
		None = 0,
		[EnumDisplayName("������ ������")]
		Newspaper = 1,
		[EnumDisplayName("������� IT ������������ � �������������")]
		It = 2,
		[EnumDisplayName("������� �����������, ���������� � ����������������")]
		Buh = 4,
		[EnumDisplayName("������� ����������, ��������������� � ���-������������")]
		Design = 8,
		[EnumDisplayName("������� ���������� � ����������������")]
		School = 16,
	}
}