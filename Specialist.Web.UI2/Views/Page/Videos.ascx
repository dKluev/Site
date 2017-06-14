<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Center.ViewModels.VideosVM>" %>
<%@ Import Namespace="Specialist.Web.Root.Center.Views" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>

<h2>Новое видео</h2>
<%= VideoCategoryView.Videos(Url, Model.NewVideos) %>

<% var rootCategories = Model.Categories.OrderBy(x => x.WebSortOrder).ToList(); %>
<div>
<%= H.l(rootCategories.Cut(2).Select(c => H.Div("tab_2column")[c.Select(y =>  {
		var categories = y.VideoCategories.OrderBy(x => x.WebSortOrder).ToList();
		return H.l(H.h2[y.Name],H.Raw(y.Description), categories.Any() 
			?  
			H.Raw(Htmls.DefaultList(categories.Select(x => 
				H.h3[Url.Center().VideoCategory(x.Id,x.Name)].ToString())))
				:null);
	})])) %>
	
</div>
<div class="clear"></div>
<h2>Лучшее видео</h2>
<%= VideoCategoryView.Videos(Url, Model.Videos) %>
<h2><a href="http://www.youtube.com/user/SpecialistTV/videos">Все видео</a></h2>