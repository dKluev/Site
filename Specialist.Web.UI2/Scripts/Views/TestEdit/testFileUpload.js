function initTestFileUpload(uploadUrl, deleteUrl, controlUrl, extesions){
	utils.loadScript("/Scripts/fileuploader.js", function(){
		var $content = $("div.test-image-content:not(.is-init-done)");
		var id = "upload-button-" + (new Date()).getTime();
		var uploadButton = $("div.upload-button", $content);
		var deleteButton = $("button.delete-button", $content);
		deleteButton.button();
		uploadButton.button();

		$content.addClass("is-init-done");
		var uploader = new qq.FileUploader({
        template: '<div class="qq-uploader">' + 
                '<div style="display:none;" class="qq-upload-drop-area"><span>Загрузить</span></div>' +
                '<div class="qq-upload-button ui-button-text">Загрузить</div>' +
                '<ul style="display:none;"  class="qq-upload-list"></ul>' + 
             '</div>', 

			element: uploadButton[0],
			action: uploadUrl,
			onSubmit: function (id,file) {
				controls.showLoad();
				return true;
			},
			onComplete: function (id,file, response) {
				controls.hideLoad();
				if (response.Message == "Size") {
					alert('Слишком большой файл');
				} else if(response.Message == "Ext") {
					alert('Не верный формат, расширение файла должно быть одно из ' + extesions.join(", "));
				} else if(response.Message == "ok") {
					$("div.test-image-control", $content).hide().load(utils.addRandomPostfix(controlUrl), function(){
						var img = $("img:first",$(this));
						img.attr('src', utils.addRandomPostfix(img.attr('src')));
						$(this).show();
				});
				}
				return false;

			},
			showMessage: function(message){ alert(message); }
		}); 
		deleteButton.click(function(){
			utils.postShowLoad(deleteUrl, function(){
				$("div.test-image-control", $content).empty();
			});
			return false;
		});
	});

}

