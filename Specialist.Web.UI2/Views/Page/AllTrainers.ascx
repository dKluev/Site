<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<IEntityCommonInfo>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>


<%= Htmls2.Tabs(new[] { "А...Я", "Направления", "Сертификации" }, 
	   new object[]{
		   
	   Html.Site().ThreeColumns(
                   Model.GroupByFirstLetter(x => x.Name)),
	   Html.Action("TrainerSections","Center"), 
	       Html.Action("TrainerCertifications","Center")}) %>

<script src="/Scripts/Views/Group/jquery.autocomplete-min.js" type="text/javascript"></script>

<script src="/Scripts/Views/Group/autocomplete.js" type="text/javascript"></script>

<script type="text/javascript">

    $(function() {
            
    setAutocomplete('#cert-name',
        '<%=Url.Action<CertificationController>(c => c.CertificationNames(null)) %>');
        var courseListUrl = '<%= Url.Action<CertificationController>(c => 
                    c.CertificationList(null) ) %>';
        $("#search-elearning").click(function() {
            lazyContent("#course-list", courseListUrl + "?name=" + encodeURIComponent($("#cert-name").val().replace()), 
                ".indicator");
            return false;
        });
    });

</script>

