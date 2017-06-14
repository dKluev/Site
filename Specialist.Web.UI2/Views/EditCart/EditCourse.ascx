<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EditCourseVM>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<% var webinarTC = PriceTypes.GetWebinar(Model.OrderDetail.Order.IsOrganization); %>
<script type="text/javascript">
    var morningGroups = 
        [ <%= Model.MorningDayGroupIDs.Select(x => x.ToString()).JoinWith(",") %> ];
    setGroupSelection(morningGroups, "<%= PriceTypes.Webinar %>", 
        '<%= PriceTypes.Individual %>', '<%= PriceTypes.IntraExtra %>');
</script>
Здесь Вы можете выбрать удобную для Вас форму обучения (очное или дистанционное) и дату начала занятий. Ваша выгода при выборе дистанционного обучения составит от 15% до 40%.
<h4>Курс:</h4>
<p> <%= Html.CourseLink(Model.OrderDetail.Course) %> </p>
<h4> Выберите тип обучения:</h4>
<form action="">
<% if(Model.GetAllCity().Any()){ %>
    <% if(Model.Groups.Any()){ %>
    <% var hasDiscount = Model.Groups.Any(x => x.Discount > 0); %>
    <p>
        <%= Html.RadioButtonFor(x => x.PriceTypeTC, "")%>
        Очно в группе <%= hasDiscount ? H.span.Class("discount_color")["(Скидки до 20%)"] : null %>
    <%= Html.DropDownListFor(x => x.CityTC, Model.GetAllCity(), new{@class="inGroup", style="display:none;"}) %> 
    </p>
    <% } %>

<% } %>
<% if(Model.DistancePrices.Any()){ %>
	<p>
    	<%= Html.RadioButtonFor(x => x.PriceTypeTC, PriceTypes.Webinar)%>
		Вебинар
	</p>
<% } %>

<% if(Model.HasIntraExtra){ %>
	<p>
    	<%= Html.RadioButtonFor(x => x.PriceTypeTC, PriceTypes.IntraExtra)%>
		Очно-заочное <span class="discount_color">(Скидки до 40%)</span>
	</p>
<% } %>

</form>


<div id="citySection" >

<% if (Model.DistancePrices.Count > 0){%>
<div id="<%= PriceTypes.Webinar %>Section">
<%using (Html.BeginForm<EditCartController>(c => c.EditCourse(null))) {%>
    <%= Html.HiddenFor(x => x.OrderDetail.OrderDetailID) %>
    <%= H.Hidden(Model.For(x => x.PriceTypeTC), webinarTC)%>
    <div id="selectCourseBin" >
        <% Html.RenderPartial(PartialViewNames.GroupList, 
           Model.GetGroupListVMForWebinar(Cities.Moscow)); %> 
    </div>

    <% if(Model.OrderDetail.Order.IsOrganization) { %>
    <div>
    	<h4>Количество слушателей:</h4>
    	<%= Html.TextBoxFor(x => x.OrderDetail.Count) %>
    	<h4>ФИО слушателей:</h4>
    	<%= Html.TextAreaFor(x => x.OrderDetail.OrgStudents, 5, 100, null)%>
    </div>
    <% } %>
    <p><%= Images.Submit("ok") %></p>
<% } %>
</div>
<% } %>
    
    
<% if (Model.HasIntraExtra){%>
<div id="<%= PriceTypes.IntraExtra %>Section">
<%using (Html.BeginForm<EditCartController>(c => c.EditCourse(null))) {%>
    <%= Html.HiddenFor(x => x.OrderDetail.OrderDetailID) %>
    <div id="selectCourseBin" >
        <% Html.RenderPartial(PartialViewNames.GroupList, 
           Model.GetGroupListVM(Cities.Moscow,onlyIntraExtra:true)); %> 
    </div>

    <% if(Model.OrderDetail.Order.IsOrganization) { %>
    <div>
    	<h4>Количество слушателей:</h4>
    	<%= Html.TextBoxFor(x => x.OrderDetail.Count) %>
    	<h4>ФИО слушателей:</h4>
    	<%= Html.TextAreaFor(x => x.OrderDetail.OrgStudents, 5, 100, null)%>
    </div>
    <% } %>
    <p><%= Images.Submit("ok") %></p>
<% } %>
</div>
<% } %>



<% foreach (var city in Model.Cities) { %>
    <div id="<%= city.City_TC %>Section">
    <%using (Html.BeginForm<EditCartController>(c => c.EditCourse(null))) {%>
  
    <%= Html.HiddenFor(x => x.OrderDetail.OrderDetailID) %>
    
    <div id="selectCourseBin">
        <% Html.RenderPartial(PartialViewNames.GroupList, 
           Model.GetGroupListVM(city.City_TC)); %> 
    </div>
    <% if(Model.OrderDetail.Order.IsOrganization) { %>
    <div>
    	<h4>Количество слушателей:</h4>
    	<%= Html.TextBoxFor(x => x.OrderDetail.Count) %>
        <h4>ФИО слушателей:</h4>
    	<%= Html.TextAreaFor(x => x.OrderDetail.OrgStudents, 5, 100, null)%>
    </div>
    <% } %>
    
    <p><%= Images.Submit("ok").Attr(new {name = "okButton"}) %></p>
    <% } %>
    </div>
<% } %>

</div>




<div id="business-dialog" style="width:400px;display:none">
    <p>
       Вы выбрали курс в интенсивном режиме. <br />
       Рекомендуем Вам включить в стоимость
        обучения обеды и кофе-брейки.</p>
    <div>
        <span>
            <input id="add-business" class="button" type="button" value="Заказать питание" />
        </span><span>
            <input id="cancel-business" class="button" type="button" value="Не заказывать питание" />
        </span>
    </div>
</div>




