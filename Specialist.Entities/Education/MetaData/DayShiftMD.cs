using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;

namespace Specialist.Entities.Filter.MetaData
{
    public class DayShiftMD : BaseMetaData<DayShift>
    {
        public override void Init()
        {
            this.DisplayByName();
        }
    }
}