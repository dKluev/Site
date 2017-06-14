using SimpleUtils.FluentAttributes.Core;

namespace Specialist.Web.Cms.MetaData.Utils {
    public class CmsMetaData<Te>: BaseMetaData<Te> {
        public CmsMetaData() {
            this.TryAddUpdateAndChanger();

        }
    }
}