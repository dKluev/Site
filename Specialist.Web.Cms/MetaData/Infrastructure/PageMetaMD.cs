using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class PageMetaMD:BaseMetaData<PageMeta>
    {
        public override void Init()
        {
            this.Display("Мета теги");
			this.Deletable();
            this.DisplayBy(x => x.Url);
            For(x => x.Url);
            For(x => x.Keywords).UIHint(Controls.TextArea);
            For(x => x.Description).UIHint(Controls.TextArea);
            For(x => x.Title).UIHint(Controls.TextArea);
        }
    }
}