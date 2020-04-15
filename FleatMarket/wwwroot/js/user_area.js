function testRemove() {
    var declarActions = $('.declarActions');
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
            $(partialPost[id]).hide();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText);
        }
    });
}

function myProfile() {
    $('#myDeclarationAction').hide();
    $('#userPersonalAreaCard').hide();
    $('#deletedDeclarationAction').hide();
    $('#myProfileAction').load("/User/MyProfile");
    $('#myProfileAction').show();

}

function myAds() {
    $('#myProfileAction').hide();
    $('#userPersonalAreaCard').hide();
    $('#deletedDeclarationAction').hide();
    $('#myDeclarationAction').load("/User/GetUserDeclarations");
    $('#myDeclarationAction').show();
}

function removedByUsersAds() {
    $('#myProfileAction').hide();
    $('#userPersonalAreaCard').hide();
    $('#myDeclarationAction').hide();
    $('#deletedDeclarationAction').load("/User/RemovedDeclarations");
    $('#deletedDeclarationAction').show();
}