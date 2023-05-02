$(function () {
    $("#SendSeriyalNumber").click(function () {
        $("#divErrorMessage").empty();
        ajax_post("", { SeriyalNumber: $("#serialNumber").val() }, resultAjaxSendSeriyalNumber);

    });

    function resultAjaxSendSeriyalNumber(data) {
        $("#divErrorMessage").empty();
        if (data.ResultFlag == true) {
            $("#divSeriyalNumber").addClass("hide");
            $("#divVerificationData").removeClass("hide");
        } else {
            for (var i = 0; i < data.ErrorMessages.length; i++) {
                $("#divErrorMessage").append("<div>" + data.ErrorMessages[i] + "</div>");
            }
        }
    }
    $("#SendVerificationData").click(function () {
        SendVerificationCode();
    });
    $("#ReSendVerificationCode").click(function () {
        SendVerificationCode();
    });
    $("#SendVerificationCode").click(function () {
        $("#divErrorMessage").empty();
        ajax_post("/Login/VerificationCode",
            {
                nationalCode: $("#NationalCode").val(),
                mobilePhone: $("#Mobile").val(),
                acceptCode: $("#VerificationCode").val()
            }, resultAjaxVerificationCode);

    });

    function resultAjaxVerificationCode(data) {
        $("#divErrorMessage").empty();
        if (data.ResultFlag == true) {
            window.location.href = url;
        } else {
            for (var i = 0; i < data.ErrorMessages.length; i++) {
                $("#divErrorMessage").append("<div>" + data.ErrorMessages[i] + "</div>");
            }
        }
    }
});
function SendVerificationCode() {
    $("#divErrorMessage").empty();
    ajax_post("/Login/Verification",
        {
            nationalCode: $("#NationalCode").val(),
            mobilePhone: $("#Mobile").val()
        }, resultAjaxVerification);

}

function resultAjaxVerification(data) {
    $("#divErrorMessage").empty();
    if (data.ResultFlag == true) {
        $("#divVerificationData").addClass("hide");
        $("#divVerificationCode").removeClass("hide");
        //if (data.Result != null) {
        //    $(".tstc").empty().append("<h4>کد جهت تست :</h4><h2>" + data.Result + "</h2>");
        //}
        $("#Timer").empty().ready(function () {
            ExecuteTimer(300);
        });
    } else {
        for (var i = 0; i < data.ErrorMessages.length; i++) {
            $("#divErrorMessage").append("<div>" + data.ErrorMessages[i] + "</div>");
        }
        $("#divVerificationData").removeClass("hide");
        $("#divVerificationCode").addClass("hide");
    } 
}