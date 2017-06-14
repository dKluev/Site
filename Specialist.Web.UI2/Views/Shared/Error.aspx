<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<ErrorVM>" %>
<%@ Import Namespace="Microsoft.Security.Application" %>
<%@ Import Namespace="Specialist.Web.Common.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="MvcContrib.UI.Pager"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Page"%>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	    <% var path = Sanitizer.GetSafeHtmlFragment(Model.AspxErrorPath); %>
    <% if(Model.IsNotFound){ %>
      <h3>
            К сожалению, страница <%= HtmlControls.Anchor(path) %>, не существует.
      </h3>
        <%= Htmls.TextBlock(SimplePages.NotFound) %>
        <%= Htmls.Mapster %>

    <% }else{ %>
    <h3>
        К сожалению, на странице 
        <%= HtmlControls.Anchor(path) %> что-то пошло не так.
    </h3>
    
    <h3>
        Попробуйте найти информацию, начав с <a href="/">главной страницы</a></h3>
    <div class="attention">
        <p>
            <strong>Запись на курсы:</strong> Госпитальный переулок, д 4/6 ( с 10 до 19 по будням,
            с 11 до 14 по субботам)</p>
        <p>
            <strong>Телефон:</strong> 232-32-16</p>
        <p>
            <strong>URL:</strong> <a href="http://www.specialist.ru/">specialist.ru</a>
        </p>
        <p>
            <strong>E-mail:</strong> <a href="mailto:info@specialist.ru">info@specialist.ru</a>
        </p>
    </div>
    <p>
        По вопросам, связанным с функционированием сайта, обращайтесь, пожалуйста, к 
        <%= Html.ActionLink<PageController>(c => c.SendForWebMaster(string.Empty),
                        "веб-мастеру")%>.</p>


    <% } %>
 



</asp:Content>
