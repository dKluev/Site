$(function(){
	function initMapResize(){
		var ImageMap = function (guide,map) {
			var n,
			areas = map.getElementsByTagName('area'),
			len = areas.length,
			coords = [],
			previousWidth = guide.naturalWidth;
			for (n = 0; n < len; n++) {
				coords[n] = areas[n].coords.split(',');
			}
			this.resize = function () {
				var n, m, clen,
				x = guide.clientWidth / previousWidth;
				for (n = 0; n < len; n++) {
					clen = coords[n].length;
					for (m = 0; m < clen; m++) {
						coords[n][m] = parseInt(coords[n][m] * x,10);
					}
					areas[n].coords = coords[n].join(',');
				}
				previousWidth = guide.clientWidth;
				return true;
			};
			window.onresize = this.resize;
		},
		imageMap = new ImageMap(document.getElementById("guide-img"), document.getElementById('guide-map'));
		imageMap.resize();
	}
	utils.whenLoadAllImages2($("#guide-img"), initMapResize);
});
