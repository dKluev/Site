using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;

namespace Specialist.Services.Interface.SitePart
{
    public interface IBannerService
    {
//        IQueryable<Banner> GetAllByTags(List<Tag> tags);
//        Banner GetFirst();
        List<Banner> GetBanner(string url);
    	Banner GetSideBanner();
    	Banner GetMainPageImageBanner();
    }
}