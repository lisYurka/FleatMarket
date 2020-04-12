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
        document.getElementById("uploadUserPhoto").style.backgroundColor = "#f0d037";
        document.getElementById("uploadUserPhoto").style.color = "#ffffff";
    }
    else {
        document.getElementById("uploadUserPhoto").style.backgroundColor = "#ffffff";
        document.getElementById("uploadUserPhoto").style.color = "#f0d037";
    }
}

function uploadPhotoBtn(bool) {
    if (bool == true) {
        document.getElementById("showUserPhoto").style.backgroundColor = "#1a19b8";
        document.getElementById("showUserPhoto").style.color = "#ffffff";
    }
    else {
        document.getElementById("showUserPhoto").style.backgroundColor = "#ffffff";
        document.getElementById("showUserPhoto").style.color = "#1a19b8";
    }
}

function savePhotoBtn(bool) {
    if (bool == true) {
        document.getElementById("saveUserPhotoBtn").style.backgroundColor = "#0cb634";
        document.getElementById("saveUserPhotoBtn").style.color = "#ffffff";
    }
    else {
        document.getElementById("saveUserPhotoBtn").style.backgroundColor = "#ffffff";
        document.getElementById("saveUserPhotoBtn").style.color = "#0cb634";
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

//кнопка "Подтвердить"(при изменении юзера), функция валидации ниже(за 800 строкой и дальше смотри)
$('.UserData').on('click', 'input[name=UpdateUserButtonAction]', function () {
    var string = $('.UserData');
    var loaderArr = $("div[name=RemoveLoader]");
    var delButtons = $('.DeleteUserButton');
    var changeButtons = $('.ChangeUserButton');
    var blockButtons = $('.BlockUserButton');
    var item;

    $('#usernameError').hide();
    $('#usersurnameError').hide();
    $('#userphoneError').hide();

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

                $('#usernameError').hide();
                $('#usersurnameError').hide();
                $('#userphoneError').hide();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText);
        }
    });
});

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
    var soldBtn = $('.SoldDeclarationBtn');
    $('#SoldDeclarationId').val(event.target.id);
    var form = $('#SoldDeclarationForm');
    var id = $('#SoldDeclarationId').val();
    var status = $('#statusName_' + id);
    soldBtn.each(function (index) {
        if (soldBtn[index].id == id)
            id = index;
    });
    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function () {
            status.text("Продано");
            $(soldBtn[id]).hide();
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

//редактирование инфы пользователя в личном кабинете
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

    $('#userNameError').hide();
    $('#userSurnameError').hide();
    $('#userPhoneError').hide();

    saveUserProfileChangesBtn.on('click', function () {

        var data = new Date();
        var hour = data.getHours();
        var minute = data.getMinutes();
        var seconds = data.getSeconds();

        if (minute < 10)
            minute = "0" + minute;
        if (seconds < 10)
            seconds = "0" + seconds;
        var year = data.getFullYear();
        var month = data.getMonth() + 1;
        if (month < 10)
            month = "0" + month;
        var day = data.getDate();
        if (day < 10)
            day = "0" + day;

        var fullDate = day + "." + month + "." + year + " " + hour + ":" + minute + ":" + seconds;
        $('#lastUserEditDate').val(fullDate);

        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function () {
                var test = checkPersAreaForValid();
                if (test) {
                    for (var i = 0; i < currentData.length; i++) {
                        $(currentData[i]).text($(profileInput[i]).val());
                    }

                    saveUserProfileChangesBtn.hide();
                    profileInput.hide();
                    updateUserProfileBtn.show();
                    currentData.show();

                    $('#userNameError').hide();
                    $('#userSurnameError').hide();
                    $('#userPhoneError').hide();
                }
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

//показать текущую категорию в редактировании объявления
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

//выбор фотографии пользователя
var oldPhoto;
function openFilesFolder() {
    $('#openFolder').click();
    $('#showUserPhoto').show();
    $('#abortPhotoUpdateBtn').show(); 

    var img = $("#userAvatar").children();
    oldPhoto = $(img).attr("src");
}

//загрузить выбранную фотографию пользователя
var UserImagePhoto;
function showUserPhoto() {
    var loadPhotoInput = $('#openFolder');
    if (loadPhotoInput.prop('files').length) {
        var formData = new FormData();
        formData.append('file', loadPhotoInput.prop('files')[0]);
        $.ajax({
            type: $('#uploadUserPhotoForm').attr('method'),
            url: $('#uploadUserPhotoForm').attr('action'),
            processData: false,
            contentType: false,
            data: formData,
            success: function (data) {
                $('#sendPhotoToModel').val(data);
                var img = $("#userAvatar").children();
                $(img).attr("src", data);
                UserImagePhoto = data;
                $('#saveUserPhotoBtn').show();
            },
            error: function () { alert("Файл не отправлен!") }
        });
    }
    else {
        alert("Сперва выберите фотографию!");
        $('#saveUserPhotoBtn').hide();
    }
}

//сохранение фотографии пользователя
function saveUserPhotoAction() {
    if (UserImagePhoto == null) {
        alert('Неверный формат!');
    }
    else {
        var form = $('#saveUserPhotoForm');
        $.ajax({
            type: $(form).attr('method'),
            url: $(form).attr('action'),
            data: $(form).serialize(),
            success: function () {
                $('#showUserPhoto').hide();
                $('#saveUserPhotoBtn').hide();
                $('#abortPhotoUpdateBtn').hide();
                alert("Успешно обновлено!");
            },
            error: function () {
                $('#saveUserPhotoBtn').hide();
                alert("Данные не отправлены!")
            }
        });
    }
}

//отмена обновления фотографии пользователя
function abortPhotoUpdate() {
    $('#showUserPhoto').hide();
    $('#saveUserPhotoBtn').hide();
    $('#abortPhotoUpdateBtn').hide();

    var img = $("#userAvatar").children();
    $(img).attr("src", oldPhoto);
}

//выбрать фотографию для объявления
var oldDeclarPhoto;
function openFileFolder() {
    $('#openFolder').click();
    $('#showDeclarPhoto').show();
    $('#abortDeclarImgUpdateBtn').show();

    var img = $("#declarPhoto").children();
    oldDeclarPhoto = $(img).attr("src");
}

//загрузить выбранную фотографию для объявления
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

//отмена добавления фотографии объявления(создание)
function abortDeclarImgUploadBtn() {
    $('#showDeclarPhoto').hide();
    $('#abortDeclarImgUpdateBtn').hide();

    var img = $("#declarPhoto").children();
    $(img).removeAttr("src");
    $('#sendImageToModel').val("");
    $(img).hide();
}

//отмена добавления фотографии объявления(редактирование)
function abortDeclarImgUpdateBtn() {
    $('#showDeclarPhoto').hide();
    $('#abortDeclarImgUpdateBtn').hide();

    var img = $("#declarPhoto").children();
    $(img).attr("src", oldDeclarPhoto);
    $('#sendImageToModel').val(oldDeclarPhoto);
}

//переключение между вкладками в упралении пользователями
$('#myTab a').on('click', function (e) {
    e.preventDefault()
    $(this).tab('show')
})

//валидация на регистрацию
function checkRegistrForValid() {
    var mail = $('#input_EMail').val();
    var name = $('#inputName').val();
    var surname = $('#inputSurname').val();
    var phone = $('#input_Phone').val();
    var password = $('#input_Password').val();

    var emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/;
    var isValidMail = emailPattern.test(mail);

    var anyLetter = /^[A-Za-zА-Яа-яЁё]+$/;
    var isNameValid = anyLetter.test(name);
    var isSurnameValid = anyLetter.test(surname);

    var onlyNumbers = /^\d+$/
    var isPhoneValid = onlyNumbers.test(phone);

    var passwordPattern = /^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z])(?=.*[!#$%&'*+\/=?^_`{|}~.-]).*$/
    var isPasswordValid = passwordPattern.test(password);

    $('#emailError').hide();
    $('#nameError').hide();
    $('#surnameError').hide();
    $('#phoneError').hide();
    $('#passwordError').hide();

    //почта
    if (mail.length == 0) {
        document.getElementById('emailError').innerHTML = "E-Mail не может быть пустым!";
        $('#emailError').removeClass("text-success").addClass("text-danger");
        $('#emailError').show();
        return false;
    }
    else if (mail.length > 0 && !isValidMail) {
        document.getElementById('emailError').innerHTML = "E-Mail имеет неверный формат!";
        $('#emailError').removeClass("text-success").addClass("text-danger");
        $('#emailError').show();
        return false;
    }
    else {
        document.getElementById('emailError').innerHTML = "Отлично!";
        $('#emailError').removeClass("text-danger").addClass("text-success");
        $('#emailError').show();
    }

    //имя
    if (name.length < 1) {
        document.getElementById('nameError').innerHTML = "Имя не должно быть пустым!";
        $('#nameError').removeClass("text-success").addClass("text-danger");
        $('#nameError').show();
        return false;
    }
    else if (!isNameValid) {
        document.getElementById('nameError').innerHTML = "Имя должно содержать только буквы!";
        $('#nameError').removeClass("text-success").addClass("text-danger");
        $('#nameError').show();
        return false;
    }
    else {
        document.getElementById('nameError').innerHTML = "Отлично!";
        $('#nameError').removeClass("text-danger").addClass("text-success");
        $('#nameError').show();
    }

    //фамилия
    if (surname.length < 1) {
        document.getElementById('surnameError').innerHTML = "Фамилия не должна быть пустой!";
        $('#surnameError').removeClass("text-success").addClass("text-danger");
        $('#surnameError').show();
        return false;
    }
    else if (!isSurnameValid) {
        document.getElementById('surnameError').innerHTML = "Фамилия должно содержать только буквы!";
        $('#surnameError').removeClass("text-success").addClass("text-danger");
        $('#surnameError').show();
        return false;
    }
    else {
        document.getElementById('surnameError').innerHTML = "Отлично!";
        $('#surnameError').removeClass("text-danger").addClass("text-success");
        $('#surnameError').show();
    }

    //телефон
    if (phone.length > 0 && (phone.length < 7 || phone.length > 13)) {
        document.getElementById('phoneError').innerHTML = "Проверьте количество цифр!";
        $('#phoneError').removeClass("text-success").addClass("text-danger");
        $('#phoneError').show();
        return false;
    }
    else if (phone.length > 0 && !isPhoneValid) {
        document.getElementById('phoneError').innerHTML = "Телефон должен содержать только цифры!";
        $('#phoneError').removeClass("text-success").addClass("text-danger");
        $('#phoneError').show();
        return false;
    }
    else {
        document.getElementById('phoneError').innerHTML = "Отлично!";
        $('#phoneError').removeClass("text-danger").addClass("text-success");
        $('#phoneError').show();
    }

    //пароль
    if (password.length == 0) {
        document.getElementById('passwordError').innerHTML = "Пароль не должен быть пустым!";
        $('#passwordError').removeClass("text-success").addClass("text-danger");
        $('#passwordError').show();
        return false;
    }
    else if (password.length > 0 && !isPasswordValid) {
        document.getElementById('passwordError').innerHTML = "Пароль должен содержать минимум 8 символов," +
            "одну цифру, одну букву в нижнем и одну букву в верхнем регистрах!";
        $('#passwordError').removeClass("text-success").addClass("text-danger");
        $('#passwordError').show();
        return false;
    }
    else {
        document.getElementById('passwordError').innerHTML = "Отлично!";
        $('#passwordError').removeClass("text-danger").addClass("text-success");
        $('#passwordError').show();
    }
    return true;
}

//валидация авторизации
function checkLoginForValid() {
    var mail = $('#inputEMail').val();
    var password = $('#inputPassword').val();

    $('#eMailError').hide();
    $('#passError').hide();

    var emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/;
    var isValidMail = emailPattern.test(mail);

    //почта
    if (mail.length == 0) {
        document.getElementById('eMailError').innerHTML = "E-Mail не может быть пустым!";
        $('#eMailError').removeClass("text-success").addClass("text-danger");
        $('#eMailError').show();
        return false;
    }
    else if (mail.length > 0 && !isValidMail) {
        document.getElementById('eMailError').innerHTML = "E-Mail имеет неверный формат!";
        $('#eMailError').removeClass("text-success").addClass("text-danger");
        $('#eMailError').show();
        return false;
    }
    else {
        document.getElementById('eMailError').innerHTML = "Отлично!";
        $('#eMailError').removeClass("text-danger").addClass("text-success");
        $('#eMailError').show();
    }

    //пароль
    if (password.length == 0) {
        document.getElementById('passError').innerHTML = "Пароль не должен быть пустым!";
        $('#passError').removeClass("text-success").addClass("text-danger");
        $('#passError').show();
        return false;
    }
    else {
        document.getElementById('passError').innerHTML = "Отлично!";
        $('#passError').removeClass("text-danger").addClass("text-success");
        $('#passError').show();
    }
    return true;
}

// валидация создания объявления
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

    //название
    if (title.length == 0) {
        document.getElementById('declTitleError').innerHTML = "Название не должно быть пустым!";
        $('#declTitleError').removeClass('text-success').addClass('text-danger');
        $('#declTitleError').show();
        return false;
    }
    else {
        document.getElementById('declTitleError').innerHTML = "Отлично!";
        $('#declTitleError').removeClass('text-danger').addClass('text-success');
        $('#declTitleError').show();
    }

    //категория выбрана
    if (category == "") {
        document.getElementById('declarCategoryError').innerHTML = "Категория не выбрана!";
        $('#declarCategoryError').removeClass('text-success').addClass('text-danger');
        $('#declarCategoryError').show();
        return false;
    }
    else {
        document.getElementById('declarCategoryError').innerHTML = "Отлично!";
        $('#declarCategoryError').removeClass('text-danger').addClass('text-success');
        $('#declarCategoryError').show();
    }

    //описание
    if (description.length == 0) {
        document.getElementById('declDescrError').innerHTML = "Описание не должно быть пустым!";
        $('#declDescrError').removeClass('text-success').addClass('text-danger');
        $('#declDescrError').show();
        return false;
    }
    else {
        document.getElementById('declDescrError').innerHTML = "Отлично!";
        $('#declDescrError').removeClass('text-danger').addClass('text-success');
        $('#declDescrError').show();
    }

    //цена
    if (!isPriceValid) {
        document.getElementById('declarPriceError').innerHTML = "Пример правильного заполнения: 49,99 (запятая-разделитель)";
        $('#declarPriceError').removeClass('text-success').addClass('text-danger');
        $('#declarPriceError').show();
        return false;
    }
    else {
        document.getElementById('declarPriceError').innerHTML = "Отлично!";
        $('#declarPriceError').removeClass('text-danger').addClass('text-success');
        $('#declarPriceError').show();
    }

    return true;
}

// валидация пользователя при изменении в личном кабинете
function checkPersAreaForValid() {
    var name = $('#changeUserName').val();
    var surname = $('#changeUserSurname').val();
    var phone = $('#changeUserPhone').val();

    $('#userNameError').hide();
    $('#userSurnameError').hide();
    $('#userPhoneError').hide();

    var anyLetter = /^[A-Za-zА-Яа-яЁё]+$/;
    var isNameValid = anyLetter.test(name);
    var isSurnameValid = anyLetter.test(surname);

    var onlyNumbers = /^\d*$/
    var isPhoneValid = onlyNumbers.test(phone);

    //имя
    if (name.length < 1) {
        document.getElementById('userNameError').innerHTML = "Имя не должно быть пустым!";
        $('#userNameError').removeClass("text-success").addClass("text-danger");
        $('#userNameError').show();
        return false;
    }
    else if (!isNameValid) {
        document.getElementById('userNameError').innerHTML = "Имя должно содержать только буквы!";
        $('#userNameError').removeClass("text-success").addClass("text-danger");
        $('#userNameError').show();
        return false;
    }
    else {
        document.getElementById('userNameError').innerHTML = "Отлично!";
        $('#userNameError').removeClass("text-danger").addClass("text-success");
        $('#userNameError').show();
    }

    //фамилия
    if (surname.length < 1) {
        document.getElementById('userSurnameError').innerHTML = "Фамилия не должна быть пустой!";
        $('#userSurnameError').removeClass("text-success").addClass("text-danger");
        $('#userSurnameError').show();
        return false;
    }
    else if (!isSurnameValid) {
        document.getElementById('userSurnameError').innerHTML = "Фамилия должно содержать только буквы!";
        $('#userSurnameError').removeClass("text-success").addClass("text-danger");
        $('#userSurnameError').show();
        return false;
    }
    else {
        document.getElementById('userSurnameError').innerHTML = "Отлично!";
        $('#userSurnameError').removeClass("text-danger").addClass("text-success");
        $('#userSurnameError').show();
    }

    //телефон
    if (phone.length > 0 && (phone.length < 7 || phone.length > 13)) {
        document.getElementById('userPhoneError').innerHTML = "Проверьте количество цифр!";
        $('#userPhoneError').removeClass("text-success").addClass("text-danger");
        $('#userPhoneError').show();
        return false;
    }
    else if (phone.length > 0 && !isPhoneValid) {
        document.getElementById('userPhoneError').innerHTML = "Телефон должен содержать только цифры!";
        $('#userPhoneError').removeClass("text-success").addClass("text-danger");
        $('#userPhoneError').show();
        return false;
    }
    else {
        document.getElementById('userPhoneError').innerHTML = "Отлично!";
        $('#userPhoneError').removeClass("text-danger").addClass("text-success");
        $('#userPhoneError').show();
    }
    return true;
}

//валидация пользователя при изменении админом
function checkEditUserAdminValid() {

    var name = $('#adminChangeUserName').val();
    var surname = $('#adminChangeUserSurname').val();
    var phone = $('#adminChangeUserPhone').val();

    $('#usernameError').hide();
    $('#usersurnameError').hide();
    $('#userphoneError').hide();

    var anyLetter = /^[A-Za-zА-Яа-яЁё]+$/;
    var isNameValid = anyLetter.test(name);
    var isSurnameValid = anyLetter.test(surname);

    var onlyNumbers = /^\d*$/
    var isPhoneValid = onlyNumbers.test(phone);

    //имя
    if (name.length < 1) {
        document.getElementById('usernameError').innerHTML = "Имя не должно быть пустым!";
        $('#usernameError').removeClass("text-success").addClass("text-danger");
        $('#usernameError').show();
        return false;
    }
    else if (!isNameValid) {
        document.getElementById('usernameError').innerHTML = "Имя должно содержать только буквы!";
        $('#usernameError').removeClass("text-success").addClass("text-danger");
        $('#usernameError').show();
        return false;
    }
    else {
        document.getElementById('usernameError').innerHTML = "Отлично!";
        $('#usernameError').removeClass("text-danger").addClass("text-success");
        $('#usernameError').show();
    }

    //фамилия
    if (surname.length < 1) {
        document.getElementById('usersurnameError').innerHTML = "Фамилия не должна быть пустой!";
        $('#usersurnameError').removeClass("text-success").addClass("text-danger");
        $('#usersurnameError').show();
        return false;
    }
    else if (!isSurnameValid) {
        document.getElementById('usersurnameError').innerHTML = "Фамилия должно содержать только буквы!";
        $('#usersurnameError').removeClass("text-success").addClass("text-danger");
        $('#usersurnameError').show();
        return false;
    }
    else {
        document.getElementById('usersurnameError').innerHTML = "Отлично!";
        $('#usersurnameError').removeClass("text-danger").addClass("text-success");
        $('#usersurnameError').show();
    }

    //телефон
    if (phone.length > 0 && (phone.length < 7 || phone.length > 13)) {
        document.getElementById('userphoneError').innerHTML = "Проверьте количество цифр!";
        $('#userphoneError').removeClass("text-success").addClass("text-danger");
        $('#userphoneError').show();
        return false;
    }
    else if (phone.length > 0 && !isPhoneValid) {
        document.getElementById('userphoneError').innerHTML = "Телефон должен содержать только цифры!";
        $('#userphoneError').removeClass("text-success").addClass("text-danger");
        $('#userphoneError').show();
        return false;
    }
    else {
        document.getElementById('userphoneError').innerHTML = "Отлично!";
        $('#userphoneError').removeClass("text-danger").addClass("text-success");
        $('#userphoneError').show();
    }
    return true;
}

document.addEventListener('DOMContentLoaded', function () {

    //для показа выбранной категории при редактировании
    $('[data-onload]').each(function () {
        eval($(this).data('onload'));
    });

    //InfiniteScroll($('#LoadingPostPreview'), $('#PostPreviewScrolList'), '/Home/Index?id=', 'GET', '');

    //мои объявления
    $('#myDeclarations').on('click', function () {
        $('#myProfileAction').hide();
        $('#userPersonalAreaCard').hide();
        $('#deletedDeclarationAction').hide();
        $('#myDeclarationAction').load("/User/GetUserDeclarations");
        $('#myDeclarationAction').show();
    });

    //мой профиль
    $('#myProfile').on('click', function () {
        $('#myDeclarationAction').hide();
        $('#userPersonalAreaCard').hide();
        $('#deletedDeclarationAction').hide();
        $('#myProfileAction').load("/User/MyProfile");
        $('#myProfileAction').show();
    });

    //удаленные пользователями объявления
    $('#deletedDeclarations').on('click', function () {
        $('#myProfileAction').hide();
        $('#userPersonalAreaCard').hide();
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