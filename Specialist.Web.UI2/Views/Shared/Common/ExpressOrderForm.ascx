<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ExpressOrderVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>

<% var id = Guid.NewGuid().ToString(); %>


<% if (Model.Subscibe) { %>


<%= Htmls2.Menu2(H.l(Images.Main("blue/subscribe.png").Style("vertical-align:middle; padding:0px; margin:-1px 0 0 0;").Size(18, 18), H.span["Подписаться на рассылку"].Style("font-size:14px;"))) %>

<script type="text/javascript">
	$(function () {
//	    watermark('maillist-subscribe-email', 'Ваш E-mail адрес');
		$("#maillist-subscribe-button").click(function () {
			recordOutboundLink("MailListSubscribe", "Subscribe");
		});
	})
</script>

                  <div class="podpiska">
                      
<% } %>



<% using(Html.BeginForm<PageController>(c => c.SendExpressOrder(null),
	   FormMethod.Post,new {@class="express-order-form  v_order_discount", id = "express-form-" + id})) { %>

<%=  Images.Indicator().Style("display:none;") %>
<p class="form-message" style="color: green; display: none;"><%= Model.Message %></p>

<p class="errors" style="display:none;color:red;" ></p>

<% if (Model.Subscibe) { %>
    
<div class="success-hide">

<br>
<input type="checkbox" checked>Полезные статьи<br>
<input type="checkbox" checked>Обучающие материалы<br>
<input type="checkbox" checked>Афиши и билеты на конференции<br>
<input type="checkbox" checked>Подарки наших партнеров

   
<input type="text" required placeholder="ФИО" style="margin-bottom: 5px; width: 100%;" name="<%= Model.For(x => x.Name) %>" />
<input type="email" required placeholder="Емейл" style="width: 100%;" name="<%= Model.For(x => x.Contact) %>"/>


<p class="express-order-captcha" <%= Htmls.DisplayNone() %>>
<%= Html.TextBoxFor(x => x.ExpressCaptcha, new {placeholder="Число", required = "required", style="width:190px;"}) %>
<input type="image" value="Подписаться" id="maillist-subscribe-button" src="//cdn1.specialist.ru/Content/Image/Main/blue/subscribe-mail-button.jpg" style="margin-top:-5px; vertical-align:middle;"/>

</p>
    
</div>





<% } else { %>
    
	<p class="success-hide"><%= Html.TextBoxFor(x => x.Name, new {@class="text", placeholder = "Имя", required = "required"}) %></p>
	<p class="success-hide"><%= Html.TextBoxFor(x => x.Contact, new {@class="text", placeholder = "Телефон", required = "required"}) %></p>
	<p class="express-order-captcha success-hide" <%= Htmls.DisplayNone() %>>
<%= Html.TextBoxFor(x => x.ExpressCaptcha, new {placeholder="Число", required = "required"}) %>
	</p>

    <p class="success-hide">
    <%= H.InputImage(Urls.Main("button/recall.jpg")).Class("button").Value("Отправить")%>
    </p>
<% } %>

	<%= Html.HiddenFor(x => x.CourseTC) %>
	<%= Html.HiddenFor(x => x.GroupId) %>
	<%= Html.HiddenFor(x => x.Subscibe) %>

<% }  %>

<% if (Model.Subscibe) { %>
		</div>
<% }%>


<script type="text/javascript">
    $(function () {
//        var nameText = '<%= Model.Subscibe ? "ФИО" : "Имя" %>';
//        var contactText = '<%= Model.Subscibe ? "Емейл" : "Телефон" %>';
//		watermarkByQuery("#express-form-<%= id %> input[name='<%= Model.For(x => x.Name) %>']", nameText);
//		watermarkByQuery("#express-form-<%= id %> input[name='<%= Model.For(x => x.Contact) %>']", contactText);
//		watermarkByQuery("#express-form-<%= id %> input[name='<%= Model.For(x => x.ExpressCaptcha) %>']", 'Введите число');
	    var isReady = false;
	    function showCaptcha(form) {
		    if(!isReady) {
		        isReady = true;
		        var captcha = '<%= HtmlControls.Image(
				Url.Action<ChartController>(c => c.Captcha()) 
				+ "?rand=" + DateTime.Now.ToString("ss-fff")).Class("captcha-img") + 
			"<br/>" %>';
		        $("p.express-order-captcha", form).prepend(captcha);
		        
		    }
	        $("p.express-order-captcha", form).fadeIn();
	        
	    }
		$("#express-form-<%= id %> input[name='<%= Model.For(x => x.Name) %>']").unbind('focus').focus(function () {
		    showCaptcha($(this).parents("form:first"));
		});
		$("#express-form-<%= id %> input[name='<%= Model.For(x => x.Contact) %>']").unbind('focus').focus(function () {
		    showCaptcha($(this).parents("form:first"));
		});
	    
    	$('#express-form-<%= id %>').unbind('submit').submit(function () {
    	    
			var form = $(this);
    	    $("img.ajax-indicator", form).show();
			$(".errors", form).hide();
			$.post(
				form.attr("action"), 
				form.serializeJson(), function (data) {
				    $("img.ajax-indicator", form).hide();
                    if (data.error) {
			        	$(".errors", form).text(data.error).show();
                    } else if (data == "captcha") {
					    showCaptcha(form);
			        	$(".errors", form).text("Число неверно");
			        	$(".errors", form).show();
				        var img = $("img.captcha-img",form);
						img.attr('src', utils.addRandomPostfix(img.attr('src')));
				    }else {
					 $("input[type!='submit']", form).val("");
						$("textarea", form).val("");
						$(".form-message", form).fadeIn();
                        $(".success-hide").hide();
                    }
				});
    	    recordOutboundLink("ExpressOrder", "ClickButton");
			return false;
		});


	});
</script>

