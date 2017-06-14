<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<OrgResponseVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $('#original').click(function() {
                var img = $(this);
                width = img.attr("width");
                if (width == 200) {
                    img.removeAttr("width");
                }
                else {
                    img.attr("width", 200);
                }

                return false;
            });
        });
    </script>
    
    <%= Images.Organization(Model.Response.Organization).FloatLeft() %>
    
    <%= Model.Response.Description %>  
    
    <p class="signature">
         <%= Model.Response.Authors %> 
    </p>
    <div class="enterprise_respons">
        <%= Images.OrgResponse(Model.Response).Size(200, null)
            .Style("cursor: e-resize;").Id("original") %>
         <%--   <%= HtmlControls.Image("http://www.specialist.ru/Center/Images/Scan/2.gif")
                .Size(200, null).Style("cursor: e-resize;").Id("original")%>--%>
    </div>
</asp:Content>
