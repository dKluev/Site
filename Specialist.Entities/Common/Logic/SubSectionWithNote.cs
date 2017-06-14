using System;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Web.Entities.Center
{
    public class SubSectionWithNote : IEntityCommonInfo
    {
        public IEntityCommonInfo Entity { get; set; }

        public string Note { get; set; }

        public SubSectionWithNote(IEntityCommonInfo entity, string note)
        {
            Entity = entity;
            Note = note;
        }

        public string Name
        {
            get { return Entity.Name; }
        }

        public string Description
        {
            get { return Entity.Description; }
        }

        public string UrlName
        {
            get { return Entity.UrlName; }
        }

        public int WebSortOrder {
            get { return Entity.WebSortOrder; }
        }
    }
}