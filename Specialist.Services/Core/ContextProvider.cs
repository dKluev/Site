using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using SimpleUtils.Reflection;
using Specialist.Entities.Passport;
using Specialist.Entities.Secondary;
using Specialist.Entities.Tests;
using Specialist.Services.Core.Interface;
using SimpleUtils.Extension;

namespace Specialist.Services.Core
{
    public class ContextProvider: BaseContextProvider
    {
       

        public override DataContext Get(Type entityType)
        {
            if (entityType == typeof(MailTemplate))
                return new SpecialistWebDataContext();
            var context = (DataContext) _contextList.FirstOrDefault(
                                     pair => pair.Value.Contains(entityType)).Key.Create();
            if (context.GetType().In(typeof(SpecialistWebDataContext), 
                typeof(PassportDataContext), typeof(SpecialistTestDataContext), typeof(ForSecondaryDataContext)))
                return new SpecialistDataContext();
            return context;
        }


      
    }
}