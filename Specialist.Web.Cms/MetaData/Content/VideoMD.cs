using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class VideoMD : CmsMetaData<Video>
    {
        public override void Init()
        {
            this.Display("�����");
            this.DisplayBy(x => x.Name);
            this.AddName();
            this.TryAddStandartProperties();
            For(x => x.YouTubeID).Display("YouTube ID");
            For(x => x.ShortDescription).Display("���������").UIHint(Controls.TextArea);
            For(x => x.Employee_TC).Display("��� �������������");
            For(x => x.CategoryId).Display("������");
            For(x => x.IsNew).Display("������");
            For(x => x.CourseTCList).Display("�����").UIHint(CommonConst.CourseTCList);
 
        }
    }
}