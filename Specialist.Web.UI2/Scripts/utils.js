function setCookie(c_name, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) +
((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
}

Array.prototype.contains = function(element) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == element) {
            return true;
        }
    }
    return false;
}

function getSelectedText() {
    var txt = '';
    if (window.getSelection) {
        txt = window.getSelection();
    }
    else if (document.getSelection) {
        txt = document.getSelection();
    }
    else if (document.selection) {
        txt = document.selection.createRange().text;
    }
    return txt;
}


function updateURLParameter(url, param, paramVal)
{
    var TheAnchor = null;
    var newAdditionalURL = "";
    var tempArray = url.split("?");
    var baseURL = tempArray[0];
    var additionalURL = tempArray[1];
    var temp = "";

    if (additionalURL) 
    {
        var tmpAnchor = additionalURL.split("#");
        var TheParams = tmpAnchor[0];
            TheAnchor = tmpAnchor[1];
        if(TheAnchor)
            additionalURL = TheParams;

        tempArray = additionalURL.split("&");

        for (i=0; i<tempArray.length; i++)
        {
            if(tempArray[i].split('=')[0] != param)
            {
                newAdditionalURL += temp + tempArray[i];
                temp = "&";
            }
        }        
    }
    else
    {
        var tmpAnchor = baseURL.split("#");
        var TheParams = tmpAnchor[0];
            TheAnchor  = tmpAnchor[1];

        if(TheParams)
            baseURL = TheParams;
    }

    if(TheAnchor)
        paramVal += "#" + TheAnchor;

    var rows_txt = temp + "" + param + "=" + paramVal;
    return baseURL + "?" + newAdditionalURL + rows_txt;
}

Array.prototype.areEqual = function(array2) {
    if (array2 == null)
        return false;
    array1 = this;
    var temp = new Array();
    if ((!array1[0]) || (!array2[0])) { // If either is not an array
        return false;
    }
    if (array1.length != array2.length) {
        return false;
    }
    // Put all the elements from array1 into a "tagged" array
    for (var i = 0; i < array1.length; i++) {
        key = (typeof array1[i]) + "~" + array1[i];
        // Use "typeof" so a number 1 isn't equal to a string "1".
        if (temp[key]) { temp[key]++; } else { temp[key] = 1; }
        // temp[key] = # of occurrences of the value (so an element could appear multiple times)
    }
    // Go through array2 - if same tag missing in "tagged" array, not equal
    for (var i = 0; i < array2.length; i++) {
        key = (typeof array2[i]) + "~" + array2[i];
        if (temp[key]) {
            if (temp[key] == 0) { return false; } else { temp[key]--; }
            // Subtract to keep track of # of appearances in array2
        } else { // Key didn't appear in array1, arrays are not equal.
            return false;
        }
    }
    // If we get to this point, then every generated key in array1 showed up the exact same
    // number of times in array2, so the arrays are equal.
    return true;
}

function confirmDialog() {
    if (confirm("Уверены?") == true)
        return true;
    else
        return false;
}


function confirmDelete() {
    $(function() {
        $("img[src*='del']")
                .parent().click(function() {
                    return confirmDialog();
                });
    });
}

function confirmClick() {
			$("a.js-confirm-click").click(function() { return confirmDialog(); });
}
$(function(){
	confirmClick();
});

function initFbConnect(){
	window.fbAsyncInit = function () {
		FB.init({
		    appId: '180742018637213', // App ID
			status: true, // check login status
			cookie: true, // enable cookies to allow the server to access the session
			xfbml: true  // parse XFBML
		});

	// $(function () {
	// 	FB.Event.subscribe('auth.authResponseChange', cb);
	// });
	};

	(function (d) {
		var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
		if (d.getElementById(id)) { return; }
		js = d.createElement('script'); js.id = id; js.async = true;
		js.src = "//connect.facebook.net/en_US/all.js";
		ref.parentNode.insertBefore(js, ref);
	} (document));
}
