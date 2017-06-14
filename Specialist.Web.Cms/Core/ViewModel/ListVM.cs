using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Reflection;
using System.Linq;
using SimpleUtils.Extension;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Controls;
using Specialist.Web.Cms.Core.ViewModel.Interfaces;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Util;
using Specialist.Web.Common.Html;
using SimpleUtils;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.Core.ViewModel
{
    public class ListVM : IExtraControls    
    {
        public class Row
        {
            public object Id { get; set; }

            public object Entity { get; set; }

            public List<object> Values { get; set; }
        }

        public class EntityLinkList : List<EntityLink>
        {
            public override string ToString()
            {
                return this.Select(x => x.ToString()).JoinWith("<br/>"); 
            }
            
        }

        public class EntityLink
        {
            public object Entity { get; set; }

            private UrlHelper _urlHelper;

            public BaseMetaData MetaData { get; set; }

            public EntityLink(object entity, BaseMetaData metaData, UrlHelper urlHelper)
            {
                Entity = entity;
                MetaData = metaData;
                _urlHelper = urlHelper;
            }

            public override string ToString()
            {
                if (Entity == null)
                    return string.Empty;
                return HtmlControls.Anchor(
                    _urlHelper.Action("Edit", Entity.GetType().Name 
                    + Const.Common.ControlPosfix,
                        new { id = LinqToSqlUtils.GetPK(Entity) }),
                        EntityDisplayName
                            .CutLong(MetaData.DisplayProperty().GetValue(Entity).ToString())
                        ).ToString();
            }
        }

        public List<ExtraControl> ExtraControls { get; set; } 

        public Dictionary<string, object> FilterValues { get; set; }

        public OrderColumn OrderColumn { get; set; }

        public List<Row> TableData { get; set; }

        public IPagedList Entities { get; set; }

        public BaseMetaData MetaData { get; set; }

        public List<string> AdditionalColumns { get; set; }

        public List<PropertyMetaData> ExtraFilters { get; set; }

        public IEnumerable<PropertyMetaData> Filters
        {
            get
            {
                return MetaData.GetProperties()
                    .Where(p => p.Control().In(Controls.Select, 
						Controls.Text, Controls.CheckBox, Controls.TextArea) && !p.IsReadOnly()
                        && !p.NotFilter() || p.Info == MetaData.DisplayProperty()
                        ).Union(ExtraFilters);
            }
        }

     /*   public string GetFilterQueryString()
        {
            var nameValueCollection = new NameValueCollection() ;
            foreach (var pair in FilterValues)
            {
                nameValueCollection.Add(pair.Key, pair.Value.ToString());
            }
            return "?" + nameValueCollection.ToQueryString();
        }*/
        public RouteValueDictionary GetFilterRouteValues()
        {
            var values = new RouteValueDictionary();
            foreach (var pair in FilterValues)
            {
                values.Add(pair.Key, pair.Value);
            }
            return values;
        }

        public ListVM(BaseMetaData metaData, IPagedList entities, List<Row> tableData)
        {
            FilterValues = new Dictionary<string, object>();
            ExtraControls = new List<ExtraControl>();
            AdditionalColumns = new List<string>();
            ExtraFilters = new List<PropertyMetaData>();
            MetaData = metaData;
           
            Entities = entities;
            TableData = tableData;
        }


    }
}