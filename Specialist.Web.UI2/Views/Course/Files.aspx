<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<FileListVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>


<% Html.RenderPartial(PartialViewNames.FileList, Model); %>


<%= JavaScripts.TinyMce() %>
<%= JavaScripts.Local("Views/Message/messageUtils.js") %>
<script type="text/javascript">
    setupMce("course-description");
</script>