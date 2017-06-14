using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Services.UnityInterception;

namespace Specialist.Services.Interface  
{
    public interface IPriceService
    {
        List<PriceView> GetAllPricesForCourse(string courseTC, string trackTC);

        List<PriceView> GetAllPricesForCourseFilterByCustomerTye (
            string courseTC, string customerType, string trackTC);

        List<string> GetElearningCourses();

    	decimal GetPrice(Extras extras, string courseTC);

    	List<PriceView> GetAllCurrent();

    	HashSet<string> CourseWithWebinar();

	    Dictionary<string, int> CoursePriceIndex();
	    decimal? GetPriceByType(string courseTC, string priceTypeTC, string trackTC);
	    HashSet<string> CourseWithUnlimite();
	    Dictionary<string, decimal> CourseWithUnlimitePrice();
	    Dictionary<string, short?> WebinarDiscouns();
	    decimal? GetUnlimitPrice(string courseTC);
	    short? GetGroupDiscount(Group group, bool isWebinar);
	    List<PriceView> GetTrackCoursesPrices(string trackTC, string priceTypeTC);
	    HashSet<string> DopUslCourses();
    }
}