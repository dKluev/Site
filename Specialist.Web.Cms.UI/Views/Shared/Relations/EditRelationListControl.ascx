<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EditRelationListVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>

<script type="text/javascript">
    $(function() {
        $('#addForm').ajaxForm(function() {
            hideList();
            loadList();
            $('#addForm').resetForm();
        });
        $(document).on('click', '.delete', function() {
            if(confirmDelete()){
                hideList();
                $.post('<%= Url.Action<SiteObjectRelationEntityController>(
                    c => c.Delete(null)) %>/' + $(this).attr('id'), function(){
                    loadList();
                });
            }
            return false;
        });
        
        loadList();
		$("#delete-all-button").click(function(){
			if(confirmDelete()){
                hideList();
				 $.post('<%= Url.Action<SiteObjectRelationEntityController>(
                    c => c.DeleteAll(Model.SiteObject.Type, Model.SiteObject.ID)) %>', function(){
                    loadList();
                });
			}
		});
        $('#show-entities-form-button').click(function() {
            showEntitiesForm();
            $(this).hide();

        });
        
        function showEntitiesForm() {
            $('#main-entities-list').show();
        $.get('<%= Url.Action<SiteObjectRelationEntityController>(c => 
            c.MainEntityList(Model.SiteObject.Type, Model.SiteObject.ID)) %>&rand=' + (new Date()).getTime(),
			function (data) {
			    $('#main-entities-list').html(data);
			    $('#add-entities-form').submit(function() {
	       			var form = $(this);
			        var button = $('button', form);
			        button.attr('disabled', 'disabled');
			        $.post(form.attr("action"), form.serialize(), function() {
	        			$('html, body').stop().animate({
							scrollTop: 100
						}, 300);
			            loadList();
				        button.removeAttr('disabled');
			        });
			        return false;
			    });
			    
			});

        }
    });
    
    function hideList() {
         $('#relationList').hide();
         $("#indicator").fadeIn('slow');
    }
    function loadList() {
        
        $('#relationList')
            .load('<%= Url.Action<SiteObjectRelationEntityController>(c => 
                c.RelationList(Model.SiteObject.Type, Model.SiteObject.ID, 
                    Model.ForLinkCreator)) %>&rand=' + (new Date()).getTime(), 
            function(){$(this).fadeIn('slow'); $("#indicator").hide(); return true;});
    }
    

</script>



<table>
    <tr >
      <% using (Html.BeginForm<SiteObjectRelationEntityController>(c => c.Add(),
          FormMethod.Post, new {id = "addForm"})) {%>
        <td>
            
             <%= HtmlControls.Hidden("ObjectType", Model.SiteObject.Type) %>
            <%= HtmlControls.Hidden("Object_ID", Model.SiteObject.ID) %>
           <% Html.RenderPartial(PartialViewNames.SiteObjectSelector, 
           new ControlVM{PropertyName = "SiteObject2_ID"}); %>
        </td>
       
     <td width="20px"><%= HtmlControls.ImgSubmit("/Content/Image/Add.png") %></td> 
    <% } %>

    
    </tr>

</table>
    <div id="indicator" class="ui-state-highlight ui-corner-all" style="width:35px; margin: auto">
                <%= HtmlControls.Image("/Content/Image/LoadIndicator.gif") %> </div>
<div id="relationList">
</div>
<input type="submit" id="save-sort-button" value="Сохранить сортировку" style="display: none;" /> 
<input type="submit" id="delete-all-button" value="Удалить все теги" /> 
<input type="submit" id="show-entities-form-button" value="Показать теги" />
<div id="main-entities-list" style="display:none;"> 
<div class="ui-state-highlight ui-corner-all" style="width:35px; margin: auto">
                <%= HtmlControls.Image("/Content/Image/LoadIndicator.gif") %> </div></div>

