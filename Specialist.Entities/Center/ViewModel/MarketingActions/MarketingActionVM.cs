using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel {
    public class MarketingActionVM: IViewModel {

        public SimplePage MarketingActions { get; set; }

        public MarketingAction MarketingAction { get; set; }


        public string Title {
            get { return (MarketingAction.IsSpecialOffer ? null : "Акция! ") + MarketingAction.Name; }
        }
    }
}
