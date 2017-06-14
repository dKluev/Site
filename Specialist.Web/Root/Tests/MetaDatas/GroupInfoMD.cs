using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class GroupInfoMD : BaseMetaData<GroupInfo> {
		public override void Init() {
			For(x => x.Notes).Display("Примечание").UIHint(Controls.TextArea);
		}
	}
}