<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GroupListVM>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>

<h4>�������� ����:</h4>
<p>
    <%= Html.RadioButtonFor(x => x.OrderDetail.Group_ID, string.Empty) %>
    <label>
        �������� ���� �����
    </label>
</p>
<% if(Model.Groups.Any()){ %>

<% if(Model.Groups.Count > 10){ %>
<%= Htmls.BorderBegin() %>
<div class="tabs-control">

	<ul class="bookmarks">
	    <% Model.GetTabs.ForEach(monthTab => { %>
    		<li class='<%= monthTab.IsActive ? "act1" : "" %>'>
    		    <a href="#" rel="tab-<%= monthTab.Id %>"><%= monthTab.Name%></a>
    		</li>
        <% }); %>

    </ul>
<%= Html.HiddenFor(x => x.IsBusiness) %> 

    <% foreach (var monthTab in Model.GetTabs.OrderBy(mt => mt.IsActive)) { %>
    <div class="tab-<%= monthTab.Id %> tab_content2" <%= Htmls.DisplayNone(!monthTab.IsActive) %> >
      <table class="defaultTable">
        <tr class="sectionAct">
            <th style="width: 15px;"></th>
            <th> ���� ������ </th>
            <th> ���� ��������� </th>
            <th> ����� ���������� </th>
            <th> ����� ���������� </th>
            <th> ������������� </th>
		<% if(Model.ShowType){ %>
            <th> ��� �������� </th>
	    <% } %>
		<% if(!Model.HidePrice){ %>
            <th> ������ </th>
            <th> ���� </th>
	    <% } %>
        </tr>
  <% foreach (var group in monthTab.Groups) { %>
        <tr>
            <td><%= Html.RadioButtonFor(x => x.OrderDetail.Group_ID, group.Group_ID) %></td>
            <td>
                <%= Html.Encode(group.DateBeg.DefaultString()) %>
            </td>
            <td>
                <%= Html.Encode(group.DateEnd.DefaultString()) %>
            </td>
            <td>
                <%= Html.Encode(group.Get(x => x.DayShift.Name)) %>
            </td>
            <td>
                <%= Html.ComplexLink(group.Complex) %>
            </td>
            <td>
                <%= Html.EmployeeLink(group.Teacher) %>
            </td>
		<% if(Model.ShowType){ %>
            <td>
                
         <% if (group.IsOpenLearning) { %>
		 <%= H.Anchor(SimplePages.FullUrls.OpenClasses, "�������� ��������").Title("������ ������ ���������� � ������ ��������� ��������") %>
        <% } %>
        
        <% if (group.IsIntraExtramural) { %>
		 <%= H.Anchor(SimplePages.FullUrls.IntraExtramural, "����-������� ��������").Title("������ ������ ���������� � ������ ����-�������� ��������") %>
        <% } %>

            </td>
	    <% } %>
		<% if(!Model.HidePrice){ %>
			<% var price = Model.GetPrice(group); %>
            <td>
            	<%= Htmls2.Discount(group) %>
            </td>
            <td>
                <%= Htmls2.DiscountPrice(group.Discount, price) %>
            </td>
	    <% } %>
         
        </tr>
    <% } %>
    </table>
    </div>
    <% } %>
    </div>
<%= Htmls.BorderEnd %>
<% }else{ %>

    <table class="defaultTable">
        <tr class="sectionAct">
            <th style="width: 15px;">
            </th>
            <th>
                ���� ������
            </th>
            <th>
                ���� ���������
            </th>
            <th> ������ </th>
            <th>
                ����� ����������
            </th>
            <th>
                ����� ����������
            </th>
            <th>
                �������������
            </th>
		<% if(Model.ShowType){ %>
            <th> ��� �������� </th>
        <% } %>
        </tr>
        <% foreach (var group in Model.Groups) { %>
        <tr>
            <td>
                <%= Html.RadioButtonFor(x => x.OrderDetail.Group_ID, group.Group_ID) %>
            </td>
            <td>
                <%= Html.Encode(group.DateBeg.DefaultString()) %>
            </td>
            <td>
                <%= Html.Encode(group.DateEnd.DefaultString()) %>
            </td>
            <td>
                <% if(group.Discount.HasValue){ %>
                <span class="discount_color">
                    <%= group.Discount.ToString() %>%</span>
                <% } %>
            </td>
            <td>
                <%= Html.Encode(group.Get(x => x.DayShift.Name)) %>
            </td>
            <td>
                <%= Html.ComplexLink(group.Complex) %>
            </td>
            <td>
                <%= Html.EmployeeLink(group.Teacher) %>
            </td>

		<% if(Model.ShowType){ %>
            <td>
                
         <% if (group.IsOpenLearning) { %>
		  <%= H.Anchor(SimplePages.FullUrls.OpenClasses, "�������� ��������").Title("������ ������ ���������� � ������ ��������� ��������") %>
        <% } %>
        
        <% if (group.IsIntraExtramural) { %>
		  <%= H.Anchor(SimplePages.FullUrls.IntraExtramural, "����-������� ��������").Title("������ ������ ���������� � ������ ����-�������� ��������") %>
        <% } %>

            </td>
        <% } %>
        </tr>
        <% } %>
    </table>

<% } %>
    



<% }else{ %>
    <p> ��������� ����� ���</p>
<% } %>



