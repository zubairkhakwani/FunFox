// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AjaxRequest(type = "GET", url, data) {
    return new Promise((resolve, reject) => {
        var result;
        $.ajax({
            url: url,
            data: data,
            type: type,
            success: function (response) {
                resolve(response);
            },
            error: function (response) {
                if (response.status == 401) {
                    window.location.replace("/user/login");
                }
                var errors = response.responseJSON.error.errors;
                for (var i = 0; i < errors.length; i++) {
                    toastr.error(errors[i]);
                }
                reject(errors);
            }
        });
    });

}

function ShowParamMessages() {
    var urlParams = new URLSearchParams(window.location.search); //get all parameters
    var msg = urlParams.get('msg');
    var err = urlParams.get('err');

    if (msg) { //check if msg parameter is set to anything
        toastr.success(msg);
    }

    if (err) { //check if err parameter is 'bar'
        toastr.success(err);
    }
}