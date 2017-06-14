function setAutocomplete(selector, serUrl, charCount) {
    $(function() {
        $(selector).autocomplete({
            serviceUrl: serUrl,
            minChars: charCount || 3,
            maxHeight: 400,
            width: 600,
            deferRequestBy: 300,
            zIndex: 9999
        });
    });
}
 
 /*function setAutocomplete(controlName, serviceUrl)
 {
    $(document).ready(function() {
        $(controlName).autocomplete(serviceUrl, {
            
            minChars: 3,
            matchSubset: false,
            dataType: 'json',
            formatItem: function(data,i,max,value,term){
                      return value;
            },
            parse: function(data) {
              var rows = new Array();
              for(var i=0; i<data.length; i++){
                  rows[i] = { data:data[i], value:data[i].name, result:data[i].name };
              }
              return rows;
          }
        
        });
    }); 
 }
 */
  
