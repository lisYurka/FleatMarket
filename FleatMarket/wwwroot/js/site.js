// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function overCategoryBtn(id) {
    document.getElementById(id).style.backgroundColor = "#c8d2e3";
    document.getElementById(id).style.cursor = "pointer";
}

function outOfCategoryBtn(id) {
    document.getElementById(id).style.backgroundColor = "white";
}

function clickCategoryBtn(id) {
    //сделать
}

//менять цвет, есливыбрана стоимость или фри для продукта
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
//

//подтверждение перед удалением юзера(потом использовать, НЕ ЗАБЫТЬ!)
function confirmToRemove(id, isRemoving) {
    var del_span = '#deleteSpan_' + id;
    var conf_del_span = '#confirmDeleteSpan_' + id;

    if (isRemoving) {
        $(del_span).removeClass("d-inline").addClass("d-none");
        $(conf_del_span).removeClass("d-none").addClass("d-inline");
    }
    else {
        $(del_span).removeClass("d-none").addClass("d-inline");
        $(conf_del_span).removeClass("d-inline").addClass("d-none");
    }
}

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
    var loaderArr = $("div[name=RemoveLoader]");
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
            $(loaderArr[item]).show();
            delButtons.hide();
            changeButtons.hide();
            blockButtons.hide();
        },
        success: function () {
            string[item].remove();
            $(loaderArr[item]).hide();
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
    var regEx = /^[[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$/;
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
            var adminChangeUserMail = $('#adminChangeUserMail').val();
            var adminChangeUserName = $('#adminChangeUserName').val();
            var adminChangeUserSurname = $('#adminChangeUserSurname').val();
            var adminChangeUserPhone = $('#adminChangeUserPhone').val();
            
            $(".error").remove();

            var validEmail = regEx.test(adminChangeUserMail);

            if (adminChangeUserMail.length < 1) {
                $('#adminChangeUserMail').after('<span class="error"><div id="colorRed">Проверьте правильность заполнения поля "EMail"</div></span>');
            }
            if (adminChangeUserName.length < 1) {
                $('#adminChangeUserName').after('<span class="error"><br /><div id="colorRed">Проверьте правильность заполнения поля "Имя"</div></span>');
            }
            if (adminChangeUserSurname.length < 1) {
                $('#adminChangeUserSurname').after('<span class="error"><br /><div id="colorRed">Проверьте правильность заполнения поля "Фамилия"</div></span>');
            }
            if (adminChangeUserPhone.length < 1) {
                $('#adminChangeUserPhone').after('<span class="error"><div id="colorRed">Проверьте правильность заполнения поля "Телефон"</div></span>');
            }
            else if (!validEmail) {
                $('#adminChangeUserMail').after('<span class="error"><div id="colorRed">Проверьте правильность заполнения поля "EMail"</div></span>');
            }
            else {
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

//валидация регистрации
(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();