<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Center.ViewModel.SuccessStoryVM>" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <% if(Model.CourseLink != null){ %>
            <strong>Курс:</strong>
            <%= Html.CourseLink(Model.CourseLink) %>  <br/>
        <% } %>
        <strong>Автор истории:</strong>
        <% if(Model.SuccessStory.Author != null){ %>
            <%= Model.SuccessStory.Author %>  <br />
        <% } %>
            
        <% if(Model.SuccessStory.Profession != null){ %>
            <strong>Профессия:</strong>
            <%= Html.ProfessionLink(Model.SuccessStory.Profession) %>
        <% } %>
    </p>
    <p>
    <%= Model.SuccessStory.Description %>
    </p>

    <div class="img_history">
        <% Model.Images.ForEach((image, i) => { %> 
            <% if(image.HasFile){ %> 
                <%= Images.UserStoryImage(Model.SuccessStory.SuccessStoryID, i)%>
            <% } %>
        <% }); %>
    </div>
</asp:Content>
