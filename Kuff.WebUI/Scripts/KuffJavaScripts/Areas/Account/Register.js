﻿
//برای اینکه قبل از انجام سابمیت بخواهیم کاری انجام شود 
//(بعد از انجام ولیدیشن)
//باید در ایونتِ سابمیت در فرم این کدها را در داخل متد قرار دهیم
//نکته اینجاست که اگر این کد را در داخل ایونتِ  کلیک بر روی دکمه سابمیت قرار دهیم قبل از ولیدیشن کد داخل متد مشخص شده در این ایونت اجرا می شود
// و ولیدیشن انجام نمی شود
$(document).ready(function () {
    $("form").submit(function (event) {
        if (!$("#yes_agreement").is(":checked")) {
            $("#yes_agreement").focus();
            event.preventDefault();
        }
    })
});

