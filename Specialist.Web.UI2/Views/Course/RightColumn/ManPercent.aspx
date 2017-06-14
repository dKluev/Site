<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/RightColumn/RightBlock.Master"
    Inherits="ViewPage<CourseVM>" %>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>

<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">


   <%-- <p>
        По данным нашего Центра слушателями курса являются:
        <br />
        <% var manPercent = Model.ManPercent == 100 ? 95 : Model.ManPercent; %>
        <%= HtmlControls.Image("http://chart.apis.google.com/chart?" +
            "cht=p3&chd=t:" + manPercent + "," + (100 - manPercent)
							+ "&chco=599AF1|F8BB5A&chs=200x100&chdl=Мужчины|Женщины&chdlp=b&chf=bg,s,E4EEF8")%>

    </p>--%>

</asp:Content>
