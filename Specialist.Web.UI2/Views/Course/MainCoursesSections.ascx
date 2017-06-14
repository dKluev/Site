<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MainCoursesVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>


<%=
	Specialist.Web.Common.Utils.CacheUtils.Get(ViewContext.View.GetWebFormViewName(),
	() =>
	Htmls2.Tabs(new[] { "Направления", "Вендоры", "Профессии", "Продукты", "Отрасли и технологии" },
	new []{
   	Htmls.HtmlBlock(HtmlBlocks.MainCourseSections) + Html.Site().ThreeColumns(
                   Model.GetSections().Select(sList =>
					   sList.Select(s => 
					   Grouping.New(Html.SectionAnchor(s.Key)
                       .Class(s.Key.Section_ID == Sections.School ? "school-link" : null).ToString(), s)))).ToString(),
					   Htmls.HtmlBlock(HtmlBlocks.MainCourseVendors) + 
Html.Site().ThreeColumns( 
                   Model.Vendors.Cast<IEntityCommonInfo>()
					   .GroupByFirstLetter(x => x.Name)), 
	
   	Html.Site().ThreeColumns(
                   Model.GetProfessions().Select(sList =>
					   sList.Select(s => 
					   Grouping.New(Html.SectionLink(s.Key), s)))).ToString(),
					   Htmls.HtmlBlock(HtmlBlocks.MainCourseProducts) + 
	 Html.Site().ThreeColumns(
                   Model.Products.Cast<IEntityCommonInfo>()
						   .GroupByFirstLetter(x => x.Name)).ToString(),
	 Html.Site().ThreeColumns(
                   Model.SiteTerms.Cast<IEntityCommonInfo>()
						   .GroupByFirstLetter(x => x.Name)).ToString()
						   
	}, true)) %>

