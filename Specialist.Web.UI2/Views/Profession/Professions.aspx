<%@  Page Title="" Language="C#" 
    MasterPageFile="~/Views/Shared/RightColumn/RightBlock.Master"
    Inherits="ViewPage<List<Profession>>" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">

<p><%= CourseVM.WithCoursePrefix(ViewBag.CourseName ?? string.Empty) %> пользуется огромной популярностью среди специалистов, а также тех, кто только выбрал для себя такие востребованные и высокооплачиваемые профессии:</p>

<%= Html.Site().Professions(Model) %>

</asp:Content>