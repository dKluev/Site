using SimpleUtils.Reflection;
using SimpleUtils.Reflection.Extensions;

namespace Specialist.Web.Cms.Core.ViewModel
{
    public class ControlVM
    {
        public EditVM EditVM { get; set; }

        public string PropertyName { get; set; }

        public object Value
        {
            get
            {
                return EditVM.Entity.GetValue(PropertyName);
            }
        }
    }
}