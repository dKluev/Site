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
Поздравляем Вас с получением почетного звания «Лучший Выпускник»!  Ваше стремление к постоянному совершенствованию, повышению квалификации, получению новых знаний и навыков заслуживает глубочайшего уважения! Мы гордимся, что такой специалист, как Вы – выпускник нашего Центра!
</p>
<p>
Вы можете распечатать именной сертификат «Лучшего Выпускника»! 
</p>
<%= Url.Link<GraduateController>(c => c.BestAvatar(), "Создать аватар") %>
<% } %>
<% if(Model.Data.V3 == UserImages.RealSpecialist){ %>
<%= Url.Link<GraduateController>(c => c.RealAvatar(), "Создать аватар") %>
<% } %>
<br/>
<%= Images.Root("/User/{1}/Certificate/{0}.png".FormatWith(Model.Data.V2, Model.Data.V3)).Size(600,null) %>
<br />
<br />
<%= Html.ActionLinkImage<GraduateController>(c => c.DownloadGraduateCertificate(Model.Data.V3, Model.Data.V2), 
    Urls.Button("download")) %>

</asp:Content>
