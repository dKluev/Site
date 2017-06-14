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
                    new SiteObjectType("����", typeof(Course)),
                    new SiteObjectType("������", typeof(Banner)),
                    new SiteObjectType("�����", typeof(Response)),
                    new SiteObjectType("�������", typeof(News)),
                    new SiteObjectType("�������", typeof(Product)),
                    new SiteObjectType("���������", typeof(Profession)),
                    new SiteObjectType("������������", typeof(Certification)),
                    new SiteObjectType("�����������", typeof(Section)),
                    new SiteObjectType("�������������", typeof(Employee)),
                    new SiteObjectType("�����", typeof(City)),
                    new SiteObjectType("��������", typeof(Complex)),
                    new SiteObjectType("�����", typeof(Discount)),
                    new SiteObjectType("������", typeof(Vendor)),
                    new SiteObjectType("�����", typeof(Video)),
                    new SiteObjectType("������������", typeof(Guide)),
                    new SiteObjectType("����������", typeof(SiteTerm)),
                    new SiteObjectType("����", typeof(Test)),
                    new SiteObjectType("��������", typeof(SimplePage)),


                };

		public static List<SiteObjectType> GetAll() {
			return _list;

		}
    }
}