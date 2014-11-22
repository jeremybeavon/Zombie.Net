$(function () {
    $("#testPost").click(function () {
        $.ajax({
            url: '/api/me',
            type: 'POST',
            dataType: 'json',
            data: {
                Hometown: $("#hometown").val() + " in da house",
            },
            success: function (result) {
                $("#result").text(result.hometown);
            }
        });
    });
});