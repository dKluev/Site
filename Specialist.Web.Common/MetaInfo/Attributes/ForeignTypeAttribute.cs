using System;
using System.Reflection;

namespace Specialist.Web.Cms.Core.FluentMetaData.Attributes
{
    public class ForeignTypeAttribute : Attribute
    {
        private Type _foreignType;
        public Type ForeignType
        {
            get
            {
                if (ForeignInfo != null)
                    return ForeignInfo.PropertyType;
                return _foreignType;
            }
            set { _foreignType = value; }
        }

        public PropertyInfo ForeignInfo { get; set; }

        public ForeignTypeAttribute(Type foreignType)
        {
            ForeignType = foreignType;
        }

        public ForeignTypeAttribute(PropertyInfo foreignInfo)
        {
            ForeignInfo = foreignInfo;
        }
    }
}