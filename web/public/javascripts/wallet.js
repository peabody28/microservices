$("#createWalletForm").submit(function () {
    $.ajax({
        type: "POST",
        url: "/wallet/create",
        success: function (resp) {
            $("#wallets").append(`<li>${resp.number}</li>`);
        },
        error: function (resp) {
            $("#message").html("<span style='color:red'>fail!<span>");
        }
    });

    return false;
})