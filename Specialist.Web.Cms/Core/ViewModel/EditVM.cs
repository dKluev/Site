using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core;
using System.Linq;
using SimpleUtils;
using SimpleUtils.FluentAttributes.Core.Controls;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.Core.ViewModel.Interfaces;

namespace Specialist.Web.Cms.Core.ViewModel
{
    public class EditVM : IExtraControls
    {
        public string PrefixPropertyName { get; set; }

        public int TabFocus { get; set; }

        public SiteObject SiteObject { get; set; }

        public Dictionary<string, object> FilterValues { get; set; }

        public string SiteUrl { get; set; }

        public string HtmlProperties
        {
            get
            {
                return MetaData.GetProperties().Where(p => p.Control() == Controls.Html)
                    .Select(p => PrefixPropertyName + p.Name).JoinWith(",");
            }
        }

        public EditVM(object item, BaseMetaData metaData, string prefixPropertyName) : 
            this(item, metaData)
        {
            PrefixPropertyName = prefixPropertyName + ".";
        }

        public EditVM(object item, BaseMetaData metaData)
        {
            FilterValues = new Dictionary<string, object>();
            ExtraControls = new List<ExtraControl>();
            Entity = item;
            MetaData = metaData;
        }

        public string EntityTypeName {get { return MetaData.EntityType.Name; }}

        public string QueryString { get; set; }

        public object Entity { get; set; }

        public BaseMetaData MetaData { get; set; }

        public List<ExtraControl> ExtraControls { get; set; }
    }
}