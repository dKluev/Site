using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Const;

namespace Specialist.Entities.Context
{
    public partial class City : IEntityCommonInfo
    {
        public IEnumerable<string> PhoneList
        {
            get { return Phones == null ? new string[0] : Phones.Split(','); }
        }

        public Complex MainComplex
        {
            get
            {
                Complex complex = null;
                if(City_TC == Cities.Moscow) {
                	var branchOffice = BranchOffices
                		.FirstOrDefault(bo => bo.BranchOffice_TC == "МГТУ");
					if(branchOffice != null)
						return branchOffice
                        .Complexes.FirstOrDefault(c => c.Complex_TC == "СТИЛОБАТ");
                }

            	complex = BranchOffices.FirstOrDefault()
                    .GetOrDefault(bo => bo.Complexes.FirstOrDefault());
                if (complex == null)
                    complex = new Complex();
                return complex;
            }
        }


        public string Name
        {
            get { return CityName; }
        }

	    public string CustomName { get {
		    return City_TC == Cities.Moscow ? "Администрация Центра" : CityName;
	    } }

        public int WebSortOrder {
            get { return 0; }
        }
    }
}