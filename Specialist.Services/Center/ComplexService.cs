using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Center.Extension;
using Specialist.Services.Interface.Center;
using Specialist.Web.Common.Utils;

namespace Specialist.Services.Center
{
    public class ComplexService : Repository2<Complex>, IComplexService
    {
        public ComplexService(IContextProvider contextProvider) : base(contextProvider)
        {
        }

	    public Dictionary<string,Complex> List() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => GetAll().ToList()
				.ToDictionary(x => x.Complex_TC, x => x));
	    } 
    }
}