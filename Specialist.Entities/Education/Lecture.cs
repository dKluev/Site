using SimpleUtils.FluentAttributes.Utils;

namespace Specialist.Entities.Context
{
    public partial class Lecture {

        public string TimeInterval {
            get {
	            return LectureDateBeg.ToShortTimeString() + " - " +
		            LectureDateEnd.ToShortTimeString();
            }
        }

	    public double Hours {
		    get { return ((LectureDateEnd - LectureDateBeg).TotalMinutes - Breaks)/60; }
	    }

    }
}