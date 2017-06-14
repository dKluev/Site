using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class PollOptionMD: CmsMetaData<PollOption>
    {
        public override void Init()
        {
            this.Display("������� ������");
            this.Deletable();
            For(x => x.Text).Display("�����");
            For(x => x.PollID).Display("�����");
            For(x => x.Url).Display("������");
            For(x => x.Message).Display("���������").UIHint(Controls.TextArea);
			this.TryAddStandartProperties();

        }
    }
}