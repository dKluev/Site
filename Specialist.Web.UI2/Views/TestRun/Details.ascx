<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Core.Views.TestRunDetailsVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<div id="testrundetails-content">
</div>

<script type="text/javascript">
    $(function() {
        controls.showLoad();
        $("#testrundetails-content").load(utils.addRandomPostfix('<%= Url.TestRun().Urls.Start(Model.Test.Id, Model.CourseTC, Model.ModuleSetId) %>'), function() {
            controls.hideLoad();
        });
    });
</script>