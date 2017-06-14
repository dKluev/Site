using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Services.UnityInterception;

namespace Specialist.Services.Interface
{
    public interface ITrackService
    {
        Course GetByUrlName(string urlName);
        List<TrackDiscount> GetTrackDiscountForCourse(string courseTC);
        IEnumerable<TrackDiscount> GetTrackDiscounts(IEnumerable<Course> tracks);
    	IQueryable<Course> GetAllTracksWithCourse(string courseTC);

	    Dictionary<string, Dictionary<string, decimal?>> TrackFullPrices();
	    Dictionary<string, Dictionary<string,Tuple<decimal,decimal>>> TrackLastCourseDiscounts();
    }
}