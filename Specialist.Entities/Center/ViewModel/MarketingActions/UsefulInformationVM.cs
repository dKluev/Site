using System;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel
{
    public class UsefulInformationVM : IViewModel
    {

        public SimplePage UsefulInformation { get; set; }

        public UsefulInformation UsefulInfo { get; set; }
        public string Title
        {
            get { return
                
                UsefulInfo.Name; }
        }
    }
}
