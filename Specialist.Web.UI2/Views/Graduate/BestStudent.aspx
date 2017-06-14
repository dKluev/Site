<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Common.CommonVM<SimpleUtils.Common.Tuple<SimplePage, string,string>>>" %>
<%@ Import Namespace="Specialist.Services.Common.Utils" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% if(Model.Data.V1.Description != null){ %>
<% var userName = Model.Data.V2.Split(' ').Skip(1).JoinWith(" "); %>
<%= TemplateEngine.GetText(Model.Data.V1.Description, new {userName}) %>
<% } %>

<% if(Model.Data.V3 == UserImages.BestGraduate){ %>
<p>
����������� ��� � ���������� ��������� ������ ������� ���������!  ���� ���������� � ����������� �����������������, ��������� ������������, ��������� ����� ������ � ������� ����������� ������������ ��������! �� ��������, ��� ����� ����������, ��� �� � ��������� ������ ������!
</p>
<p>
�� ������ ����������� ������� ���������� �������� ����������! 
</p>
<%= Url.Link<GraduateController>(c => c.BestAvatar(), "������� ������") %>
<% } %>
<% if(Model.Data.V3 == UserImages.RealSpecialist){ %>
<%= Url.Link<GraduateController>(c => c.RealAvatar(), "������� ������") %>
<% } %>
<br/>
<%= Images.Root("/User/{1}/Certificate/{0}.png".FormatWith(Model.Data.V2, Model.Data.V3)).Size(600,null) %>
<br />
<br />
<%= Html.ActionLinkImage<GraduateController>(c => c.DownloadGraduateCertificate(Model.Data.V3, Model.Data.V2), 
    Urls.Button("download")) %>

</asp:Content>
