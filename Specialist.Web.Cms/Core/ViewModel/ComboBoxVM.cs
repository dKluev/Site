using System.Collections.Generic;
using System.Web.Mvc;
using SimpleUtils.FluentAttributes.Core;

namespace Specialist.Web.Cms.Core.ViewModel
{
    public class ComboBoxVM
    {
        public string PropertyName { get; set; }

        public List<SelectListItem> Source { get; set; }
    }
}