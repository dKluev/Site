<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master"
    Inherits="System.Web.Mvc.ViewPage<ProfileVM>" %>

<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Passport" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="Specialist.Web.Common.Utils.Logic" %>
<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Entities.Profile" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Root.Profile.Logic" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <% Html.RenderAction<PageController>(c => c.Banner()); %>
    
    <% if(!Model.User.IsCompany && !Model.User.IsStudent){ %>
    <div class="attention">
        <p>
            <%= Html.ActionLink<ProfileController>(c => c.ChangeStatus(),
                                "Получить доступ к сервисам слушателя (активировать данные из памятки выпускника)")%>.</p>
    </div>
    <% } %>
    <%= Model.User.IsCompany ? H.h3["Представитель: " + Model.User.FullName] : null %>
    <%= Model.EmployeeCompanyName.IsEmpty() ? null : 
        H.h3["Сотрудник компании " + StringUtils.AngleBrackets(Model.EmployeeCompanyName)] %>
    
<div style="width:100%; overflow:hidden;">                  
<div style="float:left; overflow:hidden;">
<p>   
    <%= Images.Image(CdnFiles.ImageUrls.ImageSimplePage + "corp1213.jpg").Style("width:120px;") %>
</p>
<h3 style="margin-bottom:30px;">
    <%= Url.Profile().Subscribes("Закажите бесплатно!") %>
</h3>
</div>


</div>

 

<% Html.RenderPartial(Views.Profile.ClabCard, Model.User.Student ?? new Student()); %>

    

    <% if(Model.User.IsStudent && !Model.User.BirthDate.HasValue){ %>
       <b>
            <%= Url.Profile().EditProfile("Пожалуйста, заполните дату своего рождения для аутентификации!")
            .Style("color:red;") %>
       </b>
    <% } %>

    <%= H.Div("overflow")[Model.Menu.Select(x => H.Div("tab_2column")[
    x.Select(y => H.Div("link_block2")[
        Images.Common(y.Icon + ".jpg").Class("ico").ToString(),
        H.h3[y.Name, y.IsLearning ? H.span.Id("current-attendance") : null].Class("profile_h3"),
        H.Ul(y.Links).Class("mark_arr")
        
        ])
    ])] %> 
    
    <div id="top-students"></div>


    <script type="text/javascript">
        recordOutboundLink("Profile", "Email", "<%= Model.User.Email %>");
        $(function() {
     		$("#current-attendance").load('<%= Url.Group().Urls.CurrentAttendance() %>');
     		$("#top-students").load('<%= Url.Profile().Urls.TopStudents() %>');
        });
    </script>

</asp:Content>
