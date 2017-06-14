<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CustomerTypeChoiceVM>" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Services" %>
<%@ Import Namespace="Specialist.Web.Cms.Root.Socials" %>


<form action='<%= Model.ActionUrl %>' method='get' id="customer-type-form">
	    <%= Html.Hidden("nextUrl", Model.NextUrl) %>
    <div class="registr_form2">
                <p>
                    <%= Html.RadioButtonFor(x => x.CustomerType, 
								    OrderCustomerType.PrivatePerson) %>
                    <label for="<%= OrderCustomerType.PrivatePerson %>" style="font-size: 20px;font-weight: bold">
                        Частное лицо</label></p>
                <%= Htmls.HtmlBlock(HtmlBlocks.PrivatPerson) %> 
                <p>
                    <%= Html.RadioButtonFor(x => x.CustomerType, 
    						        OrderCustomerType.Organization) %>
                    <label for="<%= OrderCustomerType.Organization %>"  style="font-size: 20px;font-weight: bold">
                        Организация</label>
                </p>
                <%= Htmls.HtmlBlock(HtmlBlocks.Organization) %> 
    </div>

<% if (Model.IsRegister) { %>
	    <%= H.Hidden("token", "").Id("token-input") %>
    <%= H.button["Использовать Email"].Class("ui-button") %>
    <%= H.button["Использовать аккаунт Facebook"].Type("button").Id("facebook-register").Class("ui-button") %>

    <script>
        $(function() {
            initFbConnect();
            function processResponse(response) {
                if (response.status === 'connected') {
                    $("#token-input").val(response.authResponse.accessToken);
                    $("#customer-type-form").submit();
                }
            }
            $("#facebook-register").click(function (e) {
                e.preventDefault();
                FB.login(processResponse, { scope: '<%= FacebookService.ConnectPermission %>' });
            });

            function checkType() {
                if ($('input[name=CustomerType]:checked').val() == '<%= OrderCustomerType.Organization %>') {
                    $("#facebook-register").hide();
                } else {
                    $("#facebook-register").show();
                }
            }

            checkType();
            $('input[name=CustomerType]').change(checkType);

        });
    </script>
<% }else { %>
    <%= H.button["Выбрать"].Class("ui-button") %>
<% } %>
        

        
   
    </form>
    