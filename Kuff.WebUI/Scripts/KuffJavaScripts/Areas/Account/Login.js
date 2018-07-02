$(document).ready(function () {
    $("form").submit(function (event) {

        var usernameVal = $("#UserName").val();
        var passVal = $("#Password").val();

        if (emailVal != "", passVal != "") {
            // If we don't put event.preventDefault() here the submit will be happened and the ajax request will not work.
            // maybe it's because we are inside a submit event.
            event.preventDefault();
            $.ajax({
                type: "POST",
                url: "FindAndAuthUserWithAjax/",
                data: { email: usernameVal, pass: passVal },
                success: function (result) {
                    if (result == "True") {
                        // If we don't put $("form").unbind() here and after that call $("form").submit(), the whole process that we have defined for submit event will be
                        //called again and again (from line:31 until calling submit()) so we end up having send ajax request again and again and the application will collapse.
                        $("form").unbind();
                        $("form").submit();
                    } else {
                        event.preventDefault();
                        alert("نام عبور یا رمز کاربری اشتباه است");
                        //$("#security_div").append("<text>نام عبور یا رمز کاربری اشتباه است</text>");
                    }
                }
            });
        } else {
            event.preventDefault();
        }

    });
});
