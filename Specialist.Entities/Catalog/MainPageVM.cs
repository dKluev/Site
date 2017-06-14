using System.Collections.Generic;
using Specialist.Entities.Announcement;
using Specialist.Entities.Context;
using SimpleUtils;
using Specialist.Entities.Core;

namespace Specialist.Entities.Catalog
{
    public class MainPageVM
    {
    	public bool IsSecond { get; set; }
        public List<Section> Sections { get; set; }


        public List<Profession> Professions { get; set; }

        public List<Vendor> Vendors { get; set; }

        public List<Product> Products { get; set; }
        public List<News> News { get; set; }
        public List<Banner> Banners { get; set; }

        public List<SiteTerm> SiteTerms { get; set; }

        public List<CertType> Documents { get; set; }

    	public SimplePage About { get; set; }

	    public List<Video> Videos { get; set; }
    }

}