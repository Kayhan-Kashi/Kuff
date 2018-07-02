var propertyCounter;
var propertyCounterToShow;
var dataTypeOptions = "";
var addedPropertyCounter = 0;


function getDataTypes() {
    $.getJSON("../../SharedServices/GetDataTypes", function (data) {
        $.each(data, function (i) {
            dataTypeOptions += '<option value = ' + data[i].Id + '>' + data[i].Name + '</option>'
        });
        items = data;
    });
}


//calling GetDataTypes service and recieving JSON Datatype objects
getDataTypes();


$("#addProperty").click(function () {
    alert();
    propertyCounter++;
    propertyCounterToShow++;
    addedPropertyCounter++;
    $("#ProductTypeProperties").append(
        '<div id = "panel' + propertyCounter + '" class = "panel panel-default">' +
            '<div class="panel-heading">' + 'خصیصه شماره ' + propertyCounterToShow + '</div>' +
            '<div class = "form-group">' +
                '<label for = "ProductTypeProperties[' + propertyCounter + '].Title" style = "margin-top:5px;" >عنوان خصیصه شماره' + ' ' + propertyCounterToShow + '</label>' +
                '<input id = "ProductTypeProperties[' + propertyCounter + '].Title" name = "ProductTypeProperties[' + propertyCounter +
                    '].Title" type = "text" class = "form-control" style = "margin-top:5px;" dir = "rtl" required />' +
                '<label for = "ProductTypeProperties[' + propertyCounter +
                    '].DataTypeId" >نوع داده ای</label>' +
                '<select class = "form-control" name = "ProductTypeProperties[' + propertyCounter +
                    '].DataTypeId" style="margin-top:10px;">' + dataTypeOptions + ' required </select>' +
                '<label for = "ProductTypeProperties[' + propertyCounter + '].IsUserDecision" >کاربر باید مقدار خصیصه را مشخص کند ؟</label>' +
                '<select class= "form-control" name = "ProductTypeProperties[' + propertyCounter + '].IsUserDecision" style = "margin-top:5px;" dir="rtl">' +
                    '<option value = "true" >بلی</Option>' +
                    '<option value="false">خیر</Option>' +
                '</select>' +
            '</div>' +
        '</div>'
        );
})

$("#removeProperty").click(function () {
    alert();
    if (addedPropertyCounter > 0) {
        $("#panel" + propertyCounter).remove();
        propertyCounter--;
        propertyCounterToShow--;
        addedPropertyCounter--;
    }
})