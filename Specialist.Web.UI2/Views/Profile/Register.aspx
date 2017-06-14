<%@ Page Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<RegisterVM>" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Web.Helpers"%>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel"%>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.0/themes/redmond/jquery-ui.css" type="text/css" />
<title><%= Model.Title %></title>
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial(PartialViewNames.RegisterControl, Model); %>
    
    <script>
        $(function () {
/*
            $("#register-form").submit(function () {
                var emptyField = (!$('#User_WorkBranch_ID').val()) || ($('#Year').length > 0 && !$('#Year').val())
                    || ($('#User_Metier_ID').length > 0 && !$('#User_Metier_ID').val());
                if (emptyField) {
                    return confirm("Вы не заполнили все поля. Купон не будет выслан. Продолжить?");
                }
                return true;
            });
*/
            <% if (!Model.IsCompany) { %>
               controls.initSelectCascade("#User_WorkBranch_ID", "#User_Metier_ID",
                <%= JsonConvert.SerializeObject(Model.Metiers) %>, "Не выбрано", true);
            <% } %>
        });
    </script>
</asp:Content>
