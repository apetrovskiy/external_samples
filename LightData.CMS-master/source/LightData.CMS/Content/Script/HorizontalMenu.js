(function ($) {
    $.fn.horizontalMenu = function (options) {
        // This is the easiest way to have default options.
        var settings = $.extend({
            datasource: []
        }, options);

        var container = $(this);
        container.render = function () {
            container.html("");
            var menuContainer = $("<ul></ul>");
            container.append(menuContainer);
            function renderChildren(items, parent) {
                $.each(items, function () {
                    if (this.publish == undefined || this.publish) {
                        var li = $("<li menuId='" + this.id + "'></li>");
                        li.append("<a href='" + this.uri + "'>" + this.displayName + "</a>");
                        if (this.uri === null || this.uri.length <= 1)
                            li.children("a").last().click(function () { return false; });

                        if (this.children && this.children.length > 0) {
                            li.append('<span class="dropRight"></span>');
                            var ul = $("<ul></ul>");
                            li.append(ul);
                            renderChildren(this.children, ul);
                        }
                        parent.append(li);
                    }
                });
            }
            $.each(settings.datasource, function () {
                var li = $("<li menuId='" + this.id + "'></li>");
                li.append("<a href='" + this.uri + "'>" + this.displayName + "</a>");
                if (this.children && this.children.length > 0) {
                    var ul = $("<ul></ul>");
                    li.append('<span class="dropBottom"></span>');
                    li.append(ul);
                    renderChildren(this.children, ul);
                }
                menuContainer.append(li);
            });
        }

        container.render();
        container.menumaker({
            title: "Menu",
            format: "multitoggle"
        });
        return container;
    };


    $.fn.menumaker = function (options) {

        var cssmenu = $(this), settings = $.extend({
            title: "Menu",
            format: "dropdown",
            sticky: false
        }, options);

        return this.each(function () {
            cssmenu.prepend('<div id="menu-button">' + settings.title + '</div>');
            $(this).find("#menu-button").on('click', function () {
                $(this).toggleClass('menu-opened');
                var mainmenu = $(this).next('ul');
                if (mainmenu.hasClass('open')) {
                    mainmenu.hide().removeClass('open');
                }
                else {
                    mainmenu.show().addClass('open');
                    if (settings.format === "dropdown") {
                        mainmenu.find('ul').show();
                    }
                }
            });

            cssmenu.find('li ul').parent().addClass('has-sub');

            multiTg = function () {
                cssmenu.find(".has-sub").prepend('<span class="submenu-button"></span>');
                cssmenu.find('.submenu-button').on('click', function () {
                    $(this).toggleClass('submenu-opened');
                    if ($(this).siblings('ul').hasClass('open')) {
                        $(this).siblings('ul').removeClass('open').hide();
                    }
                    else {
                        $(this).siblings('ul').addClass('open').show();
                    }
                });
            };

            if (settings.format === 'multitoggle') multiTg();
            else cssmenu.addClass('dropdown');

            if (settings.sticky === true) cssmenu.css('position', 'fixed');

            resizeFix = function () {
                if ($(window).width() > 768) {
                    cssmenu.find('ul').show();
                }

                if ($(window).width() <= 768) {
                    cssmenu.find('ul').hide().removeClass('open');
                    cssmenu.find(".menu-opened").removeClass("menu-opened");
                    cssmenu.find(".submenu-opened").removeClass("submenu-opened");
                }
            };
            resizeFix();
            return $(window).on('resize', resizeFix);

        });
    };

}(jQuery));