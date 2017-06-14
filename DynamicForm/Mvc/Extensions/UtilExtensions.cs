using System.Web.Mvc;

namespace DynamicForm.Mvc.Extensions
{
    public static class UtilExtensions
    {
        public static bool HasError(this ModelStateDictionary modelState,
            string propertyName)
        {
            return modelState.ContainsKey(propertyName) &&
                   modelState[propertyName].Errors.Count > 0;
        }
    }
}