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
			<strong class="text_red">Курс:</strong> <%= Html.CourseLink(gr.Course) %>
    <% if(Model.ShowAllDiscountLink){ %>
         (<%= Url.Group().WithDiscount(gr.Course_TC, "Все группы со скидками") %>)

    <% } %>
	 </td>  
					<td class="table_c_tr">&nbsp;</td>
	 
	 </tr>     
    <% } %>
	<% var classes = (gr.IsOpenLearning ? "complex " : "") +
        (gr.IsIntraExtramural ? "intra-extramural " : "") +
        (Model.HideRows ? "show-on-click-course-groups" : ""); %>
<tr <%= Htmls.DisplayNone(Model.HideRows) %> class='<%= classes %>' <%= gr.IsOpenLearning ? "title='Данная группа проводится в режиме Открытого обучения'" : "" %>
    <%= gr.IsIntraExtramural ? "title='Данная группа проводится в режиме Очно-заочного обучения'" : "" %>
     >
	<td class="table_c_tl">&nbsp;</td>

    
    <td class="td_date" style="text-align:left;">
        
          <%= gr.DateBeg.DefaultString() %>&nbsp;—<br/> 
            <%= gr.DateEnd.DefaultString() %>
    </td>
    <td class='td_time'  style="text-align:left;">
         <%= gr.DaySequence %> <%= gr.DayShift.GetOrDefault(x => x.Name) %> <br />
      <%= Model.WebinarTab ? H.span.Class("add-local-time")[gr.TimeInterval].ToString() : gr.TimeInterval %>
        <% if (gr.ProbOz != null) { %>
             <br/>(вводное занятие - <%= MonthUtil.DayNames[gr.DateBeg.GetValueOrDefault().DayOfWeek] %>  <br/>
            <%= gr.ProbOz.TimeInterval %>)
        <% } %>
         <% if (gr.IsOpenLearning) { %>
		 <br /> <%= H.Anchor(SimplePages.FullUrls.OpenClasses, "Открытое обучение").Title("Данная группа проводится в режиме Открытого обучения") %>
        <% } %>
        
        <% if (gr.IsIntraExtramural) { %>
		 <br /> <%= H.Anchor(SimplePages.FullUrls.IntraExtramural, "Очно-заочное обучение").Title("Данная группа проводится в режиме Очно-заочного обучения") %>
        <% } %>
         <% if (gr.IsWebinarOnly) { %>
		 <br/><span style='color:red;'>Только вебинар</span>
        <% } %>
	</td>
    <% if(!Model.HideComplex){ %>
    <td class="td_metro" style="text-align:left;"> «<%= Html.ComplexLink(gr.Complex) %>»<br/> 
         <% if (!stations.IsEmpty()) { %>
            <%= Images.Common("metro.gif") %> <%= stations %> <br />
        <% } %>

        <%= gr.AlmostComplete ? H.span["Группа почти укомплектована. Успейте записаться на свободные места!"].Class("discount_color") : null %>
    </td>
    <% } %>
    <% if (!Model.HideTrainer) { %>
    <td class="td_lecturer" style="text-align:left;"><%= Html.TrainerLink(gr.Teacher) %></td>
    <% } %>
    <% if (Model.ShowDiscount) { %>
    <td>
        <% if (gr.Discount.HasValue) { %>
            <%= Htmls2.Discount(gr) %><% if (gr.IsOpenLearning) { %><div title="<%= CommonTexts.OpenClasses %>открытого обучения" class='div-pointer' style="color:#AC3D4B;">*</div>
        <br/><%= H.Anchor(SimplePages.FullUrls.OpenClasses, "Открытое обучение")
				 	.Style("font-weight:normal;font-size:10px;") %>

	        <% } %>
            <br/>
        <% } %> 
        <% if(gr.RemainPlaces > 0) { %>
        <%= H.span["Осталось {0} {1}".FormatWith(gr.RemainPlaces, Linguistics.Plural("место", gr.RemainPlaces.Value)) ]
            .Class(gr.RemainPlaces <= 3 ? "discount_color" : "") %>
		<% }else if(gr.RemainPlaces == 0) { %>
			Только вебинар!
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
