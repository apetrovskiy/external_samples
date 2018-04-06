/*
* jHtmlArea 0.8 - WYSIWYG Html Editor jQuery Plugin
* Copyright (c) 2013 Chris Pietschmann
* http://jhtmlarea.codeplex.com
* Licensed under the Microsoft Reciprocal License (Ms-RL)
* http://jhtmlarea.codeplex.com/license
*/
(function ($, window) {

    var $jhtmlarea = window.$jhtmlarea = {};
    var $browser = $jhtmlarea.browser = {};
    (function () {
        $browser.msie = false;
        $browser.mozilla = false;
        $browser.safari = false;
        $browser.version = 0;

        if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
            $browser.msie = true;
            $browser.version = parseFloat(RegExp.$1);
        } else if (navigator.userAgent.match(/Trident\/([0-9]+)\./)) {
            $browser.msie = true;
            $browser.version = RegExp.$1;
            if (navigator.userAgent.match(/rv:([0-9]+)\./)) {
                $browser.version = parseFloat(RegExp.$1);
            }
        }
        if (navigator.userAgent.match(/Mozilla\/([0-9]+)\./)) {
            $browser.mozilla = true;
            if ($browser.version === 0) {
                $browser.version = parseFloat(RegExp.$1);
            }
        }
        if (navigator.userAgent.match(/Safari ([0-9]+)\./)) {
            $browser.safari = true;
            $browser.version = RegExp.$1;
            if (navigator.userAgent.match(/Version\/([0-9]+)\./)) {
                if ($browser.version === 0) {
                    $browser.version = parseFloat(RegExp.$1);
                }
            }
        }
    })();

    $.fn.htmlarea = function (opts) {
        if (opts && typeof (opts) === "string") {
            var args = [];
            for (var i = 1; i < arguments.length; i++) { args.push(arguments[i]); }
            var htmlarea = jHtmlArea(this[0]);
            var f = htmlarea[opts];
            if (f) { return f.apply(htmlarea, args); }
        }
        return this.each(function () { jHtmlArea(this, opts); });
    };
    var jHtmlArea = window.jHtmlArea = function (elem, options) {
        if (elem.jquery) {
            return jHtmlArea(elem[0]);
        }
        if (elem.jhtmlareaObject) {
            return elem.jhtmlareaObject;
        } else {
            return new jHtmlArea.fn.init(elem, options);
        }
    };
    jHtmlArea.fn = jHtmlArea.prototype = {

        // The current version of jHtmlArea being used
        jhtmlarea: "0.8",

        init: function (elem, options) {
            if (elem.nodeName.toLowerCase() === "textarea") {
                var opts = $.extend({}, jHtmlArea.defaultOptions, options);
                elem.jhtmlareaObject = this;


                var textarea = this.textarea = $(elem);
                var container = this.container = $("<div/>").addClass("jHtmlArea").width(textarea.width()).insertAfter(textarea);



                var toolbar = this.toolbar = $("<div/>").addClass("ToolBar").appendTo(container);
                this.codeMirror = CodeMirror.fromTextArea(this.textarea[0], {
                    lineNumbers: true,
                    mode: "htmlmixed",
                    theme: "night",

                });
                priv.initToolBar.call(this, opts);

                var iframe = this.iframe = $("<iframe/>").css({ "min-height": textarea.height(), "min-width": textarea.width() });
                iframe.width(textarea.width());

                var htmlarea = this.htmlarea = $("<div/>").append(iframe);

                container.append(htmlarea).append(textarea.hide());
                priv.initEditor.call(this, opts);
                priv.attachEditorEvents.call(this);

                // Fix total height to match TextArea
                iframe.css({ "min-height": (iframe.height() - toolbar.height()) });

                toolbar.width(textarea.width());
                textarea[0].loadThemes(function (items) {
                    $.each($(items), function () {
                        var str = this.toString();
                        elem.jhtmlareaObject.find("head").append(str);
                    });
                })

                this.codeMirror.setSize(this.textarea.width(), iframe.height() - toolbar.height());
                if (opts.loaded) { opts.loaded.call(this); }

                $(".CodeMirror").hide();

                $("body").resize(function () {


                })
            }
        },
        dispose: function () {
            this.textarea.show().insertAfter(this.container);
            this.container.remove();
            this.textarea[0].jhtmlareaObject = null;
        },
        execCommand: function (a, b, c) {
            this.iframe[0].contentWindow.focus();
            if ($browser.msie === true && $browser.version >= 11) {
                if (this.previousRange) {
                    var rng = this.previousRange;
                    var sel = this.getSelection()
                    sel.removeAllRanges();
                    sel.addRange(rng);
                }
            }

            this.editor.execCommand(a, b || false, c || null);
            this.updateTextArea();
        },
        ec: function (a, b, c) {
            this.execCommand(a, b, c);
        },
        queryCommandValue: function (a) {
            this.iframe[0].contentWindow.focus();
            return this.editor.queryCommandValue(a);
        },
        qc: function (a) {
            return this.queryCommandValue(a);
        },
        getSelectedHTML: function () {
            if ($browser.msie) {
                return this.getRange().htmlText;
            } else {
                var elem = this.getRange().cloneContents();
                return $("<p/>").append($(elem)).html();
            }
        },
        find: function (exp) {
            return $(this.editor).find(exp);
        },
        getSelection: function () {
            if ($browser.msie === true && $browser.version < 11) {
                //return (this.editor.parentWindow.getSelection) ? this.editor.parentWindow.getSelection() : this.editor.selection;
                return this.editor.selection;
            } else {
                return this.iframe[0].contentDocument.defaultView.getSelection();
            }
        },
        getRange: function () {
            var s = this.getSelection();
            if (!s) { return null; }
            if (s.getRangeAt) {
                if (s.rangeCount > 0) {
                    return s.getRangeAt(0);
                }
            }
            if (s.createRange) {
                return s.createRange();
            }
            return null;
            //return (s.getRangeAt) ? s.getRangeAt(0) : s.createRange();
        },
        html: function (v) {
            if (v !== undefined) {
                this.codeMirror.setValue(v);
                //this.textarea.val(v);
                this.updateHtmlArea();
            } else {
                return this.toHtmlString();
            }
        },
        pasteHTML: function (html) {
            this.iframe[0].contentWindow.focus();
            var r = this.getRange();
            if ($browser.msie) {
                r.pasteHTML(html);
            } else if ($browser.mozilla) {
                if (r.deleteContents)
                    r.deleteContents();
                r.insertNode($((html.indexOf("<") != 0) ? $("<span/>").append(html) : html)[0]);
            } else { // Safari
                if (r.deleteContents)
                    r.deleteContents();
                r.insertNode($(this.iframe[0].contentWindow.document.createElement("span")).append($((html.indexOf("<") != 0) ? "<span>" + html + "</span>" : html))[0]);
            }
            r.collapse(false);
            if (r.select)
                r.select();
        },
        cut: function () {
            this.ec("cut");
        },
        copy: function () {
            this.ec("copy");
        },
        paste: function () {
            this.ec("paste");
        },
        bold: function () { this.ec("bold"); },
        italic: function () { this.ec("italic"); },
        underline: function () { this.ec("underline"); },
        strikeThrough: function () { this.ec("strikethrough"); },
        image: function (url) {
            if ($browser.msie === true && !url) {
                this.ec("insertImage", true);
            } else {
                if (!(typeof url === "string")) {
                    var htmlImage = $('<a></a>').append($(url).clone()).html();
                    this.pasteHTML(htmlImage);
                } else {
                    this.ec("insertImage", false, (url || prompt("Image URL:", "http://")));
                }
            }
        },
        removeFormat: function () {
            this.ec("removeFormat", false, []);
            this.unlink();
        },
        link: function () {
            if ($browser.msie === true) {
                this.ec("createLink", true);
            } else {
                this.ec("createLink", false, prompt("Link URL:", "http://"));
            }
        },
        unlink: function () { this.ec("unlink", false, []); },
        orderedList: function () { this.ec("insertorderedlist"); },
        unorderedList: function () { this.ec("insertunorderedlist"); },
        superscript: function () { this.ec("superscript"); },
        subscript: function () { this.ec("subscript"); },

        p: function () {
            this.formatBlock("<p>");
        },
        h1: function () {
            this.heading(1);
        },
        h2: function () {
            this.heading(2);
        },
        h3: function () {
            this.heading(3);
        },
        h4: function () {
            this.heading(4);
        },
        h5: function () {
            this.heading(5);
        },
        h6: function () {
            this.heading(6);
        },
        heading: function (h) {
            this.formatBlock($browser.msie === true ? "Heading " + h : "h" + h);
        },

        indent: function () {
            this.ec("indent");
        },
        outdent: function () {
            this.ec("outdent");
        },

        insertHorizontalRule: function () {
            this.ec("insertHorizontalRule", false, "ht");
        },

        justifyLeft: function () {
            this.ec("justifyLeft");
        },
        justifyCenter: function () {
            this.ec("justifyCenter");
        },
        justifyRight: function () {
            this.ec("justifyRight");
        },

        increaseFontSize: function () {
            if ($browser.msie === true) {
                this.ec("fontSize", false, this.qc("fontSize") + 1);
            } else if ($browser.safari) {
                this.getRange().surroundContents($(this.iframe[0].contentWindow.document.createElement("span")).css("font-size", "larger")[0]);
            } else {
                this.ec("increaseFontSize", false, "big");
            }
        },
        decreaseFontSize: function () {
            if ($browser.msie === true) {
                this.ec("fontSize", false, this.qc("fontSize") - 1);
            } else if ($browser.safari) {
                this.getRange().surroundContents($(this.iframe[0].contentWindow.document.createElement("span")).css("font-size", "smaller")[0]);
            } else {
                this.ec("decreaseFontSize", false, "small");
            }
        },

        forecolor: function (c) {
            this.ec("foreColor", false, c !== undefined ? c : prompt("Enter HTML Color:", "#"));
        },

        formatBlock: function (v) {
            this.ec("formatblock", false, v || null);
        },

        showHTMLView: function () {
            this.updateTextArea();
            var item = this;
            if (globalSettings && globalSettings.htmlFormater) {
                $.ajax({
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    data: JSON.stringify({ html: encodeURIComponent(this.codeMirror.getValue()) }),
                    url: globalSettings.htmlFormater,
                    success: function (data) {
                        $(".CodeMirror").show();
                        item.codeMirror.setValue(data);
                        $(item.textarea).val(data);
                        //item.textarea.show();

                        item.htmlarea.hide();
                        $("ul li:not(li:has(a.html))", item.toolbar).hide();
                        $("ul:not(:has(:visible))", item.toolbar).hide();
                        $("ul li a.html", item.toolbar).addClass("highlighted");
                    }
                });
            } else {
                //this.textarea.show();
                $(".CodeMirror").show();
                this.htmlarea.hide();
                $("ul li:not(li:has(a.html))", this.toolbar).hide();
                $("ul:not(:has(:visible))", this.toolbar).hide();
                $("ul li a.html", this.toolbar).addClass("highlighted");
            }

        },
        hideHTMLView: function () {
            this.updateHtmlArea();
            this.textarea.hide();
            $(".CodeMirror").hide();
            this.htmlarea.show();
            $("ul", this.toolbar).show();
            $("ul li", this.toolbar).show().find("a.html").removeClass("highlighted");
        },
        toggleHTMLView: function () {
            //(this.textarea.is(":hidden")) ? this.showHTMLView() : this.hideHTMLView();
            ($(".CodeMirror").is(":hidden")) ? this.showHTMLView() : this.hideHTMLView();

        },

        toHtmlString: function () {
            return this.editor.body.innerHTML;
        },
        toString: function () {
            return this.editor.body.innerText;
        },

        updateTextArea: function () {
            //this.textarea.val(this.toHtmlString());
            this.codeMirror.setValue(this.toHtmlString());
            var tables = this.find("table");
            var JHtml = this.editor.priv().toolbarButtons;
            var $this = this;
            if (tables.length > 0) {
                tables.each(function () {
                    var table = $(this)
                    table.contextMenu({
                        dataSource: [{ text: "Properties", item: table }, { text: "Drop", item: table }],
                        click: function (item) {
                            if (item.text == "Properties") {
                                JHtml.table(undefined, item.item, $this);
                            } else {
                                item.item.remove();
                            }
                        }
                    });
                });
            }


            var images = this.find("img");
            if (images.length > 0) {
                images.each(function () {
                    var image = $(this)
                    image.contextMenu({
                        dataSource: [{ text: "Properties", item: image }, { text: "Drop", item: image }],
                        click: function (item) {
                            if (item.text == "Properties") {
                                $("body").files({
                                    imageProperties: item.item,
                                    allowedFiles: ["PNG", "GIF", "JPEG"]
                                });
                            } else item.item.remove();
                        }
                    });
                });
            }
        },
        updateHtmlArea: function () {
            this.editor.body.innerHTML = this.codeMirror.getValue();

        }
    };
    jHtmlArea.fn.init.prototype = jHtmlArea.fn;
    var priv = undefined;
    jHtmlArea.defaultOptions = {
        toolbar: [
            ["html"], ["bold", "italic", "underline", "strikethrough", "|", "subscript", "superscript"],
            ["increasefontsize", "decreasefontsize"],
            ["orderedlist", "unorderedlist"],
            ["indent", "outdent"],
            ["justifyleft", "justifycenter", "justifyright"],
            ["link", "unlink", "image", "horizontalrule"],
            ["p", "h1", "h2", "h3", "h4", "h5", "h6"],
            ["cut", "copy", "paste", "table"]
        ],
        css: null,
        toolbarText: {
            bold: "Bold", italic: "Italic", underline: "Underline", strikethrough: "Strike-Through",
            cut: "Cut", copy: "Copy", paste: "Paste",
            h1: "Heading 1", h2: "Heading 2", h3: "Heading 3", h4: "Heading 4", h5: "Heading 5", h6: "Heading 6", p: "Paragraph",
            indent: "Indent", outdent: "Outdent", horizontalrule: "Insert Horizontal Rule",
            justifyleft: "Left Justify", justifycenter: "Center Justify", justifyright: "Right Justify",
            increasefontsize: "Increase Font Size", decreasefontsize: "Decrease Font Size", forecolor: "Text Color",
            link: "Insert Link", unlink: "Remove Link", image: "Insert Image",
            orderedlist: "Insert Ordered List", unorderedlist: "Insert Unordered List",
            subscript: "Subscript", superscript: "Superscript",
            html: "Show/Hide HTML Source View", table: "Manage Table Settings"
        }
    };
    priv = {
        toolbarButtons: {
            strikethrough: "strikeThrough", orderedlist: "orderedList", unorderedlist: "unorderedList",
            horizontalrule: "insertHorizontalRule",
            justifyleft: "justifyLeft", justifycenter: "justifyCenter", justifyright: "justifyRight",
            increasefontsize: "increaseFontSize", decreasefontsize: "decreaseFontSize",
            html: function (btn) {
                this.toggleHTMLView();
            },
            table: function (btn, tb, jHtml) {
                var cn = this;
                var iframe = $("<iframe></iframe>").width(1040).height(700);
                var appendOnly = tb != undefined;
                var jHtml = typeof jHtml == "undefined" ? this : jHtml;
                var table = $("<table class='block'><tfoot><tr><td colspan='15'><input readyonly='readonly' /><span>x</span><input readyonly='readonly' /></td></tr></tfoot><tbody></tbody></table>");
                var div = $("<table style='width:900px;'><tbody><tr><td style='width:35%;vertical-align: top;border-right: 1px solid #CCC;'><div class='inputContainer'></div></td><td style='width:65%;vertical-align: top;'><div class='inputContainer'></div></td></tr><tbody></table>");
                var generatedTable = undefined;

                iframe[0].onload = function () {
                    var headItems = $("head").find("link,script").clone();
                    headItems.append($(jHtml.editor).find("head").find("link,script").clone());
                    iframe.contents().find("body").append(div);
                    iframe.contents().find("head").append(headItems);
                    function renderDemo(loaded) {
                        div.find(".inputContainer:last").html("");
                        var demoTable = appendOnly ? tb.clone() : $("<table class='" + div.find(".theme").val() + "'><thead></thead><tbody></tbody></table>");
                        demoTable.attr("class", div.find(".theme").val());
                        var totalTd = eval(table.find("input").last().val());
                        var totalTr = eval(table.find("input").first().val());
                        var createThead = div.find("td:first .inputContainer").find("input.chkHeader").is(":checked");
                        var demoText = div.find("td:first .inputContainer").find("input.chkText").is(":checked");
                        var float = div.find("td:first .inputContainer").find("input.float").val();
                        for (var i = 1; i <= totalTr; i++) {
                            var tr = $("<tr></tr>");
                            if (appendOnly && tb.find("tbody tr").length >= i)
                                tr = demoTable.find("tbody tr:nth-child(" + i + ")");
                            for (var x = 1; x <= totalTd; x++) {
                                if (tr.find("td:nth-child(" + x + ") ").length > 0)
                                    continue;
                                tr.append("<td>" + (demoText ? "cell" + i + "_" + x : "&nbsp;") + "</td>");
                            }
                            if (demoTable.find("tbody tr:nth-child(" + i + ")").length <= 0)
                                demoTable.find("tbody").append(tr);
                        }
                        if (createThead) {
                            var tr = $("<tr></tr>");
                            if (demoTable.find("thead tr").length > 0)
                                tr = demoTable.find("thead tr").first();
                            for (var x = 1; x <= totalTd; x++) {
                                if (tr.find("th").length >= x)
                                    continue;
                                tr.append("<th>" + (demoText ? "head" + x : "&nbsp;") + "</th>");
                            }
                            if (demoTable.find("thead tr").length <= 0)
                                demoTable.find("thead").append(tr);
                        } else
                            demoTable.find("thead").remove();


                        demoTable.css({ float: float });
                        div.find(".inputContainer:last").append(demoTable);
                        if (loaded || div.find("td:first .inputContainer").find(".height").val().length <= 1) {
                            div.find("td:first .inputContainer").find(".width").val(demoTable.css("width"));
                            div.find("td:first .inputContainer").find(".height").val(demoTable.css("height"));
                        } else {
                            demoTable.css("width", div.find("td:first .inputContainer").find(".width").val());
                            demoTable.css("height", div.find("td:first .inputContainer").find(".height").val());
                            div.find("td:first .inputContainer").find(".width").val(demoTable.css("width"));
                            div.find("td:first .inputContainer").find(".height").val(demoTable.css("height"));
                        }
                        generatedTable = demoTable;
                    }


                    div.find("td:first .inputContainer").append("<label>Theme</label>");
                    div.find("td:first .inputContainer").append("<input class='theme' type='text' value='default' />");
                    var themes = [{
                        text: "default",
                        id: 0,
                    }, {
                        text: "greenLight",
                        id: 1
                    }, {
                        text: "rwd-table",
                        id: 2
                    },
                    {
                        text: "blured-table",
                        id: 3
                    }];

                    function readTable(demoTable) {
                        div.find("td:first .inputContainer").find(".width").val(demoTable.css("width"));
                        div.find("td:first .inputContainer").find(".height").val(demoTable.css("height"));
                        if (demoTable.find("thead").length > 0)
                            div.find("td:first .inputContainer").find("input.chkHeader").prop("checked", true);
                        else div.find("td:first .inputContainer").find("input.chkHeader").prop("checked", false);

                        if (demoTable.css("float") != "")
                            div.find("td:first .inputContainer").find("input.float").val(demoTable.css("float"));
                        div.find("td:first .inputContainer").find(".width").val(demoTable.css("width"));
                        div.find("td:first .inputContainer").find(".height").val(demoTable.css("height"));
                        table.find("input").last().val(demoTable.find("tbody tr:first td").length - 1);
                        table.find("input").first().val(demoTable.find("tbody tr").length - 1);

                        var css = demoTable.attr("class");
                        if (css.indexOf("default") != -1)
                            div.find("td:first .inputContainer").find(".theme").val("default");
                        else if (css.indexOf("greenLight") != -1)
                            div.find("td:first .inputContainer").find(".theme").val("greenLight");
                        else if (css.indexOf("rwd-table") != -1)
                            div.find("td:first .inputContainer").find(".theme").val("rwd-table");
                        else if (css.indexOf("blured-table") != -1)
                            div.find("td:first .inputContainer").find(".theme").val("blured-table");
                        else {
                            div.find("td:first .inputContainer").find(".theme").val(css);
                            themes.push({
                                text: css,
                                id: 4
                            });
                        }
                        renderDemo();
                    }

                    div.find("td:first .inputContainer").find("input.theme").autoFill({
                        datasource: themes,
                        textField: "text",
                        idField: "id",
                        onSelect: function () {
                            renderDemo(true);
                        }
                    });


                    div.find("td:first .inputContainer").append("<label>Float</label>");
                    div.find("td:first .inputContainer").append("<input class='float' type='text' value='none' />");
                    div.find("td:first .inputContainer").find(".float").autoFill({
                        datasource: [{
                            text: "none",
                            id: "none",
                        }, {
                            text: "left",
                            id: "left"
                        }, {
                            text: "right",
                            id: "right"
                        },
                        {
                            text: "inherit",
                            id: "inherit"
                        },
                        {
                            text: "initial",
                            id: "initial"
                        },
                        {
                            text: "unset",
                            id: "unset"
                        }],
                        textField: "text",
                        idField: "id",
                        onSelect: function () {
                            renderDemo(true);
                        }
                    });

                    div.find("td:first .inputContainer").append("<label>Width</label>");
                    div.find("td:first .inputContainer").append("<input class='width' type='text' />");

                    div.find("td:first .inputContainer").append("<label>Height</label>");
                    div.find("td:first .inputContainer").append("<input class='height' type='text' />");

                    div.find("td:first .inputContainer").append("<label>Include Header</label>");
                    div.find("td:first .inputContainer").append("<input type='checkbox' checked='checked' class='chkHeader' checkType='Yes,NO' value='none' />");

                    div.find("td:first .inputContainer").append("<label>Demo Text</label>");
                    div.find("td:first .inputContainer").append("<input type='checkbox' checked='checked' class='chkText' checkType='Yes,NO' value='none' />");

                    div.find("td:first .inputContainer").append("<label>Table-Size</label>");

                    div.find(".width, .height ,input[type='checkbox']").change(function () {
                        renderDemo();
                    });



                    for (var i = 1; i <= 15; i++) {
                        var tr = $("<tr></tr>");
                        for (var x = 1; x <= 15; x++) {
                            tr.append("<td></td>");
                            if (appendOnly && tb.find("tbody tr").length >= i && tb.find("tbody tr:first td").length >= x) {
                                tr.find("td:last").addClass("selected").addClass("disabled");
                            }
                        }
                        table.find("tbody").append(tr);
                    }

                    var clickd = false;
                    table.mousedown(function () {
                        clickd = !clickd;
                        if (clickd == false)
                            renderDemo();
                    });


                    table.find("tbody").find("td").mouseover(function () {
                        if (!clickd)
                            return;
                        var trIndex = $(this).parent().index();
                        var tdIndex = $(this).index();
                        var addClass = $(this).hasClass("selected");
                        $(table).find(".selected:not(.disabled)").removeClass("selected");
                        $(table).find("tr").each(function () {
                            if (trIndex >= $(this).index()) {
                                $(this).find("td").each(function () {
                                    if (tdIndex >= $(this).index()) {
                                        if (!$(this).hasClass("disabled"))
                                            $(this).addClass("selected")
                                        table.find("input").last().val(tdIndex + 1);
                                        table.find("input").first().val(trIndex + 1);
                                    }
                                });
                            }
                        });

                    });
                    div.find("td:first .inputContainer").append(table);
                    if (appendOnly)
                        readTable(tb);
                }

                $(this.editor).dialog({
                    content: iframe,
                    title: "Table Properties",
                    onSave: function () {
                        generatedTable.attr("title", "Right click to edit")
                        $(generatedTable).find("disabled").removeClass("disabled");
                        if (!tb)
                            jHtml.pasteHTML($('<a></a>').append($(generatedTable).clone()).html());
                        else tb.replaceWith($('<a></a>').append($(generatedTable).clone()).html());
                        jHtml.updateTextArea();
                    }
                }).show();


            }
        },
        initEditor: function (options) {
            var edit = this.editor = this.iframe[0].contentWindow.document;
            edit.priv = function () { return priv; };
            edit.designMode = 'on';
            edit.open();
            edit.write(this.textarea.val());
            edit.close();
            if (options.css) {
                var e = edit.createElement('link'); e.rel = 'stylesheet'; e.type = 'text/css'; e.href = options.css; edit.getElementsByTagName('head')[0].appendChild(e);
            }
        },
        initToolBar: function (options) {
            var that = this;

            var menuItem = function (className, altText, action) {
                return $("<li/>").append($("<a href='javascript:void(0);'/>").addClass(className).attr("title", altText).click(function () { action.call(that, $(this)); }));
            };

            function addButtons(arr) {
                var ul = $("<ul/>").appendTo(that.toolbar);
                for (var i = 0; i < arr.length; i++) {
                    var e = arr[i];
                    if ((typeof (e)).toLowerCase() === "string") {
                        if (e === "|") {
                            ul.append($('<li class="separator"/>'));
                        } else {
                            var f = (function (e) {
                                // If button name exists in priv.toolbarButtons then call the "method" defined there, otherwise call the method with the same name
                                var m = priv.toolbarButtons[e] || e;
                                if ((typeof (m)).toLowerCase() === "function") {
                                    return function (btn) { m.call(this, btn); };
                                } else {
                                    return function () { this[m](); this.editor.body.focus(); };
                                }
                            })(e.toLowerCase());
                            var t = options.toolbarText[e.toLowerCase()];
                            ul.append(menuItem(e.toLowerCase(), t || e, f));
                        }
                    } else {
                        ul.append(menuItem(e.css, e.text, e.action));
                    }
                }
            };
            if (options.toolbar.length !== 0 && priv.isArray(options.toolbar[0])) {
                for (var i = 0; i < options.toolbar.length; i++) {
                    addButtons(options.toolbar[i]);
                }
            } else {
                addButtons(options.toolbar);
            }
        },
        attachEditorEvents: function () {
            var t = this;

            var fnHA = function () {
                t.updateHtmlArea();
            };

            this.textarea.click(fnHA).
                keyup(fnHA).
                keydown(fnHA).
                mousedown(fnHA).
                blur(fnHA);

            this.iframe.blur(function () {
                t.previousRange = t.getRange();
            });

            var fnTA = function () {
                t.updateTextArea();
            };

            $(this.editor.body).click(fnTA).
                keyup(fnTA).
                keydown(fnTA).
                mousedown(fnTA).
                blur(fnTA);

            $('form').submit(function () { t.toggleHTMLView(); t.toggleHTMLView(); });
            //$(this.textarea[0].form).submit(function() { //this.textarea.closest("form").submit(function() {


            // Fix for ASP.NET Postback Model
            if (window.__doPostBack) {
                var old__doPostBack = __doPostBack;
                window.__doPostBack = function () {
                    if (t) {
                        if (t.toggleHTMLView) {
                            t.toggleHTMLView();
                            t.toggleHTMLView();
                        }
                    }
                    return old__doPostBack.apply(window, arguments);
                };
            }

        },
        isArray: function (v) {
            return v && typeof v === 'object' && typeof v.length === 'number' && typeof v.splice === 'function' && !(v.propertyIsEnumerable('length'));
        }
    };
})(jQuery, window);