using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;

namespace Specialist.Services.Interface
{
    public interface IDictionariesService
    {
        List<Country> GetCountries();
        List<Source> GetSources();
	    List<WorkBranch> GetWorkBranches();
	    Dictionary<decimal, List<Metier>> GetMetiers();
    }
}
