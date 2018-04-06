(function ($) {
    $.fn.LightDataAjax = function (options) {
        var settings = $.extend({
            contentType: "application/json",
            dataType: "json",
            type: "POST",
            data: undefined,
            async: true,
            url: undefined,
            success: function () { },
            error: function () { },
            ignoreLoader: false,
        }, options);


        var loader = !settings.ignoreLoader ? $(this).loader().Start() : undefined;

        var clonedSettings = $.extend({}, options);

        clonedSettings.success = function (data) {
            if (loader)
                loader.Stop();
            settings.success(data);
        }

        clonedSettings.error = function (e, m) {
            if (loader)
                loader.Stop();
            if (m === "parsererror") {
                settings.success(m);
                return;
            }
            settings.error(e, m);
            $("body").dialog({
                content: "<p class='error'>Something went wrong, please contact the administrator if it happened again</p>"
            }).show();
            //alert(e);
        }

        $.ajax(clonedSettings);
    };

    $.LightDataAjax = function (options) {
        var settings = $.extend({
            contentType: "application/json",
            dataType: "json",
            type: "POST",
            data: undefined,
            async: true,
            url: undefined,
            success: function () { },
            error: function () { },
            ignoreLoader: false,
        }, options);
        settings.ignoreLoader = true;
        $("body").LightDataAjax(settings);
    }
}(jQuery));