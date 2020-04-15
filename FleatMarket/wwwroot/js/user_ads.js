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
