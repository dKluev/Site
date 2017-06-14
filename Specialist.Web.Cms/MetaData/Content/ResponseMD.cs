using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Center;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class ResponseMD : CmsMetaData<Response>
    {
        public override void Init()
        {
            this.Display("�����");
            this.Deletable();
            this.DisplayBy(x => x.Description);
            For(x => x.Description).Display("�����").UIHint(Controls.Html);
            For(x => x.Rating).Display("�������").ForeignType(typeof(ResponseRating));
            For(x => x.Authors).Display("�����");
            For(x => x.Course_TC).Display("��� �����").UIHint(Controls.Text);
            For(x => x.Employee_TC).Display("��� �������������").UIHint(Controls.Text);
            For(x => x.IsActive).Display("�������");
            For(x => x.IsWebinar).Display("�������");
            For(x => x.IsIntraExtra).Display("��");
            For(x => x.IsDiplom).Display("��");
            For(x => x.Type).Display("���").ForeignType(typeof(RawQuestionnaireType));
            For(x => x.Complex_TC).Display("��������").ForeignType(typeof(Complex));
        }
    }
}