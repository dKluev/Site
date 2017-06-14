var individualTCs;
priceTypeRadioSelector = 'input[type="radio"][id="PriceTypeTC"]';
var webinarTC;
var intraExtraTC;
var askDialog;
var currentForm;
var currentGroupID;
function updateWithPriceType(priceTypeTC, cityTCSectionId) {
    //console.log("update" + cityTCSectionId);
    var div = $("[id='" + cityTCSectionId + "']").find("[id=selectCourseBin]");
    if (priceTypeTC === "" || priceTypeTC === webinarTC) {
        div.fadeIn();
    } else {
        $("input[name='OrderDetail.Group_ID']").removeAttr('checked');
    }
    

    if (individualTCs == priceTypeTC) {
        div.hide();
    }
}

function hideCitySections(withGroups, cityTC, cityTCClass) {
    
    if (cityTCClass == "inGroup") {
        $("#citySection").children().hide();
        var citySectionId = "#" + cityTC + "Section";
        $(citySectionId).fadeIn();
        priceTypeTC = $(citySectionId).find("input[name='PriceTypeTC']:checked").val();
        if (withGroups) {
            updateWithPriceType(priceTypeTC, cityTC + "Section");
        }
    }
}

function setIsBusiness(val) {
	  controls.hideDialog();
    $('[id="IsBusiness"]').val(val);
}

function setupCitySections(withGroups) {
    $(".CityTCHidden").val($("#CityTC").val());
    $("select[name='CityTC']").change(function() {
        //hideCitySections(withGroups, $(this).val(), $(this).attr("class"));
        var priceTypeTC = $(this).parent().children(priceTypeRadioSelector).val();
        var cityTC = $(this).val();
        if($(this).attr('class') == "inGroup")
            processSections(priceTypeTC, cityTC);

        $(".CityTCHidden").val(cityTC);
    });
    
    processSections($(priceTypeRadioSelector + ":checked").val(),  $("#CityTC").val());
    //hideCitySections(withGroups, $("#CityTC").val());
}

function setupPriceTypeChoice() {
    $(priceTypeRadioSelector).click(function() {
        var priceTypeTC = $(this).attr("value");
				var cityTC = $(this).parent().children("select[name='CityTC']").val();
				processSections(priceTypeTC, cityTC);
    });
}

function processSections(priceTypeTC, cityTC) {
    if (!priceTypeTC) {
        $("#citySection").children().hide();
        $("#" + cityTC + "Section").fadeIn();
    } else if (priceTypeTC == webinarTC || priceTypeTC == intraExtraTC) {
        $("#citySection").children().hide();
        $("#" + priceTypeTC + "Section").fadeIn();
    }
}

function setGroupSelection(mdGroupIDs, pwebinarTC, pindividualTCs, pintraExtraTC) {
        webinarTC = pwebinarTC;
		individualTCs = pindividualTCs;
		intraExtraTC = pintraExtraTC;
		$(function() {

		    setupCitySections(true);
		    setupPriceTypeChoice();

		    $("input[name='OrderDetail.Group_ID']").change(function() {
		        $(this).parents(".CitySection").
							find("input[name='PriceTypeTC'][value='']").attr('checked', 'checked');
		    });

		    var groupRadioSelector = "input[type='radio'][name='OrderDetail.Group_ID'][checked]";


		    currentGroupID = $(groupRadioSelector).val();

		    var currentSubmitButton = null;

		    $("#add-business").click(function() {
		        setIsBusiness('true');
		        currentSubmitButton.parents("form").submit();
		    });

		    $("#cancel-business").click(function() {
		        setIsBusiness('false');
		        currentSubmitButton.parents("form").submit();
		    });

		    $("input[name='okButton']").click(function() {

		        currentForm = $(this).parents("form");

		        var groupID = $(groupRadioSelector + "[value!='" + currentGroupID + "']").val();
		        if (!groupID) {
		            groupID = currentGroupID;
		        }
		        if (mdGroupIDs.contains(groupID)) {
		            currentSubmitButton = $(this);
		            controls.showDialog("#business-dialog");
		            return false;
		        } else {
		            setIsBusiness('false');
		            return true;
		        }


		    });

		});   
}  
