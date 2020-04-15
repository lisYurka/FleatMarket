//выбор фотографии пользователя
var oldPhoto;
function openFilesFolder() {
    $('#openFolder').click();
    $('#showUserPhoto').show();
    $('#abortPhotoUpdateBtn').show();

    var img = $("#userAvatar").children();
    oldPhoto = $(img).attr("src");
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

    var flag = true;

    //имя
    if (name.length < 1) {
        document.getElementById('userNameError').innerHTML = "Имя не должно быть пустым!";
        $('#userNameError').removeClass("text-success").addClass("text-danger");
        document.getElementById('changeUserName').style.borderColor = "#e12d2d";
        $('#userNameError').show();
        flag = false;
    }
    else if (!isNameValid) {
        document.getElementById('userNameError').innerHTML = "Имя должно содержать только буквы!";
        $('#userNameError').removeClass("text-success").addClass("text-danger");
        document.getElementById('changeUserName').style.borderColor = "#e12d2d";
        $('#userNameError').show();
        flag = false;
    }
    else {
        document.getElementById('userNameError').innerHTML = "Отлично!";
        $('#userNameError').removeClass("text-danger").addClass("text-success");
        document.getElementById('changeUserName').style.borderColor = "#30da49";
        $('#userNameError').show();
    }

    //фамилия
    if (surname.length < 1) {
        document.getElementById('userSurnameError').innerHTML = "Фамилия не должна быть пустой!";
        $('#userSurnameError').removeClass("text-success").addClass("text-danger");
        document.getElementById('changeUserSurname').style.borderColor = "#e12d2d";
        $('#userSurnameError').show();
        flag = false;
    }
    else if (!isSurnameValid) {
        document.getElementById('userSurnameError').innerHTML = "Фамилия должно содержать только буквы!";
        $('#userSurnameError').removeClass("text-success").addClass("text-danger");
        document.getElementById('changeUserSurname').style.borderColor = "#e12d2d";
        $('#userSurnameError').show();
        flag = false;
    }
    else {
        document.getElementById('userSurnameError').innerHTML = "Отлично!";
        $('#userSurnameError').removeClass("text-danger").addClass("text-success");
        document.getElementById('changeUserSurname').style.borderColor = "#30da49";
        $('#userSurnameError').show();
    }

    //телефон
    if (phone.length > 0 && (phone.length < 7 || phone.length > 13)) {
        document.getElementById('userPhoneError').innerHTML = "Проверьте количество цифр!";
        $('#userPhoneError').removeClass("text-success").addClass("text-danger");
        document.getElementById('changeUserPhone').style.borderColor = "#e12d2d";
        $('#userPhoneError').show();
        flag = false;
    }
    else if (phone.length > 0 && !isPhoneValid) {
        document.getElementById('userPhoneError').innerHTML = "Телефон должен содержать только цифры!";
        $('#userPhoneError').removeClass("text-success").addClass("text-danger");
        document.getElementById('changeUserPhone').style.borderColor = "#e12d2d";
        $('#userPhoneError').show();
        flag = false;
    }
    else {
        document.getElementById('userPhoneError').innerHTML = "Отлично!";
        $('#userPhoneError').removeClass("text-danger").addClass("text-success");
        document.getElementById('changeUserPhone').style.borderColor = "#30da49";
        $('#userPhoneError').show();
    }
    if (flag == true) return true;
    else return false;
}