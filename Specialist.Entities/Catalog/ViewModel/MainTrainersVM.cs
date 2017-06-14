using System.Collections.Generic;
using SimpleUtils.Common;
using SimpleUtils.Util;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using System.Linq;
using Tab = SimpleUtils.Common.Tuple<string, string, bool>;

namespace Specialist.Entities.Catalog.ViewModel
{
    public class MainTrainersVM
    {
        //        public List<Section> RootSections { get; set; }

        //        public List<TagWithEntity<Vendor>> VendorTags { get; set; }

        public List<Employee> Trainers { get; set; }

        //public List<Section> RootSections { get; set; }

        //public List<Vendor> Vendors { get; set; }

       // public List<Profession> Professions { get; set; }

        //public List<Product> Products { get; set; }

        public EntityCommonVM PageVM { get; set; }

        public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
            RootSections { get; set; }

        public SimplePage SimplePage
        {
            get
            {
                return PageVM.Entity as SimplePage;
            }
        }

    }
}