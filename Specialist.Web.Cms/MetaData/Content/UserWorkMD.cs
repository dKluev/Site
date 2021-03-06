using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class UserWorkMD : CmsMetaData<UserWork>
    {
        public override void Init()
        {
            this.Display("������ ���������");
            this.TryAddStandartProperties();
	        this.DisplayBy(x => x.FullName);

            For(x => x.FullName).Display("���");
            For(x => x.Section_ID).Display("�����������");
            For(x => x.Course_TC).Display("����").UIHint(Controls.Text);
            For(x => x.WorkSectionID).Display("������");
            For(x => x.SmallImage).Display("������");
            For(x => x.Trainer_TC).Display("������.").UIHint(Controls.Text);
        }
    }
}