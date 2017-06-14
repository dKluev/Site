<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<SimpleUtils.Common.Tuple <Specialist.Entities.Catalog.Links.CourseLink, List<Specialist.Entities.Context.Group>>>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
			<table width="100%" cellspacing="1" cellpadding="4" align="center" class="table1">
				<tr>
					<td class="dwl3">
						Курс
					</td>
					<td class="dwl3" width="150" align="center">
						Ближайшая дата
					</td>
				</tr>
				<% foreach(var course in Model){ %>
				<tr>
					<td class="dwl2">
						<%= Html.CourseLinkAnchor(course.V1.UrlName, course.V1.GetName()).AbsoluteHref() %>
					</td>
					<td nowrap class="dwl2" align="center">
				<% foreach(var group in course.V2){ %>
					<%= group.DateBeg.DefaultString() %>
					<%= group.Discount.IfNotNull(x => " Скидка " + x + "%") %> <br />
				<% } %>

					</td>
				</tr>
				<% } %>
</table>