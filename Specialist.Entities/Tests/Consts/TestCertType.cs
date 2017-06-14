using SimpleUtils.Common.Enum;

namespace Specialist.Entities.Tests.Consts {
	public class TestCertType : BaseNamedId<TestCertType> {
		[EnumDisplayName("Бумажный")]
		public const int Papper = 1;
		[EnumDisplayName("Электронный")]
		public const int Image = 2;
	}
}