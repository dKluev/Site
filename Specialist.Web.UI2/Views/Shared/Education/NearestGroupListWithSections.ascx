<%@ Control Language="C#" 
Inherits="System.Web.Mvc.ViewUserControl<List<Tuple<Section,NearestGroupsVM>>>" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>


<% foreach (var tuple in Model) { %>
<h3> <%= tuple.Item1.Name %>  </h3>
<% Html.RenderPartial(Views.Shared.Education.NearestGroupList, tuple.Item2); %>

<% } %>