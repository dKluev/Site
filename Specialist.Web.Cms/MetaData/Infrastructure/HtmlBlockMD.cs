using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class HtmlBlockMD : CmsMetaData<HtmlBlock>
    {
        public override void Init()
        {
            this.Display("Html ����");
            this.DisplayBy(x => x.Name);

	        For(x => x.Name).Display("��������");
	        For(x => x.ViewHtml).Display("�����������").UIHint(Controls.None).ReadOnly();
            For(x => x.Description).Display("Html").UIHint(Controls.TextArea);
        }
    }
}
