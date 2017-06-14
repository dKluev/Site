function initSimpleTest(resultUrl){
	var answers = [0,0,0,0,0,0,0];
	var currentQuestion = 0;
	var questionCount = $("div.test-question").length;

	function showQuestion(){
		$("div.question-" + currentQuestion).show();
	}
	function hideQuestion(){
		$("div.question-" + currentQuestion).hide();
	}

	$("div.simple-test-body button").button();

	$(".simple-test-start-button").on("click",function(e){
		$("div.simple-test-desc").hide();
		$('html, body').stop().animate({
			scrollTop: 100
		}, 100);
		showQuestion();
	});
	$("button.question-answer").each(function(){
		$(this).on("click", function(){
			var answerIndex = parseInt($(this).data("answer-index"),10);
			answers[answerIndex] = (answers[answerIndex] | 0) + 1;
			hideQuestion();
			currentQuestion += 1;
			if(currentQuestion >= questionCount){
				var max = Math.max.apply(Math, answers);
				var result = answers.indexOf(max);
				document.location.href = resultUrl + "/" + (result + 1);
			}else{
				showQuestion();
			}
		});
	});

}

