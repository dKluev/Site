using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Interface.Center
{
    public interface IComplexService: IRepository<Complex>
    {
	    Dictionary<string,Complex> List();
    }
}