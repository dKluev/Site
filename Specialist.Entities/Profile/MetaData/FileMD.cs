using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;

namespace Specialist.Entities.Profile.MetaData
{
    public class FileMD:BaseMetaData<UserFile>
    {
        public override void Init()
        {
            this.DisplayByName();
            For(x => x.Name).Display("Название");
            For(x => x.Description).Display("Описание").UIHint(Controls.TextArea);
            For(x => x.UserFileID).UIHint(Controls.Hidden);
        }
    }
}