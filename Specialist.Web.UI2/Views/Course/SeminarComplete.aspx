<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Catalog.ViewModel.SeminarCompleteVM>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    Ваша заявка на участие в семинаре/консультации <%= Model.GroupSeminar.Group.Title %>
     принята. В ближайшее время на ваш электронный адрес будет направлено письмо с подтверждением регистрации на семинар. По возникающим вопросам обращайтесь - 
    <%= HtmlControls.MailTo("info@specialist.ru")%> <br />
    Будем рады видеть Вас на нашем мероприятии!


   
</asp:Content>
