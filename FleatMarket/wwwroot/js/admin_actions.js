//переключение между вкладками в упралении пользователями
$('#myTab a').on('click', function (e) {
    e.preventDefault()
    $(this).tab('show')
})

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

    var flag = true;

    //имя
    if (name.length < 1) {
        document.getElementById('usernameError').innerHTML = "Имя не должно быть пустым!";
        $('#usernameError').removeClass("text-success").addClass("text-danger");
        document.getElementById('adminChangeUserName').style.borderColor = "#e12d2d";
        $('#usernameError').show();
        flag = false;
    }
    else if (!isNameValid) {
        document.getElementById('usernameError').innerHTML = "Имя должно содержать только буквы!";
        $('#usernameError').removeClass("text-success").addClass("text-danger");
        document.getElementById('adminChangeUserName').style.borderColor = "#e12d2d";
        $('#usernameError').show();
        flag = false;
    }
    else {
        document.getElementById('usernameError').innerHTML = "Отлично!";
        $('#usernameError').removeClass("text-danger").addClass("text-success");
        document.getElementById('adminChangeUserName').style.borderColor = "#30da49";
        $('#usernameError').show();
    }

    //фамилия
    if (surname.length < 1) {
        document.getElementById('usersurnameError').innerHTML = "Фамилия не должна быть пустой!";
        $('#usersurnameError').removeClass("text-success").addClass("text-danger");
        document.getElementById('adminChangeUserSurname').style.borderColor = "#e12d2d";
        $('#usersurnameError').show();
        flag = false;
    }
    else if (!isSurnameValid) {
        document.getElementById('usersurnameError').innerHTML = "Фамилия должно содержать только буквы!";
        $('#usersurnameError').removeClass("text-success").addClass("text-danger");
        document.getElementById('adminChangeUserSurname').style.borderColor = "#e12d2d";
        $('#usersurnameError').show();
        flag = false;
    }
    else {
        document.getElementById('usersurnameError').innerHTML = "Отлично!";
        $('#usersurnameError').removeClass("text-danger").addClass("text-success");
        document.getElementById('adminChangeUserSurname').style.borderColor = "#30da49";
        $('#usersurnameError').show();
    }

    //телефон
    if (phone.length > 0 && (phone.length < 7 || phone.length > 13)) {
        document.getElementById('userphoneError').innerHTML = "Проверьте количество цифр!";
        $('#userphoneError').removeClass("text-success").addClass("text-danger");
        document.getElementById('adminChangeUserPhone').style.borderColor = "#e12d2d";
        $('#userphoneError').show();
        flag = false;
    }
    else if (phone.length > 0 && !isPhoneValid) {
        document.getElementById('userphoneError').innerHTML = "Телефон должен содержать только цифры!";
        $('#userphoneError').removeClass("text-success").addClass("text-danger");
        document.getElementById('adminChangeUserPhone').style.borderColor = "#e12d2d";
        $('#userphoneError').show();
        flag = false;
    }
    else {
        document.getElementById('userphoneError').innerHTML = "Отлично!";
        $('#userphoneError').removeClass("text-danger").addClass("text-success");
        document.getElementById('adminChangeUserPhone').style.borderColor = "#30da49";
        $('#userphoneError').show();
    }
    if (flag == true) return true;
    else return false;
}