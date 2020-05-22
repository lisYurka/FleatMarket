function showCheckBox(id) {
    var notif_id = document.getElementById("notifCard_" + id);
    notif_id.style.cursor = "pointer";
    document.getElementById("checkBox_" + id).style.display = "flex";
}

function hideCheckBox(id) {
    var notif_id = document.getElementById("notifCard_" + id);
    notif_id.style.cursor = "default";
    document.getElementById("checkBox_" + id).style.display = "none";
}

var id_mas = [];
function toggleCheckBox(elem, id) {
    if (elem.checked == true) {
        document.getElementById("notifCard_" + id).onmouseout == null;
        id_mas.push(id);
    }
    if (elem.checked == false) {
        var index = id_mas.findIndex((element) => element == id);
        id_mas.splice(index, 1);
    }
}

function chooseAllNotifs() {
    alert($('#sendNotifIdToAction').val());
}

function readNotif() {
    var form = $('#readNotifForm');
    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function () {
            $('#sendNotifIdToAction').val(id_mas);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText);
        }
    });
}