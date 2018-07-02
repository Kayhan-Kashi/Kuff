
function removeOrderItem(orderItemId, counter) {
    // alert(counter);

    $.ajax({
        type: "GET",
        url: "RemoveOrderItemFromSession?orderItemId=" + orderItemId,
        success: function (result) {
            updatePrice(counter);
            //alert("[data-remove=" + counter + "]");
            $("#orderItem_" + counter).remove();
            $("[data-remove=" + counter + "]").remove();
            updatePaymentSummaryWithRedirect();
        }
    });

}


function updatePrice(itemNumber) {
    var itemPrice = $("#OrderItems_" + itemNumber + "__QuantityXPrice").val();
    var oldTotalPrice = $("#FinalPriceWithoutShipmentCost").val();
    var newTotalPrice = oldTotalPrice - itemPrice;
    $("#FinalPriceWithoutShipmentCost_output").text(newTotalPrice);
    $("#TotalPriceWithEachOrderItemDiscount_output").text(newTotalPrice);
    $("#FinalPriceWithoutShipmentCost").val(newTotalPrice);
    $("#TotalPriceWithEachOrderItemDiscount").val(newTotalPrice);
}