using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class ExamMD : CmsMetaData<Exam>
    {
        public override void Init()
        {
            this.Display("Экзамен");
            this.DisplayBy(x => x.Exam_TC);

            For(x => x.Exam_TC).Display("Номер");
            this.AddName();
        }
    }
}