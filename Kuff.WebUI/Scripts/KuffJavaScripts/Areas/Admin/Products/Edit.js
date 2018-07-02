var transferredCounter;
var numOfAddedPic = 0;

function undoChange(counter, picId) {
    $("#img_" + counter).attr("src", "/Admin/Products/GetProductImageById/" + picId);
    $("#files_" + counter).attr("style", "visibility:hidden");
}

function showFileInput(counter) {
    $("#files_" + counter).attr("style", "visibility:visible");
}

function showPicInImg(event, counter) {
    var output = document.getElementById("img_" + counter);
    output.src = URL.createObjectURL(event.target.files[0]);
}

function addImage() {
    $("#imagePanel").append(
        '<div class="panel panel-default" id="imageInnerPanel_' + transferredCounter + '" >' +
        '<label style="margin:10px">' + (transferredCounter + 1) + ' تصویر شماره </label>' +
            '<div class="panel-body">' +
                '<div class="form-group">' +
                    '<img id="img_' + transferredCounter + '" src="" alt="no picture" style="padding:5px;display:block;border:1px solid black;" height="200px" width="200px" />' +
                    '<input  type="checkbox" dir="rtl" id="ProductPictures_' + transferredCounter + '__IsForSummaryPreview"' +
                        'name="ProductPictures[' + transferredCounter + '].IsForSummaryPreview" style="padding:5px;" value="true">' +
                    '<input name="ProductPictures[' + transferredCounter + '].IsForSummaryPreview" value="false" type="hidden">' +
                    '<label dir="rtl" for="ProductPictures[' + transferredCounter + '].IsForSummaryPreview" style="padding:5px;">آیا برای نمایش خلاصه استفاده شود ؟ </label>' +
                    '<br/>' +
                    '<input  dir="rtl" id="ProductPictures_' + transferredCounter + '__IsMainPicture" name="ProductPictures[' + transferredCounter + '].IsMainPicture" style="padding:5px;" value="true" type="checkbox" />' +
                    '<input name="ProductPictures[' + transferredCounter + '].IsMainPicture" value="false" type="hidden">' +
                    '<label dir="rtl" for="ProductPictures[' + transferredCounter + '].IsMainPicture" style="padding:5px;">آیا به عنوان تصویر اصلی استفاده شود ؟ </label>' +
                    '<br>' +
                    '<label for="ProductPictures[' + transferredCounter + '].PictureOrder">ترتیب عکس</label>' +
                    '<select class="form-control" dir="rtl" id="ProductPictures_' + transferredCounter + '__PictureOrder"' +
                        'name="ProductPictures[' + transferredCounter + '].PictureOrder">' +
                        '<option selected="selected" value="' + transferredCounter + '">' + transferredCounter + '</option>' +
                     '</select>' +
                     '<br/>' +
                     '<input type="file" id="files_' + transferredCounter + '" name="files[' + transferredCounter + ']" style="margin-left:5px;margin-top:10px;" ' +
                        'onchange="showPicInImg(event, ' + transferredCounter + ')" />' +
                  '</div>' +
               '</div>' +
            '</div>' +
        '</div>'
        );
    transferredCounter++;
    numOfAddedPic++;
}

function removeImage() {
    if (numOfAddedPic > 0) {
        $("#imageInnerPanel_" + --transferredCounter).remove();
        numOfAddedPic--;
    }
}

function returnToDefaultValueById(propertyValueId, elementId) {
    $.ajax({
        type: "GET",
        url: "../GetPropertyValueById/" + propertyValueId,
        success: function (result) {
            $("#" + elementId).val(result);
        }
    });
}

function returnToDefaultValueByTitle(productId, propertyTitle, elementId) {
    $.ajax({
        type: "GET",
        url: "../GetProductPropertyByTitle/" + productId + "/" + propertyTitle,
        success: function (result) {
            $("#" + elementId).val(result);
        }
    });
}

function setPicState(i) {
    alert();
    var value = $('input[type="radio"][name="changeRadio_' + i + '"]:checked').val();
    alert(value);
    $("#picStateHidden_" + i).val(value);
}
