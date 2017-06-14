using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;

namespace Specialist.Entities.Const
{
    public class OrderCustomerType
    {
        public const string PrivatePerson = "chl";
        public const string Organization = "org";

        public static string GetOpposite(string customerType)
        {
            if (customerType == PrivatePerson)
                return Organization;
            return PrivatePerson;
        }

        [Column(IsPrimaryKey = true)]
        public string CustomerType { get; set; }

        public string Name { get; set; }

        public static string GetName(string customerType) {
            return GetAll().First(x => x.CustomerType == customerType).Name;
        }

		public static string GetType(bool isCompany) {
			return isCompany ? Organization : PrivatePerson;
		}

        public static List<OrderCustomerType> GetAll()
        {
            return
                new List<OrderCustomerType>
                {
                    new OrderCustomerType {Name = "Частное лицо", CustomerType = PrivatePerson},
                    new OrderCustomerType {Name = "Организация", CustomerType = Organization},
                };
        }


    }
}