using Specialist.Entities.Context;

namespace Specialist.Entities.Order.ViewModel {
    public class ReceiptVM {
        
        public Context.Order Order { get; set; }

        public OurOrg OurOrg { get; set; }

        public OurOrgBank OurOrgBank { get; set; }
    }
}