using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Interface;
using SimpleUtils;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Const;

namespace Specialist.Entities.Context
{
    public partial class Course : ICourseLink, IEntityCommonInfo
    {

        public List<PriceView> Prices { get; set; }

	    public string GetNameOrEng(bool isEng) {
		    return (isEng ? NameOfficialEn : WebName) ?? WebName;
	    }
	    public bool IsExpensive {
		    get { return AuthorizationTypes.Expensive.Contains(AuthorizationType_TC); }
	    }

	    public bool IsTheoryRoom {
		    get { return this.ClassRoomType_ID == ClassRoomTypes.Theory; }
	    }

	    public bool IsVideo {
		    get { return Course_TC.StartsWith(CourseTC.Video); }
	    }

	    public bool IsSchool {
		    get { return CourseDirectionA_TC == CourseDirections.School; }
	    }

	    public bool IsEnglish {
		    get { return CourseDirectionA_TC == CourseDirections.English; }
	    }

	    public bool IsIntraExtraTrack {
		    get { return IsTrackBool && IntraExtramuralHours > 0; }
	    }

	    public decimal BaseHourCalc {
		    get { return IsIntraExtraTrack ? IntraExtramuralHours + IntraExtramuralHoursOut : BaseHours; }
	    }

	    public bool IsSpecialTrack {
		get { 
			return CourseTC.HalfTracks.ContainsKey(Course_TC);
		}
    	}


    	public string CourseTCOrFirst {
    		get {
    			if(IsTrackBool)
    				return TrackFirstCourseTC;
    			return Course_TC;

    		}
    	}

	    public decimal FullHours {
		    get { return BaseHours + AdditionalHours; }
	    }

    	public string TrackFirstCourseTC { get; set; }

	    public bool IsTrackNotDiplom {
		    get {
			    return IsTrackBool && !IsDiplom;
		    }
	    }
	    public bool IsDiplom {
		    get { return BaseHours >= CommonConst.DiplomHours
					|| (IntraExtramuralHours + IntraExtramuralHoursOut >= CommonConst.DiplomHours); }
	    }

	    public bool HideTrackDiscount {
		    get { return IsDiplom || (IsSchool && AuthorizationType_TC == AuthorizationTypes.OneC); }
	    }
    	public bool IsTrackBool {
    		get { return IsTrack.GetValueOrDefault(); }
    	}

	    public bool HasIcon {
		    get { return IsDiplom || IsHit || IsNew; }
	    }

	    public Tuple<string,string> Icon {
		    get {
			    if (IsTrackBool && IsDiplom) return Tuple.Create("diplom", "Дипломная программа");
			    if (IsHit) return Tuple.Create("hit", "Хит продаж");
			    if (IsNew) return Tuple.Create("new", "Новый курс");
			    return null;
		    }
	    }

    	public bool IsHit {
    		get { return WebPublishSchedule.GetValueOrDefault(); }
    	}
        public PriceView GetPrice(string priceTypeTC)
        {
            if(Prices == null)
                return null;
            return Prices.FirstOrDefault(pv => pv.PriceType_TC == priceTypeTC);
        }

	    public int? TestId {
		    get { 
//				if(Course_TC == "М2274") return 490;
			    return null;
		    }
	    }

	    public bool IsMs {
		    get { return AuthorizationType_TC == AuthorizationTypes.Microsoft; }
	    }


		private EntitySet<CourseFile> _CourseFiles = new EntitySet<CourseFile>();
        [Association(Storage = "_CourseFiles", ThisKey = "Course_TC"
           , OtherKey = "Course_TC")]
        public EntitySet<CourseFile> CourseFiles
        {
            get
            {
                return this._CourseFiles;
            }
            set
            {
                this._CourseFiles.Assign(value);
            }
        }
    }
}