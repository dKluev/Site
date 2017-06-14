using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class ImageMetaMD:BaseMetaData<ImageMeta>
    {
        public override void Init()
        {
            this.Display("Описание картинки");
			this.Deletable();
            this.DisplayBy(x => x.Name);
            For(x => x.Name);
            For(x => x.Description).UIHint(Controls.TextArea);
        }
    }
}