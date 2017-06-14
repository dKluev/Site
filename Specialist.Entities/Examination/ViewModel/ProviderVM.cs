using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Entities.ViewModel
{
    public class ProviderVM: IViewModel
    {
        public Provider Provider { get; set; }

    	public List<Vendor> Vendors { get; set; }

        public string Title
        {
            get { return "Провайдер " + Provider.Name; }
        }
    }
}
