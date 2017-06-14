using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;
using System.Linq;
using SimpleUtils.Common.Extensions;

namespace Specialist.Entities.Context
{
	public class SiteObjectType {
		[Column(IsPrimaryKey = true)]
		public string SysName { get; set; }

		public string Name { get; set; }

		public string ClassName { get; set; }



		public Type Type { get; set; }

		public SiteObjectType(string name, Type type) {
			Name = name;
			Type = type;
			SysName = LinqToSqlUtils.GetTableName(type);
			ClassName = type.Name;
		}

		public static Dictionary<string, SiteObjectType> AllBySysName = null;
		public static Dictionary<string, string> SmallNamesBySysName = null;

		static SiteObjectType() {
			AllBySysName = GetAll().ToDictionary(x => x.SysName, x => x);
			SmallNamesBySysName = GetAll().ToDictionary(x => x.SysName, 
				x => "[{0}]".FormatWith(
					(x.Name.First().ToString() + x.Name.Last()).ToUpper()));
		}

		static List<SiteObjectType> _list = 

			new List<SiteObjectType>
                {
                    new SiteObjectType("Курс", typeof(Course)),
                    new SiteObjectType("Баннер", typeof(Banner)),
                    new SiteObjectType("Отзыв", typeof(Response)),
                    new SiteObjectType("Новость", typeof(News)),
                    new SiteObjectType("Продукт", typeof(Product)),
                    new SiteObjectType("Профессия", typeof(Profession)),
                    new SiteObjectType("Сертификация", typeof(Certification)),
                    new SiteObjectType("Направление", typeof(Section)),
                    new SiteObjectType("Преподаватель", typeof(Employee)),
                    new SiteObjectType("Город", typeof(City)),
                    new SiteObjectType("Комплекс", typeof(Complex)),
                    new SiteObjectType("Бонус", typeof(Discount)),
                    new SiteObjectType("Вендор", typeof(Vendor)),
                    new SiteObjectType("Видео", typeof(Video)),
                    new SiteObjectType("Путеводитель", typeof(Guide)),
                    new SiteObjectType("Технология", typeof(SiteTerm)),
                    new SiteObjectType("Тест", typeof(Test)),
                    new SiteObjectType("Страница", typeof(SimplePage)),


                };

		public static List<SiteObjectType> GetAll() {
			return _list;

		}
    }
}