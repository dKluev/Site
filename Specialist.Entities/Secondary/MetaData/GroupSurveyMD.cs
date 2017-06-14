using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;
using Specialist.Entities.Secondary;

namespace Specialist.Entities.Profile.MetaData
{
    public class GroupSurveyMD: BaseMetaData<GroupSurvey>
    {
        public override void Init()
        {
            For(x => x.Reply1)
				.Display(GroupSurvey.Replay1)
				.UIHint("MediumTextArea");
        	For(x => x.Reply2)
				.Display(GroupSurvey.Replay2).UIHint("MediumTextArea");
        }
    }
}