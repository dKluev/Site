using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class CompetitionMD: CmsMetaData<Competition>
    {
        public override void Init()
        {
            this.Display("�������");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
            For(x => x.ToClose).Display("��������� �� ����� ������");
            For(x => x.Congratulation).Display("������������").UIHint(Controls.TextArea);
            For(x => x.Course_TC).Display("��� �����");
            For(x => x.Discount).Display("������ %");
            For(x => x.WinnerID).Display("����������").ForeignType(typeof(User)).NotFilter();
//            For(x => x.WinnerName).Display("���������� �������");
            For(x => x.Result).Display("�����").UIHint(Controls.Html);
            For(x => x.OpenDate).Display("���� ������");
            For(x => x.CloseDate).Display("���� ���������");
            For(x => x.CourseTCList).Display("�����").UIHint(CommonConst.CourseTCList);
        }
    }
}