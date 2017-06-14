<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Bootstrap.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Pages.BaseVM>" %>
<%@ Import Namespace="Specialist.Web" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SpecialistTest.Web.Common" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
     <%= ViewData.ContainsKey(Htmls.AdditionalStyle) 
        ? Htmls.CssLink("Views/" + ViewData[Htmls.AdditionalStyle]) : null %>
<% var title = Model.Title ?? new TitleCreator().Get(Model.Parts.FirstOrDefault(x => x.Model != null).GetOrDefault(x => x.Model)); %> 
	<% if(!title.IsEmpty()) %> <title><%= title %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="main" runat="server">
    
        <div class="navbar navbar-inverse navbar-static-top">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
            <%= Url.Lms().Details("СП").Class("navbar-brand").Style("font-size:26px;font-weight:bold;") %>
            <%= Url.Lms().Details(CommonSiteHelper.GetUser().ShortFullName).Class("navbar-brand") %>
        </div>
        <div class="collapse navbar-collapse">
          <ul class="nav navbar-nav">
            <li><%= Url.Lms().Groups("Группы") %></li>
            <li><%= Url.Lms().TimeSheet(null,"Табель") %></li>
            <li><%= Url.Lms().Lecture(null, "Занятие") %></li>
            <li><%= Url.Lms().GroupStudents(null, "Раскидовка") %></li>
            <li><%= Url.Lms().Courses("Курсы") %></li>
            <li><%= H.Anchor("/", "Сайт") %></li>
            <li><%= Url.Link<AccountController>(c => c.LogOff(null),
                "Выход".FormatWith(CommonSiteHelper.GetUser().ShortFullName) ) %></li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
    </div>
    <div class="container">
<% var title = Model.Title ?? new TitleCreator().Get(Model.Parts.FirstOrDefault(x => x.Model != null).GetOrDefault(x => x.Model)); %> 
	<% if(!title.IsEmpty()){ %> 
        <h1><%= title %></h1> 
    <% } %>
        <%= BootHtmls.ShowMessage() %>
<% Html.RenderPartial(PartialViewNames.BaseViewContent);%>
        <div style="height: 200px;"></div>
        </div>
    <script>
        $(function () {
            $('ul.navbar-nav a[href="' + document.location.pathname + '"]').parent().addClass('nav-active');
        })
    </script>

</asp:Content>
