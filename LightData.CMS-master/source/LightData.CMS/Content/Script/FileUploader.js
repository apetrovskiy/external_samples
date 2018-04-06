(function ($) {
    $.fn.files = function (options) {
        var settings = $.extend({
            getUri: undefined,
            saveUri: undefined,
            deleteUri: undefined,
            folderGetUri: undefined,
            folderSaveUri: undefined,
            folderDeleteUri: undefined,
            accept: "image/*",
            getImageUri: undefined,
            saveFileItemUri: undefined,
            onInsert: function (img) { },
            imageProperties: undefined,
            ImageFiletypes: ["PNG", "GIF", "JPEG", "JPG"],
            fileTypes: { JAVASCRIPT: "JAVASCRIPT", CSS: "CSS", Image: "Image", HtmlEmbedded: "HtmlEmbedded", ROOT: "ROOT", ThemeContainer: "ThemeContainer" },
            view: "dialog",
            isImage: function (file) {
                var result = false;
                $.each(settings.fileTypes, function () {
                    result = result || file.toUpperCase().indexOf(this) != -1;
                });
                return result;
            }
        }, options);
        var $this = $(this);
        var dg;
        var container = $("<div class='fileManager'></div>");
        var folderContainer = $("<div class='folderContainer tableTree'></div>");
        var fileContainer = $("<div class='fileContainer'></div>");
        var filePreview = $("<div class='filePreview'></div>");
        container.append(folderContainer).append(fileContainer).append(filePreview);

        var timeOut = undefined;
        var pageNumber = 0;
        var selectedFolder = undefined;
        var folders = undefined;

        container.clearPreview = function () {
            filePreview.html("");
            dg.find(".btnSave").unbind();
            dg.find(".enlarge").unbind();
            dg.find(".delete,.zoomIn,.zoomOut,.enlarge,.btnSave").hide();

            dg.find(".delete,.zoomIn,.zoomOut,.enlarge,.btnSave").addClass("disabled");

        }

        container.renderFolders = function () {
            folderContainer.html("");
            filePreview.html("");
            container.LightDataAjax({
                contentType: "application/json",
                dataType: "json",
                type: "POST",
                url: settings.folderGetUri,
                success: function (data) {
                    var renderFiles = selectedFolder === undefined;
                    folders = data;
                    function renderChildren(items, parent) {
                        $.each(items, function () {
                            var tItem = this;
                            var div = $("<li></li>");
                            div.append("<a class='text'>" + this.name + "</a><span itemId='" + tItem.id + "' class='collapsed'></span>");

                            div.find("a").click(function () {
                                folderContainer.find(".selected").removeClass("selected");
                                $(this).addClass("selected");
                                selectedFolder = tItem
                                container.renderFiles(true);
                                var cloned = $.extend({}, tItem);
                                cloned.children = [];
                                SetValue("SelectedImageFolder", JSON.stringify(cloned));
                                SetValue(cloned.id, true);
                                container.clearPreview();
                            });

                            bindEdit(div.find(".text"), this);
                            if (this.children && this.children.length > 0) {
                                var ul = $("<ul class='subItem'></ul>");
                                ul.hide();
                                GetValue(this.id.toString(),
                                    function (data) {
                                        if (data === true) {
                                            div.find("span[itemId='" + tItem.id + "']").removeClass("collapsed").addClass("expanded");
                                            ul.show();
                                        }
                                    });
                                div.append(ul);
                                renderChildren(this.children, ul);
                            } else div.find("span[itemId='" + tItem.id + "']").css("visibility", "hidden");

                            parent.append(div);
                        });
                    }

                    var ulContainer = $("<ul style='display:block'></ul>");
                    folderContainer.append(ulContainer);
                    $(data).each(function () {
                        if (selectedFolder === undefined)
                            selectedFolder = this;
                        var tItem = this;
                        var div = $("<li></li>");
                        div.append("<a class='text'>" + this.name + "</a><span itemId='" + tItem.id + "' class='collapsed'></span>");
                        div.find("a").click(function () {
                            folderContainer.find(".selected").removeClass("selected");
                            $(this).addClass("selected");
                            selectedFolder = tItem
                            container.renderFiles(true);
                            var cloned = $.extend({}, tItem);
                            cloned.children = [];
                            SetValue("SelectedImageFolder", JSON.stringify(cloned));
                            SetValue(cloned.id, true);
                            container.clearPreview();
                        });

                        bindEdit(div.find(".text"), this);
                        if (this.children && this.children.length > 0) {
                            var ul = $("<ul class='subItem'></ul>");
                            ul.hide();
                            GetValue(this.id.toString(),
                                function (t) {
                                    if (t === true) {
                                        div.find("span[itemId='" + tItem.id + "']").removeClass("collapsed").addClass("expanded");
                                        ul.show();
                                    }
                                });
                            div.append(ul);
                            renderChildren(this.children, ul);
                        } else div.find("span[itemId='" + tItem.id + "']").css("visibility", "hidden");
                        ulContainer.append(div);
                    });

                    function folder() {
                        folderContainer.find("span:not([itemId=''])").click(function () {
                            if ($(this).hasClass("expanded")) {
                                $(this).parent().children("ul").hide();
                                $(this).removeClass("expanded").addClass("collapsed");
                                SetValue($(this).attr("itemId"), false);
                            } else {
                                $(this).parent().children("ul").show();
                                $(this).removeClass("collapsed").addClass("expanded");
                                SetValue($(this).attr("itemId"), true);
                            }
                        });
                    }

                    function bindEdit(li, item) {

                        var dataSource = [
                            {
                                text: "Rename",
                                id: 0,
                                item: item,
                                li: li
                            }, {
                                text: "Delete",
                                id: 1,
                                item: item,
                                li: li
                            }, {
                                text: (item.folderType == settings.fileTypes.Image ? "Upload" : "Create File"),
                                id: 2,
                                item: item,
                                li: li
                            }, {
                                text: (item.folderType == settings.fileTypes.ROOT ? "Create New Theme" : "New Folder"),
                                id: 3,
                                item: item,
                                li: li
                            },
                        ];
                        if (item.folderType === settings.fileTypes.ROOT) {
                            dataSource.shift();
                            dataSource.shift();
                            dataSource.shift();
                        } else if (item.folderType === settings.fileTypes.ThemeContainer) {
                            dataSource = [];
                        }

                        if (dataSource.length <= 0)
                            return;
                        li.contextMenu({
                            dataSource: dataSource,
                            click: function (tItem) {
                                if (tItem.id === 0) {
                                    var renameContainer = $("<div class='inputContainer'><label>Enter the folder name</label> <input type='text' value='" + tItem.item.name + "' </div>");
                                    $("body").dialog({
                                        title: "Enter the folder name",
                                        content: renameContainer,
                                        saveText: "Save",
                                        onSave: function (dialog) {

                                            if (renameContainer.find("input").val().length > 1)
                                                tItem.item.name = renameContainer.find("input").val();
                                            fileContainer.LightDataAjax({
                                                contentType: "application/json",
                                                dataType: "json",
                                                type: "POST",
                                                data: JSON.stringify({ folder: tItem.item }),
                                                url: settings.folderSaveUri,
                                                success: function (data) {
                                                    container.renderFolders();
                                                    dialog.close();
                                                }
                                            });
                                            return false;
                                        }
                                    }).show();

                                }
                                else if (tItem.id === 1) {
                                    $("body").dialog({
                                        title: "Please Confirm",
                                        content: "<p class='warning'>You will be deleting this folder and all its content, are you sure?</p>",
                                        saveText: "Yes",
                                        deleteText: "No",
                                        onSave: function (dialog) {
                                            fileContainer.LightDataAjax({
                                                contentType: "application/json",
                                                dataType: "json",
                                                type: "POST",
                                                data: JSON.stringify({ folderId: tItem.item.id }),
                                                url: settings.folderDeleteUri,
                                                success: function (data) {
                                                    container.renderFolders();
                                                    dialog.close();
                                                }
                                            });
                                            return false;
                                        }
                                    }).show();
                                } else if (tItem.id === 3) {
                                    var renameContainer = $("<div class='inputContainer'><label>Enter the new folder name</label> <input type='text' value='' </div>");
                                    $("body").dialog({
                                        title: (item.FolderType == settings.fileTypes.ROOT ? "Enter the new theme name" : "Enter the new folder name"),
                                        content: renameContainer,
                                        saveText: "Save",
                                        onSave: function (dialog) {
                                            var newItem = { name: renameContainer.find("input").val(), parent_Id: tItem.item.id, children: [] }
                                            if (tItem.item.folderType == settings.fileTypes.ROOT) {
                                                newItem.folderType = settings.fileTypes.ThemeContainer;
                                                newItem.children.push({ name: "CSS", folderType: settings.fileTypes.CSS });
                                                newItem.children.push({ name: "JAVASCRIPT", folderType: settings.fileTypes.JAVASCRIPT });
                                                newItem.children.push({ name: "MasterPage", folderType: settings.fileTypes.HtmlEmbedded });
                                                newItem.children.push({ name: "Images", folderType: settings.fileTypes.Image });
                                            } else {
                                                newItem.folderType = tItem.item.folderType;
                                            }

                                            fileContainer.LightDataAjax({
                                                contentType: "application/json",
                                                dataType: "json",
                                                type: "POST",
                                                data: JSON.stringify({ folder: newItem }),
                                                url: settings.folderSaveUri,
                                                success: function (data) {
                                                    container.renderFolders();
                                                    dialog.close();
                                                }
                                            });
                                            return false;

                                        }
                                    }).show();

                                } else if (tItem.id === 2) {
                                    container.newFile(tItem.item.id, tItem.item.name, tItem.item.folderType);
                                }
                            }
                        });
                    }

                    folder();

                    GetValue("SelectedImageFolder",
                        function (data) {
                            if (data && selectedFolder != data && data != "parsererror") {
                                selectedFolder = JSON.parse(data);
                                if (selectedFolder.parent_Id > 0 && !folderContainer.find("span[itemId='" + selectedFolder.parent_Id + "']").hasClass("expanded"))
                                    folderContainer.find("span[itemId='" + selectedFolder.parent_Id + "']").click();
                                folderContainer.find("span[itemId='" + selectedFolder.id + "']").parent().children("a").click();
                            }
                        });

                    folderContainer.find("span[itemId='" + selectedFolder.id + "']").parent().children("a").addClass("selected");

                    if (renderFiles)
                        container.renderFiles();
                }

            });
        }

        container.renderFiles = function (clean) {
            function bindEdit(li, item) {
                var moveTo = {
                    text: "Move To",
                    id: 4,
                    item: item,
                    li: li,
                    children: []
                }

                $.each(folders, function () {
                    var folder = this;
                    function renderChild(children, parent) {
                        $.each(children, function () {
                            var xfolder = this;
                            if (item.folder.folderType === xfolder.folderType && item.folder.id != xfolder.id) {
                                var arr = {
                                    text: xfolder.name,
                                    id: 5,
                                    folder: xfolder,
                                    item: item,
                                    li: li,
                                    children: [],
                                    isImage: settings.isImage(item.fileName)
                                }
                                if (xfolder.children.length) {
                                    renderChild(xfolder.children, arr);
                                }

                                parent.children.push(arr);
                            } else
                                if (xfolder.children.length) {
                                    renderChild(xfolder.children, parent);
                                }
                        });

                    }

                    renderChild([folder], moveTo);
                });

                var datasource = [
                    {
                        text: (item.folder.folderType === settings.fileTypes.Image ? "Upload" : "Create File"),
                        id: 2,
                        item: item,
                        li: li
                    },
                    {
                        text: "Rename",
                        id: 0,
                        item: item,
                        li: li
                    }, {
                        text: "Delete",
                        id: 1,
                        item: item,
                        li: li
                    },
                    moveTo
                ]
                if (moveTo.children.length <= 0)
                    datasource.pop();

                if (item.isSystem) {
                    datasource.shift();
                }

                li.contextMenu({
                    dataSource: datasource,
                    click: function (tItem) {
                        if (tItem.id === 0) {
                            var renameContainer = $("<div class='inputContainer'><label>Enter the folder name</label> <input type='text' value='" + tItem.item.fileName + "' </div>");
                            $("body").dialog({
                                title: "Enter the folder name",
                                content: renameContainer,
                                saveText: "Save",
                                onSave: function (dialog) {
                                    if (renameContainer.find("input").val().length > 1)
                                        tItem.item.fileName = renameContainer.find("input").val();
                                    fileContainer.LightDataAjax({
                                        contentType: "application/json",
                                        dataType: "json",
                                        type: "POST",
                                        data: JSON.stringify({ file: tItem.item }),
                                        url: settings.saveFileItemUri,
                                        success: function (data) {
                                            container.renderFiles(true);
                                            dialog.close();
                                        }
                                    });
                                    return false;
                                }
                            }).show();

                        }
                        else if (tItem.id === 1) {
                            $("body").dialog({
                                title: "Please Confirm",
                                content: "<p class='warning'>You will be deleting this file, are you sure?</p>",
                                saveText: "Yes",
                                deleteText: "No",
                                onSave: function (dialog) {
                                    fileContainer.LightDataAjax({
                                        contentType: "application/json",
                                        dataType: "json",
                                        type: "POST",
                                        data: JSON.stringify({ items: [tItem.item.id] }),
                                        url: settings.deleteUri,
                                        success: function (data) {
                                            container.renderFiles(true);
                                            dialog.close();
                                        }
                                    });
                                    return false;
                                }
                            }).show();
                        } else if (tItem.id === 2) {
                            container.newFile(tItem.item.folder_Id, tItem.item.folder.name, tItem.item.folder.folderType);
                        } else if (tItem.id > 4) {
                            tItem.item.folder_Id = tItem.folder.id;
                            $("body").LightDataAjax({
                                contentType: "application/json",
                                dataType: "json",
                                type: "POST",
                                data: JSON.stringify({ file: tItem.item }),
                                url: settings.saveFileItemUri,
                                success: function (data) {
                                    container.renderFiles(true);
                                }
                            });
                        }
                    }
                });
            }
            dg.find(".btnSave").unbind();
            dg.find(".enlarge").unbind();
            if (timeOut)
                clearTimeout(timeOut);
            if (clean) {

                filePreview.find(".CodeMirror").remove();
                filePreview.html("");
                settings.codeMirror = undefined;
                fileContainer.html("");
                pageNumber = 1;
            } else if (pageNumber == 0)
                pageNumber = 1;
            timeOut = setTimeout(function () {
                fileContainer.LightDataAjax({
                    contentType: "application/json",
                    dataType: "json",
                    type: "POST",
                    data: JSON.stringify({ pageNr: (pageNumber + (Math.ceil(fileContainer.find("div").length % 20))), folderId: selectedFolder.id }),
                    url: settings.getUri,
                    success: function (data) {
                        if (!data || data.length <= 0)
                            return;
                        pageNumber++;
                        $(data).each(function () {
                            var item = this;
                            var img = $("<div><img src='" + (item.folder.folderType === settings.fileTypes.Image ? "data:image/png" : "data:image/svg+xml") + ";base64," + this.base64ThumpFile + "' /><p> " + this.fileName + " </p></div>")
                            fileContainer.append(img);
                            bindEdit(img, item);
                            img.click(function () {
                                dg.find(".btnSave").unbind();
                                dg.find(".enlarge").unbind();
                                dg.find(".delete,.zoomIn,.zoomOut, .enlarge,.btnSave").show().removeClass("disabled");
                                var editContainer = $("<div class='inputContainer'></div>");
                                fileContainer.find(".selected").removeClass("selected");
                                img.addClass("selected");
                                filePreview.html("").append(editContainer);

                                if (item.folder.folderType !== settings.fileTypes.Image) {
                                    editContainer.append("<p>" + item.fileName + "</p>");
                                    editContainer.append("<textarea class='codeContainer'></textarea>");
                                    settings.codeMirror = CodeMirror.fromTextArea(editContainer.find("textarea")[0], {
                                        lineNumbers: true,
                                        mode: (item.FileType == 3 ? "javascript" : "css"),
                                        theme: "night",
                                    });
                                    var text = decodeURIComponent(item.text);
                                    settings.codeMirror.setValue(text);
                                    settings.codeMirror.setSize(500, 418);

                                    dg.find(".enlarge").click(function () {
                                        if ($(this).hasClass("disabled"))
                                            return false;
                                        settings.codeMirror.setSize($(window).width() - 50, 1000);
                                        var codeMirror = editContainer.find(".CodeMirror");
                                        var ddv = $("<div class='inputContainer'></div>");
                                        ddv.append(codeMirror);
                                        $("body").dialog({
                                            content: ddv,
                                            cancelText: "X",
                                            onCancel: function (dialog) {
                                                editContainer.append(codeMirror);
                                                settings.codeMirror.setSize(500, 418);
                                            },
                                            customButtons: [{
                                                text: "Image Manager",
                                                class: "imagemanager",
                                                click: function () {
                                                    dg.find(".imageManager").click();
                                                    return false;
                                                }
                                            }]
                                        }).show();
                                    });

                                    dg.find(".btnSave").click(function () {
                                        if ($(this).hasClass("disabled"))
                                            return false;
                                        item.text = encodeURIComponent(settings.codeMirror.getValue());
                                        editContainer.LightDataAjax({
                                            contentType: "application/json",
                                            dataType: "json",
                                            type: "POST",
                                            data: JSON.stringify({ file: item }),
                                            url: settings.saveFileItemUri,
                                            success: function (data) {
                                                container.renderFiles(true);
                                            }
                                        });
                                    })


                                } else if (item.folder.folderType === settings.fileTypes.Image) {
                                    dg.find(".btnSave,.imageManager ").hide();
                                    dg.find(".enlarge").hide();
                                    var tabControl = filePreview.tabs({
                                        onSelect: function () {
                                            var tab = tabControl.tab("Preview");
                                            var img = tab.content.find("img");
                                            img.attr("alt", editContainer.find(".alt").val());
                                            img.css({
                                                width: editContainer.find(".width").val(),
                                                height: editContainer.find(".height").val(),
                                                border: editContainer.find(".borderWidth").val() + "px " + editContainer.find(".borderStyle").val() + " #" + editContainer.find(".borderColor").val(),
                                            });
                                        }
                                    });

                                    var preview = tabControl.add("Preview", "Preview");
                                    var properties = tabControl.add("Properties", "Properties");
                                    preview.content.append("<p>" + item.fileName + "</p>");
                                    preview.content.append("<input type='text' style='    width: 99%;padding: 5px; border: 1px solid #CCC;' readonly class='fileUri' />");
                                    preview.content.append("<img src='" + (settings.getImageUri + "?id=" + item.id) + "' />");

                                    filePreview.GetItem = function () {
                                        return item;
                                    }

                                    preview.content.find(".fileUri").val((settings.getImageUri + "?id=" + item.id))
                                    tabControl.selectTab("Preview");

                                    if (settings.view == "dialog") {
                                        dg.settings.customButtons[0].click = function () {
                                            preview.content.find("img").css({ width: "+=10", height: "+=9" });
                                            editContainer.find(".width").val(preview.content.find("img").width());
                                            editContainer.find(".height").val(preview.content.find("img").height());
                                            return false;
                                        }

                                        dg.settings.customButtons[1].click = function () {
                                            preview.content.find("img").css({ width: "-=10", height: "-=9" });
                                            editContainer.find(".width").val(preview.content.find("img").width());
                                            editContainer.find(".height").val(preview.content.find("img").height());
                                            return false;
                                        }

                                    }
                                    properties.content.append(editContainer);
                                    editContainer.append("<label>Title:</label>");
                                    editContainer.append("<input type='text' class='title' value='" + item.title + "' />");

                                    editContainer.append("<label>Alt:</label>");
                                    editContainer.append("<input type='text' class='alt' value='" + item.alt + "' />");

                                    editContainer.append("<label>Width:</label>");
                                    editContainer.append("<input type='text' class='decimal width' value='" + item.width + "' />");
                                    editContainer.append("<label>Height:</label>");
                                    editContainer.append("<input type='text' class='decimal height' value='" + item.height + "' />");

                                    editContainer.append("<label>Border Width:</label>");
                                    editContainer.append("<input type='text' class='decimal borderWidth' value='" + item.borderWidth + "' />");

                                    editContainer.append("<label>Border Color:</label>");
                                    editContainer.append("<input type='text' class='borderColor color' value='" + item.borderColor + "' />");

                                    editContainer.append("<label>Border Style:</label>");
                                    editContainer.append("<input type='text' class='borderStyle' value='' />");
                                }
                            });

                        });

                    }

                });

            }, 100);
        }

        container.newFile = function (id, text, folderType) {
            folderType = typeof folderType === "undefined" ? selectedFolder.folderType : folderType;
            id = typeof id === "undefined" ? selectedFolder.id : id;
            text = typeof text === "undefined" ? selectedFolder.name : text;
            var allowedFile = "";
            $(settings.ImageFiletypes).each(function () {
                allowedFile += (allowedFile != "" ? "," : "") + this;
            });


            var uploadContainer = $("<div><table style='width:100%;'><tr><td colspan='2'>Folder: " + text + "</td> </tr><tr><td colspan='2'>AllowedFiles: " + allowedFile + "</td> </tr></table></div>");
            if (folderType !== settings.fileTypes.Image) {
                uploadContainer.find("tr").last().remove();
                var inputContainer = $("<div class='inputContainer'></div>");
                inputContainer.append("<label>File Name</label>");
                inputContainer.append("<input type='text' value='' />");

                inputContainer.append("<label>File type</label>");
                inputContainer.append("<input type='text' value='" + folderType + "' />");
                inputContainer.find("input").last().autoFill({
                    datasource: [
                        { name: "JAVASCRIPT", id: 0 },
                        { name: "CSS", id: 1 },
                        { name: "HtmlEmbedded", id: 2 }
                    ],
                    textField: "name",
                    valueField: "id",
                    disabled: true
                });
                uploadContainer.append(inputContainer);
                $("body").dialog({
                    title: "Create File",
                    content: uploadContainer,
                    saveText: "Save",
                    onSave: function (dialog) {
                        var fileItem = {
                            fileName: uploadContainer.find("input:first").val(),
                            fileType: uploadContainer.find("input:last").val(),
                            folder_Id: id
                        };

                        uploadContainer.LightDataAjax({
                            contentType: "application/json",
                            dataType: "json",
                            type: "POST",
                            data: JSON.stringify({ file: fileItem }),
                            url: settings.saveFileItemUri,
                            success: function (data) {
                                container.renderFiles(true);
                                dialog.close();
                            }
                        });
                        return false;
                    }
                }).show();

            } else {

                $("body").dialog({
                    title: "Add Files",
                    content: uploadContainer,
                    saveText: "Upload",
                    deleteText: "Upload file",
                    onSave: function (dialog) {
                        uploadContainer.find("uploadItem:hidden").remove();
                        var formData = new FormData();
                        uploadContainer.find("input").each(function (i, a) {
                            formData.append("image" + i, this.files[0]);
                        });
                        formData.append("folderId", id);
                        uploadContainer.LightDataAjax({
                            contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
                            processData: false, // NEEDED, DON'T OMIT THIS
                            type: "POST",
                            dataType: 'json',
                            data: formData,
                            url: settings.saveUri,
                            success: function (data) {
                                dialog.close();
                                container.renderFiles(true);
                            }

                        });
                        return false;

                    },
                    onDelete: function () {
                        uploadContainer.find("uploadItem:hidden").remove();
                        var uploadItem = $("<table style='width:100%;' class='uploadItem'><tr> <td> <img width='30' /> </td> <td> <input accept='" + settings.accept + "' type='file' /></td> <td><span class='delete'><span></span></span></td> </tr> </table>").hide();
                        function preview_image() {
                            var files = uploadItem.find("input")[0].files;
                            uploadItem.find("img").attr("src", URL.createObjectURL(files[0]));
                            var imgCloned = uploadItem.find("img").clone();
                            imgCloned.width("auto")
                            uploadItem.find("img").click(function () {
                                $("body").dialog({
                                    content: imgCloned,
                                }).show();
                            });
                        }

                        uploadContainer.append(uploadItem);
                        uploadItem.find("input").change(function (e) {
                            preview_image();
                            var value = $($(this).val().split("\\")).last();
                            uploadItem.find("td:nth-child(2)").append("<p>" + value[0] + "</p>");
                            uploadItem.show();
                            uploadItem.find("input").hide();

                        });

                        uploadItem.find(".delete").click(function () {
                            uploadItem.remove();
                        });

                        uploadItem.find("input").click();
                        return false;
                    }
                }).show()
            }
        }

        fileContainer.scroll(function () {
            container.renderFiles();
        });

        if (!settings.imageProperties) {
            container.renderFolders();
            if (settings.view == "dialog") {
                dg = $("body").dialog({
                    title: "Enter the folder name",
                    content: container,
                    saveText: "+",
                    deleteText: "Insert",
                    customButtons: [{
                        class: "zoomIn",
                        text: ""
                    },
                    {
                        class: "zoomOut",
                        text: ""
                    }],
                    onDelete: function () {
                        var img = filePreview.find("img").clone();
                        settings.onInsert(img, filePreview.GetItem());
                    },
                    onSave: function (dialog) {
                        container.newFile();
                        return false;
                    }
                });
                dg.show();
                dg.find(".delete").addClass("disabled");
            } else {
                container.prepend("<h2>Manage Themes <span class='imageManager' title='Image Manager'></span><span title='Enlarge' class='enlarge'></span><span title='Save Changes' class='btnSave'>Save</span></h2>");
                dg = container.children("h2").first();
                //dg.find(".enlarge,.btnSave").addClass("disabled");
                $this.append(container);
                container.children("h2").find(".imageManager").click(function () {
                    $("body").files({
                        getUri: settings.getUri,
                        saveUri: settings.saveUri,
                        deleteUri: settings.deleteUri,
                        folderGetUri: settings.folderGetUri,
                        folderSaveUri: settings.folderSaveUri,
                        folderDeleteUri: settings.folderDeleteUri,
                        getImageUri: settings.getImageUri,
                        saveFileItemUri: settings.saveFileItemUri,
                        fileTypes: settings.fileTypes,
                        onInsert: function (img) {

                        }
                    });
                });
            }

        } else {
            var img = $(settings.imageProperties);
            var editContainer = $("<div class='inputContainer'></div>");

            editContainer.append("<label>Alt:</label>");
            editContainer.append("<input type='text' class='alt' value='" + img.attr("alt") + "' />");

            editContainer.append("<label>Width:</label>");
            editContainer.append("<input type='text' class='decimal width' value='" + img.outerWidth() + "' />");
            editContainer.append("<label>Height:</label>");
            editContainer.append("<input type='text' class='decimal height' value='" + img.outerHeight() + "' />");

            editContainer.append("<label>Border Width:</label>");
            editContainer.append("<input type='text' class='decimal borderWidth' value='" + img.css("borderWidth").replace("px", "") + "' />");

            editContainer.append("<label>Border Color:</label>");
            editContainer.append("<input type='text' class='borderColor color' value='" + img.css("borderColor") + "' />");

            editContainer.append("<label>Border Style:</label>");
            editContainer.append("<input type='text' class='borderStyle' value='" + img.css("borderStyle") + "' />");

            $("body").dialog({
                content: editContainer,
                saveText: "Save",
                onSave: function () {
                    img.attr("alt", editContainer.find(".alt").val());
                    img.css({
                        width: editContainer.find(".width").val(),
                        height: editContainer.find(".height").val(),
                        border: editContainer.find(".borderWidth").val() + "px " + editContainer.find(".borderStyle").val() + " #" + editContainer.find(".borderColor").val(),
                    });
                }
            }).show();
        }

        return container;
    };
}(jQuery));