(function ($) {
    $.fn.loader = function () {
        var container = $(this);
        var className = "LightDataTableLoader LightDataTableLoader" + $(".LightDataTableLoader").length;
        var loader = $("<div class='" + className + "'></div>");

        var canvas = document.createElement('canvas');
        canvas.id = "CursorLayer";
        canvas.width = 46;
        canvas.height = 46;
        loader.append(canvas);
        container.Start = function () {
            container.append(loader);
            var start = null;
            var duration = 3000;
            var boundaryIncrementer = duration / 6;
            function drawDivisionLoader(timestamp) {
                // Timing Setup
                if (!start) {
                    start = timestamp;
                }

                var ctx = canvas.getContext('2d');
                ctx.clearRect(0, 0, canvas.width, canvas.height);

                // Draw inner orange circle
                ctx.lineWidth = 1.5;
                ctx.strokeStyle = '#d89747';
                ctx.beginPath();
                ctx.arc(22, 22, 13, 0, 2 * Math.PI);
                ctx.stroke();
                ctx.closePath();

                // Draw outer circle
                ctx.lineWidth = 1;
                ctx.strokeStyle = '#363537';
                ctx.beginPath();
                ctx.arc(22, 22, 20.5, 0, 2 * Math.PI);
                ctx.stroke();

                // Draw animating arcs
                ctx.lineWidth = 2.5;

                // Find the remainder
                var remainder = (timestamp - start) % 3000;

                // Find out where the remainder lies within the boundaries
                if (remainder >= 0 && remainder <= (boundaryIncrementer)) {
                    // Arc 1
                    ctx.strokeStyle = '#d89747';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (332 * Math.PI) / 180, (27 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 2
                    ctx.strokeStyle = '#363537';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (32 * Math.PI) / 180, (87 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 3
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (92 * Math.PI) / 180, (147 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 4
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (152 * Math.PI) / 180, (207 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 5
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (212 * Math.PI) / 180, (267 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 6
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (272 * Math.PI) / 180, (327 * Math.PI) / 180);
                    ctx.stroke();
                }

                if (remainder > (boundaryIncrementer) && remainder <= (boundaryIncrementer * 2)) {
                    // Arc 1
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (332 * Math.PI) / 180, (27 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 2
                    ctx.strokeStyle = '#d89747';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (32 * Math.PI) / 180, (87 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 3
                    ctx.strokeStyle = '#363537';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (92 * Math.PI) / 180, (147 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 4
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (152 * Math.PI) / 180, (207 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 5
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (212 * Math.PI) / 180, (267 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 6
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (272 * Math.PI) / 180, (327 * Math.PI) / 180);
                    ctx.stroke();
                }

                if (remainder > (boundaryIncrementer * 2) && remainder <= (boundaryIncrementer * 3)) {
                    // Arc 1
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (332 * Math.PI) / 180, (27 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 2
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (32 * Math.PI) / 180, (87 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 3
                    ctx.strokeStyle = '#d89747';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (92 * Math.PI) / 180, (147 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 4
                    ctx.strokeStyle = '#363537';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (152 * Math.PI) / 180, (207 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 5
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (212 * Math.PI) / 180, (267 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 6
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (272 * Math.PI) / 180, (327 * Math.PI) / 180);
                    ctx.stroke();
                }

                if (remainder > (boundaryIncrementer * 3) && remainder <= (boundaryIncrementer * 4)) {
                    // Arc 1
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (332 * Math.PI) / 180, (27 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 2
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (32 * Math.PI) / 180, (87 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 3
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (92 * Math.PI) / 180, (147 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 4
                    ctx.strokeStyle = '#d89747';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (152 * Math.PI) / 180, (207 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 5
                    ctx.strokeStyle = '#363537';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (212 * Math.PI) / 180, (267 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 6
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (272 * Math.PI) / 180, (327 * Math.PI) / 180);
                    ctx.stroke();
                }

                if (remainder > (boundaryIncrementer * 4) && remainder <= (boundaryIncrementer * 5)) {
                    // Arc 1
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (332 * Math.PI) / 180, (27 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 2
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (32 * Math.PI) / 180, (87 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 3
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (92 * Math.PI) / 180, (147 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 4
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (152 * Math.PI) / 180, (207 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 5
                    ctx.strokeStyle = '#d89747';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (212 * Math.PI) / 180, (267 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 6
                    ctx.strokeStyle = '#363537';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (272 * Math.PI) / 180, (327 * Math.PI) / 180);
                    ctx.stroke();
                }

                if (remainder > (boundaryIncrementer * 5) && remainder < (boundaryIncrementer * 6)) {
                    // Arc 1
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (332 * Math.PI) / 180, (27 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 2
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (32 * Math.PI) / 180, (87 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 3
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (92 * Math.PI) / 180, (147 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 4
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (152 * Math.PI) / 180, (207 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 5
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (212 * Math.PI) / 180, (267 * Math.PI) / 180);
                    ctx.stroke();

                    // Arc 6
                    ctx.strokeStyle = '#d89747';
                    ctx.beginPath();
                    ctx.arc(22, 22, 17, (272 * Math.PI) / 180, (327 * Math.PI) / 180);
                    ctx.stroke();
                }

                window.requestAnimationFrame(drawDivisionLoader);

            }

            window.requestAnimationFrame(drawDivisionLoader);
            $(canvas).center(true, loader);
            loader.center(true, container);
            //
            return container;

        }

        container.Stop = function () {
            loader.remove();
        }

        return container;
    };

}(jQuery));