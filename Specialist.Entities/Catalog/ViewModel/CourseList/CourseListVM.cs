using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using SimpleUtils.Extension;

namespace Specialist.Entities.ViewModel
{
    public abstract class CourseListVM<T> where T: CourseListItem
    {
        public List<T> Items { get; set; }

	    public bool IsTrackPage { get; set; }


	    public bool IsTrainingProgramsPage { get; set; }
		public bool IsDiplomPage { get; set; }

        virtual protected List<string> PriceTypes { get {return new List<string>();} }

        virtual protected bool HasPrice(T courseListItem)
        {
            return courseListItem.Prices.Any(p => PriceTypes.Contains(p.CommonPriceTypeTC));
        }

    	private bool? _hasNewOrHit = null;
    	public bool HasIcons {
    		get {
    			if (IsTrainingProgramsPage || IsDiplomPage || IsTrackPage) return false;
    			if(_hasNewOrHit == null)
    				_hasNewOrHit = this.Items.Any(x => x.Course.HasIcon);
    			return _hasNewOrHit.Value;
    		}
    	}

        private List<PriceType> _priceTypeColumns;
        public List<PriceType> PriceTypeColumns
        {
            get
            {
                return _priceTypeColumns ?? (_priceTypeColumns =
                    Items.SelectMany(i => i.Prices).Select(p => p.PriceType)
                    .Distinct(x => x.CommonPriceTypeTC)
                    .Where(pt => PriceTypes.Contains(pt.CommonPriceTypeTC))
                    .OrderBy(pt => pt.SortOrder).ToList());
            }
        }
      /*  public List<T> GetTracks()
        {
            return Items.
                Where(cli => cli.Course.IsTrack.GetValueOrDefault()).ToList();
        }*/


        public bool HasCourses { get
        {
            return GetFilteredByPriceCourses().Any();
        }}

	    public bool OnlyVideo {
		    get { return GetFilteredByPriceCourses().All(x => x.Course.IsVideo); }
	    }
	    private List<T> _courses;
		public List<T> GetFilteredByPriceCourses() {
	        if (_courses == null) {
		        if (IsTrackPage) {
			        _courses = Items;
		        } else {
					_courses = Items
						.Where(HasPrice)
						.OrderBy(x => x.NearestDate == null)
						.ToList();
				}
			}
			return _courses;
		}
	}
}