<%@ Control Language="C#" 
Inherits="System.Web.Mvc.ViewUserControl<NearestGroupsVM>" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>



    <% foreach (var gr in Model.Groups) { %>
    <% var stations = gr.Complex.GetOrDefault(x => x.Metro); %>

    <% if(!Model.HideCourse){ %>
    <tr>
	<td class="table_c_tl">&nbsp;</td>
	 <td class="td_course2 last_td" colspan="<%= Model.ColNumber %>">
			<strong class="text_red">����:</strong> <%= Html.CourseLink(gr.Course) %>
    <% if(Model.ShowAllDiscountLink){ %>
         (<%= Url.Group().WithDiscount(gr.Course_TC, "��� ������ �� ��������") %>)

    <% } %>
	 </td>  
					<td class="table_c_tr">&nbsp;</td>
	 
	 </tr>     
    <% } %>
	<% var classes = (gr.IsOpenLearning ? "complex " : "") +
        (gr.IsIntraExtramural ? "intra-extramural " : "") +
        (Model.HideRows ? "show-on-click-course-groups" : ""); %>
<tr <%= Htmls.DisplayNone(Model.HideRows) %> class='<%= classes %>' <%= gr.IsOpenLearning ? "title='������ ������ ���������� � ������ ��������� ��������'" : "" %>
    <%= gr.IsIntraExtramural ? "title='������ ������ ���������� � ������ ����-�������� ��������'" : "" %>
     >
	<td class="table_c_tl">&nbsp;</td>

    
    <td class="td_date" style="text-align:left;">
        
          <%= gr.DateBeg.DefaultString() %>&nbsp;�<br/> 
            <%= gr.DateEnd.DefaultString() %>
    </td>
    <td class='td_time'  style="text-align:left;">
         <%= gr.DaySequence %> <%= gr.DayShift.GetOrDefault(x => x.Name) %> <br />
      <%= Model.WebinarTab ? H.span.Class("add-local-time")[gr.TimeInterval].ToString() : gr.TimeInterval %>
        <% if (gr.ProbOz != null) { %>
             <br/>(������� ������� - <%= MonthUtil.DayNames[gr.DateBeg.GetValueOrDefault().DayOfWeek] %>  <br/>
            <%= gr.ProbOz.TimeInterval %>)
        <% } %>
         <% if (gr.IsOpenLearning) { %>
		 <br /> <%= H.Anchor(SimplePages.FullUrls.OpenClasses, "�������� ��������").Title("������ ������ ���������� � ������ ��������� ��������") %>
        <% } %>
        
        <% if (gr.IsIntraExtramural) { %>
		 <br /> <%= H.Anchor(SimplePages.FullUrls.IntraExtramural, "����-������� ��������").Title("������ ������ ���������� � ������ ����-�������� ��������") %>
        <% } %>
         <% if (gr.IsWebinarOnly) { %>
		 <br/><span style='color:red;'>������ �������</span>
        <% } %>
	</td>
    <% if(!Model.HideComplex){ %>
    <td class="td_metro" style="text-align:left;"> �<%= Html.ComplexLink(gr.Complex) %>�<br/> 
         <% if (!stations.IsEmpty()) { %>
            <%= Images.Common("metro.gif") %> <%= stations %> <br />
        <% } %>

        <%= gr.AlmostComplete ? H.span["������ ����� ��������������. ������� ���������� �� ��������� �����!"].Class("discount_color") : null %>
    </td>
    <% } %>
    <% if (!Model.HideTrainer) { %>
    <td class="td_lecturer" style="text-align:left;"><%= Html.TrainerLink(gr.Teacher) %></td>
    <% } %>
    <% if (Model.ShowDiscount) { %>
    <td>
        <% if (gr.Discount.HasValue) { %>
            <%= Htmls2.Discount(gr) %><% if (gr.IsOpenLearning) { %><div title="<%= CommonTexts.OpenClasses %>��������� ��������" class='div-pointer' style="color:#AC3D4B;">*</div>
        <br/><%= H.Anchor(SimplePages.FullUrls.OpenClasses, "�������� ��������")
				 	.Style("font-weight:normal;font-size:10px;") %>

	        <% } %>
            <br/>
        <% } %> 
        <% if(gr.RemainPlaces > 0) { %>
        <%= H.span["�������� {0} {1}".FormatWith(gr.RemainPlaces, Linguistics.Plural("�����", gr.RemainPlaces.Value)) ]
            .Class(gr.RemainPlaces <= 3 ? "discount_color" : "") %>
		<% }else if(gr.RemainPlaces == 0) { %>
			������ �������!
		<% } %>
    </td>
    <% } %>
	<% if (Model.ShowPrice) { %>
		<td>
            <%= Htmls2.DiscountPrice(gr.Discount, Model.GetPrice(gr)) %>
		</td>
	<% } %>

    <td><%= Html.Calendar(gr.Group_ID) %>  </td>

    <% if(!Model.HideCart){ %>
    <td class="last_td">  <%= Html.AddToCart(x => x.AddGroup(gr.Group_ID)) %></td>
    <% } %>
					<td class="table_c_tr">&nbsp;</td>
</tr>
 

    <% } %>
