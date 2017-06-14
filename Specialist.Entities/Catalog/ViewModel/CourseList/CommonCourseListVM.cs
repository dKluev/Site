namespace Specialist.Entities.ViewModel
{
    public class CommonCourseListVM : CourseListVM<CommonCourseListItemVM>
    {

        protected override bool HasPrice(CommonCourseListItemVM courseListItem)
        {
            return courseListItem.MinFulltimePrice.Item1.HasValue 
				|| courseListItem.DistancePrice.Item1.HasValue 
				|| courseListItem.GetPrice(Const.PriceTypes.IntraExtra).HasValue;
        }
        
    }
}