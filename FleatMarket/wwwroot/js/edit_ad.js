//отмена добавления фотографии объявления
function abortDeclarImgUpdateBtn() {
    $('#showDeclarPhoto').hide();
    $('#abortDeclarImgUpdateBtn').hide();

    var img = $("#declarPhoto").children();
    $(img).attr("src", oldDeclarPhoto);
    $('#sendImageToModel').val(oldDeclarPhoto);
}

//показать текущую категорию
function showCategoryColor() {
    var id = $('.showOldCategory').attr('id');
    document.getElementById("category_element_" + id).style.color = "#0ced66";
    document.getElementById("category_element_" + id).style.backgroundColor = "#e6940f";

    if (id == 5) {
        $('#pricedProduct').attr("disabled", true);
        checkForFree();
    }
    else {
        $('#pricedProduct').attr("disabled", false);
        checkForPrice();
    }
}

function saveAdAchanges() {
    var a = $('.declarationId').attr('id');
    $('#editDeclarationId').val(a);

    var isChooseCategory = $('#choosenCategoryId').val();
    if (isChooseCategory == "") {
        var b = $('.showOldCategory').attr('id');
        $('#choosenCategoryId').val(b);
    }
}

document.addEventListener('DOMContentLoaded', function () {
    //для показа выбранной категории при редактировании
    $('[data-onload]').each(function () {
        eval($(this).data('onload'));
    });
});