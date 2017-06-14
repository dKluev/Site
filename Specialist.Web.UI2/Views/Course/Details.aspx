<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CourseVM>" %>

<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Services.Common.Utils" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Web.Common.Logic" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
<script type="application/ld+json">
{
  "@context": "http://schema.org",
  "@type": "Course",
  "name": "<%= Model.Course.WebName %>",
  "description": "<%= Model.Course.AnnounceDescription %>",
  "provider": {
    "@type": "Organization",
    "name": "Центр «Специалист» при МГТУ им. Н.Э.Баумана",
    "sameAs": "http://www.specialist.ru/"
  }
}
</script>
    
    

	 <% if (Model.Course.IsNew || Model.Course.IsHit){ %> 
	<div style="float:left;margin-right: 1em;clear: right;">
		 <%= Images.Main("{0}-course.png"
			.FormatWith(Model.Course.IsNew ? "new" : "hit")) %>
	</div>
	<% } %>
	
	<%= Htmls.Title(Model.Course.GetName()) %>
	<% Html.RenderPartial(PartialViewNames.CourseDescription, Model); %>
    <% Html.RenderAction<PageController>(c => c.Banner()); %>
	<a name="trainers"></a>
	<% Html.RenderPartial(PartialViewNames.CourseTrainers, Model); %>
	<a name="prerequisites"></a>
	<% Html.RenderPartial(PartialViewNames.CoursePrerequisites, Model); %>
	
	<% Html.RenderPartial(Views.Course.RightColumn.RelevantCourses, Model.NextCourses); %>

<% if (Model.Tests.Any()) { %>
	<h2 class="h2_block">	Тестирование по курсу</h2>
	<%= H.Ul(Model.Tests.Select(x => Url.TestLink(x))) %>
<% } %>


<% if (Model.HasCertExams) { %>
	<a name="exams"></a>
	<h2 class="h2_block">Сертификации и экзамены</h2>
	<% Html.RenderPartial(ViewNames.CourseCertExams, Tuple.Create(Model.Certifications, Model.Exams, false)); %>
	<% } %>

	<a name="contents"></a>
	<% Html.RenderPartial(PartialViewNames.CourseContents, Model); %>
    <%= Model.Is3dPrint ? Htmls.HtmlBlock(HtmlBlocks.Print3d) : null %>

	<a name="groups"></a>
	<% Html.RenderPartial(PartialViewNames.CourseGroups, Model); %>
	<a name="prices"></a>
	<% Html.RenderPartial(PartialViewNames.CoursePrices, Model); %>
    
	
		 <% if (Model.MaxDiscount == CommonConst.GoldFallDiscount){ %> 
		 <%= TemplateEngine.GetText(Htmls.HtmlBlock(HtmlBlocks.GoldFall), 
				new {Url = Url.Action<GroupController>(c => c.WithDiscount(Model.Course.Course_TC))}) %>
		<% } %>



	<a name="documets"></a>
	<% Html.RenderPartial(Views.Shared.Catalog.CourseDocuments, Model); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightColumn" runat="server">
    <% Html.RenderPartial(Views.Course.RightColumn.VisitedCourses, Model.VisitedCourses); %>
	<%= Htmls2.ChamBegin(true) %>
    <% Html.RenderAction<PageController>(c => c.GuideFor(Model.Course)); %>
<%--	<% Html.RenderAction<PageController>(c => c.SideBanner()); %>--%>
<% if (Model.HasCertExams) { %>
	<%= Htmls2.Menu2("Сертификации и экзамены")%>
	<% Html.RenderPartial(ViewNames.CourseCertExams, Tuple.Create(Model.Certifications, Model.Exams, true)); %>
<% } %>
	<% Html.RenderAction<PageController>(c => c.StudyTypes()); %>
	<% Html.RenderAction<PageController>(c => c.UserWorksFor(Model.Course)); %>
	<%= Htmls2.BlockEnd() %>
    <% Html.RenderAction<PageController>(c => c.VideoFor(Model.Course)); %>
	<%= Htmls2.ChamBegin(true) %>
	<%= Htmls2.Menu2("Знаете ли Вы, что...") %>
	<% Html.RenderAction<ProfessionController>(
		   c => c.Professions(Model.Course.Course_TC)); %>
	<div id="statisticBlock">
	</div>
	<div id="tagBlock">
	</div>
	<% Html.RenderPartial(PartialViewNames.SuperJobVacancies, Model.Vacancies); %>
	<%= Htmls2.BlockEnd() %>
	<%= Htmls2.ChamBegin(true) %>
	<% Html.RenderPartial(PartialViewNames.Best); %>
	<%= Htmls2.BlockEnd() %>
	<script type="text/javascript">
		$(function () {
			lazyContent("#trackBlock",
            '<%=Url.Action<CourseController>(c => c.Tracks(Model.Course.Course_TC,false)) %>');
		    <% if (Htmls.IsSecond || Request.Url.AbsolutePath != "/course/tbuh-i") { %>
    		    lazyContent("#trackBlock2",
                    '<%=Url.Action<CourseController>(c => c.Tracks(Model.Course.Course_TC,true)) %>');
            <% } %>
			lazyContent("#tagBlock",
                '<%=Url.Action<CourseController>(c => c.Tags(Model.Course.Course_TC)) %>');
		});
	</script>
	<!--useful <%= Model.Course.Course_TC %>-->
</asp:Content>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <% var title = HtmlTitleCreator.Get(Model); %>
<%= Htmls.MetaTitle(title) %>
</asp:Content>