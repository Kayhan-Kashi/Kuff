function calculateCost(shipmentCost, totalCost) {
    $("#shipmentCost").text(shipmentCost + " تومان ");
    $("#totalCost").text((totalCost + shipmentCost) + " تومان ");
    $("#totalcost-input").val((totalCost + shipmentCost));
    var final = totalCost + shipmentCost;
    $("#FinalPriceWithShipmentCost_hidden").val(final);
    $("#ShipmentCostId_Hidden").val(shipmentCost);

}

var radio = $('input[name="ShipmentCostId"]:checked');
$('input[type="radio"][name="ShipmentCostId"]:checked').trigger("change");
$('input[type="radio"][name="ShipmentCostId"]:checked').trigger("click");



