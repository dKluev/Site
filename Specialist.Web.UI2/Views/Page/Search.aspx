<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<SearchVM>" %>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="MvcContrib.UI.Pager"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Page"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <gcse:searchbox queryParameterName="text" newWindow="false">
      <b style="color: red;">Для работы поиска необходимо разрешить использование javascript</b> 
   </gcse:searchbox>
 <% Html.RenderAction<CenterController>(c => c.ActionsBlock()); %>
	<div class="attention2">
		<p>
		<strong>Не нашли нужный Вам курс?</strong>
		 <%= Html.ActionLink<PageController>(c => c.CourseIdea(), "Напишите, и мы его сделаем!") %>
			
		</p>
	</div>

<script>
    (function () {
        var cx = '000858823624175825084:WMX2107877080';
        var gcse = document.createElement('script');
        gcse.type = 'text/javascript';
        gcse.async = true;
        gcse.src = (document.location.protocol == 'https:' ? 'https:' : 'http:') +
            '//www.google.com/cse/cse.js?cx=' + cx;
        var s = document.getElementsByTagName('script')[0];
        s.parentNode.insertBefore(gcse, s);
    })();
</script>
    <gcse:searchresults></gcse:searchresults>

	<% Html.RenderPartial(PartialViewNames.ContactsBlock, new ContactBlockVM()); %>

<%--
    <% using(Html.BeginForm<PageController>(c => c.Search(null, null), FormMethod.Post,
           new {@class="search2"})){ %>
        <%= Html.TextBoxFor(x => x.Text, new { @class = "search_text" })%>
        <input type="submit" value="Найти"/>
		<% if(!Model.Suggestion.IsEmpty()){ %>
			<br/><span style="color:brown;font-size: 14px;">Быть может, вы искали:</span> <b><%= Url.Link<PageController>(c => 
				c.Search(Model.Suggestion,null),Model.Suggestion) %></b>
			<br/>
	    <% } %>
    <% } %>

    <% if(Model.ResponseData != null && Model.ResponseData.Results.Any()){ %>
    <% foreach (var result in Model.ResponseData.Results) { %>
    <h3 class="searh_h3">
        <%= HtmlControls.Anchor(Server.UrlDecode(result.Url), result.Title) %></h3>
    <p class="searh_text">
        <%= result.Content %>
      
    </p>
    <% } %>
    
     <%= Html.GetNumericPager(null, Model.TotalRecords , SearchVM.PageSize,
           Model.PageIndex - 1 ) %>
    <% }else{ %>
	<p>
	<strong>
	К сожалению, по вашему запросу ничего не найдено. Попробуйте изменить ваш запрос или воспользуйтесь помощью нашего менеджера.

	</strong>
	
	</p>
	

	<% } %>
	
	

--%>

</asp:Content>
