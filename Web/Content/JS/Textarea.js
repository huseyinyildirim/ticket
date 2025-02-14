﻿;
(function(e) {
    e.fn.autogrow = function(t) {

        function s(n) {
            var r = e(this), i = r.innerHeight(), s = this.scrollHeight, o = r.data("autogrow-start-height") || 0, u;
            if (i < s) {
                this.scrollTop = 0;
                t.animate ? r.stop().animate({ height: s }, t.speed) : r.innerHeight(s);
            } else if (n.which == 8 || n.which == 46 || n.ctrlKey && n.which == 88) {
                if (i > o) {
                    u = r.clone().addClass(t.cloneClass).css({ position: "absolute", zIndex: -10 }).val(r.val());
                    r.after(u);
                    do {
                        s = u[0].scrollHeight - 1;
                        u.innerHeight(s);
                    } while (s === u[0].scrollHeight);
                    s++;
                    u.remove();
                    s < o && (s = o);
                    i > s && t.animate ? r.stop().animate({ height: s }, t.speed) : r.innerHeight(s);
                } else {
                    r.innerHeight(o);
                }
            }
        }

        var n = e(this).css({ overflow: "hidden", resize: "none" }), r = n.selector, i = { context: e(document), animate: true, speed: 200, fixMinHeight: true, cloneClass: "autogrowclone" };
        t = e.isPlainObject(t) ? t : { context: t ? t : e(document) };
        t = e.extend({}, i, t);
        n.each(function(n, r) {
            var i, s;
            r = e(r);
            if (r.is(":visible") || parseInt(r.css("height"), 10) > 0) {
                i = parseInt(r.css("height"), 10) || r.innerHeight();
            } else {
                s = r.clone().addClass(t.cloneClass).val(r.val()).css({ position: "absolute", visibility: "hidden", display: "block" });
                e("body").append(s);
                i = s.innerHeight();
                s.remove();
            }
            if (t.fixMinHeight) {
                r.data("autogrow-start-height", i);
            }
            r.css("height", i);
        });
        t.context.on("keyup paste", r, s);
    };
})(jQuery);