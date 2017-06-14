using System;
using System.Collections.Generic;
using SimpleUtils.Util;
using Specialist.Entities.Common;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.ViewModel;

namespace Specialist.Entities.Context.ViewModel
{
    public class SmartGroupChoiceVM
    {
        public List<DayOfWeek> DayOfWeeks { get; set; }

        public List<WeekDay> WeekDays { get; set; }

        public List<DayShift> DayShifts { get; set; }

        public OrderTrack OrderTrack { get; set; }

//        public List<Tuple<Course, Group>> Courses { get; set; }

        public string ComplexTC { get; set; }

        public List<Complex> Complexes { get; set; }

        public bool IsResult { get; set; }

        public string DayShiftTC { get; set; }

        public string TrackTC { get; set; }

        public bool FullFillComplex { get; set; }

        public bool FullFillDayShift { get; set; }

        public bool FullFillWeekDays { get; set; }

        public SmartChoiceType Type { get; set; }


        /*public SmartGroupChoiceVM()
        {
            FullFillComplex = FullFillDayShift = FullFillWeekDays = true;
        }*/
    }
}