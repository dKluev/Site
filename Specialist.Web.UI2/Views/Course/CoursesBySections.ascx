<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Specialist.Entities.Utils.Grouping<Section,Course>>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<%= 
H.l( 
				   Model.Select(x => H.l(H.h3[x.Key.Name], 
				   Htmls.DefaultList(x.Select(y => Html.CourseLink(y))))))
	%>