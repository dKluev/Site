<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.ViewModel.CityFilterVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%--
<% var dropDownListID = Model.For(x => x.GlobalCityTC); %>
<% if(Model.IsMain) { %>
<script type="text/javascript">

    $(function() {
        var selector = "select[name='<%= dropDownListID %>']";
        $(selector).val("<%= Model.GlobalCityTC %>");
        
        $(selector).change(function() {
            var cityTC = $(this).val();
            var data = { cityTC: cityTC };
            $.post('<%= Url.Action<MasterPageController>(c => c.CityFilter(null)) %>', data,
            function() { window.location.reload(); }, "json");

        });
    });   

</script>
<% } %>

<%= Html.DropDownListFor(x => x.GlobalCityTC, Model.GetAllCity()) %>
--%>

