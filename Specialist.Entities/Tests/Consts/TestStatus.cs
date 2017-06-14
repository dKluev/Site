using SimpleUtils.Common.Enum;

namespace Specialist.Entities.Tests.Consts {
	public class TestStatus:BaseNamedId<TestStatus> {
	
		[EnumDisplayName("��������������")]
		public const byte Edit = 1; 
		[EnumDisplayName("��������")]
		public const byte Audit = 2; 
		[EnumDisplayName("��������")]
		public const byte Active = 3;
		[EnumDisplayName("�����")] 
		public const byte Archive = 4;
	
	}
}