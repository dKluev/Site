using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;
using SimpleUtils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Order.Const;

namespace Specialist.Services.Catalog.Extension
{
    public static class GroupQueryable
    {

/*
        public static IQueryable<Group> NotExam(this IQueryable<Group> groups)
        {
            return
                from gr in groups
                where !CourseTC.Exams.Contains(gr.Course_TC)
                select gr;
        }
*/
        public static IQueryable<Group> ByDateBegin(this IQueryable<Group> groups,
			DateTime? left, DateTime? right)
        {
            if (left.HasValue)
                groups = groups.Where(g => g.DateBeg >= left);
            if (right.HasValue)
                groups = groups.Where(g => g.DateBeg <= right);
	        return groups;
        }

        public static IQueryable<Group> NotSpecial(this IQueryable<Group> groups)
        {
            return
                from gr in groups
                where !CourseTC.AllSpecial.Contains(gr.Course_TC)
                select gr;
        }


        public static IQueryable<Group> NotSeminars(this IQueryable<Group> groups) {
        	var categories = new[] {Categories.Consultations, Categories.Seminars, Categories.ConsOthers};
            return
                from gr in groups
                where gr.Course.CourseCategories.All(cc => !categories.Contains(cc.Category_TC)) 
				&& !(Groups.Ubiley.Contains(gr.Group_ID) || 
				Groups.Ubiley.Contains(gr.MegaGroup_ID.GetValueOrDefault()))
                select gr;
        }

        public static IQueryable<Group> ByCity(
            this IQueryable<Group> groups, string cityTC)
        {
            return
                from gr in groups
                where cityTC == null || gr.BranchOffice.TrueCity_TC == cityTC
                select gr;
        }

        public static IQueryable<Group> NotBegin(this IQueryable<Group> groups)
        {
            return
            	groups.NotBeginWithoutMaxLimit()
					.Where(gr => gr.MaxNumOfStudents > gr.GroupCalc.NumOfStudents);
        }

	    public static IQueryable<Group> SeminarsFilter(this IQueryable<Group> groups) {
		    return groups.NotBeginWithoutMaxLimit()
			    .Where(x => x.MaxNumOfStudents > x.GroupCalc.NumOfStudents
				    || x.MaxNumOfWebinarists.GetValueOrDefault(CommonConst.MaxNumOfWebinarists) > x.GroupCalc.NumOfWebinarists);
	    }

        public static IQueryable<Group> NotBeginWithoutMaxLimit(this IQueryable<Group> groups)
        {
            return
            	groups.Where(gr => Colors.ForSite.Contains(gr.Color_TC)
					&& !CourseTC.HideGroups.Contains(gr.Course_TC)
            		&& gr.DateBeg != null && gr.TimeBeg != null
        			&& gr.DateBeg.Value.AddHours(gr.TimeBeg.Value.Hour) >= 
					DateTime.Now.AddHours(1))
					.Where(gr => !gr.IsFirstTeachersGroup
						|| gr.MaxNumOfStudents > 
						gr.GroupCalc.NumOfStudents + gr.GroupCalc.NumOfWebinarists);
        }

       /* public static IQueryable<Group> ByCourse(
            this IQueryable<Group> groups, string courseTC)
        {
            return
                from gr in groups
                where gr.Course_TC == courseTC
                select gr;
        }*/ 

    /*    public static IQueryable<Group> GetGroupsForCourse(
            this IQueryable<Group> groups, string courseTC, string cityTC)
        {
            return
                groups.PlannedAndNotBegin().ByCourse(courseTC).ByCity(cityTC);
        }
*/
        public static IQueryable<Group> PlannedAndNotBegin(this IQueryable<Group> groups)
        {
            return
                from gr in groups.NotBegin()
                where gr.LectureType_TC == LectureTypes.Planned
                select gr;

        }

        public static IQueryable<Group> WebinarOnly(this IQueryable<Group> groups)
        {
            return
                from gr in groups.NotBeginWithoutMaxLimit()
                where gr.LectureType_TC == LectureTypes.Planned
					&& gr.MaxNumOfStudents <= gr.GroupCalc.NumOfStudents && gr.WebinarExists
                select gr;

        }

      /*  public static IQueryable<Group> HotGroups(this IQueryable<Group> groups)
        {
            return
                from gr in groups.NotBegin()
                where gr.IsBlazing
                select gr;

        }*/

      /*  public static Group SmartChoice(this IQueryable<Group> groups,
            SmartGroupChoiceVM model, string courseTC, DateTime previousDateEnd,
            string cityTC)
        {
            groups = groups.PlannedAndNotBegin()
                    .ByCourse(courseTC).ByCity(cityTC)
                    .Where(g => g.DateBeg > previousDateEnd && g.Lectures.Any());
            groups = groups.SmartChoice(model);

            return groups.OrderBy(g => g.DateBeg).FirstOrDefault();

        }*/

    /*    public static IQueryable<Group> SmartChoice(this IQueryable<Group> groups,
            SmartGroupChoiceVM model) {
            var complexTC = model.ComplexTC;
            var dayShiftTC = model.DayShiftTC;
            if (model.DayOfWeeks.Count > 0)
            {
                var groupByDay = groups.Where(
                    g => g.Lectures.All(l =>
                        model.DayOfWeeks.Contains(l.LectureDateBeg.DayOfWeek)));
                model.FullFillWeekDays = groupByDay.Any();
                if (model.FullFillWeekDays)
                    groups = groupByDay;
            }
            if (!dayShiftTC.IsEmpty())
            {
                var groupByDayShift = groups.Where(g => g.DayShift_TC == dayShiftTC);
                model.FullFillDayShift = groupByDayShift.Any();
                if (model.FullFillDayShift)
                    groups = groupByDayShift;
            }
            if (!complexTC.IsEmpty())
            {
                var groupByComplex = groups.Where(g => g.Complex_TC == complexTC);
                model.FullFillComplex = groupByComplex.Any();
                if (model.FullFillComplex)
                    groups = groupByComplex;
            }
            return groups;
        }*/
    }
}