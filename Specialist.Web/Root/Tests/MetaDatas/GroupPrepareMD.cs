using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;
using Specialist.Web.Root.Tests.ViewModels;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class GroupPrepareMD : BaseMetaData<GroupPrepareVM> {
		public override void Init() {
			For(x => x.GroupID).Display("Номер группы");
			For(x => x.Email).Display("Email организатора");
		}
	}
}