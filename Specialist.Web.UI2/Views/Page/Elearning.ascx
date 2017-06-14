<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Core.EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<script src="/Scripts/Views/Group/jquery.autocomplete-min.js" type="text/javascript"></script>

<script src="/Scripts/Views/Group/autocomplete.js" type="text/javascript"></script>

<script type="text/javascript">

  setAutocomplete('.course-name',
        '<%=Url.Action<CourseController>(c => c.ElearningNames(null)) %>');
    $(function() {
  var courseListUrl = '<%= Url.Action<CourseController>(c => 
                    c.ElearningList(null) ) %>';
        $("#search-elearning").click(function() {
            lazyContent("#course-list", courseListUrl + "/" + escape($(".course-name").val()), 
            ".indicator");
            return false;
        });
    });
        
</script>

<form action="">
<strong>Введите часть названия интересующего вас курса</strong> <br />
    <%= Html.TextBox("text","", new {@class="course-name", size=113}) %>
    <%= HtmlControls.Submit("Показать", "search-elearning") %>
</form>

<div class="indicator"> </div>
<div id="course-list"> </div>
<br />


  



