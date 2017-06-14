<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ExpressOrderVM>" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>


<% if (!DateUtils.IsWorkTime())  return; %>
<%= Htmls2.Menu2("Обратный звонок")%>

<div class="block_chamfered_in">

<% Html.RenderPartial(PartialViewNames.ExpressOrderForm);%>

</div>

