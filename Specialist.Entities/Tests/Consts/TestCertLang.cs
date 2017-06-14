using SimpleUtils.Common.Enum;

namespace Specialist.Entities.Tests.Consts {
	public class TestCertLang:BaseNamedId<TestCertLang> {
		[EnumDisplayName("Русский")]
		public const int Rus = 1;
		[EnumDisplayName("Английский")]
		public const int Eng = 2;
		[EnumDisplayName("Русский и английский")]
		public const int RusEng = 3;
	}
}