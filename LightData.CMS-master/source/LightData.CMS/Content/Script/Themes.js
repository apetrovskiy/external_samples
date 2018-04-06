(function ($) {
    $.themeLoader = function (options) {
        var settings = $.extend({
            getUri: undefined,
        }, options);

        this.load = function (id, func) {
            $.LightDataAjax({
                contentType: "application/json",
                dataType: "json",
                type: "POST",
                data: JSON.stringify({ folderId: id }),
                url: settings.getUri,
                success: function (data) {
                    var items = [];
                    $.each(data.css, function () {
                        items.push('<link href="' + this + '" rel="stylesheet"/>')
                    });
                    $.each(data.js, function () {
                        items.push('<script src="' + this + '"/>')
                    });
                    if (func) {
                        func(items);
                    }
                }
            });
        }
        return this;

    };
}(jQuery));