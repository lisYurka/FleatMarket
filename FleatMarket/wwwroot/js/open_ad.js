//удаление объявления
function testRemove2() {
    var form = $('#DeleteDeclarFromDbForm');
    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function () {
            $('.RemoveDeclFromDbBtn').hide();
            $('.EditDeclarationBtn').hide();
            document.getElementById('deletedStatusName').innerHTML = "Удалено навсегда!";
            document.getElementById('deletedStatusName').style.color = "red";
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText);
        }
    });
}

//передать id объявления для его редактирования
function sendDeclarId() {
    var id = $('.EditDeclarationBtn').attr("id").replace("editDeclBtn_","");
    $('#declar_Id').val(id);
}