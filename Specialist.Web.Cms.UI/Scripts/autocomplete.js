 function setAutocomplete(controlName, hiddenControlName, serviceUrl)
 {
     $(document).ready(function() {


         $(controlName).autocomplete(serviceUrl, {

             minChars: 3,
             matchSubset: false,
             selectOnly: true,
             dataType: 'json',
             formatItem: function(item) {
                 return item.name;
             },
             parse: function(data) {
                 var rows = new Array();
                 for (var i = 0; i < data.length; i++) {
                     rows[i] = { data: data[i], value: data[i].name, result: data[i].name };
                 }
                 return rows;
             }

         })
           .result(function(event, item) {
               $(hiddenControlName).val(item.id)
           });


     });
 }


 
  