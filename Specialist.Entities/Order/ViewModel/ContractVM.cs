using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Context.ViewModel
{
    public class ContractVM: IViewModel
    {
        public bool HasPassport { get; set; }

        public CartVM Cart { get; set; }

    	public List<string> StudyReasons { get; set; }

    	public string Title {
    		get { return "Договор"; }
    	}
    }
}