using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Context
{
    public class NewsType
    {
        public const string Main = "main";

        [Column(IsPrimaryKey = true)]
        public byte NewsTypeID { get; set; }

        public string Name { get; set; }

        public string UrlName { get; set; }

		public bool HideFromTabs { get; set; }

        public const byte Video = 5;

	    public const byte TrainerPublish = 6;
	    public const byte Publish = 3;
	    public static List<byte> Publishes = _.List(Publish, TrainerPublish); 
		public static List<NewsType> All =  
                new List<NewsType>
                {
                    new NewsType{Name = "Новости", NewsTypeID = 1, UrlName = Main},
                    new NewsType{Name = "Пресс-релизы", NewsTypeID = 2,
                        UrlName = "press"},
                    new NewsType{Name = "Публикации", NewsTypeID = Publish,
                        UrlName = "publish"},
                    new NewsType{Name = "События", NewsTypeID = 4,
                        UrlName = "event"},
                    new NewsType{Name = "Видеоновости", NewsTypeID = Video,
                        UrlName = "video"},
                    new NewsType{Name = "Публикации преподавателя", NewsTypeID = TrainerPublish, 
						UrlName = "publish", HideFromTabs = true},
                };

	    public static Dictionary<byte, NewsType> AllById = All.ToDictionary(x => x.NewsTypeID, x => x);

	    public static List<NewsType> GetAll() {
		    return All;
	    }

    }
}