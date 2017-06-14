using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class GuideMD : CmsMetaData<Guide>
    {
        public override void Init()
        {
            this.Display("������������");
            this.DisplayBy(x => x.Name);
            this.AddName();
            this.TryAddStandartProperties();
            For(x => x.Image).Display("��������");
            For(x => x.SmallImage).Display("���������");
            For(x => x.Areas).Display("Areas").UIHint(Controls.TextArea);
 
        }
    }
}