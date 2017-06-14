using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Order.ViewModel {
    public class OrderConfirmVM: IViewModel {

        public const string ConfirmInfoName =
            "Содержание штампа контрольно - кассовой машины";
        [DisplayName(ConfirmInfoName)]
        [UIHint(Controls.TextArea)]
        public string ConfirmInfo { get; set; }

        public CartVM Cart { get; set; }

        public decimal OrderID { get; set; }

        public string Title {
            get { return "Подтверждение оплаты через Cбербанк"; }
        }
    }
}