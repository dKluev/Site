using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Center;
using Specialist.Entities.ViewModel;

namespace Specialist.Entities.Context {
	public class MarketingActionType {
		 
        [Column(IsPrimaryKey = true)]
        public byte Id { get; set; }

        public string Name { get; set; }

		public const int Microsoft = 1;
		public const int Cisco = 2;
		public const int Design = 3;
		public const int Buh = 4;
		public const int Sapr = 5;
		public const int Management = 6;
		public const int School = 7;



		static List<MarketingActionType> All = new List<MarketingActionType>
                {
                    new MarketingActionType{Name = "Microsoft", Id = Microsoft},
                    new MarketingActionType{Name = "Cisco", Id = Cisco},
                    new MarketingActionType{Name = "Графика, 3D, анимация", Id = Design},
                    new MarketingActionType{Name = "Бухгалтерский учет и 1С", Id = Buh},
                    new MarketingActionType{Name = "САПР", Id = Sapr},
                    new MarketingActionType{Name = "Менеджмент", Id = Management},
                    new MarketingActionType{Name = "Для школьников", Id = School}
                };

        public static List<MarketingActionType> GetAll() {
	        return All;
        }
	}
}