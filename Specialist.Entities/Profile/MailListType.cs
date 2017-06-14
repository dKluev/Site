using System;
using System.ComponentModel;
using SimpleUtils.Common.Enum;

namespace Specialist.Entities.Profile {
	[Flags]
	public enum MailListType {
		None = 0,
		[EnumDisplayName("IT-специалистам и IT-менеджерам")]
		It = 1,
		[EnumDisplayName("Дизайнерам, проектировщикам, 3D")]
		Design = 2,
		[EnumDisplayName("Веб-разработчикам, веб-маркетологам, веб-дизайнерам")]
		Web = 4,
		[EnumDisplayName("Бухгалтерам, кадровикам, сметчикам")]
		Buh = 8,
		[EnumDisplayName("Менеджерам и предпринимателям")]
		Manager = 16,
		[EnumDisplayName("Специалистам по настройке и ремонту ПК, пользователям PC и Apple")]
		Computer = 32,
	}
}