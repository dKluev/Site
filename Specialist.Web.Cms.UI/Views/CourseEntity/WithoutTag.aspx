<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Course>>" %>
<%@ Import Namespace="Specialist.Web.Cms"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core.Controls"%>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Курсы без связей</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% var metaData = MvcApplication.MetaDataProvider.Get(typeof(Course)); %>
    <b>Курсы без связей</b>
    <br />
    <br />
    
    <table class="simple-table">
    <% foreach (var course in Model) { %>
        <% var tagControl = new RelationsLinkControl(); %>
        <tr> <td>
            <%= Html.ActionLink<CourseEntityController>(c => c.Edit(course.Course_TC, null),
                course.Name) %>
        </td> 
        <td>
             <% Html.RenderPartial("Controls/" + tagControl.Name, 
                            new ExtraControlVM(metaData, course, tagControl)); %>
        </td>
        </tr>
    <% } %>
    </table>
</asp:Content>

