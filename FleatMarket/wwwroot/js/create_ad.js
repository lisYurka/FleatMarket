//отмена добавления фотографии объявления(создание)
function abortDeclarImgUploadBtn() {
    $('#showDeclarPhoto').hide();
    $('#abortDeclarImgUpdateBtn').hide();

    var img = $("#declarPhoto").children();
    $(img).removeAttr("src");
    $('#sendImageToModel').val("");
    $(img).hide();
}