$(document).ready(function () {
    $("form")
        .submit(function (event) {
            var hasValidationError = false;
            for (c = 0; c < specC; c++) {
                var inputValue = $("#OrderItemSpecifications_" + c + "_Value").val();
                if (inputValue == "") {   
                    hasValidationError = true;
                    break;
                    //var property = $("#OrderItemSpecifications_" + c + "_Title");
                    //alert(property.val());
                }
            }
            if (hasValidationError) {
                alert("لطفا مشخصات محصول مورد نظر را انتخاب کنید");
                event.preventDefault();
            }
        });
    //var count = specC;
});