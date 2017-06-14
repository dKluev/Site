$(function(){
	try{
		function initFBTrack(){
			if (FB && FB.Event && FB.Event.subscribe) {
				FB.Event.subscribe('edge.create', function(targetUrl) {
					_gaq.push(['_trackSocial', 'facebook', 'like']);
				});
				FB.Event.subscribe('edge.remove', function(targetUrl) {
					_gaq.push(['_trackSocial', 'facebook', 'unlike']);
				});
			}
		}

		window.fbAsyncInit = function () { initFBTrack(); };




		$('#social-buttons').prepend(
			'<div style="display:inline-block;padding-bottom:10px;">'+ 
			'<div id="vk_like" style="float:left;"></div><div id="ok_shareWidget" style="float:left;padding-right:20px;"></div>' + '<div style="float:left;"><div data-size="medium"  class="g-plusone" data-annotation="none"></div></div>' + 
			'<div style="float:left;padding-left:20px;"><div class="fb-like" data-href="' + 
			document.location + 
			'" data-send="false" data-layout="button_count" data-width="150" data-show-faces="false"></div></div>' +
			 '</div><br/>');


			(function () {
				window.___gcfg = { lang: 'ru' };
				var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
				po.src = 'https://apis.google.com/js/plusone.js';
				var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
			})();

			(function(d, s, id) {
				var js, fjs = d.getElementsByTagName(s)[0];
				if (d.getElementById(id)) return;
				js = d.createElement(s); js.id = id;
				js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&amp;appId=180742018637213";
				fjs.parentNode.insertBefore(js, fjs);
			}(document, 'script', 'facebook-jssdk'));


			(function (d, id, did, st) {
				var js = d.createElement("script");
				js.src = "https://connect.ok.ru/connect.js";
				js.onload = js.onreadystatechange = function () {
				if (!this.readyState || this.readyState == "loaded" || this.readyState == "complete") {
					if (!this.executed) {
						this.executed = true;
						setTimeout(function () {
							if(window.OK){ OK.CONNECT.insertShareWidget(id,did,st);}
						}, 0);
					}
				}};
				d.documentElement.appendChild(js);
			})(document,"ok_shareWidget",document.location,"{width:170,height:30,st:'straight',sz:20,ck:3}");




	}catch(e){ }

});
