$(function () {
    $(window).load(function () {
        $("#spinner").fadeOut("slow");
    });
    $("li.havechild").click(function() {
        $(this).toggleClass("openchild");
    });
});
function ajax_post(url, myData, callBack) {
    $("#spinner").addClass("op-70").fadeIn();
    $.ajax({
        url: url,
        async: false,
        type: 'POST',
        data: myData,
        success: function (data) {
            $("#spinner").fadeOut("slow");
            callBack(data);
        },
        error: function () {
            $("#spinner").fadeOut("slow");
            $("#divErrorMessage").append("<div>درخواست قابل انجام نیست!</div>");
        }
    });
}
function ajax_post_formdata(url, myData, callBack) {
    $("#spinner").show().addClass("op-70");
    $.ajax({
        url: url,
        type: 'POST',
        data: myData,
        contentType: false,
        processData: false,
        success: function (data) {            
            callBack(data);
            $("#spinner").fadeOut("slow");
        },
        error: function () {
            $("#spinner").fadeOut("slow");
            $("#divErrorMessage").append("<div>درخواست قابل انجام نیست!</div>");
        }
    });
}
function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
function SetClassDirty(elementId) {
    var element = $(elementId);
    if ($.trim(element.val()) === $.trim(element.data('previous'))) {
        element.removeClass('dirty');
    } else {
        element.addClass('dirty');
    }
}
function SetDataToFormData(elementId, formData) {
    if ($("#" + elementId).hasClass('dirty')) {
        formData.append(elementId, $("#" + elementId).val());
    }
    return formData;
}
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function showSocial() {
    $(".share-page-social div.social").toggle({ direction: "left" });
}