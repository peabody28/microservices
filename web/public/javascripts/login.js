$("#loginForm").submit(function () {
    $.ajax({
        type: "POST",
        data: $(this).serialize(),
        success: function (resp) {
            if (resp.status)
                window.location.replace(resp.redirectUrl);
        },
        error: function (resp) {
            alert(resp)
        }
    });

    return false;
})