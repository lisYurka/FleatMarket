//поменять цвет кнопки
function changeBtnColor(id, color) {
    document.getElementById(id).style.backgroundColor = color;
    document.getElementById(id).style.color = "#ffffff";
}
function returnBtnColor(id,color) {
    document.getElementById(id).style.backgroundColor = "#ffffff";
    document.getElementById(id).style.color = color;
}
function overElementInChoose(id) {
    document.getElementById(id).style.cursor = "pointer";
    document.getElementById(id).style.backgroundColor = "#e1e5eb";
}
function outOfElementInChoose(id) {
    document.getElementById(id).style.backgroundColor = "#edeef0";
}

//для каждого элемента при поиске по категориям или статусам 
function overElementInSearch(id, color) {
    document.getElementById(id).style.color = color;
    document.getElementById(id).style.cursor = "pointer";
}
function outOfElementInSearch(id) {
    document.getElementById(id).style.color = "#000000";
}

//менять цвет, если выбрана стоимость или фри для продукта(создание/редактирование)
function checkForFree() {
    document.getElementById("pricedProduct").style.backgroundColor = "white";
    document.getElementById("pricedProduct").style.color = "#4cd4a9";
    document.getElementById("freeProduct").style.backgroundColor = "#4cd4a9";
    document.getElementById("freeProduct").style.color = "white";

    document.getElementById("priceInputLabel").style.display = "none";
}
function checkForPrice() {
    document.getElementById("pricedProduct").style.backgroundColor = "#4cd4a9";
    document.getElementById("pricedProduct").style.color = "white";
    document.getElementById("freeProduct").style.backgroundColor = "white";
    document.getElementById("freeProduct").style.color = "#4cd4a9";

    document.getElementById("priceInputLabel").style.display = "flex";
}

//удаление объявления из базы данных(для админа)
function deleteFromDb() {
    $('#DeleteDeclarId').val(event.target.id);
}

//выбрать фотографию для объявления(создание/редактирование)
var oldDeclarPhoto;
function openFileFolder() {
    $('#openFolder').click();
    $('#showDeclarPhoto').show();
    $('#abortDeclarImgUpdateBtn').show();

    var img = $("#declarPhoto").children();
    oldDeclarPhoto = $(img).attr("src");
}

//загрузить выбранную фотографию для объявления(создание/редактирование)
var declarImg;
function showDeclarPhoto() {
    var loadPhotoInput = $('#openFolder');
    if (loadPhotoInput.prop('files').length) {
        var formData = new FormData();
        formData.append('file', loadPhotoInput.prop('files')[0]);
        $.ajax({
            type: $('#uploadDeclarPhotoForm').attr('method'),
            url: $('#uploadDeclarPhotoForm').attr('action'),
            processData: false,
            contentType: false,
            data: formData,
            success: function (data) {
                $('#sendImageToModel').val(data);
                var img = $("#declarPhoto").children();
                $(img).attr("src", data);
                declarImg = data;
                $(img).show();
            },
            error: function () { alert("Файл не отправлен!") }
        });
    }
    else {
        alert("Сперва выберите фотографию!");
    }
}

// валидация создания/редактирования объявления
function checkNewDeckarForValid() {
    var title = $('#declar_title').val();
    var category = $('#choosenCategoryId').val();
    var description = $('#declar_description').val();
    var price = $('#inputPrice').val();

    var pricePattern = /^\d+(,\d{2})?$/;
    var isPriceValid = pricePattern.test(price);

    $('#declTitleError').hide();
    $('#declarCategoryError').hide();
    $('#declDescrError').hide();
    $('#declarPriceError').hide();

    var flag = true;
    //название
    if (title.length == 0) {
        document.getElementById('declTitleError').innerHTML = "Название не должно быть пустым!";
        $('#declTitleError').removeClass('text-success').addClass('text-danger');
        document.getElementById('declar_title').style.borderColor = "#e12d2d";
        $('#declTitleError').show();
        flag = false;
    }
    else {
        document.getElementById('declTitleError').innerHTML = "Отлично!";
        $('#declTitleError').removeClass('text-danger').addClass('text-success');
        document.getElementById('declar_title').style.borderColor = "#30da49";
        $('#declTitleError').show();
    }
    //категория выбрана
    if (category == "") {
        document.getElementById('declarCategoryError').innerHTML = "Категория не выбрана!";
        $('#declarCategoryError').removeClass('text-success').addClass('text-danger');
        document.getElementById('category_choose').style.borderColor = "#e12d2d";
        $('#declarCategoryError').show();
        flag = false;
    }
    else {
        document.getElementById('declarCategoryError').innerHTML = "Отлично!";
        $('#declarCategoryError').removeClass('text-danger').addClass('text-success');
        document.getElementById('category_choose').style.borderColor = "#30da49";
        $('#declarCategoryError').show();
    }
    //описание
    if (description.length == 0) {
        document.getElementById('declDescrError').innerHTML = "Описание не должно быть пустым!";
        $('#declDescrError').removeClass('text-success').addClass('text-danger');
        document.getElementById('declar_description').style.borderColor = "#e12d2d";
        $('#declDescrError').show();
        flag = false;
    }
    else {
        document.getElementById('declDescrError').innerHTML = "Отлично!";
        $('#declDescrError').removeClass('text-danger').addClass('text-success');
        document.getElementById('declar_description').style.borderColor = "#30da49";
        $('#declDescrError').show();
    }
    //цена
    if (!isPriceValid) {
        document.getElementById('declarPriceError').innerHTML = "Пример правильного заполнения: 49,99 (запятая-разделитель)";
        $('#declarPriceError').removeClass('text-success').addClass('text-danger');
        document.getElementById('inputPrice').style.borderColor = "#e12d2d";
        $('#declarPriceError').show();
        flag = false;
    }
    else {
        document.getElementById('declarPriceError').innerHTML = "Отлично!";
        $('#declarPriceError').removeClass('text-danger').addClass('text-success');
        document.getElementById('inputPrice').style.borderColor = "#30da49";
        $('#declarPriceError').show();
    }

    if (flag == true) return true;
    else return false;
}

document.addEventListener('DOMContentLoaded', function () {

    //открыть объявление
    $('body').on('click', '.OneDeclaration', function () {
        $('#DeclarationId').val(this.id);
        $("#DeclarationOpen").submit();
    });

    //выбор категории товара при создании/редактировании объявления
    $('.ChooseCategory').on('click', function () {
        var id_old = $('.showOldCategory').attr('id');
        if (id_old != "") {
            var category_id = $(this).attr("id").replace("category_element_", "");
            $('#choosenCategoryId').val(category_id);

            if (category_id == 5) {
                $('#pricedProduct').attr("disabled", true);
                checkForFree();
            }
            else {
                $('#pricedProduct').attr("disabled", false);
                checkForPrice();
            }
            $(".lastClicked").removeClass("lastClicked");
            $(this).addClass("lastClicked");
            id_old = ""
        }
        if (id_old == "") {
            document.getElementById("category_element_" + id_old).style.backgroundColor = "#ffffff";
            document.getElementById("category_element_" + id_old).style.color = "#000000";
        }
    });

    //кнопка "Изменить данные"
    $('.UserData').on('click', '.ChangeUserButton', function () {
        var string = $('.UserData');
        var loaderArr = $("div[name=RemoveLoader]");
        var delButtons = $('.DeleteUserButton');
        var changeButtons = $('.ChangeUserButton');
        var blockButtons = $('.BlockUserButton');
        var item;
        string.each(function (index) {
            if (string[index].id == event.target.id)
                item = index;
        });
        $('#InputStringId').val(event.target.id);
        $.ajax({
            type: $('#InputUser').attr('method'),
            url: $('#InputUser').attr('action'),
            data: $('#InputUser').serialize(),
            beforeSend: function () {
                $(loaderArr[item]).show();
                delButtons.hide();
                changeButtons.hide();
                blockButtons.hide();
            },
            success: function (data) {
                $(string[item]).html(data);
                $(loaderArr[item]).hide();
            },
            error: function () { alert("Данные не отправлены!") }
        });
    });

    //кнопка "Изменить статус"
    $('.UserData').on('click', '.BlockUserButton', function () {
        $('#BlockUserId').val(event.target.id);
        var item;
        var status = $('.BlockStatus');
        status.each(function (index) {
            if (status[index].id == event.target.id)
                item = index;
        });
        $.ajax({
            type: $('#BlockUserForm').attr('method'),
            url: $('#BlockUserForm').attr('action'),
            data: $('#BlockUserForm').serialize(),
            success: function (data) {
                $(status[item]).text(data);
            },
            error: function () { alert("Данные не отправлены!") }
        });
    });

    //кнопка "Удалить"(юзера)
    $('.UserData').on('click', '.DeleteUserButton', function () {
        var string = $('.UserData');
        var delButtons = $('.DeleteUserButton');
        var changeButtons = $('.ChangeUserButton');
        var blockButtons = $('.BlockUserButton');
        var item;
        string.each(function (index) {
            if (string[index].id == event.target.id)
                item = index;
        });
        $('#DeleteUserId').val(event.target.id);
        $.ajax({
            type: $('#DeleteUserForm').attr('method'),
            url: $('#DeleteUserForm').attr('action'),
            data: $('#DeleteUserForm').serialize(),
            beforeSend: function () {
                delButtons.hide();
                changeButtons.hide();
                blockButtons.hide();
            },
            success: function () {
                string[item].remove();
                delButtons.show();
                changeButtons.show();
                blockButtons.show();
            },
            error: function () { alert("Данные не отправлены!") }
        });
    });

    //кнопка "Подтвердить"(при изменении юзера)
    $('.UserData').on('click', 'input[name=UpdateUserButtonAction]', function () {
        var string = $('.UserData');
        var loaderArr = $("div[name=RemoveLoader]");
        var delButtons = $('.DeleteUserButton');
        var changeButtons = $('.ChangeUserButton');
        var blockButtons = $('.BlockUserButton');
        var item;

        string.each(function (index) {
            if (string[index].id == event.target.id)
                item = index;
        });
        $.ajax({
            type: $('#UpdateUserForm').attr('method'),
            url: $('#UpdateUserForm').attr('action'),
            data: $('#UpdateUserForm').serialize(),
            beforeSend: function () {
                $(loaderArr[item]).show();
            },
            success: function (data) {
                var test = checkEditUserAdminValid();
                if (test) {
                    $(string[item]).html(data);
                    $(loaderArr[item]).hide();
                    delButtons.show();
                    changeButtons.show();
                    blockButtons.show();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.responseText);
            }
        });
    });
});