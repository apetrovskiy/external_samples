(function ($) {
    $.inputValidator = function () {
        function build() {
            var items = $("input.dateTime:visible:not(.initialized),input.borderStyle:visible:not(.initialized),input.color:visible:not(.initialized),input.notEmpty:visible:not(.initialized),input.decimal:visible:not(.initialized),input.int:visible:not(.initialized)");
            $(items).each(function () {
                var o = $(this);
                if (!o.hasClass(".initialized")) {
                    o.addClass("initialized");
                    if (o.hasClass("decimal")) {
                        o.keypress(function (evt) {
                            var charCode = (evt.which) ? evt.which : event.keyCode;
                            if (charCode == 46 && evt.target.value.split('.').length > 1) {
                                return false;
                            }
                            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                                return false;
                            return true;
                        });
                    } else if (o.hasClass("color")) {
                        o.css('backgroundColor', '#' + o.val());
                        o.prop("readonly", true);
                        o.ColorPicker({
                            onSubmit: function (hsb, hex, rgb, el) {
                                $(el).val(hex);
                                $(el).ColorPickerHide();
                            },
                            onBeforeShow: function () {
                                $(this).ColorPickerSetColor(this.value);
                            },
                            onChange: function (hsb, hex, rgb) {
                                o.val(hex);
                                o.css('backgroundColor', '#' + hex);
                            }
                        })
                            .bind('keyup', function () {
                                $(this).ColorPickerSetColor(this.value);
                            });
                    } else if (o.hasClass("dateTime")) {
                        o.fancyDatePicker({
                            closeOnSelect: true,
                            useTime: true, // this indicate a date and time picker
                            timeOnly: false, // true for only time
                            useMask: true, // use text mask
                            culture: "en-US",/// need to upload globalization maps to use aditional languages, se globalization mapp
                            format: "mm/dd/yy", // use culture or override the culture format.
                            globalizationMapPath: "/globalization/", // globalization mapp path on server.
                            closeOnSelect: true, // close the date picker on day select,
                            readOnly: true, // read only eg use cant type and have to use the date picker
                        });
                    } else if (o.hasClass("borderStyle")) {
                        var items = [{
                            id: 0,
                            text: "none"
                        }, {
                            id: 1,
                            text: "dotted"
                        }, {
                            id: 2,
                            text: "dashed"
                        }, {
                            id: 3,
                            text: "solid"
                        }, {
                            id: 4,
                            text: "double"
                        }, {
                            id: 5,
                            text: "groove"
                        }, {
                            id: 5,
                            text: "ridge"
                        }, {
                            id: 6,
                            text: "inset"
                        }, {
                            id: 7,
                            text: "outset"
                        }, {
                            id: 8,
                            text: "initial"
                        }, {
                            id: 9,
                            text: "inherit"
                        }];
                        o.autoFill({
                            datasource: items,
                            textField: "text",
                            valueField: "id",
                            selectedValue: 0
                        })
                    }
                }
            });
            setTimeout(build, 100);
        }

        build();
    }
}(jQuery));