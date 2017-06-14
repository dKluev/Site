<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.ViewModel.CourseVM>" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<% var forPrint = ViewData.ContainsKey("ForPrint"); %>
<% var isTest = Model.Course.Course_TC == "�2274"; %>
<% if(forPrint) { %>
<h3>����� ������������� �������� ����������� ��� ���� ��.�.�.�������.</h3>
<strong>��������� ����� <%= Model.Course.GetName() %></strong>
<% } else { %>
<h2 class="h2_block">
	��������� �����
	
	</h2>
<div style="padding-right:5px;padding-bottom:5px;text-align:right;"> 
<%= Images.Main("/Icon/print.gif").Style("vertical-align:bottom;") %> 
<%= Url.Link<CourseController>(c => c.Print(Model.Course.UrlName),"��� ������") %></div>
<%= SiteHtmls.JQuery(@"$('#free-hours-link').
	fancybox({content:$('#free-hours-content').html(), padding:20});") %>
<% } %>
<div class="table_tfoot">
	<table class="table">
		<tr class="thead">
			<th class="table_c_tl">
				&nbsp;
			</th>
			<th rowspan="2" style="width: %;">
				<strong>����</strong>
			</th>
			<th rowspan="2" class="last_td">
				<strong>��.&nbsp;�����</strong>
			</th>
			<th class="table_c_tr">
				&nbsp;
			</th>
		</tr>
		<tr>
			<th class="table_c_bl">
				&nbsp;
			</th>
			<th class="table_c_br">
				&nbsp;
			</th>
		</tr>
		<% foreach (var courseContent in
			Model.CourseContents.OrderBy(cc => cc.ModuleNumber)) { %>
		<tr>
			<td class="table_c_tl">
				&nbsp;
			</td>
			<td class="td_subject">
				<% if (isTest){ %>
					<strong>������ <%= courseContent.ModuleNumber %>. 
					<%= courseContent.ModuleName %></strong>	
				<%= Htmls.ShowOnClick("course-content-" 
				+ courseContent.ModuleNumber,"��������", 
				H.Div("td_hiden")[courseContent.ModuleDescription].ToString()) %>
				<% }else{ %>
					<strong>������ <%= courseContent.ModuleNumber %>. 
					<%= courseContent.ModuleName %></strong>	
					<% if(!courseContent.ModuleDescription.IsEmpty()){ %>			
					<div class="td_hiden" id="moduleContent<%= courseContent.ModuleNumber %>">
						<%= courseContent.ModuleDescription %>
					</div>
					<% } %>
				<% } %>
			</td>
			<td class="last_td">
				<% if (courseContent.Hours > 0){ %>
					<strong><%= courseContent.Hours.ToString("0.#", CultureInfo.InvariantCulture) %></strong>
				<% } %>
			</td>
			<td class="table_c_tr">
				&nbsp;
			</td>
		</tr>
		<% } %>

        <% if(Model.Course.IsVideo){ %>
        
		<tr>
			<td class="table_c_tl">
				&nbsp;
			</td>
			<td>
			</td>
			<td>
				<strong>
                    <%= Model.Course.BaseHours.ToIntString() %>
				</strong>
			</td>
			<td class="table_c_tr">
				&nbsp;
			</td>
		</tr>

		<% }else{ %>
		<tr class="tfoot">
			<td class="table_c_tl">
				&nbsp;
			</td>
			<td class="td_subject">
				���������� �������� � ������ � ��������������
			</td>
			<td class="last_td">
				<strong>
					<%= Model.Course.BaseHours.ToIntString() %>
				<% if(Model.Course.AdditionalHours > 0){ %>
				<span class="discount_color" style="color:#AC3D4B;">
					<%=  Model.Course.IsTheoryRoom ? "" : ("+" + (int)Model.Course.AdditionalHours + "<br/>") %>
                    <%= H.Anchor("#", Model.Course.IsTheoryRoom ? 
                    ("+" + (int)Model.Course.AdditionalHours) : "���������")
                    .Class("not-link red").Id("free-hours-link") %>

				</span>
				<% } %>	
				</strong>
			</td>
			<td class="table_c_tr">
				&nbsp;
			</td>
		</tr>
        <% var minHours = 16; %>
        <% var labHours = minHours - (Model.Course.BaseHours + Model.Course.AdditionalHours) ; %>
       <% if (labHours > 0) { %> 
		<tr class="tfoot">
			<td> &nbsp; </td>
			<td class="td_subject">
				 ��������������� ������, ������������ ������ � �.�.
			</td>
			<td class="last_td"> <strong> <%= (int) labHours %> </strong> </td>
			<td> &nbsp; </td>
		</tr>
		<tr class="tfoot">
			<td> &nbsp; </td>
			<td class="td_subject">
				 �����:
			</td>
			<td class="last_td"> <strong> 16 </strong> </td>
			<td> &nbsp; </td>
		</tr>
       <% } %> 

		<tr class="tfoot">
			<td class="table_c_bl">
				&nbsp;
			</td>
			<td class="td_subject">
                �� ��������� �������� �� ����� ���������� �������� ����������. ���������� ���������� � ���� ����� �� ��������� ������� ��� �� ��������� ������ ������������ �����, ����������� �� ����� �������� �� �����.
			</td>
			<td class="last_td">
			</td>
			<td class="table_c_br">
				&nbsp;
			</td>
		</tr>
		<% } %>
	</table>
</div>

<div <%= Htmls.DisplayNone() %> id="free-hours-content">
<div style="width:400px;">
<%= Htmls.HtmlBlock(Model.Course.IsTheoryRoom ? HtmlBlocks.AdditionalHoursTheory : HtmlBlocks.AdditionalHours) %>
</div>
</div>

