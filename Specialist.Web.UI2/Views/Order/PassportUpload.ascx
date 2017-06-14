<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.ViewModel.ContractVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<p>
В целях соблюдения действующих норм и правил, положений действующего законодательства в области образования, Центр осуществляет идентификацию слушателей, осуществляющих обучение полностью или частично с использованием дистанционных методов (вебинар). Для идентификации слушателю необходимо предоставить
копию паспорта (страниц с Ф.И.О., данными органа выдавшего документ, данными о регистрации), или иного документа, удостоверяющего личность.
</p>
<ul>
<li>
Вы можете загрузить копию сейчас
<% var extensions = Urls.PassportExts.JoinWith(", "); %>
<button id="upload-button">Загрузить</button>

<img <%= Htmls.DisplayNone() %> id="indicator" src="//cdn.specialist.ru/Content/Image/Common/indicator.gif"/>
(<%= extensions %>, размер не больше <%= UserImages.MaxPassportSize.MBytes %> Mb)
</li>
<li>
Или после оформления заказа отправить ее по электронной почте на <%= HtmlControls.MailTo("info@specialist.ru") %> 
</li>
</ul>




<script src="/Scripts/ajaxupload.js" type="text/javascript"></script>
<script type="text/javascript">
$(function () {
    new AjaxUpload('upload-button', {
        action: '<%= Url.Action<OrderController>(c => c.UploadPassport(null)) %>',
        responseType: false,
        onSubmit: function (file, ext) {
            $("#indicator").show();
        },
        onComplete: function (file, response) {
            $("#indicator").hide();
            if (response == "Size") {
                alert('Слишком большой файл');
            } else if(response == "Ext") {
                alert('Не верный формат, расширение файла должно быть одно из <%= extensions %>');
            } else if(response == "ok") {
                alert('Файл успешно отправлен');
            }
        }
    });
});

</script>