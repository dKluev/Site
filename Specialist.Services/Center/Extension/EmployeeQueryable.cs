using System.Linq;
using Specialist.Entities.Context;

namespace Specialist.Services.Center.Extension
{
    public static class EmployeeQueryable
    {
        public static IQueryable<Employee> CommonList(this IQueryable<Employee> list)
        {
            return list.Where(e => e.SiteVisible).OrderBy(e => e.LastName);
        }
    }
}