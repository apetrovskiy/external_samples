(function ($) {
    $.fn.dialog = function (options) {
        var settings = $.extend({
            onSave: undefined,
            onCancel: undefined,
            onDelete: undefined,
            deleteText: "Delete",
            saveText: "Save",
            cancelText: "Cancel",
            content: undefined,
            title: undefined,
            autoFillDataUrl: undefined,
            customButtons: [],
            screen: false
        }, options);

        var dialog = $("<div class='lightDataDialog'><h1>" + (settings.title ? settings.title : "&nbsp;") + "<span class='cancel'>X</span></h1><nav></nav></div>");
        var dim = undefined;
        if (settings.screen)
            dialog.css({ width: "99%", height: "99%" });
        dialog.settings = settings;
        if ($(".lightDataDialog").length > 0) {
            var max = 0;
            $(".lightDataDialog").each(function () {
                max = Math.max(parseInt($(this).css("z-index")), max);
            });
            dialog.css("zIndex", max + 1);
        }

        function dimScreen() {
            var dimClass = "dim" + $(".dim").length + "dim dim";
            var dim = $("<div class='" + dimClass + "'></div>");
            if ($(".dim").length > 0) {
                var max = 0;
                $(".dim").each(function () {
                    max = Math.max(parseInt($(this).css("z-index")), max);
                });
                dim.css("zIndex", max + 1);
            }
            return dim;
        }

        dialog.resize = function (bound) {
            setTimeout(function () {
                dialog.center(true);
                if (!bound)
                    dialog.find("h1").width(dialog[0].getBoundingClientRect().width);
                else {
                    dialog.find("h1").css("max-width", dialog[0].getBoundingClientRect().width);
                    if (bound.width)
                        dialog.find("h1").css("width", bound.width);
                    if (bound.height)
                        dialog.find("h1").css("height", bound.height);

                    dialog.center(true);
                }
            }, 1);
        }

        dialog.close = function () {
            dialog.remove();
            dim.remove();
        }

        dialog.show = function () {
            dim = dimScreen();
            dialog.find("nav").append(settings.content);

            if (!settings.screen)
                dialog.draggable({ handle: "h1" });

            dialog.find("span.cancel").click(function () {
                dialog.remove();
                dim.remove();
                if (settings.onCancel)
                    settings.onCancel();
            });

            if (settings.onCancel) {
                dialog.find("span.cancel").remove();
                dialog.find("h1").append("<span class='cancel'>" + settings.cancelText + "</span>");
                dialog.find("h1> .cancel").click(function () {
                    if ($(this).hasClass("disabled"))
                        return;
                    if (settings.onCancel(dialog) !== false) {
                        dialog.remove();
                        dim.remove();
                    }
                });
            }

            if (settings.onSave) {
                dialog.find("h1").append("<span class='save'>" + settings.saveText + "</span>");
                dialog.find("h1> .save").click(function () {
                    if ($(this).hasClass("disabled"))
                        return;
                    if (settings.onSave(dialog) !== false) {
                        dialog.remove();
                        dim.remove();
                    }
                });
            }

            if (settings.onDelete) {
                dialog.find("h1").append("<span class='delete'>" + settings.deleteText + "</span>");
                dialog.find("h1> .delete").click(function () {
                    if ($(this).hasClass("disabled"))
                        return;
                    if (settings.onDelete(dialog) !== false) {
                        dialog.remove();
                        dim.remove();
                    }
                });
            }

            $(settings.customButtons).each(function () {
                var btn = $("<span class='" + this.class + "'>" + this.text + "</span>");
                dialog.find("h1").append(btn);
                var item = this;
                btn.click(function () {
                    if ($(this).hasClass("disabled"))
                        return;
                    if (typeof item.click == "undefined" || item.click(dialog) !== false) {
                        dialog.remove();
                        dim.remove();
                    }
                });

            });

            $("body").append(dialog).append(dim);

            dialog.resize();
        }

        return dialog;
    };

}(jQuery));