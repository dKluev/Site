<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Tests.ViewModels.UserTestsVM>" %>
<%@ Import Namespace="Specialist.Entities.Tests.Consts" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
    
<h4 style="margin: 5px 0"><%= Url.TestCertificates() %></h4>

<% if(Model.List.Any()){ %> 
<table class="defaultTable">
    <tr>
	<th>Тест</th>
	<th>Количество попыток</th>
	<th>Лучший результат</th>
	<th>Дата</th>
	</tr>
<% foreach(var userTest in Model.List){ %>
    <tr>
        <td> <%= Url.UserTestLink(userTest) %></td>
		<td> <%= Model.TestTryCounts[userTest.TestId] %></td>
        <td> <%= NamedIdCache<UserTestStatus>.GetName(userTest.Status) %>
		<% if (!userTest.IsPass) { %>
			<br/> <%= Url.TestLink(userTest.Test, "Пересдать") %>
		<% } %>
		 </td>
		<td> <%= userTest.RunDate %></td>
    </tr>
<% } %>
</table>
<%= Html.GetNumericPagerPretty(Model.List) %>
<%}else{ %>
У вас еще нет ни одного сданного теста. Вы можете сдать любой тест, выбрав его из <%= Url.Tests("каталога тестов") %> 
<%} %>