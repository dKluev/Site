using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class AdviceMD : CmsMetaData<Advice>
    {
        public override void Init()
        {
            this.Display("Совет");
            this.DisplayBy(x => x.Name);
            this.AddName();
            this.TryAddStandartProperties();
            For(x => x.ShortDescription).Display("Аннотация").UIHint(Controls.TextArea);
            For(x => x.Employee_TC).Display("Преподаватель")
                .UIHint(Controls.Text);
 
        }
    }
}