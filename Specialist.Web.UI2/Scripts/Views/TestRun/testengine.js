function initTestEngine(testTimeUrl, testResultPostUrl, testResultUrl){
	var mainContent = $("#test-engine-content");

	function initSelectAnswers($ul, one){
		var $lis = $("li", $ul);
		function setCheck(){
			$lis.each(function(){
				var $li = $(this);
				var checkIcon = $("span.ui-icon-check", $li);
				if($li.hasClass("selected-answer") && !checkIcon.length){
					$li.prepend('<span class="ui-icon ui-icon-check" style="display:none;float:left;"></span>');
					$("span.ui-icon-check", $li).fadeIn();
				}else{
					checkIcon.remove();
				}
			});

		}

		(function initCheck(){
			$lis.each(function(){
				var $li = $(this);
				var iconClass = one ? 'ui-icon-check' : 'ui-icon-triangle-1-e';
				$li.prepend('<span class="ui-icon ' + iconClass + '" style="display:none;float:left;"></span>');
			});
		})();

		$lis.click(function(){
			if(one){
				$lis.removeClass("selected-answer");
			}
			$(this).toggleClass("selected-answer");
			$lis.each(function(){
				var $li = $(this);
				var isSelected = $li.hasClass("selected-answer");
				$li.toggleClass("ui-state-highlight", isSelected);
				if(isSelected)
					$("span.ui-icon", $li).fadeIn('slow');
				else
					$("span.ui-icon", $li).hide();
			});
		});
	}

		/*$wrapper = $("#questions-wrapper");
		function hideAndShow($hideDiv, $showDiv){
			$hideDiv.fadeOut('fast', function(){
				$wrapper.animate({
					height: $showDiv.height() + 'px'
				},'fast',function(){
					$showDiv.fadeIn();
					isBusy = false;
				});
			});
		}

*/


	function updateTimeControl(totalSeconds){
		if(totalSeconds <= 0)
			completeTest(true);

		var seconds = totalSeconds % 60;
		var minutes = Math.floor(totalSeconds/60);
		var prefix = String(seconds).length == 1 ? "0" : "";

		$("#time-control").html( minutes + ":" + prefix + seconds);
	}
	var restSeconds = 0;
	function updateTimeByServer(callback){
		$.get(utils.addRandomPostfix(testTimeUrl), function(data){
			restSeconds = data;
			updateTimeControl(data);
			if(callback) callback();
		});
	}
	function getEntityId($el){
		var classes = $el.attr('class');
		if(!classes)
			return null;
		var id = /entity-id-(\d+)/.exec(classes)[1];
		return parseInt(id,10);
	}
	function getEntityIdList($el){
		return $el.map(function() {
			return getEntityId($(this));
		}).toArray();
	}

	function getAnswer($el){
		if($el.hasClass("question-type-one-answer"))
			return {OneAnswer: getEntityId($("li.selected-answer", $el))};
		if($el.hasClass("question-type-many-answers"))
			return {ManyAnswers: getEntityIdList($("li.selected-answer", $el))};
		if($el.hasClass("question-type-comparison"))
			return { Comparison:{
				Left : getEntityIdList($("td.left-answers li.test-answer", $el)),
				Right : getEntityIdList($("td.right-answers li.test-answer", $el))
			} };
			if($el.hasClass("question-type-sort"))
				return {Sort: getEntityIdList($("li.test-answer", $el))};

	}
	function getAllAnswers(){
		return $("div.test-question").map(function(){
			var $el = $(this);
			var qId = getEntityId($el);
			var answer = getAnswer($el);
			var result = { QuestionId : qId };
			$.extend(result, answer);
			return result;
		}).toArray();
	}
	function updateTimeByClient(){
		restSeconds--;
		updateTimeControl(restSeconds);
	}
	var isTestCompleted = false;
	function completeTest(isByTime){
		if(isTestCompleted) return;
		isTestCompleted = true;
		clearInterval(clientTimer);
		clearInterval(serverTimer);
		var data = getAllAnswers();
		mainContent.html("<h2>Тест завершен</h2> <strong> Идет сохранение и обработка результатов </strong>" + controls.indicator);
		scrollToContent();
		$.ajax({
			url: testResultPostUrl,
			data: JSON.stringify({Data: data}),
			contentType: "application/json; charset=utf-8",
			success: function (mydata) {
				window.onbeforeunload = null;
				window.location = testResultUrl;
			},
			type: "POST",
			datatype: "json"
		});
	}
	$("ul.sortable").sortable();

	$("div.question-type-one-answer ul.test-answers").each(function(){
		initSelectAnswers($(this), true);
	});
	$("div.question-type-many-answers ul.test-answers").each(function(){
		initSelectAnswers($(this), false);
	});

	function disableButton(button,enable){
		if(enable === false){
			button.button("enable");
		} 
		else {
			button.button("disable");
		}
	}

	$("div.test-question:first").show();
	//		$wrapper.height($wrapper.height());

	var navigation = $("#test-navigation");
	var $markQuestions = $("#mark-questions");
	var nextButton = h.tag("button", {"id":"next-button", text:"Следующий" });
	var prevButton = h.tag("button", {"id":"prev-button", text:"Предыдущий" });
	var completeButton = h.tag("button", {"id":"complete-button", text:"Завершить тест"});
	navigation.append(prevButton);
	navigation.append(nextButton);
	navigation.append(completeButton);
	function getCurrentQuestion(){
		return $("div.test-question:visible");
	}
	(function addQuestionButtons(){
		var buffer = h.tag("div");
		var i = 1;
		$("div.test-question").each(function(){
			var id = getEntityId($(this));
			var questionButton = h.tag("button", {"class":"question-button-" + id}).data("question-id",id);
			questionButton.html(i);
			questionButton.click(function(){ 
				var id = $(this).data("question-id");
				showQuestion(id);
			});
			buffer.append(questionButton);
			i++;
		});
		$markQuestions.append(buffer.children());
	})();

	function showQuestion(id){
		var current = getCurrentQuestion(); 
		var next = $("div.test-question.entity-id-" + id);
		showNextCommon(current, next);
	}
	function scrollToContent(){
		if($(window).scrollTop() > mainContent.offset().top){
			$('html, body').stop().animate({
				scrollTop: mainContent.offset().top - 50
			}, 300);
		}
	}
	function showNextCommon(current, next){
		disableButton(prevButton);
		disableButton(nextButton);
		if(next.next("div.test-question").length)
			disableButton(nextButton,false);
		if(next.prev("div.test-question").length)
			disableButton(prevButton,false);
		current.fadeOut('fast',function(){
			scrollToContent();
			next.fadeIn('fast');
		});
	}
	function showNext(isPrev){
		var current = getCurrentQuestion(); 
		var next = current.next("div.test-question");
		if(isPrev)
			next = current.prev("div.test-question");
		showNextCommon(current, next);

	}
	prevButton.click(function(){ showNext(true);});
	nextButton.click(function(){ showNext(false);});
	$("button.mark-button").click(function(){ 
		$(this).toggleClass("ui-state-highlight");
		var current = getCurrentQuestion(); 
		var id = getEntityId(current);
		$("button.question-button-" + id).toggleClass("ui-state-highlight");
	});
	completeButton.click(function(){ completeTest(); });

	var clientTimer = null;
	updateTimeByServer(function(){
		clientTimer = setInterval(function(){updateTimeByClient();}, 1000);
	});

	var serverTimer = setInterval(function(){updateTimeByServer();}, 90000);
	$("button").button();
	disableButton(prevButton);
	mainContent.show();

	window.onbeforeunload = function() {
		return "Вы уверены что хотите закрыть тест?";
	};


}

