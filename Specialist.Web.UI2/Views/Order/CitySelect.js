function CitySelected(cityControl) {

    var selectedValue = cityControl.options[cityControl.selectedIndex].value;
    var seldate = document.getElementById("seldate");
    if (options == null) {
        options = new Array();
        for (var i = 0; i < seldate.options.length; i++) {
            var option = seldate.options[i];
            if (option.value == "")
                continue;
            options.push(new Option(option.text, option.value));
        }
    }

    seldate.options.length = 0;
    addOption(seldate, "уточнить дату позже", "");
    for (var i = 0; i < options.length; i++) {
        var option = options[i];
        var piterIndex = option.text.toLowerCase().indexOf("санкт-петербург");
        var rostovIndex = option.text.toLowerCase().indexOf("ростов-на-дону");
        if ((selectedValue == "piter" && piterIndex > 0)
				|| (selectedValue == "rostov" && rostovIndex > 0)
				|| (selectedValue == "moscow" && piterIndex == -1 && rostovIndex == -1)
				|| selectedValue == "all")
            addOption(seldate, option.text, option.value);
    }



}
