using System;
using System.Collections.Generic;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Reflection.Extensions;
using Specialist.Entities.Context;
using System.Linq;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Entities.Context
{
    public partial class SiteObject {
    	private static List<Type> EntityTypes = new List<Type> {
    		typeof (Course),
    		typeof (Exam),
    		typeof (Certification),
    		typeof (Section),
    		typeof (Vendor),
    		typeof (Profession),
    		typeof (Product),
    		typeof (SiteTerm),
    	};

    	public static Dictionary<Type, string> TypeTableNames =
    		EntityTypes.ToDictionary(x => x, LinqToSqlUtils.GetTableName);

    	public static Dictionary<string, Type> TableNameTypes =
    		EntityTypes.ToDictionary(LinqToSqlUtils.GetTableName, x=>x);

		static readonly Dictionary<string,Type> Types = EntityTypes.ToDictionary(x => x.Name.ToLower(), x => x);
		public static Type GetType(string typeName) {
			return Types.GetValueOrDefault(typeName.ToLowerInvariant());
		}

		public static object GetEntity(string typeName, object pk) {
			var type = SiteObject.GetType(typeName);
			if (type == null) return null;

			pk = LinqToSqlUtils.CorrectPKType(pk, type);
			var entity = type.Create();
			LinqToSqlUtils.SetPK(entity, pk);
			return entity;
		}

        public SiteObjectType ObjectType
        {
            get
            {
                var objectType = 
                    SiteObjectType.AllBySysName.GetValueOrDefault(Type);
                if (objectType == null)
                    objectType = new SiteObjectType(Type, typeof(object));
                return objectType;
            }
        }

        public List<SiteObject> GetIsActiveObject()
        {
            return RelationObjectRelations.Select(sor => sor.Object)
                .Where(so => so != null && so.IsActive).ToList();
        }

        public object Entity { get; set; }
    }
}