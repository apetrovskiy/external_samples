(function ($) {
    $.toolTipIni = function () {
       
        function build() {
            var items = $("span:not([title='']):visible,a:not([title='']):visible,div:not([title='']):visible,input:not([title='']):visible,li:not([title='']):visible");
            $(items).each(function () {
                var o = $(this);
                if (!isNullOrEmpty(o.attr("title")) && !o.is(":hidden")) {
                    var offset = this.getBoundingClientRect();
                    var title = o.attr("title");
                    o.attr("title", "");
                    o.addClass("tooltips");
                    var toolSpan = $("<span class='toolSpan'>" + title + "</span>");
                    toolSpan.css({
                        top: (offset.top + offset.height) + 10,
                        left: offset.left + (offset.width / 2)
                    });
                    
                    o.append(toolSpan);
                    toolSpan.hide();
                    var timeOut = undefined;
                    o.mouseover(function (e) {
                        var offset = o[0].getBoundingClientRect();
                        clearTimeout(timeOut);
                        timeOut = setTimeout(function () {
                            toolSpan.css({
                                left: e.clientX,
                                top: e.clientY + 10
                            });
                            toolSpan.show();
                        },800);
                    }).mouseout(function() {
                        clearTimeout(timeOut);
                        toolSpan.hide();
                    });

                }
            });

            setTimeout(build, 100);
        }
        build();
    }

}(jQuery));