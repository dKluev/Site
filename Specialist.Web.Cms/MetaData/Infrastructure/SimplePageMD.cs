using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class SimplePageMD : CmsMetaData<SimplePage>
    {
        public override void Init()
        {
            this.Display("��������");
            this.DisplayBy(x => x.Title);
            For(x => x.Title).Display("���������");

            this.TryAddStandartProperties();
            For(x => x.LinkText).Display("����� ������");
            For(x => x.WithoutLink).Display("��� ������");
            For(x => x.CourseTCList).Display("�����").UIHint(CommonConst.CourseTCList);
            For(x => x.UseTabs).Display("����");
            For(x => x.UseAltTabs).Display("����� ����");
            

            For(x => x.SysName);
        }
    }
}