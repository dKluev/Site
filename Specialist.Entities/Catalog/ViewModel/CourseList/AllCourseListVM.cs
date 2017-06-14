using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Entities.ViewModel
{
    public class AllCourseListVM
    {
        public class Entity
        {
            public object PK { get; set; }

            public string TypeName { get; set; }

        	public string Name { get; set; }

            public Type Type { get; set; }

            public Entity(object entity)
            {
                Type = entity.GetType();
				if(LinqToSqlUtils.GetPKPropertyInfo(Type) != null)
	                PK = LinqToSqlUtils.GetPK(entity);
                TypeName = Type.Name;
				if(entity is IEntityCommonInfo)
					Name = entity.As<IEntityCommonInfo>().Name;

            }

        }


    	public string EntityName { get; set; }
		public string EntityUrl { get; set; }
    	public Type EntityType { get; set; }

        public List<Course> MainList { get; set; }


        public CommonCourseListVM Common { get; set; }

	    private string trackTC = string.Empty;

	    public string GetTrackTC() {
		    if (trackTC == string.Empty) {
			    trackTC = Common.Items.FirstOrDefault(x => x.Course.IsTrackBool).GetOrDefault(x => x.Course.Course_TC);
		    }
		    return trackTC;
	    }

	    public Dictionary<string, int> TrackCounts { get; set; }
	    public Dictionary<string, Dictionary<string,Tuple<decimal,decimal>>> TrackLastCourseDiscounts { get; set; }

	    public decimal GetTrackLastCourseSave(string type = PriceTypes.Main, bool lastCourse = true) {
		    return lastCourse ? GetLastCourseInfo(GetTrackTC(), type).Item1 : 0;
	    }
	    public decimal GetLastCourseDiscount(string trackTC, string type = PriceTypes.Main) {
		    return GetLastCourseInfo(trackTC, type).Item1;
	    }
	    public Tuple<decimal, decimal> GetLastCourseInfo(string trackTC, string type = PriceTypes.Main) {
		    var discounts = TrackLastCourseDiscounts.GetValueOrDefault(trackTC);
		    if (discounts == null) {
			    return Tuple.Create(decimal.Zero,decimal.Zero);
		    }
		    var discount = discounts[type];
		    if (discount.Item1 > 0 || type == PriceTypes.Main) {
			    return discount;
		    }
		    return discounts[PriceTypes.Main];
	    }
	    public bool IsTrack { get; set; }
	    public bool IsIntraExtra { get; set; }
		public bool HideIntraGroup { get; set; }
		public bool IsOpenClasses { get; set; }
	    public HashSet<string> CourseWithUnlimit { get; set; }
	    public bool IsDiplomPage { get; set; }

	    public bool ShowUnlimited {
		    get { return IsDiplomPage || !(IsTrack || IsIntraExtra); }
	    }

	    public bool IsTrainingProgramsPage { get; set; }
	    public bool IsTrackDiplom { get; set; }
    }
}