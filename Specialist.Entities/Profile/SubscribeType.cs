using System;
using System.ComponentModel;
using SimpleUtils.Common.Enum;

namespace Specialist.Entities.Profile {
	[Flags]
	public enum SubscribeType {
		None = 0,
		[EnumDisplayName("Газета Центра")]
		Newspaper = 1,
		[EnumDisplayName("Каталог IT специалистам и разработчикам")]
		It = 2,
		[EnumDisplayName("Каталог Бухгалтерам, менеджерам и предпринимателям")]
		Buh = 4,
		[EnumDisplayName("Каталог Дизайнерам, проектировщикам и веб-специалистам")]
		Design = 8,
		[EnumDisplayName("Каталог Школьникам и старшеклассникам")]
		School = 16,
	}
}