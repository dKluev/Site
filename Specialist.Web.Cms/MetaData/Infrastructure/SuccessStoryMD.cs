using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class SuccessStoryMD: CmsMetaData<SuccessStory>
    {
        public override void Init()
        {
            this.Display("������� ������");
            this.TryAddStandartProperties();
            For(x => x.Profession_ID).Display("���������");
            For(x => x.Author).Display("�����");
            For(x => x.Course_TC).Display("����");

        }
    }
}
