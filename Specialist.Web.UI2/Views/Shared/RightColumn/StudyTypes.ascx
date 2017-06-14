<%@ Control Language="C#" 
    Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<SimplePage>>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="SimpleUtils.FluentHtml.Tags" %>
<%= Htmls2.Menu2("Типы обучения")%>
<div class="block_chamfered_in v_type_teaching">

<%= Htmls2.MarkArrow(Model.Select(x => Html.SimplePageLink(x).ToString())) %>

</div>
