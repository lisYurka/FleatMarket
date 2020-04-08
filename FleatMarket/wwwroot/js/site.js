// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function changeAllCategColor(bool) {
    if (bool == true) {
        document.getElementById("showDeclWithAllCateg").style.fontWeight = "bold";
        document.getElementById("showDeclWithAllCateg").style.color = "#0ced66";
        document.getElementById("showDeclWithAllCateg").style.cursor = "pointer";
    }
    else {
        document.getElementById("showDeclWithAllCateg").style.fontWeight = "normal";
        document.getElementById("showDeclWithAllCateg").style.color = "#000000";
    }
}

function changeAllStatsColor(bool) {
    if (bool == true) {
        document.getElementById("showDeclWithAllStats").style.fontWeight = "bold";
        document.getElementById("showDeclWithAllStats").style.color = "#faa405";
        document.getElementById("showDeclWithAllStats").style.cursor = "pointer";
    }
    else {
        document.getElementById("showDeclWithAllStats").style.fontWeight = "normal";
        document.getElementById("showDeclWithAllStats").style.color = "#000000";
    }
}

function changePhDwnlBtn(bool) {
    if (bool == true) {
        document.getElementById("downloadUserPhoto").style.backgroundColor = "#4cd4a9";
        document.getElementById("downloadUserPhoto").style.color = "#ffffff";
    }
    else {
        document.getElementById("downloadUserPhoto").style.backgroundColor = "#ffffff";
        document.getElementById("downloadUserPhoto").style.color = "#4cd4a9";
    }
}

function changeEditUserBtn(bool) {
    if (bool == true) {
        document.getElementById("updateUserProfileBtn").style.backgroundColor = "#f0d037";
        document.getElementById("updateUserProfileBtn").style.color = "#ffffff";
    }
    else {
        document.getElementById("updateUserProfileBtn").style.backgroundColor = "#ffffff";
        document.getElementById("updateUserProfileBtn").style.color = "#f0d037";
    }
}

function overCategoryBtn(id) {
    document.getElementById("category_element_"+id).style.color = "#0ced66";
    document.getElementById("category_element_" +id).style.cursor = "pointer";
}

function outOfCategoryBtn(id) {
    document.getElementById("category_element_" +id).style.color = "#000000";
}

function overStatusBtn(id) {
    document.getElementById("status_element_"+id).style.color = "#faa405";
    document.getElementById("status_element_"+id).style.cursor = "pointer";
}

function outOfStatusBtn(id) {
    document.getElementById("status_element_" +id).style.color = "#000000";
}

//менять цвет, если выбрана стоимость или фри для продукта
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

//кнопка "Подтвердить"(при изменении юзера) ДОДЕЛАТЬ ВАЛИДАЦИЮ
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

//валидация регистрации(ДОДЕЛАТЬ)
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

//получаем список дат между выбранными датами
var getDates = function (startDate, endDate) {
    var dates = [],
        currentDate = startDate,
            addDays = function (days) {
                var date = new Date(this.valueOf());
                //date.setHours(0,0,0,0);
                date.setDate(date.getDate() + days);
                return date;
            };
    if (endDate > currentDate) {
        while (currentDate <= endDate) {
            dates.push(currentDate);
            currentDate = addDays.call(currentDate, 1);
        }
    }
    else {
        while (currentDate >= endDate) {
            dates.push(endDate);
            endDate = addDays.call(endDate, 1);
        }
    }
    return dates;
};

//создание даты
function createDate(date) {
    var day = date.split(".")[0];
    var month = date.split(".")[1];
    var year = date.split(".")[2];
    return new Date(year, month-1, day);
}

//статус объявления "Продано"
function soldStatus() {
    var declarActions = $('.declarActions');

    $('#SoldDeclarationId').val(event.target.id);
    var form = $('#SoldDeclarationForm');
    var id = $('#SoldDeclarationId').val();
    var status = $('#statusName_' + id);
    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function () {
            status.text("Продано");
            $(declarActions[id]).hide();
            $('.SoldDeclarationBtn').hide();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText);
        }
    });
}

//выбран статус объявления "Удалено"
function deleteStatus() {
    var declarActions = $('.declarActions');

    $('#RemoveDeclarationId').val(event.target.id);
    $('#RemoveThisDeclaration').on('click', function () {
        var form = $('#RemoveDeclarationForm');
        var id = $('#RemoveDeclarationId').val();
        var partialPost = $('.OneDeclaration');
        var status = $('#statusName_' + id);
        partialPost.each(function (index) {
            if (partialPost[index].id == id)
                id = index;
        });
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function () {
                status.text("Удалено");
                $(declarActions[id]).hide();
                $(partialPost[id]).hide();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.responseText);
            }
        });
    });
}

//удаление объявления из базы данных(для админа)
function deleteFromDb() {
    var declarActions = $('.declarActions');

    $('#DeleteDeclarId').val(event.target.id);
    $('#DeleteThisDeclarFromDb').on('click', function () {
        var form = $('#DeleteDeclarFromDbForm');
        var id = $('#DeleteDeclarId').val();
        var partialPost = $('.OneDeclaration');
        partialPost.each(function (index) {
            if (partialPost[index].id == id)
                id = index;
        });
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function () {
                $(declarActions[id]).hide();
                $(partialPost[id]).remove();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.responseText);
            }
        });
    });
}

//редактирование инфы пользователя
function updateUserAction() {
    var currentData = $('.currentData');
    var profileInput = $('.profileInput');
    var form = $('#userUpdateForm');
    var saveUserProfileChangesBtn = $('#saveUserProfileChangesBtn');
    var updateUserProfileBtn = $('#updateUserProfileBtn');

    updateUserProfileBtn.hide();
    currentData.hide();
    profileInput.show();
    saveUserProfileChangesBtn.show();

    saveUserProfileChangesBtn.on('click', function () {
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function () {

                for (var i = 0; i < currentData.length; i++) {
                    $(currentData[i]).text($(profileInput[i]).val());
                }

                saveUserProfileChangesBtn.hide();
                profileInput.hide();
                updateUserProfileBtn.show();
                currentData.show();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.responseText);
            }
        });
    });
}

//динамическая подгрузка объявлений
function InfiniteScroll(loadingIndicator, scrolList, url, type, dop)
{
    loadingIndicator.hide();
    var page = 0;
    var _inCallback = false;
    if (dop == undefined)
        dop = "";
    function loadItems() {
        if (page > -1 && !_inCallback) {
            _inCallback = true;
            page++;
            loadingIndicator.show();
            console.log(url + page + dop);

            //подгрузка недостающих объявлений
            $.ajax({
                type: type,
                url: url + page + dop,
                success: function (data) {
                    if (data != '') {
                        $(scrolList).append(data);
                    }
                    else {
                        page = -1;
                    }
                    _inCallback = false;
                    loadingIndicator.hide();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
    }
    //если достигнули конца скролла,объявления остались => загрузить
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            loadItems();
        }
    });
}

//передать id объявления для его редактирования
function sendDeclarId() {
    $('#declar_Id').val(event.target.id);
}

//показать текущую категорию в редактировании
function showCategoryColor() {
    var id = $('.showOldCategory').attr('id');
    document.getElementById("category_element_" + id).style.color = "#0ced66";
    document.getElementById("category_element_" + id).style.backgroundColor = "#59bbf4";

    if (id == 5) {
        $('#pricedProduct').attr("disabled", true);
        checkForFree();
    }
    else {
        $('#pricedProduct').attr("disabled", false);
        checkForPrice();
    }
}

document.addEventListener('DOMContentLoaded', function () {

    //для показа выбранной категории при редактировании
    $('[data-onload]').each(function () {
        eval($(this).data('onload'));
    });

    //для выбора профиля или объявления
    $('[data-load]').each(function () {
        eval($(this).data('load'));
    });

    //InfiniteScroll($('#LoadingPostPreview'), $('#PostPreviewScrolList'), '/Home/Index?id=', 'GET', '');

    //мои объявления
    $('#myDeclarations').on('click', function () {
        $('#myProfileAction').hide();
        $('#deletedDeclarationAction').hide();
        $('#myDeclarationAction').load("/User/GetUserDeclarations");
        $('#myDeclarationAction').show();
    });

    //мой профиль
    $('#myProfile').on('click', function () {
        $('#myDeclarationAction').hide();
        $('#deletedDeclarationAction').hide();
        $('#myProfileAction').load("/User/MyProfile");
        $('#myProfileAction').show();
    });

    $('#deletedDeclarations').on('click', function () {
        $('#myProfileAction').hide();
        $('#myDeclarationAction').hide();
        $('#deletedDeclarationAction').load("/User/RemovedDeclarations");
        $('#deletedDeclarationAction').show();
    });

    //открыть объявление
    $('body').on('click', '.OneDeclaration', function () {
        $('#DeclarationId').val(this.id);
        $("#DeclarationOpen").submit();
    });

    //поиск по дате
    $('#searchByDate').on('click', function () {
        $('.firstDateToSearch').on('click', function () {
            $(".error").remove();
        });
        $('.secondDateToSearch').on('click', function () {
            $(".error").remove();
        });
        var firstDateToSearch = $('.firstDateToSearch').val();
        var secondDateToSearch = $('.secondDateToSearch').val();
        if (firstDateToSearch == "") {
            $('.firstDateToSearch').after('<span class = "text-danger d-flex justify-content-center error">Выберите дату!</span>');
        }
        else if (secondDateToSearch == "") {
            $('.secondDateToSearch').after('<span class = "text-danger d-flex justify-content-center error">Выберите дату!</span>');
        }
        else {//если все окай
            var dates = getDates(new Date(firstDateToSearch), new Date(secondDateToSearch));
            var declarationDates = $('.declarationDate');
            var declaration = $('.OneDeclaration');
            declarationDates.each(function (index) {
                var date = $(declarationDates[index]).attr('id').split(" ")[0];//дата обявления(без времени)
                var declarationDate = createDate(date);
                declarationDate.setHours(3, 0, 0, 0);
                $(declaration[index]).hide();
                dates.forEach(function (date) {
                    if (date.getTime() == declarationDate.getTime()) {
                        $(declaration[index]).show();
                    }
                });
            });
        }
    });

    //выбор категории товара при создании объявления
    $('.ChooseCategory').on('click', function () {
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
        var id_old = $('.showOldCategory').attr('id');
        document.getElementById("category_element_" + id_old).style.backgroundColor = "#ffffff";
        document.getElementById("category_element_" + id_old).style.color = "#000000";
        $(".lastClicked").removeClass("lastClicked");
        $(this).addClass("lastClicked");
    });

    //поиск по категориям
    $('.categoryLink').on('click', function () {
        var id = $(this).attr('id').replace('category_element_', '');

        var declaration = $('.OneDeclaration');
        var partialDeclarCategory = $('.declarationCategory');
        partialDeclarCategory.each(function (index) {
            if (partialDeclarCategory[index].id != id) {
                $(declaration[index]).hide();
            }
            else {
                $(declaration[index]).show();
            }
        });
    });

    //если нажато "Все категории"
    $('#showDeclWithAllCateg').on('click', function () {
        var declaration = $('.OneDeclaration');
        declaration.each(function (index) {
            $(declaration[index]).show();
        });
    });

    //поиск по статусам
    $('.statusLink').on('click', function () {
        var id = $(this).attr('id').replace('status_element_', '');
        var declaration = $('.OneDeclaration');
        var partialDeclarCategory = $('.declarationStatus');
        partialDeclarCategory.each(function (index) {
            if (partialDeclarCategory[index].id != id) {
                $(declaration[index]).hide();
            }
            else {
                $(declaration[index]).show();
            }
        });
    });

    //если нажато "Все статусы"
    $('#showDeclWithAllStats').on('click', function () {
        var declaration = $('.OneDeclaration');
        declaration.each(function (index) {
            $(declaration[index]).show();
        });
    });

    //при сохранении изменений объявления найти его по id
    $('#successEditDeclaration').on('click', function () {
        var a = $('.declarationId').attr('id');
        $('#editDeclarationId').val(a);

        var isChooseCategory = $('#choosenCategoryId').val();
        if (isChooseCategory == "") {
            var b = $('.showOldCategory').attr('id');
            $('#choosenCategoryId').val(b);
        }
    });

});