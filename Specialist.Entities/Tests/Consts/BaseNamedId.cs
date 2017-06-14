using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using SimpleUtils.Common.Enum;
using Specialist.Entities.Tests.Consts;

namespace Specialist.Entities.Tests.Consts {
	[ReadOnly(true)]
	public class BaseNamedId<T> where T : BaseNamedId<T>, new() {
		
		public BaseNamedId<T> Init(string name, int id) {
			Name = name;
			Id = id;
			return this;
		}

		public string Name { get; set; }
		 [Column(IsPrimaryKey = true)]
		public int Id { get; set; }

		public static string GetName(int id) {
			return NamedIdCache<T>.GetName(id);
		}

		public static List<T> List {
			get {
				return NamedIdCache<T>.List;
			}
		}
	}

	public static class NamedIdCache<T> where T : BaseNamedId<T>, new() {
		public static readonly Dictionary<int,T> Dict;
		public static readonly List<T> List;

		public static string GetName(int id) {
			return Dict[id].Name;
		}

		static NamedIdCache() {
			List =
				typeof (T).GetFields().Select(x => new {
					field = x,
					attribute =
						x.GetCustomAttributes(typeof (EnumDisplayNameAttribute), false)
							.Cast<EnumDisplayNameAttribute>().FirstOrDefault()
				}).Where(x => x.attribute != null)
					.Select(x => (new T()).Init(x.attribute.DisplayName,
						Convert.ToInt32(x.field.GetValue(null)))).Cast<T>().ToList();
			Dict = List.ToDictionary(x => x.Id, x => x);

		}

		public static List<T> GetAll() {
			return List;
		}
	}
	
}