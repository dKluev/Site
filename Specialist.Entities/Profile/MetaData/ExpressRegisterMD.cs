using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Profile.MetaData {
	public class SimpleRegUserMD : BaseMetaData<SimpleRegUser> {
		public override void Init() {
			For(x => x.Name).Display("���");
			For(x => x.LastName).Display("�������");
			For(x => x.Email).Display("E-mail");
		}
	}
}