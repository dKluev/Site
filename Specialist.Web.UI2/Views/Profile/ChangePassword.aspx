<%@ Page Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<ChangePasswordVM>" %>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
        <% using (Html.DefaultForm<ProfileController>(c => c.ChangePassword(null))) { %>
                <%=Html.ValidationSummary()%>
            <% Htmls.FormSection(" ", () => {%> 
                <%=Html.ControlFor(x => x.CurrentPassword)%>
                <%=Html.ControlFor(x => x.NewEmail)%>
                <%=Html.ControlFor(x => x.NewPassword)%>
                <%=Html.ControlFor(x => x.ConfirmPassword)%>
        <% }); %> 
        	<%=Htmls.Submit("ok")%>
        <% } %>
</asp:Content>
