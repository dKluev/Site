
using System;
using SimpleUtils.Common.Enum;
using SimpleUtils.Extension;
using Specialist.Entities.Catalog.Interface;


namespace Specialist.Entities.Context.ViewModel
{
    public class StepsVM: IViewModel
    {
        public class Step
        {
            public bool IsCurrent { get; set; }

            public bool IsPass { get; set; }

            public string Link {get;set;}


        }
        public OrderStep OrderStep { get; set; }



        public StepsVM(OrderStep orderStep)
        {
            OrderStep = orderStep;
        }

        public string Title {
            get { return EnumUtils.GetDisplayName(OrderStep); }
        }
    }
}