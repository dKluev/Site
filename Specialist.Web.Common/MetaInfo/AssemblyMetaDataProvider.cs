using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.FluentAttributes.Core.Providers.Interfaces;
using SimpleUtils.Reflection;
using System.Linq;
using SimpleUtils.FluentAttributes.Core.Extensions;
using SimpleUtils.Reflection.Extensions;

namespace SimpleUtils.FluentAttributes.Core.Providers
{
    public class ManyAssemblyMetaDataProvider: IMetaDataProvider
    {
        private Dictionary<Type, BaseMetaData> _cache = new Dictionary<Type, BaseMetaData>();
        public List<Assembly> MetaDataAssemblies {get;set;}

        public static string MetaDataClassPostfix {get;set;}

        public static List<Type> MetaDataTypeList { get; set; }

        static ManyAssemblyMetaDataProvider()
        {
            MetaDataClassPostfix = "MD";
        }

        public ManyAssemblyMetaDataProvider(params Assembly[] metaDataAssembly)
        {
            MetaDataAssemblies = metaDataAssembly.ToList();

            MetaDataTypeList = MetaDataAssemblies.SelectMany(a => a.GetTypes().Where(x => x.Name.EndsWith(MetaDataClassPostfix))).ToList();
            foreach (var metaDataType in MetaDataTypeList)
            {
                var metaData = metaDataType.Create() as BaseMetaData;
                if(metaData == null)
                    continue;
                _cache.Add(metaData.EntityType, metaData);
            }
        }

        private static readonly object _lock = new object();

        public virtual BaseMetaData Get(Type type)
        {
            lock (_lock)
            {
                var metaData = _cache.GetValueOrDefault(type);
                if(metaData == null && !type.IsPrimitiveOrString() && type.IsClass) 
                    return new BaseMetaData{EntityType = type}.TryDisplayByName();
                return metaData;
          /*      var metaType = MetaDataTypeList.FirstOrDefault(t => t.Name == type.Name + MetaDataClassPostfix);
                if(metaType == null)
                    return null;
                return metaType.Create() as BaseMetaData;*/
            }
        }

        public List<BaseMetaData> GetAll()
        {
            lock (_lock)
            {
                return _cache.Select(x => x.Value).ToList();
            }
        }
    }
}