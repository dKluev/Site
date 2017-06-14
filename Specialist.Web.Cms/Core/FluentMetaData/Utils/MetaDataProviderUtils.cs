using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Providers.Interfaces;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Utils
{
    public class MetaDataProviderUtils
    {
        public static List<BaseMetaData> GetWhereForeign(IMetaDataProvider metaDataProvider, Type entityType)
        {

            var metaDataList =
                from metaData in metaDataProvider.GetAll()
                where metaData.GetProperties().Any(
                          p => p.ForeignType() != null &&
                               p.ForeignType() == entityType) &&
                      metaData.EntityType != entityType
                select metaData;
            return metaDataList.ToList();
        }
    }
}