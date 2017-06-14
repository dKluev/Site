<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Context.Group>>" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<% if (DateTime.Now.Hour.BetweenInclude(CommonConst.IpCameraBegin, CommonConst.IpCameraEnd - 1)) {%>
<% var isIe =  Htmls2.IsIe(); %>
<% var url = "http://80.76.226.2:8016/" + (isIe ? "image.jpg" : "video.cgi" ); %>
<%= H.Img(url).Id("ipcameraimage") %>

<% if (isIe) {%>
    <script type="text/javascript">
        function updateImage() {
            document.getElementById("ipcameraimage").src = utils.addRandomPostfix('<%= url %>');
        }
        document.getElementById("ipcameraimage").onload = updateImage;
        updateImage();
    </script>
<% } %>
	<% if (Model.Any()) {%>
	<h3>Анонсы ближайших групп</h3>
	<% Html.RenderPartial(PartialViewNames.NearestGroupList,
			   new NearestGroupsVM(Model.ToList())); %>
	<% } %>
<% } else { %>
<strong>Веб-камера работает с
	<%=CommonConst.IpCameraBegin%>
	до
	<%=CommonConst.IpCameraEnd%>. </strong>
<% } %>
