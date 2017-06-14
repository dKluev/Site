<%@ Page Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Pages.BaseVM>" %>
<%@ Import Namespace="Specialist.Web" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SpecialistTest.Web.Common" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/themes/redmond/jquery-ui.css"
		type="text/css" />
	<link href="/Scripts/AjaxTable/Css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
     <%= ViewData.ContainsKey(Htmls.AdditionalStyle) 
        ? Htmls.CssLink("Views/" + ViewData[Htmls.AdditionalStyle]) : null %>
<% var title = Model.Title ?? new TitleCreator().Get(Model.Parts.FirstOrDefault(x => x.Model != null).GetOrDefault(x => x.Model)); %> 
	<% if(!title.IsEmpty()) %> <title><%= title %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% var title = Model.Title ?? new TitleCreator().Get(Model.Parts.FirstOrDefault(x => x.Model != null).GetOrDefault(x => x.Model)); %> 
	<% if(!title.IsEmpty()){ %> 
        <%= Htmls.Title(title) %>
    <% } %>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js" type="text/javascript"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/i18n/jquery.ui.datepicker-ru.js" type="text/javascript"></script>
	<% if(MvcApplication.IsDebug){ %>
<script src="/Scripts/viewcommon.js" type="text/javascript"></script>
<script src="/Scripts/json2.js" type="text/javascript"></script>
  <% }else{ %>
<script src="/Scripts/view.all.min.js?v=11" type="text/javascript"></script>
	<% } %>
<% Html.RenderPartial(PartialViewNames.BaseViewContent);%>
</asp:Content>


<asp:Content ContentPlaceHolderID="RightColumn" runat="server">
    <% if(Model.RightSide.Any()){ %>
        <% foreach (var part in Model.RightSide) { %>
            <%= part.Content %>
    	<% } %>
  <% }else{ %>
	<%= Htmls2.ChamBegin(true) %>
        <% Html.RenderPartial(PartialViewNames.MainNews, true);%>
	<%= Htmls2.BlockEnd() %>
	<% } %>
   
</asp:Content>
