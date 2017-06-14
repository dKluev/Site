using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Specialist.Entities.Center
{
    public class ResponseRating
    {
        public const byte ForMain = 2;

    	public const byte Good = 1;

        public const byte Common = 0;

        [Column(IsPrimaryKey = true)]
        public byte ResponseRatingID { get; set; }

        public string Name { get; set; }

		static List<ResponseRating> All = new List<ResponseRating>
                {
                    new ResponseRating{Name = "Обычный", ResponseRatingID = Common},
                    new ResponseRating{Name = "Хороший", ResponseRatingID = Good},
                    new ResponseRating{Name = "На главную", ResponseRatingID = ForMain},
                };

        public static List<ResponseRating> GetAll() {
	        return All;
        }

    }
}