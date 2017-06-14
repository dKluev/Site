using SimpleUtils.Common.Enum;

namespace Specialist.Entities.Tests.Consts {
	public class TestStatus:BaseNamedId<TestStatus> {
	
		[EnumDisplayName("Редактирование")]
		public const byte Edit = 1; 
		[EnumDisplayName("Проверка")]
		public const byte Audit = 2; 
		[EnumDisplayName("Активный")]
		public const byte Active = 3;
		[EnumDisplayName("Архив")] 
		public const byte Archive = 4;
	
	}
}