(function () {
    $.ajaxJson = function (options) {
        options = options || {};
        options.traditional = true;

        var settings = {
            dataType: 'json',
            crossDomain: true,
            contentType: 'application/json; charset=utf-8'
        };

        $.extend(true, settings, options);

        ajax(settings);
    };

    var ajax = function (options) {
        var settings = {
            type: 'GET',
            cache: false,
            enableErrorHandling: true,
            enableRequestAbortedErrorHandling: true,
            ignoreError404: false
        };

        $.extend(true, settings, options);

        if (settings.url === undefined) { throw new Error("Missing param: url"); }

        settings.success = function (response, status, xhr) {
            if (status === "success") {
                if (typeof (options.success) === "function") {
                    options.success(response, status, xhr);
                }
            }
            else {
                if (typeof (options.error) === "function") {
                    options.error(response, status, xhr);
                }
            }
        };

        settings.error = function (xhr, status, error) {
            if (typeof (options.error) === "function") {
                options.error(xhr, status, error);
            }
        };

        settings.complete = function (xhr, status) {
            if (typeof (options.complete) === "function") {
                options.complete(xhr, status);
            }
        };

        $.ajax(settings);
    };
})();