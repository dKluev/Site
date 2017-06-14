<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Runtime.InteropServices" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>

<!-- Widget QuickRegister -->
<div class="widget_quickregister">
   <div class="qr-content">
       <form method="post" action="<%= Url.Page().Urls.SendQuickRegistration(null) %>" id="widget_registration" class="qr-regform">
            <ul class="qr-list">
                <li>Бесплатные семинары ведущих преподавателей</li>
                <li>Акции и спецпредложения</li>
                <li>Вакансии службы трудоустройства</li>
               <br /><span style="color:#006;">Обо всем этом Вы узнаете первым!</span>
            </ul>
            
            <div>
                <input type="email" name="email" required="required" placeholder="Введите email" id="quick-register-email-input"/>
            </div>
        	<div class="quick-registration-captcha" <%= Htmls.DisplayNone() %>>
                <input type="text" name="captcha" required="required" placeholder="Введите число"/>
        	</div>

            <div class="errors" style="display:none;color:red;" ></div>
            <div class="btn-line">
                <input type="submit" class="btn btn-primary btn_getacc" value="Получить аккаунт">
            	<%=  Images.Indicator().Style("display:none;") %>
            </div>
            <a href="#" class="qr-later" id="qr-close-button">Спасибо, не сейчас</a>
     </form>
    </div>
    <div class="qr-label"> </div>
</div>
<!-- End Widget QuickRegister -->
<script>
    
    $(function () {
        var $widget = $("div.widget_quickregister");
        $("a.qr-later").click(function (e) {
            e.preventDefault();
            $widget.toggleClass("opened");
        });
        $("div.qr-label").click(function () {
            $widget.toggleClass("opened");
        });

	    var isReady = false;
	    function showCaptcha(form) {
		    if(!isReady) {
		        isReady = true;
		        var captcha = '<%= HtmlControls.Image(
				Url.Chart().Urls.Captcha() 
				+ "?rand=" + DateTime.Now.ToString("ss-fff")).Class("captcha-img") + 
			"<br/>" %>';
		        $("div.quick-registration-captcha", form).prepend(captcha);
		        
		    }
	        $("div.quick-registration-captcha", form).fadeIn();
	        
	    }
		$("#quick-register-email-input").on('focus', function () {
		    showCaptcha($(this).parents("form:first"));
		});

    	$('#widget_registration').on("submit", function () {
    	    
    	    var form = $(this);
    	    var $button = $("input.btn", form);
	        log($button);
	        $button.prop("disabled", true);
    	    $("img.ajax-indicator", form).show();
    	    var $errors = $(".errors", form);
	        $errors.hide();
			$(".errors", form).hide();
			$.post(
				form.attr("action"), 
				form.serializeJson(), function (data) {
		    	    $("img.ajax-indicator", form).hide();
				    if(data == "captcha") {
				        showCaptcha(form);
				        $errors.text("Число не верно");
			        	$errors.show();
				        var img = $("img.captcha-img",form);
						img.attr('src', utils.addRandomPostfix(img.attr('src')));
				    } else if (data == "ok") {
				        document.location = "<%= Url.Profile().Urls.Details() %>";
				    } else {
				        $errors.text(data);
			        	$errors.show();
				    }
        	        $button.prop("disabled", false);
				});
    	    recordOutboundLink("ExpressOrder", "ClickButton");
			return false;
		});

    })

</script>