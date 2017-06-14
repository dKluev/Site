using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;
using Specialist.Services.Interface;
using Specialist.Web.Common.Utils;
using City=Specialist.Entities.Context.City;
using SimpleUtils.Extension;

namespace Specialist.Services.Common
{
    public class DictionariesService: IDictionariesService
    {
        SpecialistDataContext context = new SpecialistDataContext();
        public List<Source> GetSources()
        {
            var sources =
                from source in context.Sources
                where source.IsVisible4Web
                orderby source.SortOrder
                select source;
            return sources.ToList();
        }

	/*	public Dictionary<string, decimal> GetAllTerrains() {
			return MethodBase.GetCurrentMethod().Cache(() => {
				var context = new SpecialistDataContext();
				return context.Terrains.DistinctToDictionary(x => x.TerrainName,
					x => x.Terrain_ID);
			},24);
		} */

        public decimal GetCurrencyRate()
        {
            return context.Currencies.OrderByDescending(c => c.FiscalDate)
                .First().Rate;
        }

        public List<Country> GetCountries() {
        	return CacheUtils.Get("GetAllCountries", () => context.Countries.OrderBy(x => x.CountryName).ToList());
        }
        public List<WorkBranch> GetWorkBranches() {
        	return MethodBase.GetCurrentMethod().CacheDay(
				() => context.WorkBranches.OrderBy(x => x.SortOrder).ToList());
        }
        public Dictionary<decimal, List<Metier>> GetMetiers() {
        	return MethodBase.GetCurrentMethod().CacheDay(
				() => context.Metiers.Where(x => x.IsActive).OrderBy(x => x.SortOrder).ThenBy(x => x.MetierName)
					.ToList().GroupByToDictionary(x => x.Branch_ID, x => x));
        }
    }
}
