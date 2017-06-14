<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Banner>" %>
<%= Htmls2.Menu2(Model.Name) %>
<div style="text-align:center"><%= H.Anchor(Model.TargetUrl, Images.Banner(Model.Image, Model.Name).ToString())
   	.Title(Model.Name)%>
	</div>
	<p>
	<strong><%= H.Anchor(Model.TargetUrl,  "Подробнее")
			        	.Class("link").Style("color:#1458ae; font-size:12px;") %></strong>
	</p>