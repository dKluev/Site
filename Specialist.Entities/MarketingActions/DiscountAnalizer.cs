using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;

namespace Specialist.Entities.MarketingActions
{
    public class DiscountAnalizer
    {
        public IQueryable<Discount> GetDiscounts( IQueryable<Discount> discounts,
            Student student, 
            byte beforeStartInDays, 
            DateTime? groupDateBegin, 
            DateTime? groupDateEnd, 
            string dayShiftTC, 
            bool isWeekend, 
            string customerType, 
            decimal? previousOrderSum, 
            string authorizationTypeTC, 
            int? personCategoryID, 
            List<string> graduateCourseTCList,
            string groupComplexTC,
            string groupCityTC,
            string courseTC,
			string clabCardColorTC)
        {
            discounts = ByStudent(discounts, student);
            discounts = ByGroupParameters(discounts,
                beforeStartInDays, groupDateBegin, groupDateEnd, dayShiftTC, isWeekend,
                groupComplexTC, groupCityTC);
            discounts = ByCustomerType(discounts, customerType);
            discounts = ByPreviousOrderSum(discounts, previousOrderSum);
            discounts = ByClabCardColorTC(discounts, clabCardColorTC);
            discounts = ByAuthorization(discounts, authorizationTypeTC);
            discounts = ByPersonCategory(discounts, personCategoryID);
            discounts = ByGraduateCourses(discounts, graduateCourseTCList);
            discounts = ByCourses(discounts, courseTC);
            return discounts;
        }


		/*private IQueryable<Discount> ByWebinar(IQueryable<Discount> discounts, bool isWebinar) {
			if(isWebinar)
				return discounts.Where(d => Const.MarketingActions.WebinarDiscounts.Contains(d.Discount_ID));
			return discounts.Where(d => !Const.MarketingActions.WebinarDiscounts.Contains(d.Discount_ID));
		}

*/
        private IQueryable<Discount> ByGroupParameters(IQueryable<Discount> discounts,
         byte beforeStartInDays, DateTime? groupDateBegin, DateTime? groupDateEnd,
         string dayShiftTC, bool isWeekend, string complexTC, string cityTC)
        {
            if (complexTC != null)
                discounts =
                   from discount in discounts
                   where
                       (
                           discount.DiscountComplexes.Count() == 0
                           ||
                           discount.DiscountComplexes
                               .Any(dc => dc.Complex_TC == complexTC)
                       )
                   select discount;
            else
                discounts = discounts.Where(d => d.DiscountComplexes.Count() == 0);

            if (cityTC != null)
                discounts =
                   from discount in discounts
                   where
                       (
                           discount.DiscountCities.Count() == 0
                           ||
                           discount.DiscountCities
                               .Any(dc => dc.City_TC == cityTC)
                       )
                   select discount;
            else
                discounts = discounts.Where(d => d.DiscountCities.Count() == 0);

            if (beforeStartInDays > 0)
                discounts = discounts.Where(d => d.ReserveDateSpan == null
                    || d.ReserveDateSpan <= beforeStartInDays);
            else
                discounts = discounts.Where(d => d.ReserveDateSpan == null);

            if (groupDateBegin.HasValue)
                discounts = discounts.Where(d => d.GroupDateBegin == null
                                                 || d.GroupDateBegin < groupDateBegin.Value);
            else
                discounts = discounts.Where(d => d.GroupDateBegin == null);

            if (groupDateEnd.HasValue)
                discounts = discounts.Where(d => d.GroupDateEnd == null
                                                 || d.GroupDateEnd > groupDateEnd.Value);
            else
                discounts = discounts.Where(d => d.GroupDateEnd == null);

            if (dayShiftTC != null)
                discounts = discounts.Where(d => d.DayShift_TC == null
                                                 || d.DayShift_TC == dayShiftTC);
            else
                discounts = discounts.Where(d => d.DayShift_TC == null);
            if (isWeekend)
                discounts = discounts.Where(d => d.IsWeekend == null || d.IsWeekend.Value);
            else
                discounts = discounts.Where(d => d.IsWeekend == null || !d.IsWeekend.Value);
            return discounts;
        }

        private IQueryable<Discount> ByCustomerType(IQueryable<Discount> discounts,
            string customerType)
        {
            discounts =
                from discount in discounts
                where
                    (
                        discount.DiscountCustomerTypes.Count() == 0
                        ||
                        discount.DiscountCustomerTypes
                            .Any(dct => dct.CustomerType_TC == customerType)
                    )
                select discount;
            return discounts;
        }

        private IQueryable<Discount> ByStudent(IQueryable<Discount> discounts,
          Student student)
        {
            if (student != null)
            {
                discounts =
                   from discount in discounts
                   where
                       discount.MarketingAction.MarketingActionStudents.Count() == 0
                       || discount.MarketingAction.MarketingActionStudents
                              .Any(mas => mas.Student_ID == student.Student_ID)
                   select discount;
            }
               
            else
                discounts = discounts
                    .Where(d => d.MarketingAction.MarketingActionStudents.Count() == 0 
                        && !d.ForGraduate);
            return discounts;
        }

        private IQueryable<Discount> ByPersonCategory(IQueryable<Discount> discounts,
            int? userCategoryID)
        {
            if (userCategoryID != null)
                discounts =
                    from discount in discounts
                    where
                        discount.DiscountPersonCategories.Count() == 0
                        || discount.DiscountPersonCategories
                               .Any(dpc => dpc.PersonCategory_ID == userCategoryID.Value)
                    select discount;
            else
                discounts = discounts.Where(d => d.DiscountPersonCategories.Count() == 0);
            return discounts;
        }

        private IQueryable<Discount> ByGraduateCourses(IQueryable<Discount> discounts,
           List<string> courseTCList)
        {
            if (courseTCList.Count != 0)
                discounts =
                    from discount in discounts
                    let discs = discount.DiscountGraduateCourses.Where(dgc => dgc.ForStudent)
                    where
                        discs.Count() == 0
                        || discs.All(dgc => courseTCList.Contains(dgc.Course_TC))
                    select discount;
            else
                discounts = discounts.Where(
                    d => d.DiscountGraduateCourses
                        .Where(dgc => dgc.ForStudent).Count() == 0);
            return discounts;
        }

        private IQueryable<Discount> ByCourses(IQueryable<Discount> discounts, 
            string courseTC)
        {
            if (courseTC != null)
                discounts =
                    from discount in discounts
                    let discs = discount.DiscountGraduateCourses
                        .Where(dgc => !dgc.ForStudent)
                    where
                        discs.Count() == 0
                        || discs.All(dgc => dgc.Course_TC == courseTC)
                    select discount;
            else
                discounts = discounts.Where(
                    d => d.DiscountGraduateCourses
                        .Where(dgc => !dgc.ForStudent).Count() == 0);
            return discounts;
        }

        private IQueryable<Discount> ByAuthorization(IQueryable<Discount> discounts, string authorizationTypeTC)
        {
            if (authorizationTypeTC != null)
                discounts =
                    from discount in discounts
                    where
                        (discount.DiscountAuthorizationTypes.Count() == 0
						&& !discount.ExcludeAuthorizationType)
                        ||
                        (
							discount.ExcludeAuthorizationType
                            &&
							discount.DiscountAuthorizationTypes.Count() > 0
							&&
                            discount.DiscountAuthorizationTypes
                               .All(dat => dat.AuthorizationType_TC != authorizationTypeTC)
                        )
                        ||
                        (
                            !discount.ExcludeAuthorizationType
                            &&
                            discount.DiscountAuthorizationTypes
                               .Any(dat => dat.AuthorizationType_TC == authorizationTypeTC)
                        )
                    select discount;
            else
                discounts = discounts.Where(d => d.DiscountAuthorizationTypes.Count() == 0
                    || d.ExcludeAuthorizationType);
            return discounts;
        }

        private IQueryable<Discount> ByPreviousOrderSum(IQueryable<Discount> discounts, decimal? previousOrderSum)
        {
            if (previousOrderSum.HasValue)
                discounts = discounts.Where(d => d.PreviousOrderSum == null
                                                 || d.PreviousOrderSum <= previousOrderSum);
            else
                discounts = discounts.Where(d => d.PreviousOrderSum == null);
            return discounts;
        }

		  private IQueryable<Discount> ByClabCardColorTC(IQueryable<Discount> discounts, 
			  string clabCardColorTC)
        {
            if (clabCardColorTC != null)
                discounts = discounts.Where(d => d.ClabCardColor_TC == null
                                                 || d.ClabCardColor_TC == clabCardColorTC);
            else
                discounts = discounts.Where(d => d.ClabCardColor_TC == null);
            return discounts;
        }
    }
}