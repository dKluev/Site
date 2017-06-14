using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;

namespace Specialist.Entities.Profile.MetaData
{
    public class SuccessStoryMD: BaseMetaData<SuccessStory>
    {
        public override void Init()
        {
            For(x => x.Description).Display("История").UIHint(Controls.BigTextArea);
            For(x => x.Profession_ID).Display("Профессия");
        }
    }
}