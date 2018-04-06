(function ($) {

    $.fn.setInputSelection = function (startPos, endPos) {
        var input = $(this)[0];
        if (input.setSelectionRange) {
            input.focus();
            input.setSelectionRange(startPos, endPos);
        } else if (input.createTextRange) {
            var range = input.createTextRange();
            range.collapse(true);
            range.moveEnd('character', endPos);
            range.moveStart('character', startPos);
            range.select();
        }
    };



    $.fn.getCursorPosition = function () {
        var input = this.get(0);
        if (!input) return; // No (input) element found
        if ('selectionStart' in input) {
            // Standard-compliant browsers
            return input.selectionStart;
        } else if (document.selection) {
            // IE
            input.focus();
            var sel = document.selection.createRange();
            var selLen = document.selection.createRange().text.length;
            sel.moveStart('character', -input.value.length);
            return sel.text.length - selLen;
        }
    };

    $.fn.fancyDatePicker = function (options) {
        var settings = $.extend({
            selectedDate: undefined,
            onDayClick: undefined,
            handler: undefined,
            format: undefined,
            readOnly: false,
            useMask: true,
            closeOnSelect: true,
            culture: "en-US",
            useTime: false,
            timeOnly: false,
            globalizationMapPath: "/globalization/"
        }, options);
        $(this).each(function () {
            if (settings.timeOnly)
                settings.useTime = settings.timeOnly;

            Date.CultureInfo = enus();
            var inputSelectedDate = settings.selectedDate;
            var culture;

            function loadLocalCulture() {
                culture = eval(enus);
                if (culture == undefined || culture === null) {
                    culture = enus;
                }
                if (culture)
                    culture();
            }

            // load culture
            var cultureName = settings.culture.toLowerCase().replace("-", "");
            if (Date.CultureInfo == undefined || Date.CultureInfo.name != settings.culture) {
                var url = settings.globalizationMapPath + settings.culture + ".js";
                try {
                    $.ajax({
                        async: false,
                        url: url,
                        dataType: "script"
                    }).error(function () { });
                }
                catch (err) { }
            }

            if (Date.CultureInfo == undefined)
                loadLocalCulture();
            culture = Date.CultureInfo;
            var input = $(this);
            var inputFormat = settings.format;
            var char = undefined;

            var timeFormat = settings.useTime ? " " + (culture.formatPatterns.shortTime.toLowerCase() + " ").replace(/:[m]{1} /g, ":mm").replace(/^[h]{1}:/g, "hh:").trim() : "";
            if (input.attr("format") != undefined && input.attr("format") != "")
                inputFormat = input.attr("format");

            if (inputFormat === undefined || inputFormat === "")
                inputFormat = culture.formatPatterns.shortDate;

            inputFormat = inputFormat.toLowerCase();
            char = inputFormat.replace(/[a-y]/g, "")[0];
            if (inputFormat.indexOf("m") != -1 && inputFormat.indexOf("mm") == -1)
                inputFormat = inputFormat.replace(/[m]/g, "mm");

            if (inputFormat.indexOf("m") != -1 && inputFormat.indexOf("dd") == -1)
                inputFormat = inputFormat.replace(/[d]/g, "dd");

            var handler = settings.handler;

            /// lets create a handler
            if (settings.handler === undefined) {
                var inputcontainer = $("<div class='fancyDatePickerInputcontainer fancyDatePickerIdentifire'><div class='calanderBoxContainer fancyDatePickerIdentifire'><div class='calanderBox'></div></div></div>");
                inputcontainer.height(input.outerHeight(true));
                input.replaceWith(inputcontainer);
                inputcontainer.append(input);
                input.css({ width: input.outerWidth(true) + inputcontainer.find(".calanderBox").outerWidth(true) })

                var offset = inputcontainer[0].getBoundingClientRect();
                inputcontainer.find(".calanderBoxContainer").css({ left: ((offset.width) - (inputcontainer.find(".calanderBox").outerWidth(true) * 1.7)) - 0.8 });

                //(offset.width + (inputcontainer.find(".calanderBoxContainer").width() / 2) + 1) - inputcontainer.find(".calanderBoxContainer").outerWidth(true) * 1.4
                handler = inputcontainer.find(".calanderBox");
            }

            input.addClass("fancyDatePickerIdentifire");
            if (settings.readOnly)
                input.attr("readonly", "readonly");

            if (handler == undefined)
                settings.handler = $(this);



            if (input.attr("handler") != undefined && input.attr("handler") != "")
                handler = $(input.attr("handler"));

            handler.addClass("fancyDatePickerIdentifire");
            var container = $('<div class="fancyDatePicker"></div>');


            function SelectDate(getOnly, date, setOnly) {
                if (date == undefined)
                    date = new Date();

                var dateString = "";
                var stringSplitter = inputFormat.split(char);
                if (timeFormat != "") {
                    var timeSpliter = timeFormat.trim().toLowerCase().replace(":mm", ":mmm").split(":");
                    var lastItem = timeSpliter[timeSpliter.length - 1];
                    if (lastItem.indexOf("tt") != -1) {
                        timeSpliter.pop();
                        timeSpliter = timeSpliter.concat(lastItem.split(" "));
                        timeSpliter.splice(timeSpliter.length - 1, 0, " ");
                    }
                    timeSpliter.splice(1, 0, ":");
                    stringSplitter = stringSplitter.concat(timeSpliter);
                }
                var counter = 0;
                var time = "";
                var hours = date.getHours();
                if ((date.getHours() > 12 && (culture.amDesignator != "")))
                    hours = ((hours + 11) % 12 + 1);

                while (counter != stringSplitter.length) {
                    var str = stringSplitter[counter].toLowerCase();
                    var addChar = true;
                    if (counter > 0)
                        dateString += char;

                    if (str == "dd" || str == "d") {
                        dateString += date.getDate() <= 9 ? "0" + date.getDate() : date.getDate();
                    } else if (str == "mm" || str == "m") {
                        dateString += date.getMonth() + 1 <= 9 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1);
                    } else if (str == "yy" || str === "yyyy") {
                        dateString += date.getFullYear();
                    } else if (str == "hh" || str === "h") {
                        time += hours <= 9 ? "0" + (hours) : (hours);
                        addChar = false;
                    } else if (str == " ") {
                        time += " ";
                        addChar = false;
                    } else if (str == ":") {
                        time += ":";
                        addChar = false;
                    } else if (str == "mmm") {
                        time += date.getMinutes() <= 9 ? "0" + (date.getMinutes()) : (date.getMinutes());
                        addChar = false;
                    } else if (str == "tt") {
                        time += date.getHours() >= 12 ? culture.pmDesignator : culture.amDesignator;
                        addChar = false;
                    } else addChar = false;
                    if (counter > 0 && !addChar) {
                        dateString = dateString.substring(0, dateString.length - 1);// remove last char
                    }
                    counter++;

                }

                dateString += " " + time;
                if (!getOnly) {
                    $(input).val(!settings.timeOnly ? dateString : time);
                    $(".timeDialog").remove();
                    container.remove();
                } else if (setOnly) {
                    $(input).val(!settings.timeOnly ? dateString : time);
                }
                return dateString;
            }

            function validateInputDate() {
                if (inputSelectedDate === undefined && input.val().length <= 1)
                    inputSelectedDate = new Date();
                else if (inputSelectedDate === undefined) {
                    if (!settings.timeOnly)
                        inputSelectedDate = new Date(input.val());
                    else inputSelectedDate = new Date("2017-01-01 " + input.val());
                    if (isNaN(inputSelectedDate.getDate()))
                        inputSelectedDate = new Date();
                    if (!hasIni && settings.useTime && input.val().indexOf(":") === -1) {
                        var time = new Date();
                        inputSelectedDate.setHours(time.getHours(), time.getMinutes(), 0, 0);
                        SelectDate(false, inputSelectedDate, true);
                    }
                }
            }



            var now = undefined;
            var timeout = undefined;
            var orgBackGound = undefined;
            var hasIni = false;
            validateInputDate();
            function BuildDate(staticDate, slide) {
                if (input.parent().find(".calanderBoxContainer").length > 0) {
                    if (orgBackGound == undefined)
                        orgBackGound = input.parent().find(".calanderBoxContainer").css("background");
                    if (timeout != undefined)
                        clearInterval(timeout);

                    timeout = setInterval(function () {
                        if (container != undefined && container.length > 0 && container.is(":visible"))
                            input.parent().find(".calanderBoxContainer").css({ background: "inherit" });
                        else {
                            input.parent().find(".calanderBoxContainer").css({ background: orgBackGound });
                            clearInterval(timeout);
                        }
                    }, 100);
                }

                if (!staticDate) {
                    validateInputDate();
                }

                if (isNaN(inputSelectedDate.getDate()))
                    inputSelectedDate = new Date();

                var dateNum = [0, 1, 2, 3, 4, 5, 6]
                function getDaysInMonth(month, year) {
                    var day = (month < 11 ? -6 : 0);
                    var date = new Date(year, month, (month > 1 ? -3 : 1));
                    var days = [];
                    var lastDayInMonth = new Date(year, month + 1, 0).getDate();

                    while ((month < 11 ? date.getMonth() <= month : (date.getMonth() <= month && date.getDate() != lastDayInMonth))) {
                        days.push(new Date(date));
                        date.setDate(date.getDate() + 1);
                    }

                    while (day < 0) {
                        days.push(new Date(date));
                        date.setDate(date.getDate() + 1);
                        day++;
                    }
                    return days;
                }

                $(".timeDialog").remove();
                $(".fancyDatePicker").remove();
                container = $('<div class="fancyDatePicker"></div>');
                $("body").append(container);


                /// Header
                if (!settings.timeOnly) {
                    var dayContainer = $('<div class="dayContainer"></div>');
                    container.append(dayContainer);
                    dayContainer.append("<div class='prev fancyDatePickerIdentifire'></div><div class='selectedMonth fancyDatePickerIdentifire'></div><div class='next fancyDatePickerIdentifire'></div>")
                    dayContainer.find(".selectedMonth").html(culture.monthNames[inputSelectedDate.getMonth()] + " " + inputSelectedDate.getFullYear());
                    dayContainer.find(".prev").click(function () {
                        if (inputSelectedDate.getMonth() - 1 >= 0)
                            inputSelectedDate.setMonth(inputSelectedDate.getMonth() - 1);
                        else {
                            inputSelectedDate.setYear(inputSelectedDate.getFullYear() - 1);
                            inputSelectedDate.setMonth(11);
                        }
                        BuildDate(true, "toRight");
                    });

                    dayContainer.find(".next").click(function () {
                        if (inputSelectedDate.getMonth() + 1 <= 11)
                            inputSelectedDate.setMonth(inputSelectedDate.getMonth() + 1);
                        else {
                            inputSelectedDate.setYear(inputSelectedDate.getFullYear() + 1);
                            inputSelectedDate.setMonth(1);
                        }
                        BuildDate(true, "toLeft");
                    });


                }
                /// Days
                if (!settings.timeOnly) {
                    dayContainer = $('<div class="dayContainer"></div>');
                    container.append(dayContainer);
                    $(culture.shortestDayNames).each(function () {
                        var day = $('<div dayName="' + this + '" class="day">' + this + '</div>');
                        day.width(dayContainer.width() / culture.shortestDayNames.length);
                        dayContainer.append(day);
                    });

                }
                dayContainer = $('<div class="dayContainer"></div>');
                container.append(dayContainer);
                if (now == undefined)
                    now = new Date(inputSelectedDate);
                var days = getDaysInMonth(now.getMonth(), now.getFullYear());
                var tempDate = new Date(inputSelectedDate);
                var exist = $.grep(days, function (a) { return a.setHours(0, 0, 0, 0) == tempDate.setHours(0, 0, 0, 0) });
                if (exist.length <= 0) {
                    now = inputSelectedDate;
                    days = getDaysInMonth(now.getMonth(), now.getFullYear());
                }

                function AddDay(day) {
                    if (container.find(".dayContainer").last().find(".day").length === 7) {
                        container.append('<div class="dayContainer"></div>');
                    }
                    day.width(dayContainer.width() / culture.shortestDayNames.length);
                    container.find(".dayContainer").last().append(day);
                }

                if (!settings.timeOnly)
                    $(days).each(function () {
                        var date = this;
                        var dayNum = (this.getDate());
                        var dayName = dateNum[this.getDay()];
                        var tempDay = container.find(".dayContainer").last().find(".day").length;
                        var day = $('<div date="" class="day">' + (dayNum) + '</div>');
                        if (this.getMonth() != now.getMonth())
                            day.addClass("notCurrentMonth");
                        date.setHours(inputSelectedDate.getHours());
                        date.setMinutes(inputSelectedDate.getMinutes());
                        var tempDatev2 = new Date(this)
                        if (tempDatev2.setHours(0, 0, 0, 0) === tempDate.setHours(0, 0, 0, 0))
                            day.addClass("selectedDate");
                        day.click(function () {

                            inputSelectedDate = date;
                            SelectDate(false, date);
                            if (settings.onDayClick != undefined) {
                                var option = { selectedDate: inputSelectedDate };
                                $.extend(option, settings);
                                settings.onDayClick(option, SelectDate(true, date));

                            }

                            if (!settings.closeOnSelect)
                                BuildDate();
                        });
                        AddDay(day);

                    });

                container.width((container.find(".dayContainer").last().find(".day").outerWidth(true) * 7) + 5);
                container.css({ top: offset.top + offset.height, left: offset.left });
                if (settings.useTime) {
                    var timeContainer = $('<div class="dayContainer timeControl fancyDatePickerIdentifire"><span class="timeText"></span><span class="hour fancyDatePickerIdentifire"></span><span class="timeSeperator fancyDatePickerIdentifire">:</span><span class="minutes fancyDatePickerIdentifire"></span><span class="pmam fancyDatePickerIdentifire"></span></div>');
                    var hours = inputSelectedDate.getHours();
                    if (hours > 12)
                        hours = ((hours + 11) % 12 + 1);

                    if (settings.timeOnly) {
                        (timeContainer).css("border-top", "0");

                    }

                    timeContainer.find(".hour").html(hours <= 9 ? "0" + hours : hours);
                    timeContainer.find(".minutes").html(inputSelectedDate.getMinutes() < 9 ? "0" + inputSelectedDate.getMinutes() : inputSelectedDate.getMinutes());
                    //if (input.attr("indicator") != "" && input.attr("indicator") !== undefined && culture.amDesignator != "")
                    //    timeContainer.find(".pmam").html(input.attr("indicator"));
                    //else
                    timeContainer.find(".pmam").html(inputSelectedDate.getHours() >= 12 ? "PM" : "AM");


                    timeContainer.find(".minutes, .hour").click(function () {
                        $(".timeDialog").remove();
                        var span = $(this);
                        var timeControls = $("<div class='timeDialog fancyDatePickerIdentifire' ></div>");
                        if (span.hasClass("hour")) {
                            for (var i = 1; i <= (culture.pmDesignator == "" ? 12 : 11); i++) {
                                var timeText = i <= 9 ? "0" + i : i;
                                timeControls.append("<div class='tm fancyDatePickerIdentifire'>" + timeText + "</div>");
                            }
                        } else {
                            var i = 0;
                            while (i <= 55) {
                                var timeText = i <= 9 ? "0" + i : i;
                                timeControls.append("<div class='tm fancyDatePickerIdentifire'>" + timeText + "</div>");

                                i += 5;
                            }
                        }

                        timeControls.find(".tm").click(function () {
                            span.html($(this).html());
                            var time = "";
                            if (span.hasClass("hour")) {
                                timeContainer.find(".hour").html(span.html());
                            } else timeContainer.find(".minutes").html(span.html());
                            timeControls.remove();
                            convertTimeformat(timeContainer.find(".hour").html() + ":" + timeContainer.find(".minutes").html() + " " + timeContainer.find(".pmam").html());
                            BuildDate();
                        });

                        $("body").append(timeControls);
                        var offset = span[0].getBoundingClientRect();
                        if (!settings.timeOnly)
                            timeControls.css({ left: offset.left, top: (offset.top - timeControls.outerHeight(true)) });
                        else timeControls.css({ left: offset.left, top: (offset.top) });

                    });



                    function convertTimeformat(time) {
                        var hours = Number(time.match(/^(\d+)/)[1]);
                        var minutes = Number(time.match(/:(\d+)/)[1]);
                        var AMPM = time.match(/\s(.*)$/)[1];
                        if (AMPM == "AM" && ((hours >= 12 && culture.pmDesignator != "") || hours > 12))
                            hours = ((hours + 11) % 12 + 1);
                        else {
                            if (AMPM == "PM" && hours < 12) hours = hours + 12;
                            if (AMPM == "AM" && hours == 12) hours = hours - 12;
                        }
                        var sHours = hours.toString();
                        var sMinutes = minutes.toString();
                        if (hours < 10) sHours = "0" + sHours;
                        if (minutes < 10) sMinutes = "0" + sMinutes;
                        inputSelectedDate.setHours(parseInt(sHours));
                        inputSelectedDate.setMinutes(parseInt(sMinutes));
                        SelectDate(undefined, inputSelectedDate, true);
                    }

                    timeContainer.find(".pmam").click(function () {
                        var orgValue = $(this).html();
                        if (orgValue.indexOf("PM") != -1) {
                            orgValue = "AM";
                        }
                        else {
                            orgValue = "PM";
                        }
                        input.attr("indicator", orgValue);

                        convertTimeformat(inputSelectedDate.getHours() + ":" + inputSelectedDate.getMinutes() + " " + orgValue);
                        BuildDate();

                    });
                    container.append(timeContainer);
                    hasIni = true;
                }



                var dayContainer = $('<div class="dayContainer"></div>');
                var today = new Date();
                var todayString = culture.shortestDayNames[today.getDay()] + ", " + culture.monthNames[today.getMonth()] + " " + (today.getDate() > 9 ? today.getDate() : "0" + today.getDate()) + ", " + today.getFullYear();
                dayContainer.append("<div class='footer fancyDatePickerIdentifire'><div class='today fancyDatePickerIdentifire'>" + todayString + "</div></div>");

                dayContainer.click(function () {
                    inputSelectedDate = today;
                    SelectDate(false, today);
                    if (settings.onDayClick != undefined) {
                        var option = { selectedDate: inputSelectedDate };
                        $.extend(option, settings);
                        settings.onDayClick(option, SelectDate(true, today));

                    }

                    if (!settings.closeOnSelect)
                        BuildDate();
                });
                container.append(dayContainer);
                container.css("border-top", "0");
                container.css("min-width", input.outerWidth(true) + 3);
                if (settings.timeOnly) {
                    container.find(".dayContainer").first().hide();
                    container.find(".dayContainer").last().hide();
                   
                }
                
                var allExeptLastTwo = $.grep(container.find(".dayContainer"), function (d, i) { return i < container.find(".dayContainer").length - 2 && i > 0 });
                if (slide === "toLeft") {
                    $(allExeptLastTwo).css("left", "-" + $(allExeptLastTwo).first().width());
                    $(allExeptLastTwo).animate({ "left": "0px" }, 200);
                } else if (slide === "toRight") {
                    $(allExeptLastTwo).css("left", $(allExeptLastTwo).first().width());
                    $(allExeptLastTwo).animate({ "left": "0px" }, 200);
                } else if (slide === true)
                    container.hide().slideDown(300);


            }

            /// assigning Handler
            $(this).unbind("click.fancyDatePicker");
            $(handler).bind("click.fancyDatePicker", function () {
                now = undefined;
                inputSelectedDate = undefined;

                if (handler.hasClass("calanderBox") && container.is(":visible")) {
                    container.slideUp(200, function () {
                        container.remove();
                        $(".timeDialog").remove();
                    });
                } else BuildDate(undefined, true);


            });

            function isEmptyOrSpaces(str) {
                return str === undefined || str === null || str.match(/^ *$/) !== null;
            }
            if (!settings.readOnly && settings.useMask) {
                $(this).unbind("click.fancyDatePicker");
                $(input).bind("click.fancyDatePicker", function () {
                    var start = input.getCursorPosition();
                    var end = start;
                    var value = input.val();
                    while (value[start] != char && start >= 0 && !isEmptyOrSpaces(value[start]) && value[start] != ":")
                        start--;
                    if (start < 0)
                        start = 0;
                    if (value[start] === char || isEmptyOrSpaces(value[start]) || value[start] === ":")
                        start++;
                    for (var i = start; i <= value.length; i++) {
                        if (value[end] === char || isEmptyOrSpaces(value[end]) || value[end] === ":")
                            break;
                        end++;
                    }
                    input.setInputSelection(start, end);
                });
            }

            if (settings.useMask) {
                var maskText = !settings.timeOnly ? (inputFormat + " " + timeFormat.trim()).toLowerCase() : timeFormat.trim().toLowerCase();
                var result;
                var keyisDown = false;
                input.unbind("keypress.fancyDatePicker");
                input.bind("keydown.fancyDatePicker", function (event) {

                    if (keyisDown) {
                        event.preventDefault();
                        return false;
                    }
                    keyisDown = true;
                });

                input.unbind("keyup.fancyDatePicker");
                input.bind("keyup.fancyDatePicker", function (event) {
                    var key = event.keyCode == undefined ? event.which : event.keyCode;
                    if (key == 46 || key == 8) {
                        keyisDown = false;
                        return true;
                    }


                    var start = input.getCursorPosition();
                    var key = event.keyCode == undefined ? event.which : event.keyCode;
                    var reg = eval("/[" + char + "]/g");
                    var value = input.val().toUpperCase();
                    var dateString = "";
                    var stringSplitter = (inputFormat).toLowerCase().split(char);
                    if (settings.timeOnly) {
                        value = SelectDate(true, new Date());
                        if (value.indexOf(" ") != -1)
                            value = value.substring(0, value.indexOf(" ") + 1);
                        value += input.val().toUpperCase();
                    } else if (!settings.useTime) {
                        value += " hh:mm" + (culture.amDesignator != "" ? " tt" : "");
                    }

                    var lastChars = "";
                    if (value.indexOf("P") != -1)
                        lastChars = value.substring(value.indexOf("P"));
                    else if (value.indexOf("A") != -1)
                        lastChars = value.substring(value.indexOf("A"));
                    value = value.replace(eval("/([" + culture.pmDesignator + "|" + culture.amDesignator + "])/g"), "").trim();
                    if (timeFormat != "") {
                        timeFormat += " ";
                        var timeSpliter = timeFormat.toLowerCase().trim().split(":");
                        var lastItem = timeSpliter[timeSpliter.length - 1];
                        if (lastItem.indexOf("tt") != -1) {
                            timeSpliter.pop();
                            timeSpliter = timeSpliter.concat(lastItem.split(" "));
                        }
                        timeSpliter.splice(1, 0, ":");
                        stringSplitter = stringSplitter.concat(timeSpliter);
                    }

                    var valueSplitter = [];
                    if (value.indexOf(char) != -1) {
                        valueSplitter = value.split(char);
                        if (valueSplitter[valueSplitter.length - 1].indexOf(" ") != -1) {
                            var lastItem = valueSplitter[valueSplitter.length - 1];
                            valueSplitter.pop();
                            valueSplitter = valueSplitter.concat(lastItem.split(" "));
                            lastItem = valueSplitter[valueSplitter.length - 1];
                            if (lastItem.indexOf(":") != -1) {
                                valueSplitter.pop();
                                var timeCon = lastItem.split(":");
                                timeCon.splice(1, 0, ":");
                                valueSplitter = valueSplitter.concat(timeCon);
                            }
                        }
                        if (lastChars.length > 0) {
                            valueSplitter.push(" " + lastChars);
                            value = value + " " + lastChars;
                        }
                    }
                    else valueSplitter.push(value)
                    if (valueSplitter.length == 1 && stringSplitter[0].length == valueSplitter[0].length && value.match(reg) === null) {
                        value += char
                        start++;
                    }
                    else if (valueSplitter.length == 2 && stringSplitter[1].length == valueSplitter[1].length && value.match(reg).length <= 1) {
                        value += char
                        start++;
                    }
                    else if (valueSplitter.length == 3 && stringSplitter[2].length == valueSplitter[2].length) {
                        value += " ";
                        start++;
                    } else if (valueSplitter.length == 4 && stringSplitter[3].length == valueSplitter[3].length && value.match(/:/g) === null) {
                        start++;
                        value += ":";
                    } else if (valueSplitter.length == 5 && stringSplitter[4].length == valueSplitter[4].length && value.match(/:/g) !== null) {
                        start++;
                        value += " ";
                    } else if (settings.useTime && culture.pmDesignator != "" && valueSplitter.length == 6 && stringSplitter[5].length == valueSplitter[5].length && value.match(/:/g) !== null) {
                        start++;
                        value += " ";
                    }
                    if (!settings.useTime)
                        value = value.replace(" hh:mm" + (culture.amDesignator != "" ? " tt" : ""), "");

                    if (value.length > maskText.length) {
                        if (settings.timeOnly) {
                            value = value.substring(value.indexOf(" ")).trim();
                        }

                        if (value.length > maskText.length)
                            value = value.substring(0, maskText.length);
                    }
                    if (value.indexOf("12:") != -1 && culture.amDesignator != "") {
                        value = value.replace("12:", "11:");
                    }

                    result = value;
                    if (result != input.val() || (container != undefined && container.length > 0 && container.is(":visible") && SelectDate(true, inputSelectedDate) != result)) {
                        input.val(result);
                        if (container != undefined && container.length > 0 && container.is(":visible")) {
                            inputSelectedDate = undefined;
                            BuildDate();
                        }
                        input.setInputSelection(start, start);
                    }
                    keyisDown = false;
                });
                //input.attr("placeholder", maskText.replace(/([y|d|m|h|t])/g, "_"));
                input.attr("placeholder", maskText.toUpperCase());

                input.change(function () {
                    //if (settings.timeOnly)
                    inputSelectedDate = undefined;
                    validateInputDate();
                    SelectDate(false, inputSelectedDate);
                })
            }

            $(document).click(function (e) {
                var target = $(e.target);
                if (!target.hasClass("fancyDatePickerIdentifire") && !target.hasClass("day") && !target.hasClass("dayContainer") && !target.hasClass("fancyDatePicker")) {
                    container.slideUp(200, function () {
                        container.remove();
                        $(".timeDialog").remove();
                    })
                    inputSelectedDate = undefined;
                    now = undefined;
                }
            });
        });
    };

    /// Default Lang other lang us contained in globalization map
    function enus() {

        Date.CultureInfo = {
            /* Culture Name */
            name: "en-US",
            englishName: "English (United States)",
            nativeName: "English (United States)",

            /* Day Name Strings */
            dayNames: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
            abbreviatedDayNames: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
            shortestDayNames: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"],
            firstLetterDayNames: ["S", "M", "T", "W", "T", "F", "S"],

            /* Month Name Strings */
            monthNames: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
            abbreviatedMonthNames: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],

            /* AM/PM Designators */
            amDesignator: "AM",
            pmDesignator: "PM",

            firstDayOfWeek: 0,
            twoDigitYearMax: 2029,
            dateElementOrder: "mdy",

            /* Standard date and time format patterns */
            formatPatterns: {
                shortDate: "M/d/yyyy",
                longDate: "dddd, MMMM dd, yyyy",
                shortTime: "h:mm tt",
                longTime: "h:mm:ss tt",
                fullDateTime: "dddd, MMMM dd, yyyy h:mm:ss tt",
                sortableDateTime: "yyyy-MM-ddTHH:mm:ss",
                universalSortableDateTime: "yyyy-MM-dd HH:mm:ssZ",
                rfc1123: "ddd, dd MMM yyyy HH:mm:ss GMT",
                monthDay: "MMMM dd",
                yearMonth: "MMMM, yyyy"
            }
        }
    }
}(jQuery));