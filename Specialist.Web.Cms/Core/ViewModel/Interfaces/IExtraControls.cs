using System.Collections.Generic;
using SimpleUtils.FluentAttributes.Core.Controls;

namespace Specialist.Web.Cms.Core.ViewModel.Interfaces
{
    public interface IExtraControls
    {
        List<ExtraControl> ExtraControls { get; set; } 
    }
}