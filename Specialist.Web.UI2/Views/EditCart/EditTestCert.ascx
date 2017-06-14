<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OrderDetail>" %>
<%@ Import Namespace="Specialist.Entities.Tests.Consts" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<h4>Тест:</h4>
<p> <%= Url.UserTestLink(Model.UserTest) %> </p>

<% using (Html.BeginForm<EditCartController>(c => c.EditTestCert(0))) { %>
    <%= Html.HiddenFor(x => x.OrderDetailID) %>
    <h4>Выберите тип сертификата:</h4>
   <% if(Model.UserTest.Test.IsEset){ %> 
	<%= H.Hidden("type", TestCertType.Papper) %>
    Для ESET доступен только бумажный тип сетификата
   <% }else{ %>
    <%= H.div[TestCertType.List.Select((x,i) => 
	H.l(H.InputRadio("type", x.Id.ToString()).SetChecked(i == 0), x.Name, H.br))] %>
   <% } %>
    <h4>Выберите язык сертификата:</h4>
    
    <%= H.div[TestCertLang.List.Select((x,i) => H.l(H.InputRadio("lang", x.Id.ToString()).SetChecked(i == 0), x.Name, H.br))] %>
    <p><%= Images.Submit("ok") %></p>
<% } %>


 


