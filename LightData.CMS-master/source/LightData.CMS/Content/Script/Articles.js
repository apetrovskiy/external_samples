(function ($) {
    $.fn.articles = function (options) {
        var settings = $.extend({
            getUri: undefined,
            saveUri: undefined,
            deleteUri: undefined,
            header: undefined,
            allchecked: false,
            autoFillMenusDataUrl: undefined,
            themeUrl: undefined,
            cssTheme: undefined,
            getFoldersAutoFill: undefined
        }, options);
        var container = $(this);

        container.delete = function (artikelsIds) {
            if (!artikelsIds || artikelsIds.length <= 0)
                return;
            container.dialog({
                content: "You are about to delete this/those items. <br> do you wish to continue?",
                onSave: function () {
                    container.LightDataAjax({
                        contentType: "application/json",
                        dataType: "json",
                        type: "POST",
                        data: JSON.stringify({ items: artikelsIds }),
                        url: settings.deleteUri,
                        success: function (data) {
                            container.render();
                        }
                    });
                }
            }).show();
        }

        container.generateIFrame = function (onDataLoaded) {
            var iframe = $('<iframe src="' + settings.themeUrl + '" id="frameDemo"></iframe>');
            iframe[0].onload = function () {
                $(".ExternalCss").each(function () {
                    iframe.contents().find("head").append($(this).clone());
                });

                if (onDataLoaded)
                    onDataLoaded(iframe);
            }
            return iframe;
        }

        container.save = function (item) {
            if (!item)
                item = { articleName: "", metaKeywords: [], published: false, articleNodes: [], MenusId: 0, folder_Id: null, theme: "" }
            var dialog = undefined;
            var editContainer = $("<div class='inputContainer'></div>");
            var tabControl = editContainer.tabs({
                onSelect: function () {
                    tabControl.setPos();
                }
            });

            var tabBasicValues = tabControl.add("Basic", "Basic settings");
            var tabContent = tabControl.add("Article", "Content");
            tabControl.selectTab("Basic");
            tabBasicValues.content.addClass("inputContainer");
            tabBasicValues.content.append("<label>Name:</label>");
            tabBasicValues.content.append("<input type='text' class='txtname' value='" + item.articleName + "' />");
            tabBasicValues.content.append("<label>Visible:</label>");
            tabBasicValues.content.append("<input type='checkbox' class='chkPublished' checkType='Yes,NO' label='Publish' />");
            tabBasicValues.content.append("<label>Choose Menu:</label>");
            tabBasicValues.content.append("<input type='text' class='txtMenus' value='None' />");
            tabBasicValues.content.append("<label>Choose Theme:</label>");
            tabBasicValues.content.append("<input type='text' class='txtTheme' selectedValue='" + (!item.folder_Id ? "" : item.folder_Id) + "' value='" + (!item.folder_Id ? "None" : item.theme.name) + "' />");

            tabBasicValues.content.find(".chkPublished").prop("checked", item.published && item.published === true);
            tabBasicValues.content.find(".txtMenus").autoFill({
                ajaxUrl: settings.autoFillMenusDataUrl,
                textField: "displayName",
                valueField: "id",
                selectedValue: item.MenusId,
                hideValues: undefined,
                additionalValues: [{ displayName: "None", id: "" }]
            });

            tabBasicValues.content.find(".txtTheme").autoFill({
                ajaxUrl: settings.getFoldersAutoFill,
                textField: "name",
                valueField: "id",
                additionalValues: [{ name: "None", id: 0 }],
                onSelect: function (op) {
                    if (op.selectedItem.id > 0) {
                        item.folder = op.selectedItem;
                        item.folder_Id = op.selectedItem.id;

                    } else
                        item.folder = null;
                    item.folder_Id = null;
                }
            });

            var countries = base.getActiveCountries();
            $.each(countries, function () {
                var node = $.grep(item.articleNodes, function (a) { return a.languageId == this.id });
                if (node.length <= 0) {
                    item.articleNodes.push({ languageId: this.id, pageHeader: "", content: "", tags: "" });
                    node = $(item.articleNodes).last()[0];
                } else node = node[0];

                tabBasicValues.content.append("<label>Header:</label>");
                tabBasicValues.content.append("<input type='text' class='txtheader' value='" + node.pageHeader + "' />");
            });


            tabContent.content.append(container.generateIFrame(function (iframe) {
                dialog.resize({ width: "97vw" });
                var tags = iframe.contents().find("tag");
                tags.click(function () {
                    var tag = $(this);
                    var tagContent = $("<div><textarea style='width:100%; height:800px;'></textarea></div>");
                    var editor = undefined;
                    tagContent.find("textarea").val(tag.html());
                    tagContent.find("textarea")[0].loadThemes = function (func) {
                        return globalSettings.theme.load(tabBasicValues.content.find(".txtTheme").attr("selectedValue"), func);
                    }
                    dialog.dialog({
                        screen: true,
                        content: tagContent,
                        title: "Tag content",
                        onSave: function () {
                            tag.html(editor.val());
                        }
                    }).show();
                    editor = tagContent.find("textarea").htmlarea({
                        // Override/Specify the Toolbar buttons to show
                        toolbar: [
                            ["html"], ["bold", "italic", "underline", "strikethrough", "|", "subscript", "superscript"],
                            ["increasefontsize", "decreasefontsize"],
                            ["orderedlist", "unorderedlist"],
                            ["indent", "outdent"],
                            ["justifyleft", "justifycenter", "justifyright"],
                            ["link", "unlink", "horizontalrule"],
                            ["p", "h1", "h2", "h3", "h4", "h5", "h6"],
                            ["cut", "copy", "paste", "table"],
                            [
                                {
                                    // The CSS class used to style the <a> tag of the toolbar button
                                    css: 'image tooltips',

                                    // The text to use as the <a> tags "Alt" attribute value
                                    text: 'Insert Image',

                                    // The callback function to execute when the toolbar button is clicked
                                    action: function (btn) {
                                        // 'this' = jHtmlArea object
                                        // 'btn' = jQuery object that represents the <a> ("anchor") tag for the toolbar buttons
                                        var item = this;
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
                                            onInsert: function (img) {
                                                try {
                                                    item.image(img);
                                                } catch (er) {
                                                    item.image(img);
                                                }
                                                item.updateTextArea();
                                            }
                                        });

                                    }
                                }, {
                                    // The CSS class used to style the <a> tag of the toolbar button
                                    css: 'forecolor tooltips',

                                    // The text to use as the <a> tags "Alt" attribute value
                                    text: 'Forecolor',

                                    // The callback function to execute when the toolbar button is clicked
                                    action: function (btn) {
                                        // 'this' = jHtmlArea object
                                        // 'btn' = jQuery object that represents the <a> ("anchor") tag for the toolbar button
                                        var item = this;
                                        var colorPicker = btn.ColorPicker({
                                            color: '#0000ff',
                                            onShow: function (colpkr) {
                                                $(colpkr).fadeIn(500);
                                                return false;
                                            },
                                            onHide: function (colpkr) {
                                                $(colpkr).fadeOut(500);
                                                return false;
                                            },
                                            onChange: function (hsb, hex, rgb) {
                                                item.forecolor("#" + hex);
                                            }
                                        });
                                        if (!btn.hasClass("ini")) {
                                            btn.addClass("ini");
                                            btn.click();
                                        }
                                    }
                                }
                            ]
                        ]
                    });
                });
            }));

            dialog = editContainer.dialog({
                content: tabControl,
                title: item.id > 0 ? "Edit/Delete" : "Add new",
                onSave: function () {
                    //return container.save(item, editContainer);
                },
                screen: true
            });
            dialog.show();
        }

        container.render = function () {
            container.html("");
            var table = $("<table><thead><tr></tr><tr></tr></thead><tbody></tbody></table>");
            table.find("thead >tr").first().append("<th colspan='5'><h2 class='header'>" +
                settings.header +
                "<a title='Delete selected items' class='delete'><span></span></a><span title='Add new item' class='addItem'></span></h2></th>");
            table.find("thead >tr").last().append("<th><input type='checkbox' /></th>")
                .append("<th>Name</th>")
                .append("<th>Published</th>")
                .append("<th>Menu</th>")
                .append("<th style='width: 40px;'></th>");

            table.find("thead").find(".addItem").click(function () {
                container.save(undefined);
            });

            table.find("thead").find(".delete").click(function () {
                var ids = [];
                table.find("tbody").find("input.item:checked").each(function () {
                    ids.push(eval($(this).attr("itemId")));
                });
                container.delete(ids);
            });
            container.append(table);

            table.find("thead >tr").last().find("input[type='checkbox']").change(function () {
                settings.allchecked = $(this).is(":checked");
                $.checkBox({ items: table.find("tbody").find(".item") }).prop(settings.allchecked);
            });

            container.LightDataAjax({
                contentType: "application/json",
                dataType: "json",
                type: "POST",
                data: JSON.stringify({ pageNr: 1, value: "" }),
                url: settings.getUri,
                success: function (data) {
                    function renderItem(item, tr) {
                        tr
                            .append("<td><input itemId='" + item.id + "' type='checkbox' class='item' /></td>")
                            .append("<td>" + item.articleName + "</td>")
                            .append("<td><input disabled='disabled' type='checkbox' value='" + item.published + "' /></td>")
                            .append("<td>" + item.menus.displayName + "</td>")
                            .append("<td><a class='delete'><span></span></a><a class='edit'><span></span></a></td>");

                        tr.find(".delete").click(function () {
                            container.delete([item.id]);
                        });
                    }

                    $.each(data, function () {
                        var tr = $("<tr></tr>");
                        renderItem(this, tr);
                        table.find("tbody").append(tr);
                    });
                    container.tableFix();
                }
            });
        }
        container.render();
    };
}(jQuery));