using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using SimpleUtils.Reflection;
using Specialist.Entities.Passport;
using Specialist.Services.Core.Interface;
using SimpleUtils.Extension;

namespace Specialist.Services.Core
{
    public class SpecialistContextProvider: BaseContextProvider
    {
        private static string _pioneerConnectionString =
           ConfigurationManager
                   .ConnectionStrings["Specialist.Entities.Properties.Settings.PioneerConnectionString"].ConnectionString;

        public override DataContext Get(Type entityType)
        {
            return new SpecialistDataContext(_pioneerConnectionString);
        }


      
    }
}