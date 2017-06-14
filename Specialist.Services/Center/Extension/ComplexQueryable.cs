using System.Linq;
using Specialist.Entities.Context;

namespace Specialist.Services.Center.Extension
{
    public static class ComplexQueryable
    {
        public static IQueryable<Complex> IsPublished(this IQueryable<Complex> complexes)
        {
            return complexes.Where(c => c.IsPublished);
        }

        public static IQueryable<Complex> ByCity(this IQueryable<Complex> complexes, 
            string cityTC)
        {
            return complexes.Where(c => c.BranchOffice.TrueCity_TC == cityTC || 
                cityTC == null)
                .IsPublished();
        }
    }
}