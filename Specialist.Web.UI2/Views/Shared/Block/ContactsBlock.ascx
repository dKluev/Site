<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Center.ViewModel.ContactBlockVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<div class="attention" style="width: 550px;">
	<table border="0" cellpadding="10px" align="center">
		<tbody>
			<tr style="text-align: center;">
				<td style="border-right: 1px dashed #666;" valign="top">
					<p>
						<strong>Отдел физических лиц</strong></p>
					<p style="text-align: center;">
						<%= CommonTexts.Phone %></p>
					<p style="text-align: center;">
						<%= HtmlControls.MailTo(CommonConst.EmailForSite) %></p>
				</td>
				<td valign="top">
					<p>
						<strong><a href="/center/corpeducation/personal-manager">Корпоративный отдел</a></strong></p>
					<p>
						(495) 780-48-44</p>
					<p style="text-align: center;">
						<%= HtmlControls.MailTo(CommonConst.EmailForSite) %></p>
				</td>
			</tr>
		</tbody>
	</table>
</div>
