using System;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Catalog.ViewModel
{
    public class DefaultEntityCommonInfo : IEntityCommonInfo
    {
        public string Name {get;set;}

        public string Description {get;set;}

        public string UrlName { get; set; }

        public int WebSortOrder { get { return 0; } }
    }
}