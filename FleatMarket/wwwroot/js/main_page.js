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
    return new Date(year, month - 1, day);
}

//поиск по дате
function searchByDate() {
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

        var count = 0;
        declarationDates.each(function (index) {
            var date = $(declarationDates[index]).attr('id').split(" ")[0];//дата обявления(без времени)
            var declarationDate = createDate(date);
            declarationDate.setHours(3, 0, 0, 0);
            $(declaration[index]).hide();
            dates.forEach(function (date) {
                if (date.getTime() == declarationDate.getTime()) {
                    $(declaration[index]).show();
                    count += 1;
                }
            });
        });
        if (count == 0) {
            $('#hiddenText').show();
        }
        if (count != 0) {
            $('#hiddenText').hide();
        }
        count = 0;
    }
}

//динамическая подгрузка объявлений
function InfiniteScroll(loadingIndicator, scrolList, url, type, dop) {
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

document.addEventListener('DOMContentLoaded', function () {
    //InfiniteScroll($('#LoadingPostPreview'), $('#PostPreviewScrolList'), '/Home/Index?id=', 'GET', '');

    //поиск по категориям
    $('.categoryLink').on('click', function () {
        var id = $(this).attr('id').replace('category_element_', '');

        var count = 0;
        var declaration = $('.OneDeclaration');
        var partialDeclarCategory = $('.declarationCategory');
        partialDeclarCategory.each(function (index) {
            if (partialDeclarCategory[index].id != id) {
                $(declaration[index]).hide();
            }
            else {
                $(declaration[index]).show();
                count += 1;
            }
        });
        if (count == 0) {
            $('#hiddenText').show();
        }
        if (count != 0 || id == "showDeclWithAllCateg") {
            $('#hiddenText').hide();
        }
        count = 0;
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

        var count = 0;
        partialDeclarCategory.each(function (index) {
            if (partialDeclarCategory[index].id != id) {
                $(declaration[index]).hide();
            }
            else {
                $(declaration[index]).show();
                count += 1;
            }
        });
        if (count == 0) {
            $('#hiddenText').show();
        }
        if (count != 0 || id == "showDeclWithAllStats") {
            $('#hiddenText').hide();
        }
        count = 0;
    });

    //если нажато "Все статусы"
    $('#showDeclWithAllStats').on('click', function () {
        var declaration = $('.OneDeclaration');
        declaration.each(function (index) {
            $(declaration[index]).show();
        });
    });
});