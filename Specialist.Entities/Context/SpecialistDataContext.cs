using System.Reflection;

namespace Specialist.Entities.Context {
    public partial class SpecialistDataContext {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "SpecialistWeb.dbo.GetNewID", IsComposable = true)]
        public System.Nullable<System.Guid> GetNewID()
        {
            return ((System.Nullable<System.Guid>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod()))).ReturnValue));
        }
    }
}