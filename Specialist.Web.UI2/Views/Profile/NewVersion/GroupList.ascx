<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Profile.ViewModel.LearningVM.GroupList>" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<% var groups = Model.Groups.Where(g => !g.IsNotVisible).ToList(); %>
<% if(groups.Any()) { %>
    <% var debts = groups.DistinctToDictionary(g => g.Group_ID, g => Model.Model.Debt(g.Group_ID)); %>
    <% var showDebts = debts.Values.Any(x => x.HasValue); %>
	<% var hasTest = Model.ShowInfo && Model.Groups.Any(x => x.Course.TestId.HasValue); %>
        <table class="defaultTable">
             <tr>
                <th> Дата начала </th>
                <th> Дата окончания </th>
                <th> Курс </th>
                <th> <%= Images.Main("add_ingrey.gif") %> </th>
                <th> Тип обучения </th>
			    <% if(showDebts) { %>
                <th> К оплате </th>
			    <% } %>    
                <th> Страница группы </th>
			    <% if(hasTest) { %>
                <th> Тестирование </th>
			    <% } %>    
            </tr>


        <% foreach (var group in groups) { %>
            <tr>
                <td><%= group.DateBeg.DefaultString() %></td>
                <td><%= group.DateEnd.DefaultString() %></td>
                <td><%= Html.CourseLinkByGroup(group) %></td>
                <td>
                     <%= group.DateBeg.HasValue && group.DateEnd.HasValue ? Html.Calendar(group.Group_ID) : null %>  
                </td>
                <td> <%= CourseStudyTypes.GetLinks(group, Model.Model.IsWebinar(group.Group_ID)) %> </td>
			    <% if(showDebts) { %>
                <% var debt = debts[group.Group_ID]; %>
                <td> <%= debt.HasValue ? (debt > 0 ? H.b[debt.MoneyString()].Style("color:red;").ToString() : "Оплачено") : null %> </td>
			    <% } %>    
                <td><%= group.IsLightBlue ? null : Html.GroupLink(group) %></td>
			    <% if(hasTest) { %>
                <td>
				    <% if(group.Course.TestId.HasValue) { %>
	                 <%= Url.Test().CoursePlanned(group.Course_TC, "Тестирование")  %> 
				    <% } %>    
                </td>
			    <% } %>    
            </tr>
        <% } %>
        </table>
    <% }else{ %>
        <% var hotGroupList = SimpleLinks.GroupDiscounts("Выбрать курсы").ToString(); %>
       <%= Model.EmptyListText %> <%= hotGroupList %>
    <% } %>    
