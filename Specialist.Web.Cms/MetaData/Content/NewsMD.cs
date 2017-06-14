using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class NewsMD : CmsMetaData<News>
    {
        public override void Init()
        {
            this.Display("�������");
            this.DisplayBy(x => x.Title);
            For(x => x.Title).Display("���������");
            For(x => x.ShortDescription).Display("���������").UIHint(Controls.TextArea);
            For(x => x.PublishDate).Display("���� �������");
            For(x => x.ForMainPage).Display("�� �������");
			For(x => x.IsHot).Display("������");
            For(x => x.MainPageDateEnd).Display("���� �� �������");
            For(x => x.Type).Display("������").ForeignType(typeof(NewsType));
            For(x => x.SmallImage).Display("������");
            For(x => x.PriorityOrder).Display("����.");
	        For(x => x.ShowEverywhere).Display("�����");
            For(x => x.CourseTCList).Display("�����").UIHint(CommonConst.CourseTCList);
            this.TryAddStandartProperties(true);
 
        }
    }
}