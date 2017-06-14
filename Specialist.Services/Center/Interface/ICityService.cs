using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;

namespace Specialist.Services.Interface.Center
{
    public interface ICityService: IRepository<City>
    {
        DataContext Context { get; }
        char GetCityPrefix(string cityTC);

        Dictionary<string, char> GetPrefixList();
    }
}