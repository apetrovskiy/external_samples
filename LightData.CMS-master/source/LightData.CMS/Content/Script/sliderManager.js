(function ($) {
    $.fn.sliderManager = function (options) {
        var settings = $.extend({
            getUri: undefined,
            saveUri: undefined,
            deleteUri: undefined,
            deleteSliderUri: undefined
        }, options);

        var container = $(this).addClass("sliderManager");
        var itemContainer = $("<ul></ul>");
        container.append("<h2 class='header'>SliderManager <span class='addItem'></span><span class='deleteItem'></span></h2>").append(itemContainer);

        container.add = function (item) {
            var editContainer = $("<div class='inputContainer'></div>");
            editContainer.append("<label>Slider Name: </label>");
            editContainer.append("<input type='text' />");
            $("body").dialog({
                content: editContainer,
                saveText: "Save",
                onSave: function (dialog) {
                    item = typeof item != "undefined" ? item : { identifier: editContainer.find("input").val() };
                    item.identifier = editContainer.find("input").val();
                    container.LightDataAjax({
                        contentType: "application/json",
                        dataType: "json",
                        type: "POST",
                        data: JSON.stringify(item),
                        url: settings.saveUri,
                        success: function (data) {
                            container.render();
                            dialog.close();
                        }
                    });

                    return false;
                }
            }).show();
        }

        container.render = function (containerId) {
            itemContainer.html("");
            container.LightDataAjax({
                contentType: "application/json",
                dataType: "json",
                type: "POST",
                url: settings.getUri,
                success: function (data) {
                    $(data).each(function () {
                        var item = this;
                        var li = $("<li></li>").html("<span class='collapsed'></span><input type='checkbox' class='chkPublished' itemId='" + item.id + "' /> <span>" + this.identifier + "</span>");
                        var ul = $("<ul></ul>");
                        li.append(ul);
                        itemContainer.append(li);
                        ul.append("<li title='Add Image' class='ItemAdder'></li>");
                        $.each(item.sliders, function () {
                            var slider = this;
                            ul.append("<li itemId='" + slider.id + "'><img src='" + "data:image/png;base64," + slider.file.base64File + "' /><span>" + slider.file.fileName + "</span> </li>")
                        });

                        ul.find(".ItemAdder").click(function () {
                            $("body").files({
                                getUri: settings.getImageUri,
                                saveUri: settings.saveImageUri,
                                deleteUri: settings.deleteImageUri,
                                folderGetUri: settings.folderGetUri,
                                folderSaveUri: settings.folderSaveUri,
                                folderDeleteUri: settings.folderDeleteUri,
                                getImageUri: settings.getImageFullUri,
                                saveFileItemUri: settings.saveFileItemUri,
                                allowedFiles: ["PNG", "GIF", "JPEG", "JPG"],
                                onInsert: function (img, fileItem) {
                                    item.sliders = [];
                                    item.sliders.push({ FileItem_Id: fileItem.id });
                                    container.LightDataAjax({
                                        contentType: "application/json",
                                        dataType: "json",
                                        type: "POST",
                                        data: JSON.stringify(item),
                                        url: settings.saveUri,
                                        success: function (data) {
                                            container.render(item.id);
                                        }
                                    });

                                }
                            });
                        });

                        li.children("span:first").click(function () {
                            if ($(this).hasClass("collapsed")) {
                                $(this).addClass("expanded").removeClass("collapsed");
                                ul.slideDown("slow");
                            } else {
                                $(this).addClass("collapsed").removeClass("expanded");
                                ul.slideUp("slow");
                            }
                        });

                        if (containerId == item.id)
                            li.children("span:first").click();

                        ul.find("li:not(:first)").click(function () {
                            if ($(this).hasClass("selected"))
                                $(this).removeClass("selected");
                            else $(this).addClass("selected");
                        });

                    });

                }
            });


        }
        container.find(".addItem").click(function () {
            container.add();
        });

        container.find(".deleteItem").click(function () {
            $("body").dialog({
                content: "<p class='info'>You are about to remove this the selected sliders. <br> Are you sure?</p>",
                saveText: "YES",
                cancelText: "NO",
                onSave: function () {
                    container.find(".chkPublished:checked, li.selected").each(function () {
                        var itemId = $(this).attr("itemId");
                        var o = $(this)
                        container.LightDataAjax({
                            contentType: "application/json",
                            dataType: "json",
                            type: "POST",
                            data: JSON.stringify({ itemId: itemId }),
                            url: (o.prop("tagName") == "LI" ? settings.deleteSliderUri : settings.deleteUri),
                            success: function (data) {
                                container.render();
                            }
                        });
                    });
                }

            }).show();
        });
        container.render();
        return container;
    };
}(jQuery));