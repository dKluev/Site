using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using SimpleUtils.Extension;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Order.Const;
using Specialist.Web.Common.Extension;

namespace Specialist.Entities.Context
{
    public partial class Group
    {
		public bool IsFinished  {
			get {
				if (DateEnd.HasValue) {
					var dateEnd = DateEnd.Value;
					return dateEnd < DateTime.Today || (dateEnd == DateTime.Today &&
							TimeEnd.GetValueOrDefault().TimeOfDay < DateTime.Now.TimeOfDay);
				}
				return false;
			}
		}

	    public bool HasIntraExtra {
		    get {
			    return !ForWebinarOnly && WebinarExists &&
				    GroupCalc.NumOfStudents < MaxNumOfStudents;
		    }
	    }


		public string Title
		{
			get
			{
				if (IsDpCons) {
					return "Аттестация";
				}
				if (IsSem || IsSeminar)
				{
					if (Notes != null && Notes.Contains("*"))
						return Notes.Split('*').First();
					return Notes;

				}
				if(Course != null)
					return Course.WebName;
				return string.Empty;

			}
		}

	    public string GroupNumberTitle { get { return "Группа №{0}".FormatWith(Group_ID); }}


	    public bool AlmostComplete {
		    get { return GroupCalc != null && GroupCalc.NumOfStudents + GroupCalc.NumOfWebinarists >= 10; }
	    }
    	public int? RemainPlaces {
    		get {
    			var remain = MaxNumOfStudents - GroupCalc.NumOfStudents;
				if(IsFirstTeachersGroup)
					remain = remain - GroupCalc.NumOfWebinarists;
/*
    			var daysLeft = (DateBeg.GetValueOrDefault() - DateTime.Today).Days;
    			if (daysLeft <= 7 && remain > 3)
    				return 3;
    			if (daysLeft <= 14 && remain > 5)
    				return 5;
*/
				if(remain >= 4)
					return null;
    			return remain;
    		}
    	}

    	public bool ShowOnNearestGroups {
    		get { return !IsOpenLearning || MaxNumOfStudents - GroupCalc.NumOfStudents <= 8; }
    	}
		public bool IsMicrosoftseminar {
			get { return Notes != null && Notes.Contains("*microsoft"); }
		}

	    public decimal MegaOrGroupId {
		    get { return MegaGroup_ID.GetValueOrDefault(Group_ID); }
	    }

		public string GetUrl() {
			if (!UrlName.IsEmpty())
				return UrlName;
			if (Notes != null && Notes.Contains("*")) {
				var url = Notes.Split('*').ElementAt(1);
				if(url.StartsWith("/"))
					return url;
			}
			return null;

		}

	    public int? NewsId { get {
		    if (IsCareerDay)
			    return null;
		    return StringUtils.ParseInt(StringUtils.GetRegGroupValue(GetUrl(), @"/news/(\d+)"));
	    } }

    	public bool IsCareerDay {
			get { return Title == "День карьеры"; }
    	}

	    public bool IsStandart {
		    get { return !IsCareerDay && !IsImage; }
	    }

	    public bool IsImage {
		    get { return Title.GetOrDefault(x => x.StartsWith("Формирование позитивного имиджа")); }
	    }

	    public string SpecialPostfix {
		    get {
			    return GetSpecialPostfix(LectureType_TC, Complex_TC);
		    }
	    }

	    public const string Spec = "С";
	    public const string NoMoscow = "Н";
	    public const string Viezd = "В";
	    public static string GetSpecialPostfix(string lectureTypeTc, string complexTC) {
		    var inner = new List<string>();
		    if (lectureTypeTc == LectureTypes.Special) {
			    inner.Add(Spec);
		    }
		    if (complexTC == Cities.Complexes.NoMoscow) {
			    inner.Add(NoMoscow);
		    }
		    if (complexTC == Cities.Complexes.Viezd) {
			    inner.Add(Viezd);
		    }
		    if (inner.Any()) {
			    return " ({0})".FormatWith(inner.JoinWith(","));
		    }
		    return "";
	    }


	    public bool IsNotVisible
        {
            get
            {
                return Groups.IsNotVisible(Group_ID)
 					|| Course_TC == CourseTC.Deleted
					|| Course_TC == CourseTC.Dubli 
					|| Course_TC == CourseTC.Unlimit;
            }
        }

    	private bool? _isWebinarOnly = null;

    	public bool IsWebinarOnly { get {
			if (GroupCalc == null) {
				_isWebinarOnly = false;
			}
    		if(_isWebinarOnly == null) {
    			_isWebinarOnly = MaxNumOfStudents <= GroupCalc.NumOfStudents && WebinarExists; 
    		}
    		return _isWebinarOnly.Value;
    	}set { _isWebinarOnly = value; } }

        public bool IsOpenLearning
        {
            get
            {
                return MegaGroup_ID.HasValue;
            }
        }

        public string Description
        {
            get
            {
                return DateBeg.NotNullString("dd.MM.yyyy") + " " 
                    + Complex.GetOrDefault(x => x.Name) + " "
                    + " " + DayShift.GetOrDefault(x => x.Name);

            }
        }

        public string DateInterval
        {
            get
            {
                if(!DateBeg.HasValue || !DateEnd.HasValue)
                    return null;
                return DateBeg.Value.ToShortDateString() + " — " + 
                    DateEnd.Value.ToShortDateString();
            }
        }

	    public static string DateIntervalShort(DateTime? begin, DateTime? end) {
            if(!begin.HasValue || !end.HasValue)
                return null;
            return begin.Value.OnlyDM() + " - " + 
                end.Value.OnlyDM();
		    
	    }

        public string TimeInterval {
            get {
                if (!TimeBeg.HasValue || !TimeEnd.HasValue)
                    return null;
                return TimeBeg.Value.ToShortTimeString() + " — " +
                    TimeEnd.Value.ToShortTimeString();
            }
        }

        public bool IsExam
        {
            get
            {
                return CourseTC.Exams.Contains(Course_TC);
            }
        }

        public bool IsLightBlue
        {
            get
            {
                return Color_TC == Colors.LightBlue;
            }
        }
        public bool IsDpCons
        {
            get
            {
                return Course_TC == CourseTC.DpCons;
            }
        }

        public bool IsCert
        {
            get
            {
                return Course_TC == CourseTC.Srt;
            }
        }

        public bool IsCross(Group group)
        {
            var opposite = DayShifts.GetOpposite(DayShift_TC);
            if(opposite.Contains(group.DayShift_TC))
                return false;
            return group.DateBeg > DateEnd || group.DateEnd < DateBeg;
        }

		public bool IsProbWeb {get { return Course_TC == CourseTC.ProbWeb; }}

	    public bool IsSem {
		    get { return Course_TC == CourseTC.Seminar; }
	    }

	    public bool IsSeminar { get {
            if(Course == null )
                return false;
            return Course.CourseCategories.Any(c => c.Category_TC == Categories.Seminars);
        } }

        private EntitySet<GroupFile> _GroupFiles = new EntitySet<GroupFile>();
        [Association(Storage = "_GroupFiles", ThisKey = "Group_ID"
           , OtherKey = "Group_ID")]
        public EntitySet<GroupFile> GroupFiles
        {
            get
            {
                return this._GroupFiles;
            }
            set
            {
                this._GroupFiles.Assign(value);
            }
        }

	    public bool HasVimeo {
		    get {
			    return StringUtils.IsVimeoUrl(WebinarRecordURL); 
			}
	    }
	    public string VimeoAlbumId {
		    get
		    {
//			    return "4304503";
			    return StringUtils.GetVimeoAlbumId(WebinarRecordURL);
		    }
	    }

        public bool ShowTeacher
        {
            get { return Teacher != null && Teacher.SiteVisible; }
        }

	    public Group ProbOz { get; set; }
    }
}


