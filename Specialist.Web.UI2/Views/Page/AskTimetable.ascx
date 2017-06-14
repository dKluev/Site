<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Common.ViewModel.ExpressOrderVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<div id="ask-timetable" <%= Htmls.DisplayNone() %>>
<div style="width:200px;height:330px" >
<strong>Уточните расписание у менеджера</strong>

  <% Html.RenderPartial(PartialViewNames.ExpressOrderForm, Model); %> 
 
</div>
</div>




