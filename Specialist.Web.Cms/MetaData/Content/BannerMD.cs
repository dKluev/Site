using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class BannerMD : CmsMetaData<Banner>
    {
        public override void Init()
        {
            this.Display("������");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();

            For(x => x.Image).Display("��������");
            For(x => x.TargetUrl).Display("������� ��������");
            For(x => x.Pages).Display("�������� ����������").UIHint(Controls.TextArea)
				.SetAttribute(new FullLengthStringDisplayAttribute());


            For(x => x.DateBegin).Display("���� ������");
            For(x => x.ActualDate).Display("���� ���������");
            For(x => x.Priority).Display("���������");
            For(x => x.IsSide).Display("�������");

        }
    }
}