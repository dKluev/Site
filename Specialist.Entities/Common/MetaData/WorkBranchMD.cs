using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;

namespace Specialist.Entities.Common.MetaData
{
    public class WorkBranchMD : BaseMetaData<WorkBranch>
    {
        public WorkBranchMD()
        {
            this.DisplayBy(x => x.BranchName);
        }
    }
}