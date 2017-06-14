using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleUtils;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;

namespace Specialist.Entities.Catalog.ViewModel
{
    public class EntityCommonVM
    {
      
        
        public IEntityCommonInfo Entity { get; set; }

        public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
           EntityWithTags { get; set; }

       

        public EntityCommonVM(IEntityCommonInfo entity) {
        	Entity = entity;
         
            EntityWithTags = 
                new List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>();
        }

       
   

      


    }
}