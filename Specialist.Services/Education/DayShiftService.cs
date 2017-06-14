using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;

namespace Specialist.Services
{
    public class DayShiftService:Repository<DayShift>, IDayShiftService
    {
        public DayShiftService(IContextProvider contextProvider) : base(contextProvider) {}

        public List<DayShift> GetCurrent()
        {
            var current = DayShifts.GetCurrent;
            return GetAll().Where(ds =>
                current.Contains(ds.DayShift_TC)).ToList().OrderBy(ds => ds.SortOrder)
                .ToList();

        }
    }
}