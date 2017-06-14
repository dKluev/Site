<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<TrainerCoursesVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table class="defaultTable">
<thead>
    <tr><th>Курс</th></tr>
    </thead>
    <tbody>
<% foreach(var course in Model.CourseHasVideos){ %>
    <tr>
        <td style="text-align: left;"> <%= Html.ActionLink<CourseController>(c => c.Files(course.Item1.CourseTC),
                 course.Item1.Name) %> </td>
        
    </tr>
<% } %>
</tbody>
</table>

</asp:Content>
