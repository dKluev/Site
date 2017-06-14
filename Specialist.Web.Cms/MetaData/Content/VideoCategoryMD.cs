using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class VideoCategoryMD : CmsMetaData<VideoCategory>
    {
        public override void Init()
        {
            this.Display("Раздел видео");
            this.DisplayBy(x => x.Name);
            this.AddName();
            For(x => x.ParentId).Display("Родитель");
            this.TryAddStandartProperties();
        }
    }
}