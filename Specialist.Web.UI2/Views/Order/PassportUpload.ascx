<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.ViewModel.ContractVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<p>
� ����� ���������� ����������� ���� � ������, ��������� ������������ ���������������� � ������� �����������, ����� ������������ ������������� ����������, �������������� �������� ��������� ��� �������� � �������������� ������������� ������� (�������). ��� ������������� ��������� ���������� ������������
����� �������� (������� � �.�.�., ������� ������ ��������� ��������, ������� � �����������), ��� ����� ���������, ��������������� ��������.
</p>
<ul>
<li>
�� ������ ��������� ����� ������
<% var extensions = Urls.PassportExts.JoinWith(", "); %>
<button id="upload-button">���������</button>

<img <%= Htmls.DisplayNone() %> id="indicator" src="//cdn.specialist.ru/Content/Image/Common/indicator.gif"/>
(<%= extensions %>, ������ �� ������ <%= UserImages.MaxPassportSize.MBytes %> Mb)
</li>
<li>
��� ����� ���������� ������ ��������� �� �� ����������� ����� �� <%= HtmlControls.MailTo("info@specialist.ru") %> 
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
                alert('������� ������� ����');
            } else if(response == "Ext") {
                alert('�� ������ ������, ���������� ����� ������ ���� ���� �� <%= extensions %>');
            } else if(response == "ok") {
                alert('���� ������� ���������');
            }
        }
    });
});

</script>