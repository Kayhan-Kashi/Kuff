
var picCounter = 0;

function getProductTypePropertiesWithPost(productId) {
    $.ajax({
        type: "POST",
        url: "GetProductTypePropertiesInputFormWithPost/" + $("#dropdownPType").val(),
        data: { prodId: productId },
        success: function (result) {
            picCounter = 0;
            $("#ProdPropsContainer").html("");
            $("#image_container").html("");
            $("#ProdPropsContainer").prepend(result);
            $("#ProdPropsContainer").append(
                '<div>' +
                    '<input type="button" name="addImage"  id="addImage" class="btn btn-primary"' +
                        'value="Add Image" style="margin:20px" onclick="addImagePanel()" />' +
                    '<input type="button" name="removeImage" id="removeImage" value="Remove image"' +
                        'class="btn btn-danger" onclick="removeImagePanel()" />' +
                '</div>'
                );

            $("#ProdPropsContainer").append(
                '<div class="panel panel-default" style="padding:20px">' +
                    '<div id="summary_comments_container" class="form-group">' +
                        '<label for="Summary" dir="rtl">خلاصه تیتر محصول</label>' +
                        '<input type="text" name="Summary" class="form-control" style="margin:20px" />' +
                        '<label for="Comments" dir="rtl">توضیحات محصول</label>' +
                        '<textarea name="Comments" class="form-control" cols="20" rows="2" style="margin:20px"></textarea>' +
                    '</div>' +
                '</div>'
                );

            $("#submit_div").css({ "visibility": "visible" });
        }
    });
}

function addImagePanel() {
    if (picCounter >= 0) {
        $("#image_container").append(
    '<div id="image_panel_' + picCounter + '" class="panel panel-default" style="padding:10px">' +
        '<div class="panel-heading text-center">تصویر ' + (picCounter + 1) + '</div>' +

        '<div id="input_image_' + picCounter + '">' +   
            '<img id="image_' + picCounter + '" alt="image_' + picCounter + '" src="" width="300" height="300" style="margin:20px;border:1px solid black;"/>' +
            '<input type="file" name="files[' + picCounter + ']" id="files[' + picCounter + ']" value="browse" onchange="loadFile(event,' + picCounter + ')" />' +
        '</div>' +

        '<div class="form-group">' +
            '<label dir="rtl" for="ProductPictures[' + picCounter + '].IsMainPicture" style="padding:5px;">آیا به عنوان تصویر اصلی استفاده شود ؟ </label>' +
            '<input  dir="rtl" id="ProductPictures_' + picCounter + '__IsMainPicture" name="ProductPictures[' + picCounter + '].IsMainPicture" style="padding:5px;margin:5px" value="true" type="checkbox" />' +
        '</div>' +

        '<div class="form-group">' +
            '<label dir="rtl" for="ProductPictures[' + picCounter + '].IsForSummaryPreview" style="padding:5px;">آیا برای نمایش خلاصه استفاده شود ؟ </label>' +
            '<input  type="checkbox" dir="rtl" id="ProductPictures_' + picCounter + '__IsForSummaryPreview"' +
            'name="ProductPictures[' + picCounter + '].IsForSummaryPreview" style="padding:5px;" value="true">' +
        '</div>' +
    '</div>'
    );
        picCounter++;
    }
}

function removeImagePanel() {
    if (picCounter > 0) {
        picCounter--;
        $('#image_panel_' + picCounter).remove();
    }
}

function loadFile(event, counter) {
    var output = document.getElementById('image_' + counter);
    output.src = URL.createObjectURL(event.target.files[0]);
}

function getProductTypeProperties() {
    $.ajax({
        url: "GetProductTypePropertiesInputForm/" + $("#dropdownPType").val(), success: function (result) {
            picCounter = 0;
            $("#ProdPropsContainer").html("");
            $("#image_container").html("");
            $("#ProdPropsContainer").prepend(result);
            $("#ProdPropsContainer").append(
                '<div>' +
                    '<input type="button" name="addImage"  id="addImage" class="btn btn-primary"' +
                        'value="Add Image" style="margin:20px" onclick="addImagePanel()" />' +
                    '<input type="button" name="removeImage" id="removeImage" value="Remove image"' +
                        'class="btn btn-danger" onclick="removeImagePanel()" />' +
                '</div>'
                );

            $("#ProdPropsContainer").append(
                '<div class="panel panel-default" style="padding:20px">' +
                    '<div id="summary_comments_container" class="form-group">' +
                        '<label for="Summary" dir="rtl">خلاصه تیتر محصول</label>' +
                        '<input type="text" name="Summary" class="form-control" style="margin:20px" />' +
                        '<label for="Comments" dir="rtl">توضیحات محصول</label>' +
                        '<textarea name="Comments" class="form-control" cols="20" rows="2" style="margin:20px"></textarea>' +
                    '</div>' +
                '</div>'
                );

            $("#submit_div").css({ "visibility": "visible" });
        }
    });
}