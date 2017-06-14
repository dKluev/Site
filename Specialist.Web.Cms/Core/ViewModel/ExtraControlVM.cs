using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Controls;

namespace Specialist.Web.Cms.Core.ViewModel
{
    public class ExtraControlVM
    {
        public BaseMetaData MetaData { get; set; }

        public object Entity { get; set; }

        public ExtraControl ExtraControl { get; set; }

        public ExtraControlVM(BaseMetaData metaData, object entity, ExtraControl extraControl)
        {
            MetaData = metaData;
            Entity = entity;
            ExtraControl = extraControl;
        }
    }
}