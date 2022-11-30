$("#paymentForm").submit(function () {
    $.ajax({
        type: "POST",
        data: $(this).serialize(),
        success: function (resp) {
            $("#message").html("<span style='color:green'>success!<span>");
        },
        error: function (resp) {
            $("#message").html("<span style='color:red'>fail!<span>");
        }
    });

    return false;
})