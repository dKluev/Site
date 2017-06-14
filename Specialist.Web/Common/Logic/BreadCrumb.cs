using System;
using System.Collections.Generic;
using SimpleUtils;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Web.Common.Logic
{
    public class BreadCrumb
    {
        public string Link { get; set; }

        public Type ModelType { get; set; }

        public string SimplePageSysName { get; set; }

        public BreadCrumb(string simplePageSysName)
        {
            SimplePageSysName = simplePageSysName;
            Children = new List<BreadCrumb>();
        }

        public BreadCrumb(Type modelType) : this(modelType, string.Empty)
        {
        }

        public BreadCrumb(Type modelType, string link)
        {
            Link = link;
            ModelType = modelType;
            Children = new List<BreadCrumb>();

        }
        public BreadCrumb Add(params BreadCrumb[] breadCrumb)
        {
            Children.AddRange(breadCrumb);
            return this;
        }
        public List<BreadCrumb> Children { get; set; }

        public List<BreadCrumb> GetPath(Type modelType)
        {
            if(modelType == ModelType)
                return null;
            return GetPath(x => x.ModelType == modelType, this);
        }

        public List<BreadCrumb> GetPath(string simplePageSysName)
        {
            return GetPath(x => x.SimplePageSysName == simplePageSysName, this);
        }

        public List<BreadCrumb> GetPath(Func<BreadCrumb, bool> predicate, BreadCrumb parent)
        {
            foreach (var breadCrumb in parent.Children)
            {
             
                if (predicate(breadCrumb))
                    return new List<BreadCrumb>{parent};
                var result = GetPath(predicate, breadCrumb);
                if (result != null)
                    return result.AddFluent(parent);
            }
            return null;
        }

        public override string ToString()
        {
            return Link;
        }
    }
}