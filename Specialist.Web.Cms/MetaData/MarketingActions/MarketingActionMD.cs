using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.MarketingActions
{
    public class MarketingActionMD : CmsMetaData<Specialist.Entities.Context.MarketingAction>
    {
        public override void Init()
        {
            this.Display("������������� �����");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
            For(x => x.ShortDescription).Display("���������").UIHint(Controls.Html);
            For(x => x.DateBegin).Display("���� ������");
            For(x => x.DateEnd).Display("���� ���������");
            For(x => x.Type).Display("���").ForeignType(typeof(MarketingActionType));
            For(x => x.IsAdvert).Display("���������");
			For(x => x.IsOrg).Display("���.");
			For(x => x.IsSecret).Display("������");
            For(x => x.CourseTCList).Display("�����").UIHint(CommonConst.CourseTCList);
            For(x => x.ShowOnCoursePages).Display("�������� ������");
            For(x => x.IsSpecialOffer).Display("����.");
        }
    }
}