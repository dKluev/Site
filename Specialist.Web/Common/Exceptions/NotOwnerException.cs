using System;

namespace Specialist.Web.Common.Exceptions
{
    public class NotOwnerException:Exception
    {
        public string EntityName { get; set; }

        public NotOwnerException(string entityName) : base(entityName)
        {
            EntityName = entityName;
        }
    }
}