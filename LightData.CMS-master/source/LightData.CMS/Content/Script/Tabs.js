(function ($) {
    $.fn.tabs = function (options) {
        var settings = $.extend({
            onSelect: undefined,
            onTabAdded: function (addedTab) { },
            getTabByIndex: function (index) { },
            getTabByIdentifier: function (identifier) { },
            selectedTab: undefined
        }, options);

        var tabs = [];
        var container = $('<div class="tabContainer"><div class="tabs"></div></div>');
        if ($(this).hasClass("tabContainer"))
            container = $(this);

        container.tab = function (identifier) {
            var result = undefined;
            $(tabs).each(function () {
                if (this.id == identifier)
                    result = this;
            });
            return result;
        }

        container.selectTab = function (identifier) {
            container.find(".active").removeClass("active");
            $(tabs).each(function () {
                if (this.id == identifier) {
                    this.content.hide();
                    this.content.addClass("active");
                    $(this.tab).addClass("active");
                    settings.selectedTab = this;

                    this.content.show("fast");
                    if (settings.onSelect)
                        settings.onSelect(this);
                }
            });
            container.setPos();
        }

        container.setPos = function () {
            //var pos = container.find(".tabs")[0].getBoundingClientRect();
            //container.find(".tabs").find("label").width((pos.width / tabs.length) - (container.find(".tabs").find("label").outerWidth(true) - container.find(".tabs").find("label").width()));
            container.find(".tabs").find("label").css({ width: (98 / tabs.length) + "%" });
        }

        container.removeTab = function (identifier) {
            var temp = [];
            $(tabs).each(function () {
                if (this.id !== identifier)
                    temp.push(this);
            });

            container.find("[identifier='" + identifier + "']").remove();

            if (settings.selectedTab && settings.selectedTab.id === identifier)
                container.selectTab($(tabs).last().id);
            else container.setPos();
            tabs = temp;

        }

        container.add = function (identifier, text, content) {
            var tab = { id: identifier, tab: $("<label identifier='" + identifier + "'>" + text + "</label>"), content: $("<div identifier='" + identifier + "' class='tabContent'></div>").append(content) };
            tabs.push(tab);
            container.find(".tabs").append(tab.tab);
            container.append(tab.content);
            tab.tab.click(function () {
                if (!settings.selectedTab || settings.selectedTab.id !== identifier)
                    container.selectTab(identifier);
            });
            container.selectTab(identifier);
            return tab;
        }
        var timeOut = undefined
        if (timeOut === undefined)
            $(window).resize(function () {
                clearTimeout(timeOut);
                timeOut = setTimeout(container.setPos, 80);
            });

        if (!$(this).hasClass("tabContainer"))
            $(this).append(container);
        setTimeout(container.setPos, 100)
        return container;
    };
}(jQuery));