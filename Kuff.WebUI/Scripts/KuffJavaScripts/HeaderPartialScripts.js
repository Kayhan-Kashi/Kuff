function updatePaymentSummary() {
    $.ajax({
        type: "GET",
        url: "/Store/Orders/GetJSonPaymentSummary",
        success: function (result) {
            if (result.length != 0) {
                //alert("if");
                $("#btn-paymentsummary").attr("style", "visibility:visible")
                $("#cart-total").text(result.num_items + " آیتم " + " - " + result.total_price + " تومان ");
                $("#total-pay").text(result.total_price + " تومان ");
                $("#payable").text(result.total_price + " تومان ");
            } else {
                //alert("else");
                $("#cart").attr("style", "visibility:hidden");
                //alert("hidden");
                $("#btn-paymentsummary").attr("style", "visibility:hidden");
                $("#cart-total").text();
                $("#total-pay").text();
                $("#payable").text();
            }
        }
    });

    $.ajax({
        type: "GET",
        url: "/Store/Orders/GetJSonCartSummary",
        success: function (result) {
            if (result.length != 0) {
                $("#tbody-cart tr").remove();
                $.each(result, function (i) {
                    var tbody = $("#tbody-cart");
                    var tr = $('<tr></tr>');
                    $('<td class="text-center"><a href="product.html"><img height="50" width="50" class="img-thumbnail" title="کفش راحتی مردانه" alt="کفش راحتی مردانه" src="../../Admin/Products/GetProductImageById/' + result[i].PictureId + '"></a></td>').appendTo(tr);
                    $('<td class="text-left"><a href="product.html">' + result[i].Order_title + '</a></td>').appendTo(tr);
                    $('<td class="text-right">' + result[i].quantity + 'عدد</td>').appendTo(tr);
                    $('<td class="text-right">' + result[i].price + 'تومان</td>').appendTo(tr);
                    $('<td class="text-center"><button class="btn btn-danger btn-xs remove" title="حذف" onClick="" type="button"><i class="fa fa-times"></i></button></td>').appendTo(tr);
                    tr.appendTo(tbody);
                })
            }
        }
    });

    //$.getJSON("../../../Store/Orders/GetJSonPaymentSummary", function (result) {
    //    alert(result);
    //    if (result.length != 0) {
    //        $("#btn-paymentsummary").attr("style", "visibility:visible")
    //        $("#cart-total").text(result.num_items + " آیتم " + " - " + result.total_price + " تومان ");
    //        $("#total-pay").text(result.total_price + " تومان ");
    //        $("#payable").text(result.total_price + " تومان ");
    //    } else {
    //        $("#cart").attr("style", "visibility:hidden");
    //        alert("hidden");
    //        $("#btn-paymentsummary").attr("style", "visibility:hidden");
    //        $("#cart-total").text();
    //        $("#total-pay").text();
    //        $("#payable").text();
    //    }
    //});
    //$.getJSON("../../../Store/Orders/GetJSonCartSummary", function (result) {
    //    $("#tbody-cart tr").remove();
    //    if (result.length != 0) {
    //        $.each(result, function (i) {
    //            var tbody = $("#tbody-cart");
    //            var tr = $('<tr></tr>');
    //            $('<td class="text-center"><a href="product.html"><img height="50" width="50" class="img-thumbnail" title="کفش راحتی مردانه" alt="کفش راحتی مردانه" src="../../Admin/Products/GetProductImageById/' + result[i].PictureId + '"></a></td>').appendTo(tr);
    //            $('<td class="text-left"><a href="product.html">' + result[i].Order_title + '</a></td>').appendTo(tr);
    //            $('<td class="text-right">' + result[i].quantity + 'عدد</td>').appendTo(tr);
    //            $('<td class="text-right">' + result[i].price + 'تومان</td>').appendTo(tr);
    //            $('<td class="text-center"><button class="btn btn-danger btn-xs remove" title="حذف" onClick="" type="button"><i class="fa fa-times"></i></button></td>').appendTo(tr);
    //            tr.appendTo(tbody);
    //        })
    //    }
    //});
}

function updatePaymentSummaryWithRedirect() {
    $.ajax({
        type: "GET",
        url: "../../../Store/Orders/GetJSonCartSummary",
        success: function (result) {
            if (result.length != 0) {
                $("#tbody-cart tr").remove();
                $.each(result, function (i) {
                    var tbody = $("#tbody-cart");
                    var tr = $('<tr></tr>');
                    $('<td class="text-center"><a href="product.html"><img height="50" width="50" class="img-thumbnail" title="کفش راحتی مردانه" alt="کفش راحتی مردانه" src="../../Admin/Products/GetProductImageById/' + result[i].PictureId + '"></a></td>').appendTo(tr);
                    $('<td class="text-left"><a href="product.html">' + result[i].Order_title + '</a></td>').appendTo(tr);
                    $('<td class="text-right">' + result[i].quantity + 'عدد</td>').appendTo(tr);
                    $('<td class="text-right">' + result[i].price + 'تومان</td>').appendTo(tr);
                    $('<td class="text-center"><button class="btn btn-danger btn-xs remove" title="حذف" onClick="" type="button"><i class="fa fa-times"></i></button></td>').appendTo(tr);
                    tr.appendTo(tbody);
                })
            }
        }
    });

    $.ajax({
        type: "GET",
        url: "../../../Store/Orders/GetJSonPaymentSummary",
        success: function (result) {
            if (result.length != 0) {
                //alert("if");
                $("#btn-paymentsummary").attr("style", "visibility:visible")
                $("#cart-total").text(result.num_items + " آیتم " + " - " + result.total_price + " تومان ");
                $("#total-pay").text(result.total_price + " تومان ");
                $("#payable").text(result.total_price + " تومان ");
            } else {
                //alert("else");
                $("#cart").attr("style", "visibility:hidden");
                //alert("hidden");
                $("#btn-paymentsummary").attr("style", "visibility:hidden");
                $("#cart-total").text();
                $("#total-pay").text();
                $("#payable").text();
                $(window).attr("location", "../../");
            }
        }
    });


}