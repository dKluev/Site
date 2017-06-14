using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;

namespace Specialist.Entities.Common.MetaData
{
    public class SourceMD : BaseMetaData<Source>
    {
        public override void Init()
        {
            this.DisplayByName();
        }
    }
}