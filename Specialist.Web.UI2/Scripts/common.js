var h = {
	tag: function(name, attrs){
		return $("<" + name + ">", attrs);
	}
};

var utils = {
	selectOrCreateById: function (id){
		var selector = "#" + id;
		var div = $(selector);
		if(!div.length){
			div = h.tag("div", {'id': id });
			$("body").append(div);
		}
		return div;
	},

	hideAndShow: function($hideDiv, $showDiv, callback){
		var $divs = $hideDiv.add($showDiv);
		$divs.wrapAll("<div/>");
		var $wrapper = $hideDiv.parent();
		var height = $wrapper.height();
		if(height > 0)
			$wrapper.height(height);
		var heightDiff = $wrapper.height() - $hideDiv.height();
		$hideDiv.fadeOut('fast', function(){
			$wrapper.animate({
				height: heightDiff + $showDiv.height() + 'px'
			},'fast',function(){
				$showDiv.fadeIn(function(){
					$wrapper.height("auto");
					$divs.unwrap();
					if(callback)
						callback();
				});
			});
		});
	},
	addRandomPostfix: function(url){
		var prefix = "?";
		if(url.indexOf("?") > -1)
			prefix = "&";
		return url + prefix + "rand=" + (new Date()).getTime();
	},
	randomPostfix: function (selector) {
		var src = $(selector).attr("src");

		$(selector).attr("src", utils.addRandomPostfix(src));
	},
	getWidth: function(element){
		var parent = element.parent();
		var body = $('body');
		body.append(element);
		var width = element.width();
		parent.append(element);
		return width;
	},
	getHeight: function(element){
		var parent = element.parent();
		var body = $('body');
		body.append(element);
		var height = element.height();
		parent.append(element);
		return height;
	},
	whenLoadAllImages2: function($images, callback){
		if(!$images.length) {
			callback();
			return;
		}
		var imagesLoaded = 0;
		var imageCount = $images.length;
		$.each($images, function(){
			var img = new Image();
    	img.onload=function(){
				imagesLoaded++;
				if(imagesLoaded >= imageCount)
					callback();
			};
    	img.src = this.src;
		});
	},
	initABTest: function(){
		var loc = location.toString();
		if(loc.indexOf("second") > 0){
			$("div.ab-test-2-hide").hide();
		}else if(loc.indexOf("third") > 0){
			$("div.ab-test-3-hide").hide();
		}
	}
	/*	whenLoadAllImages: function(imageUrls, callback){
		var imagesLoaded = 0;
		$.each(imageUrls, function(){
		var img = new Image();
		$(img).load(function(){
		imagesLoaded++;
		if(imagesLoaded == imageUrls.length)
		callback();
		}).attr('src', (imageUrls[imagesLoaded]));
});
		}, */

};


utils.imageLoaded = function($img, callback){
	if($img.attr('complete') === true) {
		callback();
	}else{
		setTimeout(function(){
			utils.imageLoaded($img, callback);
		}, 100);
	}
};

function recordOutboundLink(category, action, label) {
	try {
		ga('send', 'event', {
				eventCategory: category,
				eventAction: action,
				eventLabel: label
		});
	} catch (err) { }
}


function watermarkElement(a, text){
	if (!a.value && document.activeElement != a) {
		a.value = text;
	}
	a.onfocus = function() {
		if (a.value == text) {
			a.value = "";
		}
	};
	a.onblur = function() {
		if (!a.value) {
			a.value = text;
		}
	};

}

function watermarkByQuery(selector, text){
	watermarkElement($(selector).get(0), text);
}

function watermark(id, text) {
	var a = document.getElementById(id);
	watermarkElement(a, text);
}

var isIE6 = (navigator.userAgent.indexOf("MSIE 6.") != -1);
var isChrome = (navigator.userAgent.indexOf("chrome") != -1);


function loadUrlTo(selector, url) {
	$(selector).load(url, function(txt) {
		$(this).hide().fadeIn('fast');
	});
}

function lazyContent(selector, url, indicatorPlaceholder, cb) {
	var prefix = "&";
	if(url.indexOf("?") < 0) { prefix = "?"; } 

	url = url + prefix + "rand=" + (new Date()).getTime();    
	var loaded = false;
	var callback = function() {
		if (indicatorPlaceholder != undefined && !loaded) {
			$(indicatorPlaceholder).html(controls.indicator).show();
		}
	};
	$(selector).fadeOut('fast', callback).load(url, function(txt) {
		loaded = true;
		if(indicatorPlaceholder) 
			$(indicatorPlaceholder).hide();
		$(this).hide().fadeIn('fast');
		if(cb){
			cb(txt);
		}
	});
}


jQuery.fn.slideFadeToggle = function (speed, easing, callback) {
	return this.animate({ opacity: 'toggle', height: 'toggle' }, speed,
											easing, callback);
};


jQuery.cookie = function (key, value, options) {
	if (arguments.length > 1 && String(value) !== "[object Object]") {
		options = jQuery.extend({}, options);
		if (value === null || value === undefined) {
			options.expires = -1;
		}
		if (typeof options.expires === 'number') {
			var days = options.expires, t = options.expires = new Date();
			t.setDate(t.getDate() + days);
		}
		value = String(value);
		return (document.cookie = [
						encodeURIComponent(key), '=',
						options.raw ? value : encodeURIComponent(value),
						options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
						options.path ? '; path=' + options.path : '',
						options.domain ? '; domain=' + options.domain : '',
						options.secure ? '; secure' : ''
		].join(''));
	}
	options = value || {};
	var result, decode = options.raw ? function (s) { return s; } : decodeURIComponent;
	return (result = new RegExp('(?:^|; )' + encodeURIComponent(key) + '=([^;]*)').exec(document.cookie)) ? decode(result[1]) : null;
};


jQuery.fn.exists = function () { return jQuery(this).length > 0; };

$.ajaxSettings.traditional = true;

$.fn.serializeJson = function()
{
	var o = {};
	var a = this.serializeArray();
	$.each(a, function() {
		if (o[this.name]) {
			if (!o[this.name].push) {
				o[this.name] = [o[this.name]];
			}
			o[this.name].push(this.value || '');
		} else {
			o[this.name] = this.value || '';
		}
	});
	return o;
};

function log(x) {
	if (window.console != undefined)
		console.log(x);
}

var htmlParts ={};
htmlParts.aCorners = 
	"<div class='block_c_tl'></div> <div class='block_c_tr'></div> <div class='block_c_br'></div> <div class='block_c_bl'></div>";

htmlParts.tHead = 
	"<tr class='thead'><th class='table_c_bl'>&nbsp;</th> <th class='table_c_br'>&nbsp;</th> </tr>";


var controlUtils = {};

controlUtils.setMaxHeight = function(items, hidden){
	hidden |= false;
	var maxHeight = 0;
	items.each(function(){
		var height = hidden ? utils.getHeight($(this)) : $(this).height();
		if(height > maxHeight)
			maxHeight = height;
	});
	if(maxHeight > 0)
		items.height(maxHeight);
};

controlUtils.setFitWidth = function(cDiv){
	var items = $('span.fit-width-item', cDiv);
	var maxWidth = 0;
	items.each(function(){
		var width = utils.getWidth($(this));
		if(width > maxWidth)
			maxWidth = width;
	});
	if(maxWidth > 0)
		cDiv.width(maxWidth + 40);
};


function initCartDialog2() {
	var currentCartButton;
	function submitCart(isStay) {
		form = currentCartButton.parents('form');
		form.append("<input type='hidden' value='" + isStay + "' name='isStay'>");
		form.submit();
	}

	$(document).on("click","input.add-cart-button", function(event) {
		controls.showDialog("#cart-dialog");
		currentCartButton = $(this);
		return false;
	});
	$("#cart-dialog-buttons input").on("click", function(){
		$("#cart-dialog-buttons").hide();
		$("#cart-dialog-indicator").show();
	});

	$("#addcartstay").click(function(e) {
		e.preventDefault();
		submitCart(true);
	});

	$("#addcartgo").click(function(e) {
		e.preventDefault();
		submitCart(false);
	});
}

function initAjaxMessageForm(){
	$('form.ajax-message-form').submit(function () {
		var form = $(this);
		$(".errors", form).remove();
		$(".input-validation-error").removeClass("input-validation-error");
		$.post(
			form.attr("action"), 
			form.serializeJson());
			$("input[type!='submit']", form).val("");
			$("textarea", form).val("");
			$(".form-message", form).fadeIn();
			return false;
	});
}

function initOpenInDialog(){
	$("a.open-in-dialog").each(function() {
		var maxWidth = $(this).data("width") || 9999;
		$(this).fancybox({
			type: "ajax",
			helpers:  {
				overlay : {
					css : {
						'background-color' : 'rgba(102, 102, 102, 0.3)'
					}
				}
			},
			maxWidth : maxWidth,
			ajax: { type: 'GET' },
			titleShow: false,
			'scrolling': 'no',
			'autoScale': false
		});
	});
}


var controls = {
	indicator: '<img class="ajax-indicator" src="//cdn.specialist.ru/Content/Image/Common/indicator.gif"/>',
	bigindicator: '<img class="ajax-big-indicator" src="//cdn.specialist.ru/Content/Image/Common/bigindicator.gif"/>'
};

controls.initSelectCascade = function (parentSelector, childSelector, data, emptyText, showEmpty) {
	$(function() {
		var $child = $(childSelector);
		var $parent = $(parentSelector);
		function update() {
			var parentValue = $parent.val();
			var list = data[parentValue] || [];
			if (!list.length || showEmpty) {
				list.splice(0,0,{ id:"", name:emptyText });
			}
			var html = $.map(list, function(x) {
				return '<option value="' + x.id + '">' + x.name + '</option>';
			}).join('');
			$child.html(html);
		}
		update();
		$parent.change(update);
	});
};

controls.loadPoll = function(cb){
		$("#poll").load('/page/poll',cb);
};

controls.initSideSlidePanel = function(panel, activeBy){
	var content = $("div.content:first");
	var width = -panel.outerWidth() - 15;
	panel.css('left', width);
	panel.show();
	function hide() {
		panel.stop().animate({ left: width });
	}
	function show(){
		panel.stop().animate({ left: 0 });
	}
	$("a.side-slide-panel-close", panel).click(function () {
		hide();
		return false;
	});
	if(activeBy == "visitCount"){
			var visitCount = $.cookie("visitCountPanel") | 0;
			var maxVisitCount = 5;
			if(visitCount == maxVisitCount){
				$.cookie("visitCountPanel", "close", { path: '/' });
				show();
			}
			if(visitCount <= maxVisitCount){
				$.cookie("visitCountPanel", visitCount + 1, { path: '/' });
			}
	}else if(activeBy == "time"){
		var timer;
		function showByTime(){
			var time = $.cookie("consultantPanel");
			if(time == "close"){
				clearInterval(timer);
				return;
			}
			if(time){
				var interval = (new Date()).getTime() - parseInt(time,10);
				if(interval >= 20000){
					clearInterval(timer);
					$.cookie("consultantPanel", "close", { path: '/' });
					show();
				}
			}else{
				$.cookie("consultantPanel", (new Date()).getTime(), { path: '/' });
			}
		}
		timer = setInterval(showByTime, 1000);
	}else	if(activeBy == "scroll"){
		var isClose = $.cookie("sectionPanel") == "close";
		if(isClose) return;
		$(window).scroll(function () {
			if(isClose)
				return;
			var height = Math.abs(content.offset().top + content.height() - 600);
			var scrollTop = $(window).scrollTop();
			if (height <= scrollTop) {
				$.cookie("sectionPanel", "close", { path: '/' });
				isClose = true;
				show();
			}
			if (scrollTop < height) {
				hide();
			}
		});
	}
};

controls.initScrollTopButton = function(){
	$("body").append(h.tag("div", {"id": "scroll-top-button" }));
	var $button = $('#scroll-top-button');
	$(window).scroll(function(){
		if ($(this).scrollTop() > 100) {
			$button.fadeIn();
		} else {
			$button.fadeOut();
		}
	});

	$button.on("click", function(){
		$("html, body").animate({ scrollTop: 0 }, 300);
		return false;
	});
};

controls.initTabs = function(tabDiv) {
	var selector = "a[rel^='tab-']";
	var isBusy = false;
	var useHash = tabDiv.hasClass("use-hash");
	//tabDiv.children("div[class^='tab-']").wrapAll("<div class='tabs-wrapper'/>");
	//var $wrapper = tabDiv.children("div.tabs-wrapper");
	var tabsUl = tabDiv.children("ul");
	function changeActiveTab(link) {
		var li = $("li.active", tabsUl);
		li.removeClass("active");
		link.parent().addClass("active");
	}
	/*
	function hideAndShowFade($hideTab, $showTab){
		$hideTab.fadeOut(function () {
			$showTab.fadeIn();
			isBusy = false;
		});
	}
	function fixWrapperHeight(){
		var height = $wrapper.height();
		if(height > 0)
			$wrapper.height(height);
	}
	 function hideAndShow($hideTab, $showTab){
		fixWrapperHeight();
		var currentHeight = $wrapper.height() - $hideTab.height();
		$hideTab.fadeOut('fast', function(){
			$wrapper.animate({
				height: currentHeight + $showTab.height() + 'px'
			},'fast',function(){
				$showTab.fadeIn(function(){
					$wrapper.height("auto");
					isBusy = false;
				});
			});
		});
	}
  */
	function processClick(link) {
		var li = $("li.active", tabsUl);
		var currentDivId = li.find("a").attr("rel");
		var id = link.attr("rel");
		if(id == currentDivId){
			isBusy = false;
			return;
		}
		changeActiveTab(link);
		utils.hideAndShow($("." + currentDivId, tabDiv), $("." + id, tabDiv), function(){ isBusy = false;});
	}


	function showFirstTab(){
		var $firstTab = $("a[rel^='tab-']:first", tabsUl);
		changeActiveTab($firstTab);
		tabDiv.children("div[class^='tab-']").hide();
		$("div." + $firstTab.attr("rel"), tabDiv).show();
	}
	var hash = location.hash;

	if (hash && useHash) {
		var hashTab = $("a[rel='tab-" + hash.substring(1) + "']:first");
		if(hashTab.length === 0){
			showFirstTab();
		}else{
			changeActiveTab(hashTab, tabDiv);
			$("div[class^='tab-']:visible", tabDiv).hide();
			$(".tab-" + hash.substring(1), tabDiv).show();
		}
	} else {
		showFirstTab();
	}

	$(selector, tabsUl).click(function () {
		if(isBusy)
			return false;
		isBusy = true;
		link = $(this);
		processClick(link);
		return useHash;
	});
/*	utils.whenLoadAllImages2($("img", tabDiv), function(){
		fixWrapperHeight();
	});*/
};

controls.initCarousel = function(cDiv){
	var isBusy = false;
	var fitWidth = cDiv.hasClass('fit-width');
	var autoPlay = cDiv.hasClass('auto-play');
	function showNext(isPrev) {
		if (isBusy)
			return;
		isBusy = true;
		var nextFunction = "next";
		var isEmptySelector = ":first";
		if (isPrev) {
			nextFunction = "prev";
			isEmptySelector = ":last";
		}

		var currentItem = $("div.carousel-item:visible", cDiv);

		var next = currentItem[nextFunction]("div.carousel-item");
		if (next.length === 0) {
			next = $("div.carousel-item" + isEmptySelector, cDiv);
		}
		currentItem.fadeOut('slow', function () {
			next.fadeIn('slow');
			isBusy = false;
		});
	}

	var autoPlayInterval = null;
	function startAutoPlay(){
		autoPlayInterval = setInterval(showNext, 5000);	
	}

	function stopAutoPlay(){
		if(autoPlayInterval)
			clearInterval(autoPlayInterval);
	}

	$("div.arrow_l", cDiv).click(function () {
		stopAutoPlay();
		showNext(true);
	});

	$("div.arrow_r", cDiv).click(function () {
		stopAutoPlay();
		showNext(false);
	});

	var items = $("div.carousel-item", cDiv);
	var imageUrls = [];
	var images = $("img", cDiv);
	images.each(function(){
		imageUrls.push($(this).attr('src'));
	});
	utils.whenLoadAllImages2(images, function(){
		items.hide();
		controlUtils.setMaxHeight(items, !cDiv.is(':visible'));
		if(fitWidth)
			controlUtils.setFitWidth(cDiv);
		items.hide().first().show();
		if(autoPlay)
			startAutoPlay();
	});
};


var currentDialog; 
controls.showDialog = function(selector, modal){
	modal = modal == undefined ? true : false;
	currentDialog = $(selector);
	var settings = {
		helpers:  {
			overlay : {
				css : {
					'background-color' : 'rgba(102, 102, 102, 0.3)'
				}
			}
		},
		modal:modal,
		'autoScale' : false,
		onClosed: function(){ currentDialog.hide(); }
	};
	var box = $.fancybox.open(currentDialog, settings);
	return box;
};

controls.hideDialog = function(){
	$.fancybox.close();
	currentDialog.hide();
};

function initAddLocalTime(){
	try{
		var gtm = parseInt(new Date().getTimezoneOffset()/-60,10);
		var msk = gtm - 3;
		var sign = msk >= 0 ? "+" : "";
		if(msk === 0) return;
		var addHours = function(date){
			function twoDigits(x){
				if(x < 10) return "0" + x;
				return x;
			}
			date.setHours(date.getHours()+msk);
			var hours = date.getHours();
			var minutes = date.getMinutes();
			return twoDigits(hours) + ":" + twoDigits(minutes);
		};

		$("span.add-local-time").each(function(){
			var interval = $(this).text();
			var m = (/(\d\d):(\d\d).*?(\d\d):(\d\d)/).exec(interval);
			if(m){
				var start = new Date(1990,1,1,m[1],m[2]);
				var end = new Date(1990,1,1,m[3],m[4]);
				var newInterval = addHours(start) + " — " +
					addHours(end) + " MSK" + sign + msk;
				$(this).html("MSK: " + $(this).text() + 
					"<br/><span class='white-space: nowrap;'>Вы:&nbsp;&nbsp;&nbsp;" + newInterval + "</span>");
			}
		});
	}catch(err){}
}

controls.initSortGroupList = function(){
		var selector = $("#group-sort-type-select");
		if(selector.length === 0){
			return;
		}
		var url = selector.data("url");
		function getUrl(type) {
			return updateURLParameter(url, "sorttype", type);
		}
		function loadGroups(type) {
			lazyContent("#group-sort-list", getUrl(type), "#group-sort-list", function(txt) {
				if (txt) {
					$("#group-sort-list-title").show();
				}
			});
		}
		loadGroups(0);
		selector.change(function() {
			loadGroups($(this).val());
		});
};

controls.initTables = function(){
	$("table.defaultTable").removeClass("defaultTable").addClass("table");
	$("table.table").each(function () {

		if ($("tr.thead", this).exists())
			return;
		$(this).find("th").attr("rowspan", 2).
			last().addClass("last_td");
		$(this).find("tr:first").
			after(htmlParts.tHead).addClass("thead").
			prepend("<th class='table_c_tl'>&nbsp;</th>").
			append("<th class='table_c_tr'>&nbsp;</th>");
		var rows = $(this).find("tr:not(.thead)");
		rows.find("td:last").addClass("last_td");
		rows.prepend("<td class='table_d'>&nbsp;</td>").
			append("<td class='table_d'>&nbsp;</td>");
	});
};

controls.initAllTabs = function(){
	$("div.tabs-control").each(function () {
		controls.initTabs($(this));
	});
};

$(function () {
	initAddLocalTime();


	var notSend = ["RoboForm", "chrome.tabs", "URIError: URI error"];
	var urls = ["//connect.facebook.net/en_US/all.js"];
	var errorIsSended = false;
	window.onerror = function (msg, url, line) {
		if(!msg)
			msg = "empty";
			
		if(!url)
			url = "empty";
		try{
		msg = msg.toString();
		url = url.toString();
		}catch(err){}
		if(errorIsSended)
			return;
		var i = 0;
		for(i = 0;i < notSend.length; i++){
			if(msg.indexOf(notSend[i]) >= 0)
				return;
		}
		for(i = 0;i < urls.length; i++){
			if(url.indexOf(urls[i]) >= 0)
				return;
		}
		$.post('/page/javascripterror', {msg: msg + " " + url, url: document.location.toString(), line: line});
		errorIsSended = true;
	};


	$("table.fullCourse").removeClass("fullCourse").addClass("table");
	$("div.attention").removeClass("attention").addClass("block_red");
	$("div.attention2").removeClass("attention2").addClass("block_yellow");

	$("div.block_red,div.block_yellow").each(function () {
		$(this).prepend(htmlParts.aCorners);
	});
  controls.initTables();
	controls.initScrollTopButton();

	$("div.carousel-control").each(function () {
		controls.initCarousel($(this));
	});

  controls.initAllTabs();
	$("form.add-calendar").submit(function () {
		_gaq.push(['_trackEvent', 'Calendar', 'Download']);
	});

	initAjaxMessageForm();
	initCartDialog2();

	$("a.show-on-click").click(function () {
		$(this).remove();
		$(".show-on-click-" + $(this).attr('rel')).show();
		return false;
	});

  initOpenInDialog();

	$('a.fancy-box').fancybox( {
			helpers:  {
				title : {
						type : 'inside'
				},
				overlay : {
					css : {
						'background-color' : 'rgba(102, 102, 102, 0.3)'
					}
				}
			}
	});


	$('a[href^="http://"]').attr("target", "_blank").click(function () {
		var href = $(this).attr('href');
		if(href.indexOf('specialist.ru') < 0)
			recordOutboundLink('Outbound Links', href);
	});

	$('a.ga-click').click(function () {
		var name = $(this).attr('rel');
		recordOutboundLink('Clicks', name);
	});

	(function initSendMisprint(){
		$(document).keydown(function (e) {
			var code = e.keyCode || e.which;
			if (e.ctrlKey && code == 13) {
				var text = getSelectedText().toString();
				if(text && text !== ''){
					if(text.length > 100) alert('Вы выбрали слишком большой объем текста');
					else if (confirm('Ошибка в тексте "' + text + '". Отправить?'))
								$.post('/page/sendmessage', {text: text, url: document.location.toString()});
				}
			}
		});
	})();

	(function initBannerButtons(){
		var ul = $("#banner-rotate");
		if(!ul.length)
			return;
		var banners = ul.parent().find("a[class^='useful-info']");
		var buttons = $("span", ul);
		buttons.hover(function(){
			buttons.removeClass("active-button");
			var number = $(this).addClass("active-button").text();
			banners.hide();
			$("a.useful-info-" + number).show();
		});
	})();

	$("a.fancy-video").each(function () {
		var x = $(this);
		x.fancybox({
			helpers:  {
				title : {
						type : 'inside'
				},
				overlay : {
					css : {
						'background-color' : 'rgba(102, 102, 102, 0.3)'
					}
				}
			},
			'title': x.attr('title'),
			'href': "//www.youtube.com/embed/" + $("img", x).attr('class') + "?autoplay=1",
			'type': 'iframe'
		});
	});
});

var facebookLikeboxUrl = 'https://www.facebook.com/plugins/likebox.php?colorscheme=light&header=false&height=175&href=https://www.facebook.com/pages/SPECIALISTRU/146201302064729&show_faces=true&stream=false&width=243';

controls.initSlider = function(startId) {
	var elementName = "div";
	var parentSelector = "";
	var linkSelector = "a[rel^='" + startId + "']";
	var isAnimation = false;
	$(linkSelector).click(function() {
		if (isAnimation)
			return false;
		$(this).toggleClass("open");
		isAnimation = true;
		var id = $(this).attr("rel");
		var selector = elementName + "[id='" + id + "']";
		var parent = $(this).parents(parentSelector + ":first");
		var children = parent.find(selector);
		var isHidden = children.is(':hidden');
		children.slideToggle('fast', function() {
			isAnimation = false;
		});
		return false;
	});
};
function getUtm() {
    var values = {};
    var cookie = $.cookie("__utmz");
    if (cookie) {
        var z = cookie.split('.'); 
        if (z.length >= 4) {
            var y = z[4].split('|');
            for (i=0; i<y.length; i++) {
                var pair = y[i].split("=");
                values[pair[0]] = pair[1];
            }
        }
    }
    return values;
}

