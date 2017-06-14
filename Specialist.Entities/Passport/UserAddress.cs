using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Context;
using SimpleUtils.Common.Extensions;

namespace Specialist.Entities.Passport
{
    public partial class UserAddress
    {
        public bool ForSberbank { get; set; }

        private EntityRef<Country> _Country = default(EntityRef<Country>);
        [Association(Storage = "_Country", ThisKey = "CountryID",
            OtherKey = "Country_ID")]
        public Country Country {
            get { return _Country.Entity; }
            set { _Country.Entity = value; }
        }

    	public bool HasFullAddress {
    		get {
    			 return Index.HasValue && !City.IsEmpty()
                    && !Address.IsEmpty();
    		}
    	}

    	public string FullAddress {
    		get { return 
				new [] {Index.NotNullString(), State, City, Address}.JoinWith(" "); }
    	}
    }
}