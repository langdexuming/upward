! function (a) {
    "function" == typeof define && define.amd ? define(["jquery", "codemirror"], a) : a(window.jQuery, window.CodeMirror)
}(function (a, b) {
    "function" != typeof Array.prototype.reduce && (Array.prototype.reduce = function (a, b) {
        var c, d, e = this.length >>> 0,
            f = !1;
        for (1 < arguments.length && (d = b, f = !0), c = 0; e > c; ++c) this.hasOwnProperty(c) && (f ? d = a(d, this[c], c, this) : (d = this[c], f = !0));
        if (!f) throw new TypeError("Reduce of empty array with no initial value");
        return d
    });
    var c = {
            bMac: navigator.appVersion.indexOf("Mac") > -1,
            bMSIE: navigator.userAgent.indexOf("MSIE") > -1 || navigator.userAgent.indexOf("Trident") > -1,
            bFF: navigator.userAgent.indexOf("Firefox") > -1,
            jqueryVersion: parseFloat(a.fn.jquery),
            bCodeMirror: !!b
        },
        d = function () {
            var a = function (a) {
                    return function (b) {
                        return a === b
                    }
                },
                b = function (a, b) {
                    return a === b
                },
                c = function () {
                    return !0
                },
                d = function () {
                    return !1
                },
                e = function (a) {
                    return function () {
                        return !a.apply(a, arguments)
                    }
                },
                f = function (a) {
                    return a
                };
            return {
                eq: a,
                eq2: b,
                ok: c,
                fail: d,
                not: e,
                self: f
            }
        }(),
        e = function () {
            var a = function (a) {
                    return a[0]
                },
                b = function (a) {
                    return a[a.length - 1]
                },
                c = function (a) {
                    return a.slice(0, a.length - 1)
                },
                e = function (a) {
                    return a.slice(1)
                },
                f = function (a, b) {
                    var c = a.indexOf(b);
                    return -1 === c ? null : a[c + 1]
                },
                g = function (a, b) {
                    var c = a.indexOf(b);
                    return -1 === c ? null : a[c - 1]
                },
                h = function (a, b) {
                    return b = b || d.self, a.reduce(function (a, c) {
                        return a + b(c)
                    }, 0)
                },
                i = function (a) {
                    for (var b = [], c = -1, d = a.length; ++c < d;) b[c] = a[c];
                    return b
                },
                j = function (c, d) {
                    if (0 === c.length) return [];
                    var f = e(c);
                    return f.reduce(function (a, c) {
                        var e = b(a);
                        return d(b(e), c) ? e[e.length] = c : a[a.length] = [c], a
                    }, [
                        [a(c)]
                    ])
                },
                k = function (a) {
                    for (var b = [], c = 0, d = a.length; d > c; c++) a[c] && b.push(a[c]);
                    return b
                };
            return {
                head: a,
                last: b,
                initial: c,
                tail: e,
                prev: g,
                next: f,
                sum: h,
                from: i,
                compact: k,
                clusterBy: j
            }
        }(),
        f = function () {
            var b = function (b) {
                    return b && a(b).hasClass("note-editable")
                },
                g = function (b) {
                    return b && a(b).hasClass("note-control-sizing")
                },
                h = function (a) {
                    var b = function (b) {
                        return function () {
                            return a.find(b)
                        }
                    };
                    return {
                        editor: function () {
                            return a
                        },
                        dropzone: b(".note-dropzone"),
                        toolbar: b(".note-toolbar"),
                        editable: b(".note-editable"),
                        codable: b(".note-codable"),
                        statusbar: b(".note-statusbar"),
                        popover: b(".note-popover"),
                        handle: b(".note-handle"),
                        dialog: b(".note-dialog")
                    }
                },
                i = function (a) {
                    return function (b) {
                        return b && b.nodeName === a
                    }
                },
                j = function (a) {
                    return a && /^DIV|^P|^LI|^H[1-7]/.test(a.nodeName)
                },
                k = function (a) {
                    return a && /^UL|^OL/.test(a.nodeName)
                },
                l = function (a) {
                    return a && /^TD|^TH/.test(a.nodeName)
                },
                m = function (a, c) {
                    for (; a;) {
                        if (c(a)) return a;
                        if (b(a)) break;
                        a = a.parentNode
                    }
                    return null
                },
                n = function (a, b) {
                    b = b || d.fail;
                    var c = [];
                    return m(a, function (a) {
                        return c.push(a), b(a)
                    }), c
                },
                o = function (b, c) {
                    for (var d = n(b), e = c; e; e = e.parentNode)
                        if (a.inArray(e, d) > -1) return e;
                    return null
                },
                p = function (a, b) {
                    var c = [],
                        d = !1,
                        e = !1;
                    return function f(g) {
                        if (g) {
                            if (g === a && (d = !0), d && !e && c.push(g), g === b) return void(e = !0);
                            for (var h = 0, i = g.childNodes.length; i > h; h++) f(g.childNodes[h])
                        }
                    }(o(a, b)), c
                },
                q = function (a, b) {
                    b = b || d.fail;
                    for (var c = []; a && (c.push(a), !b(a));) a = a.previousSibling;
                    return c
                },
                r = function (a, b) {
                    b = b || d.fail;
                    for (var c = []; a && (c.push(a), !b(a));) a = a.nextSibling;
                    return c
                },
                s = function (a, b) {
                    var c = [];
                    return b = b || d.ok,
                        function e(d) {
                            a !== d && b(d) && c.push(d);
                            for (var f = 0, g = d.childNodes.length; g > f; f++) e(d.childNodes[f])
                        }(a), c
                },
                t = function (a, b) {
                    var c = b.nextSibling,
                        d = b.parentNode;
                    return c ? d.insertBefore(a, c) : d.appendChild(a), a
                },
                u = function (b, c) {
                    return a.each(c, function (a, c) {
                        b.appendChild(c)
                    }), b
                },
                v = i("#text"),
                w = function (a) {
                    return v(a) ? a.nodeValue.length : a.childNodes.length
                },
                x = function (a) {
                    for (var b = 0; a = a.previousSibling;) b += 1;
                    return b
                },
                y = function (b, c) {
                    var f = e.initial(n(c, d.eq(b)));
                    return a.map(f, x).reverse()
                },
                z = function (a, b) {
                    for (var c = a, d = 0, e = b.length; e > d; d++) c = c.childNodes[b[d]];
                    return c
                },
                A = function (a, b) {
                    if (0 === b) return a;
                    if (b >= w(a)) return a.nextSibling;
                    if (v(a)) return a.splitText(b);
                    var c = a.childNodes[b];
                    return a = t(a.cloneNode(!1), a), u(a, r(c))
                },
                B = function (a, b, c) {
                    var e = n(b, d.eq(a));
                    return 1 === e.length ? A(b, c) : e.reduce(function (a, d) {
                        var e = d.cloneNode(!1);
                        return t(e, d), a === b && (a = A(a, c)), u(e, r(a)), e
                    })
                },
                C = function (a, b) {
                    if (a && a.parentNode) {
                        if (a.removeNode) return a.removeNode(b);
                        var c = a.parentNode;
                        if (!b) {
                            var d, e, f = [];
                            for (d = 0, e = a.childNodes.length; e > d; d++) f.push(a.childNodes[d]);
                            for (d = 0, e = f.length; e > d; d++) c.insertBefore(f[d], a)
                        }
                        c.removeChild(a)
                    }
                },
                D = function (a) {
                    return f.isTextarea(a[0]) ? a.val() : a.html()
                };
            return {
                blank: c.bMSIE ? "&nbsp;" : "<br/>",
                emptyPara: "<p><br/></p>",
                isEditable: b,
                isControlSizing: g,
                buildLayoutInfo: h,
                isText: v,
                isPara: j,
                isList: k,
                isTable: i("TABLE"),
                isCell: l,
                isAnchor: i("A"),
                isDiv: i("DIV"),
                isLi: i("LI"),
                isSpan: i("SPAN"),
                isB: i("B"),
                isU: i("U"),
                isS: i("S"),
                isI: i("I"),
                isImg: i("IMG"),
                isTextarea: i("TEXTAREA"),
                ancestor: m,
                listAncestor: n,
                listNext: r,
                listPrev: q,
                listDescendant: s,
                commonAncestor: o,
                listBetween: p,
                insertAfter: t,
                position: x,
                makeOffsetPath: y,
                fromOffsetPath: z,
                split: B,
                remove: C,
                html: D
            }
        }(),
        g = {
            version: "0.5.2",
            options: {
                width: null,
                height: null,
                focus: !1,
                tabsize: 4,
                styleWithSpan: !0,
                disableLinkTarget: !1,
                disableDragAndDrop: !1,
                codemirror: null,
                lang: "en-US",
                direction: null,
                toolbar: [
                    ["style", ["style"]],
                    ["font", ["bold", "italic", "underline", "clear"]],
                    ["fontname", ["fontname"]],
                    ["color", ["color"]],
                    ["para", ["ul", "ol", "paragraph"]],
                    ["height", ["height"]],
                    ["table", ["table"]],
                    ["insert", ["link", "picture", "video"]],
                    ["view", ["fullscreen", "codeview"]],
                    ["help", ["help"]]
                ],
                oninit: null,
                onfocus: null,
                onblur: null,
                onenter: null,
                onkeyup: null,
                onkeydown: null,
                onImageUpload: null,
                onImageUploadError: null,
                onToolbarClick: null,
                keyMap: {
                    pc: {
                        "CTRL+Z": "undo",
                        "CTRL+Y": "redo",
                        TAB: "tab",
                        "SHIFT+TAB": "untab",
                        "CTRL+B": "bold",
                        "CTRL+I": "italic",
                        "CTRL+U": "underline",
                        "CTRL+SHIFT+S": "strikethrough",
                        "CTRL+BACKSLASH": "removeFormat",
                        "CTRL+SHIFT+L": "justifyLeft",
                        "CTRL+SHIFT+E": "justifyCenter",
                        "CTRL+SHIFT+R": "justifyRight",
                        "CTRL+SHIFT+J": "justifyFull",
                        "CTRL+SHIFT+NUM7": "insertUnorderedList",
                        "CTRL+SHIFT+NUM8": "insertOrderedList",
                        "CTRL+LEFTBRACKET": "outdent",
                        "CTRL+RIGHTBRACKET": "indent",
                        "CTRL+NUM0": "formatPara",
                        "CTRL+NUM1": "formatH1",
                        "CTRL+NUM2": "formatH2",
                        "CTRL+NUM3": "formatH3",
                        "CTRL+NUM4": "formatH4",
                        "CTRL+NUM5": "formatH5",
                        "CTRL+NUM6": "formatH6",
                        "CTRL+ENTER": "insertHorizontalRule"
                    },
                    mac: {
                        "CMD+Z": "undo",
                        "CMD+SHIFT+Z": "redo",
                        TAB: "tab",
                        "SHIFT+TAB": "untab",
                        "CMD+B": "bold",
                        "CMD+I": "italic",
                        "CMD+U": "underline",
                        "CMD+SHIFT+S": "strikethrough",
                        "CMD+BACKSLASH": "removeFormat",
                        "CMD+SHIFT+L": "justifyLeft",
                        "CMD+SHIFT+E": "justifyCenter",
                        "CMD+SHIFT+R": "justifyRight",
                        "CMD+SHIFT+J": "justifyFull",
                        "CMD+SHIFT+NUM7": "insertUnorderedList",
                        "CMD+SHIFT+NUM8": "insertOrderedList",
                        "CMD+LEFTBRACKET": "outdent",
                        "CMD+RIGHTBRACKET": "indent",
                        "CMD+NUM0": "formatPara",
                        "CMD+NUM1": "formatH1",
                        "CMD+NUM2": "formatH2",
                        "CMD+NUM3": "formatH3",
                        "CMD+NUM4": "formatH4",
                        "CMD+NUM5": "formatH5",
                        "CMD+NUM6": "formatH6",
                        "CMD+ENTER": "insertHorizontalRule"
                    }
                }
            },
            lang: {
                "en-US": {
                    font: {
                        bold: "Bold",
                        italic: "Italic",
                        underline: "Underline",
                        strike: "Strike",
                        clear: "Remove Font Style",
                        height: "Line Height",
                        name: "Font Family",
                        size: "Font Size"
                    },
                    image: {
                        image: "Picture",
                        insert: "Insert Image",
                        resizeFull: "Resize Full",
                        resizeHalf: "Resize Half",
                        resizeQuarter: "Resize Quarter",
                        floatLeft: "Float Left",
                        floatRight: "Float Right",
                        floatNone: "Float None",
                        dragImageHere: "Drag an image here",
                        selectFromFiles: "Select from files",
                        url: "Image URL",
                        remove: "Remove Image"
                    },
                    link: {
                        link: "Link",
                        insert: "Insert Link",
                        unlink: "Unlink",
                        edit: "Edit",
                        textToDisplay: "Text to display",
                        url: "To what URL should this link go?",
                        openInNewWindow: "Open in new window"
                    },
                    video: {
                        video: "Video",
                        videoLink: "Video Link",
                        insert: "Insert Video",
                        url: "Video URL?",
                        providers: "(YouTube, Vimeo, Vine, Instagram, or DailyMotion)"
                    },
                    table: {
                        table: "Table"
                    },
                    hr: {
                        insert: "Insert Horizontal Rule"
                    },
                    style: {
                        style: "Style",
                        normal: "Normal",
                        blockquote: "Quote",
                        pre: "Code",
                        h1: "Header 1",
                        h2: "Header 2",
                        h3: "Header 3",
                        h4: "Header 4",
                        h5: "Header 5",
                        h6: "Header 6"
                    },
                    lists: {
                        unordered: "Unordered list",
                        ordered: "Ordered list"
                    },
                    options: {
                        help: "Help",
                        fullscreen: "Full Screen",
                        codeview: "Code View"
                    },
                    paragraph: {
                        paragraph: "Paragraph",
                        outdent: "Outdent",
                        indent: "Indent",
                        left: "Align left",
                        center: "Align center",
                        right: "Align right",
                        justify: "Justify full"
                    },
                    color: {
                        recent: "Recent Color",
                        more: "More Color",
                        background: "BackColor",
                        foreground: "FontColor",
                        transparent: "Transparent",
                        setTransparent: "Set transparent",
                        reset: "Reset",
                        resetToDefault: "Reset to default"
                    },
                    shortcut: {
                        shortcuts: "Keyboard shortcuts",
                        close: "Close",
                        textFormatting: "Text formatting",
                        action: "Action",
                        paragraphFormatting: "Paragraph formatting",
                        documentStyle: "Document Style"
                    },
                    history: {
                        undo: "Undo",
                        redo: "Redo"
                    }
                }
            }
        },
        h = function () {
            var b = function (b) {
                    return a.Deferred(function (c) {
                        a.extend(new FileReader, {
                            onload: function (a) {
                                var b = a.target.result;
                                c.resolve(b)
                            },
                            onerror: function () {
                                c.reject(this)
                            }
                        }).readAsDataURL(b)
                    }).promise()
                },
                c = function (b) {
                    return a.Deferred(function (c) {
                        a("<img>").one("load", function () {
                            c.resolve(a(this))
                        }).one("error abort", function () {
                            c.reject(a(this))
                        }).css({
                            display: "none"
                        }).appendTo(document.body).attr("src", b)
                    }).promise()
                };
            return {
                readFileAsDataURL: b,
                createImage: c
            }
        }(),
        i = {
            isEdit: function (a) {
                return -1 !== [8, 9, 13, 32].indexOf(a)
            },
            nameFromCode: {
                8: "BACKSPACE",
                9: "TAB",
                13: "ENTER",
                32: "SPACE",
                48: "NUM0",
                49: "NUM1",
                50: "NUM2",
                51: "NUM3",
                52: "NUM4",
                53: "NUM5",
                54: "NUM6",
                55: "NUM7",
                56: "NUM8",
                66: "B",
                69: "E",
                73: "I",
                74: "J",
                75: "K",
                76: "L",
                82: "R",
                83: "S",
                85: "U",
                89: "Y",
                90: "Z",
                191: "SLASH",
                219: "LEFTBRACKET",
                220: "BACKSLASH",
                221: "RIGHTBRACKET"
            }
        },
        j = function () {
            var b = function (b, d) {
                if (c.jqueryVersion < 1.9) {
                    var e = {};
                    return a.each(d, function (a, c) {
                        e[c] = b.css(c)
                    }), e
                }
                return b.css.call(b, d)
            };
            this.stylePara = function (b, c) {
                a.each(b.nodes(f.isPara), function (b, d) {
                    a(d).css(c)
                })
            }, this.current = function (c, d) {
                var e = a(f.isText(c.sc) ? c.sc.parentNode : c.sc),
                    g = ["font-family", "font-size", "text-align", "list-style-type", "line-height"],
                    h = b(e, g) || {};
                if (h["font-size"] = parseInt(h["font-size"], 10), h["font-bold"] = document.queryCommandState("bold") ? "bold" : "normal", h["font-italic"] = document.queryCommandState("italic") ? "italic" : "normal", h["font-underline"] = document.queryCommandState("underline") ? "underline" : "normal", h["font-strikethrough"] = document.queryCommandState("strikeThrough") ? "strikethrough" : "normal", c.isOnList()) {
                    var i = ["circle", "disc", "disc-leading-zero", "square"],
                        j = a.inArray(h["list-style-type"], i) > -1;
                    h["list-style"] = j ? "unordered" : "ordered"
                } else h["list-style"] = "none";
                var k = f.ancestor(c.sc, f.isPara);
                if (k && k.style["line-height"]) h["line-height"] = k.style.lineHeight;
                else {
                    var l = parseInt(h["line-height"], 10) / parseInt(h["font-size"], 10);
                    h["line-height"] = l.toFixed(1)
                }
                return h.image = f.isImg(d) && d, h.anchor = c.isOnAnchor() && f.ancestor(c.sc, f.isAnchor), h.aAncestor = f.listAncestor(c.sc, f.isEditable), h
            }
        },
        k = function () {
            var b = !!document.createRange,
                c = function (a, b) {
                    var c, d, g = a.parentElement(),
                        h = document.body.createTextRange(),
                        i = e.from(g.childNodes);
                    for (c = 0; c < i.length; c++)
                        if (!f.isText(i[c])) {
                            if (h.moveToElementText(i[c]), h.compareEndPoints("StartToStart", a) >= 0) break;
                            d = i[c]
                        }
                    if (0 !== c && f.isText(i[c - 1])) {
                        var j = document.body.createTextRange(),
                            k = null;
                        j.moveToElementText(d || g), j.collapse(!d), k = d ? d.nextSibling : g.firstChild;
                        var l = a.duplicate();
                        l.setEndPoint("StartToStart", j);
                        for (var m = l.text.replace(/[\r\n]/g, "").length; m > k.nodeValue.length && k.nextSibling;) m -= k.nodeValue.length, k = k.nextSibling; {
                            k.nodeValue
                        }
                        b && k.nextSibling && f.isText(k.nextSibling) && m === k.nodeValue.length && (m -= k.nodeValue.length, k = k.nextSibling), g = k, c = m
                    }
                    return {
                        cont: g,
                        offset: c
                    }
                },
                g = function (a) {
                    var b = function (a, c) {
                            var g, h;
                            if (f.isText(a)) {
                                var i = f.listPrev(a, d.not(f.isText)),
                                    j = e.last(i).previousSibling;
                                g = j || a.parentNode, c += e.sum(e.tail(i), f.length), h = !j
                            } else {
                                if (g = a.childNodes[c] || a, f.isText(g)) return b(g, c);
                                c = 0, h = !1
                            }
                            return {
                                cont: g,
                                collapseToStart: h,
                                offset: c
                            }
                        },
                        c = document.body.createTextRange(),
                        g = b(a.cont, a.offset);
                    return c.moveToElementText(g.cont), c.collapse(g.collapseToStart), c.moveStart("character", g.offset), c
                },
                h = function (c, h, i, j) {
                    this.sc = c, this.so = h, this.ec = i, this.eo = j;
                    var k = function () {
                        if (b) {
                            var a = document.createRange();
                            return a.setStart(c, h), a.setEnd(i, j), a
                        }
                        var d = g({
                            cont: c,
                            offset: h
                        });
                        return d.setEndPoint("EndToEnd", g({
                            cont: i,
                            offset: j
                        })), d
                    };
                    this.select = function () {
                        var a = k();
                        if (b) {
                            var c = document.getSelection();
                            c.rangeCount > 0 && c.removeAllRanges(), c.addRange(a)
                        } else a.select()
                    }, this.nodes = function (b) {
                        var g = f.listBetween(c, i),
                            h = e.compact(a.map(g, function (a) {
                                return f.ancestor(a, b)
                            }));
                        return a.map(e.clusterBy(h, d.eq2), e.head)
                    }, this.commonAncestor = function () {
                        return f.commonAncestor(c, i)
                    };
                    var l = function (a) {
                        return function () {
                            var b = f.ancestor(c, a);
                            return !!b && b === f.ancestor(i, a)
                        }
                    };
                    this.isOnEditable = l(f.isEditable), this.isOnList = l(f.isList), this.isOnAnchor = l(f.isAnchor), this.isOnCell = l(f.isCell), this.isCollapsed = function () {
                        return c === i && h === j
                    }, this.insertNode = function (a) {
                        var c = k();
                        b ? c.insertNode(a) : c.pasteHTML(a.outerHTML)
                    }, this.toString = function () {
                        var a = k();
                        return b ? a.toString() : a.text
                    }, this.bookmark = function (a) {
                        return {
                            s: {
                                path: f.makeOffsetPath(a, c),
                                offset: h
                            },
                            e: {
                                path: f.makeOffsetPath(a, i),
                                offset: j
                            }
                        }
                    }
                };
            return {
                create: function (a, d, e, f) {
                    if (0 === arguments.length)
                        if (b) {
                            var g = document.getSelection();
                            if (0 === g.rangeCount) return null;
                            var i = g.getRangeAt(0);
                            a = i.startContainer, d = i.startOffset, e = i.endContainer, f = i.endOffset
                        } else {
                            var j = document.selection.createRange(),
                                k = j.duplicate();
                            k.collapse(!1);
                            var l = j;
                            l.collapse(!0);
                            var m = c(l, !0),
                                n = c(k, !1);
                            a = m.cont, d = m.offset, e = n.cont, f = n.offset
                        }
                    else 2 === arguments.length && (e = a, f = d);
                    return new h(a, d, e, f)
                },
                createFromNode: function (a) {
                    return this.create(a, 0, a, 1)
                },
                createFromBookmark: function (a, b) {
                    var c = f.fromOffsetPath(a, b.s.path),
                        d = b.s.offset,
                        e = f.fromOffsetPath(a, b.e.path),
                        g = b.e.offset;
                    return new h(c, d, e, g)
                }
            }
        }(),
        l = function () {
            this.tab = function (a, b) {
                var c = f.ancestor(a.commonAncestor(), f.isCell),
                    d = f.ancestor(c, f.isTable),
                    g = f.listDescendant(d, f.isCell),
                    h = e[b ? "prev" : "next"](g, c);
                h && k.create(h, 0).select()
            }, this.createTable = function (b, c) {
                for (var d, e = [], g = 0; b > g; g++) e.push("<td>" + f.blank + "</td>");
                d = e.join("");
                for (var h, i = [], j = 0; c > j; j++) i.push("<tr>" + d + "</tr>");
                h = i.join("");
                var k = '<table class="table table-bordered">' + h + "</table>";
                return a(k)[0]
            }
        },
        m = function () {
            var b = new j,
                d = new l;
            this.saveRange = function (a) {
                a.data("range", k.create())
            }, this.restoreRange = function (a) {
                var b = a.data("range");
                b && b.select()
            }, this.currentStyle = function (a) {
                var c = k.create();
                return c.isOnEditable() && b.current(c, a)
            }, this.undo = function (a) {
                a.data("NoteHistory").undo(a)
            }, this.redo = function (a) {
                a.data("NoteHistory").redo(a)
            };
            for (var e = this.recordUndo = function (a) {
                    a.data("NoteHistory").recordUndo(a)
                }, g = ["bold", "italic", "underline", "strikethrough", "justifyLeft", "justifyCenter", "justifyRight", "justifyFull", "insertOrderedList", "insertUnorderedList", "indent", "outdent", "formatBlock", "removeFormat", "backColor", "foreColor", "insertHorizontalRule", "fontName"], i = 0, m = g.length; m > i; i++) this[g[i]] = function (a) {
                return function (b, c) {
                    e(b), document.execCommand(a, !1, c)
                }
            }(g[i]);
            var n = function (b, c, d) {
                e(b);
                var g = new Array(d + 1).join("&nbsp;");
                c.insertNode(a('<span id="noteTab">' + g + "</span>")[0]);
                var h = a("#noteTab").removeAttr("id");
                c = k.create(h[0], 1), c.select(), f.remove(h[0])
            };
            this.tab = function (a, b) {
                var c = k.create();
                c.isCollapsed() && c.isOnCell() ? d.tab(c) : n(a, c, b.tabsize)
            }, this.untab = function () {
                var a = k.create();
                a.isCollapsed() && a.isOnCell() && d.tab(a, !0)
            }, this.insertImage = function (a, b) {
                h.createImage(b).then(function (b) {
                    e(a), b.css({
                        display: "",
                        width: Math.min(a.width(), b.width())
                    }), k.create().insertNode(b[0])
                }).fail(function () {
                    var b = a.data("callbacks");
                    b.onImageUploadError && b.onImageUploadError()
                })
            }, this.insertVideo = function (b, c) {
                e(b);
                var d, f = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/,
                    g = c.match(f),
                    h = /\/\/instagram.com\/p\/(.[a-zA-Z0-9]*)/,
                    i = c.match(h),
                    j = /\/\/vine.co\/v\/(.[a-zA-Z0-9]*)/,
                    l = c.match(j),
                    m = /\/\/(player.)?vimeo.com\/([a-z]*\/)*([0-9]{6,11})[?]?.*/,
                    n = c.match(m),
                    o = /.+dailymotion.com\/(video|hub)\/([^_]+)[^#]*(#video=([^_&]+))?/,
                    p = c.match(o);
                if (g && 11 === g[2].length) {
                    var q = g[2];
                    d = a("<iframe>").attr("src", "//www.youtube.com/embed/" + q).attr("width", "640").attr("height", "360")
                } else i && i[0].length > 0 ? d = a("<iframe>").attr("src", i[0] + "/embed/").attr("width", "612").attr("height", "710").attr("scrolling", "no").attr("allowtransparency", "true") : l && l[0].length > 0 ? d = a("<iframe>").attr("src", l[0] + "/embed/simple").attr("width", "600").attr("height", "600").attr("class", "vine-embed") : n && n[3].length > 0 ? d = a("<iframe webkitallowfullscreen mozallowfullscreen allowfullscreen>").attr("src", "//player.vimeo.com/video/" + n[3]).attr("width", "640").attr("height", "360") : p && p[2].length > 0 && (d = a("<iframe>").attr("src", "//www.dailymotion.com/embed/video/" + p[2]).attr("width", "640").attr("height", "360"));
                d && (d.attr("frameborder", 0), k.create().insertNode(d[0]))
            }, this.formatBlock = function (a, b) {
                e(a), b = c.bMSIE ? "<" + b + ">" : b, document.execCommand("FormatBlock", !1, b)
            }, this.formatPara = function (a) {
                this.formatBlock(a, "P")
            };
            for (var i = 1; 6 >= i; i++) this["formatH" + i] = function (a) {
                return function (b) {
                    this.formatBlock(b, "H" + a)
                }
            }(i);
            this.fontSize = function (a, b) {
                e(a), document.execCommand("fontSize", !1, 3), c.bFF ? a.find("font[size=3]").removeAttr("size").css("font-size", b + "px") : a.find("span").filter(function () {
                    return "medium" === this.style.fontSize
                }).css("font-size", b + "px")
            }, this.lineHeight = function (a, c) {
                e(a), b.stylePara(k.create(), {
                    lineHeight: c
                })
            }, this.unlink = function (a) {
                var b = k.create();
                if (b.isOnAnchor()) {
                    e(a);
                    var c = f.ancestor(b.sc, f.isAnchor);
                    b = k.createFromNode(c), b.select(), document.execCommand("unlink")
                }
            }, this.createLink = function (b, d, g) {
                var h = k.create();
                e(b);
                var i = d;
                if (-1 !== d.indexOf("@") && -1 === d.indexOf(":") ? i = "mailto:" + d : -1 === d.indexOf("://") && (i = "http://" + d), (c.bMSIE || c.bFF) && h.isCollapsed()) {
                    h.insertNode(a('<A id="linkAnchor">' + d + "</A>")[0]);
                    var j = a("#linkAnchor").attr("href", i).removeAttr("id");
                    h = k.createFromNode(j[0]), h.select()
                } else document.execCommand("createlink", !1, i), h = k.create();
                a.each(h.nodes(f.isAnchor), function (b, c) {
                    g ? a(c).attr("target", "_blank") : a(c).removeAttr("target")
                })
            }, this.getLinkInfo = function () {
                var b = k.create(),
                    c = !0,
                    d = "";
                if (b.isOnAnchor()) {
                    var e = f.ancestor(b.sc, f.isAnchor);
                    b = k.createFromNode(e), c = "_blank" === a(e).attr("target"), d = e.href
                }
                return {
                    text: b.toString(),
                    url: d,
                    newWindow: c
                }
            }, this.getVideoInfo = function () {
                var a = k.create();
                if (a.isOnAnchor()) {
                    var b = f.ancestor(a.sc, f.isAnchor);
                    a = k.createFromNode(b)
                }
                return {
                    text: a.toString()
                }
            }, this.color = function (a, b) {
                var c = JSON.parse(b),
                    d = c.foreColor,
                    f = c.backColor;
                e(a), d && document.execCommand("foreColor", !1, d), f && document.execCommand("backColor", !1, f)
            }, this.insertTable = function (a, b) {
                e(a);
                var c = b.split("x");
                k.create().insertNode(d.createTable(c[0], c[1]))
            }, this.floatMe = function (a, b, c) {
                e(a), c.css("float", b)
            }, this.resize = function (a, b, c) {
                e(a), c.css({
                    width: a.width() * b + "px",
                    height: ""
                })
            }, this.resizeTo = function (a, b, c) {
                var d;
                if (c) {
                    var e = a.y / a.x,
                        f = b.data("ratio");
                    d = {
                        width: f > e ? a.x : a.y / f,
                        height: f > e ? a.x * f : a.y
                    }
                } else d = {
                    width: a.x,
                    height: a.y
                };
                b.css(d)
            }, this.removeMedia = function (a, b, c) {
                e(a), c.detach()
            }
        },
        n = function () {
            var a = [],
                b = [],
                c = function (a) {
                    var b = a[0],
                        c = k.create();
                    return {
                        contents: a.html(),
                        bookmark: c.bookmark(b),
                        scrollTop: a.scrollTop()
                    }
                },
                d = function (a, b) {
                    a.html(b.contents).scrollTop(b.scrollTop), k.createFromBookmark(a[0], b.bookmark).select()
                };
            this.undo = function (e) {
                var f = c(e);
                0 !== a.length && (d(e, a.pop()), b.push(f))
            }, this.redo = function (e) {
                var f = c(e);
                0 !== b.length && (d(e, b.pop()), a.push(f))
            }, this.recordUndo = function (d) {
                b = [], a.push(c(d))
            }
        },
        o = function () {
            this.update = function (b, c) {
                var d = function (b, c) {
                        b.find(".dropdown-menu li a").each(function () {
                            var b = a(this).data("value") + "" == c + "";
                            this.className = b ? "checked" : ""
                        })
                    },
                    f = function (a, c) {
                        var d = b.find(a);
                        d.toggleClass("active", c())
                    },
                    g = b.find(".note-fontname");
                if (g.length > 0) {
                    var h = c["font-family"];
                    h && (h = e.head(h.split(",")), h = h.replace(/\'/g, ""), g.find(".note-current-fontname").text(h), d(g, h))
                }
                var i = b.find(".note-fontsize");
                i.find(".note-current-fontsize").text(c["font-size"]), d(i, parseFloat(c["font-size"]));
                var j = b.find(".note-height");
                d(j, parseFloat(c["line-height"])), f('button[data-event="bold"]', function () {
                    return "bold" === c["font-bold"]
                }), f('button[data-event="italic"]', function () {
                    return "italic" === c["font-italic"]
                }), f('button[data-event="underline"]', function () {
                    return "underline" === c["font-underline"]
                }), f('button[data-event="strikethrough"]', function () {
                    return "strikethrough" === c["font-strikethrough"]
                }), f('button[data-event="justifyLeft"]', function () {
                    return "left" === c["text-align"] || "start" === c["text-align"]
                }), f('button[data-event="justifyCenter"]', function () {
                    return "center" === c["text-align"]
                }), f('button[data-event="justifyRight"]', function () {
                    return "right" === c["text-align"]
                }), f('button[data-event="justifyFull"]', function () {
                    return "justify" === c["text-align"]
                }), f('button[data-event="insertUnorderedList"]', function () {
                    return "unordered" === c["list-style"]
                }), f('button[data-event="insertOrderedList"]', function () {
                    return "ordered" === c["list-style"]
                })
            }, this.updateRecentColor = function (b, c, d) {
                var e = a(b).closest(".note-color"),
                    f = e.find(".note-recent-color"),
                    g = JSON.parse(f.attr("data-value"));
                g[c] = d, f.attr("data-value", JSON.stringify(g));
                var h = "backColor" === c ? "background-color" : "color";
                f.find("i").css(h, d)
            }, this.updateFullscreen = function (a, b) {
                var c = a.find('button[data-event="fullscreen"]');
                c.toggleClass("active", b)
            }, this.updateCodeview = function (a, b) {
                var c = a.find('button[data-event="codeview"]');
                c.toggleClass("active", b)
            }, this.activate = function (a) {
                a.find("button").not('button[data-event="codeview"]').removeClass("disabled")
            }, this.deactivate = function (a) {
                a.find("button").not('button[data-event="codeview"]').addClass("disabled")
            }
        },
        p = function () {
            var b = function (b, c) {
                var d = a(c),
                    e = d.position(),
                    f = d.height();
                b.css({
                    display: "block",
                    left: e.left,
                    top: e.top + f
                })
            };
            this.update = function (a, c) {
                var d = a.find(".note-link-popover");
                if (c.anchor) {
                    var e = d.find("a");
                    e.attr("href", c.anchor.href).html(c.anchor.href), b(d, c.anchor)
                } else d.hide();
                var f = a.find(".note-image-popover");
                c.image ? b(f, c.image) : f.hide()
            }, this.hide = function (a) {
                a.children().hide()
            }
        },
        q = function () {
            this.update = function (b, c) {
                var d = b.find(".note-control-selection");
                if (c.image) {
                    var e = a(c.image),
                        f = e.position(),
                        g = {
                            w: e.width(),
                            h: e.height()
                        };
                    d.css({
                        display: "block",
                        left: f.left,
                        top: f.top,
                        width: g.w,
                        height: g.h
                    }).data("target", c.image);
                    var h = g.w + "x" + g.h;
                    d.find(".note-control-selection-info").text(h)
                } else d.hide()
            }, this.hide = function (a) {
                a.children().hide()
            }
        },
        r = function () {
            var b = function (a, b) {
                a.toggleClass("disabled", !b), a.attr("disabled", !b)
            };
            this.showImageDialog = function (c, d) {
                return a.Deferred(function (a) {
                    var e = d.find(".note-image-dialog"),
                        f = d.find(".note-image-input"),
                        g = d.find(".note-image-url"),
                        h = d.find(".note-image-btn");
                    e.one("shown.bs.modal", function () {
                        f.replaceWith(f.clone().on("change", function () {
                            e.modal("hide"), a.resolve(this.files)
                        })), h.click(function (b) {
                            b.preventDefault(), e.modal("hide"), a.resolve(g.val())
                        }), g.keyup(function () {
                            b(h, g.val())
                        }).val("").focus()
                    }).one("hidden.bs.modal", function () {
                        c.focus(), f.off("change"), g.off("keyup"), h.off("click")
                    }).modal("show")
                })
            }, this.showVideoDialog = function (c, d, e) {
                return a.Deferred(function (a) {
                    var f = d.find(".note-video-dialog"),
                        g = f.find(".note-video-url"),
                        h = f.find(".note-video-btn");
                    f.one("shown.bs.modal", function () {
                        g.val(e.text).keyup(function () {
                            b(h, g.val())
                        }).trigger("keyup").trigger("focus"), h.click(function (b) {
                            b.preventDefault(), f.modal("hide"), a.resolve(g.val())
                        })
                    }).one("hidden.bs.modal", function () {
                        c.focus(), g.off("keyup"), h.off("click")
                    }).modal("show")
                })
            }, this.showLinkDialog = function (c, d, e) {
                return a.Deferred(function (a) {
                    var f = d.find(".note-link-dialog"),
                        g = f.find(".note-link-text"),
                        h = f.find(".note-link-url"),
                        i = f.find(".note-link-btn"),
                        j = f.find("input[type=checkbox]");
                    f.one("shown.bs.modal", function () {
                        g.val(e.text), h.keyup(function () {
                            b(i, h.val()), e.text || g.val(h.val())
                        }).val(e.url).trigger("focus"), j.prop("checked", e.newWindow), i.one("click", function (b) {
                            b.preventDefault(), f.modal("hide"), a.resolve(h.val(), j.is(":checked"))
                        })
                    }).one("hidden.bs.modal", function () {
                        c.focus(), h.off("keyup")
                    }).modal("show")
                }).promise()
            }, this.showHelpDialog = function (a, b) {
                var c = b.find(".note-help-dialog");
                c.one("hidden.bs.modal", function () {
                    a.focus()
                }).modal("show")
            }
        },
        s = function () {
            var d = new m,
                g = new o,
                j = new p,
                k = new q,
                l = new r,
                s = function (b) {
                    var c = a(b).closest(".note-editor");
                    return c.length > 0 && f.buildLayoutInfo(c)
                },
                t = function (b, c) {
                    d.restoreRange(b);
                    var e = b.data("callbacks");
                    e.onImageUpload ? e.onImageUpload(c, d, b) : a.each(c, function (a, c) {
                        h.readFileAsDataURL(c).then(function (a) {
                            d.insertImage(b, a)
                        }).fail(function () {
                            e.onImageUploadError && e.onImageUploadError()
                        })
                    })
                },
                u = function (a) {
                    f.isImg(a.target) && a.preventDefault()
                },
                v = function (a) {
                    var b = s(a.currentTarget || a.target),
                        c = d.currentStyle(a.target);
                    c && (g.update(b.toolbar(), c), j.update(b.popover(), c), k.update(b.handle(), c))
                },
                w = function (a) {
                    var b = s(a.currentTarget || a.target);
                    j.hide(b.popover()), k.hide(b.handle())
                },
                x = function (a) {
                    var b = a.originalEvent;
                    if (b.clipboardData && b.clipboardData.items && b.clipboardData.items.length) {
                        var c = s(a.currentTarget || a.target),
                            d = e.head(b.clipboardData.items),
                            f = "file" === d.kind && -1 !== d.type.indexOf("image/");
                        f && t(c.editable(), [d.getAsFile()])
                    }
                },
                y = function (b) {
                    if (f.isControlSizing(b.target)) {
                        var c = s(b.target),
                            e = c.handle(),
                            g = c.popover(),
                            h = c.editable(),
                            i = c.editor(),
                            l = e.find(".note-control-selection").data("target"),
                            m = a(l),
                            n = m.offset(),
                            o = a(document).scrollTop();
                        i.on("mousemove", function (a) {
                            d.resizeTo({
                                x: a.clientX - n.left,
                                y: a.clientY - (n.top - o)
                            }, m, !a.shiftKey), k.update(e, {
                                image: l
                            }), j.update(g, {
                                image: l
                            })
                        }).one("mouseup", function () {
                            i.off("mousemove")
                        }), m.data("ratio") || m.data("ratio", m.height() / m.width()), d.recordUndo(h), b.stopPropagation(), b.preventDefault()
                    }
                },
                z = function (b) {
                    var c = a(b.target).closest("[data-event]");
                    c.length > 0 && b.preventDefault()
                },
                A = function (e) {
                    var h = a(e.target).closest("[data-event]");
                    if (h.length > 0) {
                        var i, j, k, m = h.attr("data-event"),
                            n = h.attr("data-value"),
                            o = s(e.target),
                            p = o.editor(),
                            q = o.toolbar(),
                            r = o.dialog(),
                            u = o.editable(),
                            w = o.codable(),
                            x = p.data("options");
                        if (-1 !== a.inArray(m, ["resize", "floatMe", "removeMedia"])) {
                            var y = o.handle(),
                                z = y.find(".note-control-selection");
                            k = a(z.data("target"))
                        }
                        if (d[m] && (u.trigger("focus"), d[m](u, n, k)), -1 !== a.inArray(m, ["backColor", "foreColor"])) g.updateRecentColor(h[0], m, n);
                        else if ("showLinkDialog" === m) {
                            u.focus();
                            var A = d.getLinkInfo();
                            d.saveRange(u), l.showLinkDialog(u, r, A).then(function (a, b) {
                                d.restoreRange(u), d.createLink(u, a, b)
                            })
                        } else if ("showImageDialog" === m) u.focus(), l.showImageDialog(u, r).then(function (a) {
                            "string" == typeof a ? (d.restoreRange(u), d.insertImage(u, a)) : t(u, a)
                        });
                        else if ("showVideoDialog" === m) {
                            u.focus();
                            var B = d.getVideoInfo();
                            d.saveRange(u), l.showVideoDialog(u, r, B).then(function (a) {
                                d.restoreRange(u), d.insertVideo(u, a)
                            })
                        } else if ("showHelpDialog" === m) l.showHelpDialog(u, r);
                        else if ("fullscreen" === m) {
                            var C = a("html, body"),
                                D = function (a) {
                                    p.css("width", a.w), u.css("height", a.h), w.css("height", a.h), w.data("cmEditor") && w.data("cmEditor").setSize(null, a.h)
                                };
                            p.toggleClass("fullscreen");
                            var E = p.hasClass("fullscreen");
                            E ? (u.data("orgHeight", u.css("height")), a(window).on("resize", function () {
                                D({
                                    w: a(window).width(),
                                    h: a(window).height() - q.outerHeight()
                                })
                            }).trigger("resize"), C.css("overflow", "hidden")) : (a(window).off("resize"), D({
                                w: x.width || "",
                                h: u.data("orgHeight")
                            }), C.css("overflow", "auto")), g.updateFullscreen(q, E)
                        } else if ("codeview" === m) {
                            p.toggleClass("codeview");
                            var F = p.hasClass("codeview");
                            if (F) {
                                if (w.val(u.html()), w.height(u.height()), g.deactivate(q), w.focus(), c.bCodeMirror) {
                                    j = b.fromTextArea(w[0], a.extend({
                                        mode: "text/html",
                                        lineNumbers: !0
                                    }, x.codemirror));
                                    var G = p.data("options").codemirror.tern || !1;
                                    G && (i = new b.TernServer(G), j.ternServer = i, j.on("cursorActivity", function (a) {
                                        i.updateArgHints(a)
                                    })), j.setSize(null, u.outerHeight()), j.autoFormatRange && j.autoFormatRange({
                                        line: 0,
                                        ch: 0
                                    }, {
                                        line: j.lineCount(),
                                        ch: j.getTextArea().value.length
                                    }), w.data("cmEditor", j)
                                }
                            } else c.bCodeMirror && (j = w.data("cmEditor"), w.val(j.getValue()), j.toTextArea()), u.html(w.val() || f.emptyPara), u.height(x.height ? w.height() : "auto"), g.activate(q), u.focus();
                            g.updateCodeview(o.toolbar(), F)
                        }
                        v(e)
                    }
                },
                B = 24,
                C = function (b) {
                    var c = a(document),
                        d = s(b.target).editable(),
                        e = d.offset().top - c.scrollTop();
                    c.on("mousemove", function (a) {
                        var b = a.clientY - (e + B);
                        d.height(b)
                    }).one("mouseup", function () {
                        c.off("mousemove")
                    }), b.stopPropagation(), b.preventDefault()
                },
                D = 18,
                E = function (b) {
                    var c, d = a(b.target.parentNode),
                        e = d.next(),
                        f = d.find(".note-dimension-picker-mousecatcher"),
                        g = d.find(".note-dimension-picker-highlighted"),
                        h = d.find(".note-dimension-picker-unhighlighted");
                    if (void 0 === b.offsetX) {
                        var i = a(b.target).offset();
                        c = {
                            x: b.pageX - i.left,
                            y: b.pageY - i.top
                        }
                    } else c = {
                        x: b.offsetX,
                        y: b.offsetY
                    };
                    var j = {
                        c: Math.ceil(c.x / D) || 1,
                        r: Math.ceil(c.y / D) || 1
                    };
                    g.css({
                        width: j.c + "em",
                        height: j.r + "em"
                    }), f.attr("data-value", j.c + "x" + j.r), 3 < j.c && j.c < 10 && h.css({
                        width: j.c + 1 + "em"
                    }), 3 < j.r && j.r < 10 && h.css({
                        height: j.r + 1 + "em"
                    }), e.html(j.c + " x " + j.r)
                },
                F = function (b) {
                    var c = a(),
                        d = b.dropzone,
                        e = b.dropzone.find(".note-dropzone-message");
                    a(document).on("dragenter", function (a) {
                        var f = b.editor.hasClass("codeview");
                        f || 0 !== c.length || (b.editor.addClass("dragover"), d.width(b.editor.width()), d.height(b.editor.height()), e.text("Drag Image Here")), c = c.add(a.target)
                    }).on("dragleave", function (a) {
                        c = c.not(a.target), 0 === c.length && b.editor.removeClass("dragover")
                    }).on("drop", function () {
                        c = a(), b.editor.removeClass("dragover")
                    }), d.on("dragenter", function () {
                        d.addClass("hover"), e.text("Drop Image")
                    }).on("dragleave", function () {
                        d.removeClass("hover"), e.text("Drag Image Here")
                    }), d.on("drop", function (a) {
                        var b = a.originalEvent.dataTransfer;
                        if (b && b.files) {
                            var c = s(a.currentTarget || a.target);
                            c.editable().focus(), t(c.editable(), b.files)
                        }
                        a.preventDefault()
                    }).on("dragover", !1)
                };
            this.bindKeyMap = function (a, b) {
                var c = a.editor,
                    e = a.editable;
                e.on("keydown", function (a) {
                    var f = [];
                    a.metaKey && f.push("CMD"), a.ctrlKey && f.push("CTRL"), a.shiftKey && f.push("SHIFT");
                    var g = i.nameFromCode[a.keyCode];
                    g && f.push(g);
                    var h = b[f.join("+")];
                    h ? (a.preventDefault(), d[h](e, c.data("options"))) : i.isEdit(a.keyCode) && d.recordUndo(e)
                })
            }, this.attach = function (a, b) {
                this.bindKeyMap(a, b.keyMap[c.bMac ? "mac" : "pc"]), a.editable.on("mousedown", u), a.editable.on("keyup mouseup", v), a.editable.on("scroll", w), a.editable.on("paste", x), b.disableDragAndDrop || F(a), a.handle.on("mousedown", y), a.toolbar.on("click", A), a.popover.on("click", A), a.toolbar.on("mousedown", z), a.popover.on("mousedown", z), a.statusbar.on("mousedown", C);
                var e = a.toolbar,
                    f = e.find(".note-dimension-picker-mousecatcher");
                if (f.on("mousemove", E), a.editable.on("blur", function () {
                        d.saveRange(a.editable)
                    }), a.editor.data("options", b), b.styleWithSpan && !c.bMSIE && setTimeout(function () {
                        document.execCommand("styleWithCSS", 0, !0)
                    }), a.editable.data("NoteHistory", new n), b.onenter && a.editable.keypress(function (a) {
                        a.keyCode === i.ENTER && b.onenter(a)
                    }), b.onfocus && a.editable.focus(b.onfocus), b.onblur && a.editable.blur(b.onblur), b.onkeyup && a.editable.keyup(b.onkeyup), b.onkeydown && a.editable.keydown(b.onkeydown), b.onpaste && a.editable.on("paste", b.onpaste), b.onToolbarClick && a.toolbar.click(b.onToolbarClick), b.onChange) {
                    var g = function () {
                        b.onChange(a.editable, a.editable.html())
                    };
                    c.bMSIE ? a.editable.on("DOMCharacterDataModified, DOMSubtreeModified, DOMNodeInserted", g) : a.editable.on("input", g)
                }
                a.editable.data("callbacks", {
                    onAutoSave: b.onAutoSave,
                    onImageUpload: b.onImageUpload,
                    onImageUploadError: b.onImageUploadError,
                    onFileUpload: b.onFileUpload,
                    onFileUploadError: b.onFileUpload
                })
            }, this.dettach = function (a) {
                a.editable.off(), a.toolbar.off(), a.handle.off(), a.popover.off()
            }
        },
        t = function () {
            var b, d, e, g, h;
            b = {
                picture: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.image.image + '" data-event="showImageDialog" tabindex="-1"><i class="fa fa-picture-o icon-picture"></i></button>'
                },
                link: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.link.link + '" data-event="showLinkDialog" tabindex="-1"><i class="fa fa-link icon-link"></i></button>'
                },
                video: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.video.video + '" data-event="showVideoDialog" tabindex="-1"><i class="fa fa-youtube-play icon-play"></i></button>'
                },
                table: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" title="' + a.table.table + '" data-toggle="dropdown" tabindex="-1"><i class="fa fa-table icon-table"></i> <span class="caret"></span></button><ul class="dropdown-menu"><div class="note-dimension-picker"><div class="note-dimension-picker-mousecatcher" data-event="insertTable" data-value="1x1"></div><div class="note-dimension-picker-highlighted"></div><div class="note-dimension-picker-unhighlighted"></div></div><div class="note-dimension-display"> 1 x 1 </div></ul>'
                },
                style: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" title="' + a.style.style + '" data-toggle="dropdown" tabindex="-1"><i class="fa fa-magic icon-magic"></i> <span class="caret"></span></button><ul class="dropdown-menu"><li><a data-event="formatBlock" data-value="p">' + a.style.normal + '</a></li><li><a data-event="formatBlock" data-value="blockquote"><blockquote>' + a.style.blockquote + '</blockquote></a></li><li><a data-event="formatBlock" data-value="pre">' + a.style.pre + '</a></li><li><a data-event="formatBlock" data-value="h1"><h1>' + a.style.h1 + '</h1></a></li><li><a data-event="formatBlock" data-value="h2"><h2>' + a.style.h2 + '</h2></a></li><li><a data-event="formatBlock" data-value="h3"><h3>' + a.style.h3 + '</h3></a></li><li><a data-event="formatBlock" data-value="h4"><h4>' + a.style.h4 + '</h4></a></li><li><a data-event="formatBlock" data-value="h5"><h5>' + a.style.h5 + '</h5></a></li><li><a data-event="formatBlock" data-value="h6"><h6>' + a.style.h6 + "</h6></a></li></ul>"
                },
                fontname: function (a) {
                    var b = ["Serif", "Sans", "Arial", "Arial Black", "Courier", "Courier New", "Comic Sans MS", "Helvetica", "Impact", "Lucida Grande", "Lucida Sans", "Tahoma", "Times", "Times New Roman", "Verdana"],
                        c = '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" data-toggle="dropdown" title="' + a.font.name + '" tabindex="-1"><span class="note-current-fontname">Arial</span> <b class="caret"></b></button>';
                    c += '<ul class="dropdown-menu">';
                    for (var d = 0; d < b.length; d++) c += '<li><a data-event="fontName" data-value="' + b[d] + '"><i class="fa fa-check icon-ok"></i> ' + b[d] + "</a></li>";
                    return c += "</ul>"
                },
                fontsize: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" data-toggle="dropdown" title="' + a.font.size + '" tabindex="-1"><span class="note-current-fontsize">11</span> <b class="caret"></b></button><ul class="dropdown-menu"><li><a data-event="fontSize" data-value="8"><i class="fa fa-check icon-ok"></i> 8</a></li><li><a data-event="fontSize" data-value="9"><i class="fa fa-check icon-ok"></i> 9</a></li><li><a data-event="fontSize" data-value="10"><i class="fa fa-check icon-ok"></i> 10</a></li><li><a data-event="fontSize" data-value="11"><i class="fa fa-check icon-ok"></i> 11</a></li><li><a data-event="fontSize" data-value="12"><i class="fa fa-check icon-ok"></i> 12</a></li><li><a data-event="fontSize" data-value="14"><i class="fa fa-check icon-ok"></i> 14</a></li><li><a data-event="fontSize" data-value="18"><i class="fa fa-check icon-ok"></i> 18</a></li><li><a data-event="fontSize" data-value="24"><i class="fa fa-check icon-ok"></i> 24</a></li><li><a data-event="fontSize" data-value="36"><i class="fa fa-check icon-ok"></i> 36</a></li></ul>'
                },
                color: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small note-recent-color" title="' + a.color.recent + '" data-event="color" data-value=\'{"backColor":"yellow"}\' tabindex="-1"><i class="fa fa-font icon-font" style="color:black;background-color:yellow;"></i></button><button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" title="' + a.color.more + '" data-toggle="dropdown" tabindex="-1"><span class="caret"></span></button><ul class="dropdown-menu"><li><div class="btn-group"><div class="note-palette-title">' + a.color.background + '</div><div class="note-color-reset" data-event="backColor" data-value="inherit" title="' + a.color.transparent + '">' + a.color.setTransparent + '</div><div class="note-color-palette" data-target-event="backColor"></div></div><div class="btn-group"><div class="note-palette-title">' + a.color.foreground + '</div><div class="note-color-reset" data-event="foreColor" data-value="inherit" title="' + a.color.reset + '">' + a.color.resetToDefault + '</div><div class="note-color-palette" data-target-event="foreColor"></div></div></li></ul>'
                },
                bold: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.font.bold + '" data-shortcut="Ctrl+B" data-mac-shortcut="⌘+B" data-event="bold" tabindex="-1"><i class="fa fa-bold icon-bold"></i></button>'
                },
                italic: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.font.italic + '" data-shortcut="Ctrl+I" data-mac-shortcut="⌘+I" data-event="italic" tabindex="-1"><i class="fa fa-italic icon-italic"></i></button>'
                },
                underline: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.font.underline + '" data-shortcut="Ctrl+U" data-mac-shortcut="⌘+U" data-event="underline" tabindex="-1"><i class="fa fa-underline icon-underline"></i></button>'
                },
                strike: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.font.strike + '" data-event="strikethrough" tabindex="-1"><i class="fa fa-strikethrough icon-strikethrough"></i></button>'
                },
                clear: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.font.clear + '" data-shortcut="Ctrl+\\" data-mac-shortcut="⌘+\\" data-event="removeFormat" tabindex="-1"><i class="fa fa-eraser icon-eraser"></i></button>'
                },
                ul: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.lists.unordered + '" data-shortcut="Ctrl+Shift+8" data-mac-shortcut="⌘+⇧+7" data-event="insertUnorderedList" tabindex="-1"><i class="fa fa-list-ul icon-list-ul"></i></button>'
                },
                ol: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.lists.ordered + '" data-shortcut="Ctrl+Shift+7" data-mac-shortcut="⌘+⇧+8" data-event="insertOrderedList" tabindex="-1"><i class="fa fa-list-ol icon-list-ol"></i></button>'
                },
                paragraph: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" title="' + a.paragraph.paragraph + '" data-toggle="dropdown" tabindex="-1"><i class="fa fa-align-left icon-align-left"></i>  <span class="caret"></span></button><div class="dropdown-menu"><div class="note-align btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.paragraph.left + '" data-shortcut="Ctrl+Shift+L" data-mac-shortcut="⌘+⇧+L" data-event="justifyLeft" tabindex="-1"><i class="fa fa-align-left icon-align-left"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.paragraph.center + '" data-shortcut="Ctrl+Shift+E" data-mac-shortcut="⌘+⇧+E" data-event="justifyCenter" tabindex="-1"><i class="fa fa-align-center icon-align-center"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.paragraph.right + '" data-shortcut="Ctrl+Shift+R" data-mac-shortcut="⌘+⇧+R" data-event="justifyRight" tabindex="-1"><i class="fa fa-align-right icon-align-right"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.paragraph.justify + '" data-shortcut="Ctrl+Shift+J" data-mac-shortcut="⌘+⇧+J" data-event="justifyFull" tabindex="-1"><i class="fa fa-align-justify icon-align-justify"></i></button></div><div class="note-list btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.paragraph.outdent + '" data-shortcut="Ctrl+[" data-mac-shortcut="⌘+[" data-event="outdent" tabindex="-1"><i class="fa fa-outdent icon-indent-left"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.paragraph.indent + '" data-shortcut="Ctrl+]" data-mac-shortcut="⌘+]" data-event="indent" tabindex="-1"><i class="fa fa-indent icon-indent-right"></i></button></div></div>'
                },
                height: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" data-toggle="dropdown" title="' + a.font.height + '" tabindex="-1"><i class="fa fa-text-height icon-text-height"></i>&nbsp; <b class="caret"></b></button><ul class="dropdown-menu"><li><a data-event="lineHeight" data-value="1.0"><i class="fa fa-check icon-ok"></i> 1.0</a></li><li><a data-event="lineHeight" data-value="1.2"><i class="fa fa-check icon-ok"></i> 1.2</a></li><li><a data-event="lineHeight" data-value="1.4"><i class="fa fa-check icon-ok"></i> 1.4</a></li><li><a data-event="lineHeight" data-value="1.5"><i class="fa fa-check icon-ok"></i> 1.5</a></li><li><a data-event="lineHeight" data-value="1.6"><i class="fa fa-check icon-ok"></i> 1.6</a></li><li><a data-event="lineHeight" data-value="1.8"><i class="fa fa-check icon-ok"></i> 1.8</a></li><li><a data-event="lineHeight" data-value="2.0"><i class="fa fa-check icon-ok"></i> 2.0</a></li><li><a data-event="lineHeight" data-value="3.0"><i class="fa fa-check icon-ok"></i> 3.0</a></li></ul>'
                },
                help: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.options.help + '" data-event="showHelpDialog" tabindex="-1"><i class="fa fa-question icon-question"></i></button>'
                },
                fullscreen: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.options.fullscreen + '" data-event="fullscreen" tabindex="-1"><i class="fa fa-arrows-alt icon-fullscreen"></i></button>'
                },
                codeview: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.options.codeview + '" data-event="codeview" tabindex="-1"><i class="fa fa-code icon-code"></i></button>'
                },
                undo: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.history.undo + '" data-event="undo" tabindex="-1"><i class="fa fa-undo icon-undo"></i></button>'
                },
                redo: function (a) {
                    return '<button type="button" class="btn btn-default btn-sm btn-small" title="' + a.history.redo + '" data-event="redo" tabindex="-1"><i class="fa fa-repeat icon-repeat"></i></button>'
                }
            }, d = function (a) {
                return '<div class="note-popover"><div class="note-link-popover popover bottom in" style="display: none;"><div class="arrow"></div><div class="popover-content note-link-content"><a href="http://www.google.com" target="_blank">www.google.com</a>&nbsp;&nbsp;<div class="note-insert btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.link.edit + '" data-event="showLinkDialog" tabindex="-1"><i class="fa fa-edit icon-edit"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.link.unlink + '" data-event="unlink" tabindex="-1"><i class="fa fa-unlink icon-unlink"></i></button></div></div></div><div class="note-image-popover popover bottom in" style="display: none;"><div class="arrow"></div><div class="popover-content note-image-content"><div class="btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.image.resizeFull + '" data-event="resize" data-value="1" tabindex="-1"><span class="note-fontsize-10">100%</span> </button><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.image.resizeHalf + '" data-event="resize" data-value="0.5" tabindex="-1"><span class="note-fontsize-10">50%</span> </button><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.image.resizeQuarter + '" data-event="resize" data-value="0.25" tabindex="-1"><span class="note-fontsize-10">25%</span> </button></div><div class="btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.image.floatLeft + '" data-event="floatMe" data-value="left" tabindex="-1"><i class="fa fa-align-left icon-align-left"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.image.floatRight + '" data-event="floatMe" data-value="right" tabindex="-1"><i class="fa fa-align-right icon-align-right"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.image.floatNone + '" data-event="floatMe" data-value="none" tabindex="-1"><i class="fa fa-align-justify icon-align-justify"></i></button></div><div class="btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="' + a.image.remove + '" data-event="removeMedia" data-value="none" tabindex="-1"><i class="fa fa-trash-o icon-trash"></i></button></div></div></div></div>'
            };
            var e = function () {
                    return '<div class="note-handle"><div class="note-control-selection"><div class="note-control-selection-bg"></div><div class="note-control-holder note-control-nw"></div><div class="note-control-holder note-control-ne"></div><div class="note-control-holder note-control-sw"></div><div class="note-control-sizing note-control-se"></div><div class="note-control-selection-info"></div></div></div>'
                },
                i = function (a) {
                    return '<table class="note-shortcut"><thead><tr><th></th><th>' + a.shortcut.textFormatting + "</th></tr></thead><tbody><tr><td>⌘ + B</td><td>" + a.font.bold + "</td></tr><tr><td>⌘ + I</td><td>" + a.font.italic + "</td></tr><tr><td>⌘ + U</td><td>" + a.font.underline + "</td></tr><tr><td>⌘ + ⇧ + S</td><td>" + a.font.strike + "</td></tr><tr><td>⌘ + \\</td><td>" + a.font.clear + "</td></tr></tr></tbody></table>"
                },
                j = function (a) {
                    return '<table class="note-shortcut"><thead><tr><th></th><th>' + a.shortcut.action + "</th></tr></thead><tbody><tr><td>⌘ + Z</td><td>" + a.history.undo + "</td></tr><tr><td>⌘ + ⇧ + Z</td><td>" + a.history.redo + "</td></tr><tr><td>⌘ + ]</td><td>" + a.paragraph.indent + "</td></tr><tr><td>⌘ + [</td><td>" + a.paragraph.outdent + "</td></tr><tr><td>⌘ + ENTER</td><td>" + a.hr.insert + "</td></tr></tbody></table>"
                },
                k = function (a, b) {
                    var c = '<table class="note-shortcut"><thead><tr><th></th><th>' + a.shortcut.extraKeys + "</th></tr></thead><tbody>";
                    for (var d in b.extraKeys) b.extraKeys.hasOwnProperty(d) && (c += "<tr><td>" + d + "</td><td>" + b.extraKeys[d] + "</td></tr>");
                    return c += "</tbody></table>"
                },
                l = function (a) {
                    return '<table class="note-shortcut"><thead><tr><th></th><th>' + a.shortcut.paragraphFormatting + "</th></tr></thead><tbody><tr><td>⌘ + ⇧ + L</td><td>" + a.paragraph.left + "</td></tr><tr><td>⌘ + ⇧ + E</td><td>" + a.paragraph.center + "</td></tr><tr><td>⌘ + ⇧ + R</td><td>" + a.paragraph.right + "</td></tr><tr><td>⌘ + ⇧ + J</td><td>" + a.paragraph.justify + "</td></tr><tr><td>⌘ + ⇧ + NUM7</td><td>" + a.lists.ordered + "</td></tr><tr><td>⌘ + ⇧ + NUM8</td><td>" + a.lists.unordered + "</td></tr></tbody></table>"
                },
                m = function (a) {
                    return '<table class="note-shortcut"><thead><tr><th></th><th>' + a.shortcut.documentStyle + "</th></tr></thead><tbody><tr><td>⌘ + NUM0</td><td>" + a.style.normal + "</td></tr><tr><td>⌘ + NUM1</td><td>" + a.style.h1 + "</td></tr><tr><td>⌘ + NUM2</td><td>" + a.style.h2 + "</td></tr><tr><td>⌘ + NUM3</td><td>" + a.style.h3 + "</td></tr><tr><td>⌘ + NUM4</td><td>" + a.style.h4 + "</td></tr><tr><td>⌘ + NUM5</td><td>" + a.style.h5 + "</td></tr><tr><td>⌘ + NUM6</td><td>" + a.style.h6 + "</td></tr></tbody></table>"
                },
                n = function (a, b) {
                    var c = '<table class="note-shortcut-layout"><tbody><tr><td>' + j(a, b) + "</td><td>" + i(a, b) + "</td></tr><tr><td>" + m(a, b) + "</td><td>" + l(a, b) + "</td></tr>";
                    return b.extraKeys && (c += '<tr><td colspan="2">' + k(a, b) + "</td></tr>"), c += "</tbody</table>"
                },
                o = function (a) {
                    return a.replace(/⌘/g, "Ctrl").replace(/⇧/g, "Shift")
                };
            g = function (a, b) {
                var d = function () {
                        return '<div class="note-image-dialog modal" aria-hidden="false"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close" aria-hidden="true" tabindex="-1">&times;</button><h4>' + a.image.insert + '</h4></div><div class="modal-body"><div class="row-fluid"><h5>' + a.image.selectFromFiles + '</h5><input class="note-image-input" type="file" name="files" accept="image/*" /><h5>' + a.image.url + '</h5><input class="note-image-url form-control span12" type="text" /></div></div><div class="modal-footer"><button href="#" class="btn btn-primary note-image-btn disabled" disabled="disabled">' + a.image.insert + "</button></div></div></div></div>"
                    },
                    e = function () {
                        return '<div class="note-link-dialog modal" aria-hidden="false"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close" aria-hidden="true" tabindex="-1">&times;</button><h4>' + a.link.insert + '</h4></div><div class="modal-body"><div class="row-fluid"><div class="form-group"><label>' + a.link.textToDisplay + '</label><input class="note-link-text form-control span12" disabled type="text" /></div><div class="form-group"><label>' + a.link.url + '</label><input class="note-link-url form-control span12" type="text" /></div>' + (b.disableLinkTarget ? "" : '<div class="checkbox"><label><input type="checkbox" checked> ' + a.link.openInNewWindow + "</label></div>") + '</div></div><div class="modal-footer"><button href="#" class="btn btn-primary note-link-btn disabled" disabled="disabled">' + a.link.insert + "</button></div></div></div></div>"
                    },
                    f = function () {
                        return '<div class="note-video-dialog modal" aria-hidden="false"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close" aria-hidden="true" tabindex="-1">&times;</button><h4>' + a.video.insert + '</h4></div><div class="modal-body"><div class="row-fluid"><div class="form-group"><label>' + a.video.url + '</label>&nbsp;<small class="text-muted">' + a.video.providers + '</small><input class="note-video-url form-control span12" type="text" /></div></div></div><div class="modal-footer"><button href="#" class="btn btn-primary note-video-btn disabled" disabled="disabled">' + a.video.insert + "</button></div></div></div></div>"
                    },
                    g = function () {
                        return '<div class="note-help-dialog modal" aria-hidden="false"><div class="modal-dialog"><div class="modal-content"><div class="modal-body"><a class="modal-close pull-right" aria-hidden="true" tabindex="-1">' + a.shortcut.close + '</a><div class="title">' + a.shortcut.shortcuts + "</div>" + (c.bMac ? n(a, b) : o(n(a, b))) + '<p class="text-center"><a href="//hackerwins.github.io/summernote/" target="_blank">Summernote 0.5.2</a> · <a href="//github.com/HackerWins/summernote" target="_blank">Project</a> · <a href="//github.com/HackerWins/summernote/issues" target="_blank">Issues</a></p></div></div></div></div>'
                    };
                return '<div class="note-dialog">' + d() + e() + f() + g() + "</div>"
            }, h = function () {
                return '<div class="note-resizebar"><div class="note-icon-bar"></div><div class="note-icon-bar"></div><div class="note-icon-bar"></div></div>'
            };
            var p = function (b, d) {
                    b.find("button").each(function (b, d) {
                        var e = a(d),
                            f = e.attr(c.bMac ? "data-mac-shortcut" : "data-shortcut");
                        f && e.attr("title", function (a, b) {
                            return b + " (" + f + ")"
                        })
                    }).tooltip({
                        container: "body",
                        trigger: "hover",
                        placement: d || "top"
                    }).on("click", function () {
                        a(this).tooltip("hide")
                    })
                },
                q = [
                    ["#000000", "#424242", "#636363", "#9C9C94", "#CEC6CE", "#EFEFEF", "#F7F7F7", "#FFFFFF"],
                    ["#FF0000", "#FF9C00", "#FFFF00", "#00FF00", "#00FFFF", "#0000FF", "#9C00FF", "#FF00FF"],
                    ["#F7C6CE", "#FFE7CE", "#FFEFC6", "#D6EFD6", "#CEDEE7", "#CEE7F7", "#D6D6E7", "#E7D6DE"],
                    ["#E79C9C", "#FFC69C", "#FFE79C", "#B5D6A5", "#A5C6CE", "#9CC6EF", "#B5A5D6", "#D6A5BD"],
                    ["#E76363", "#F7AD6B", "#FFD663", "#94BD7B", "#73A5AD", "#6BADDE", "#8C7BC6", "#C67BA5"],
                    ["#CE0000", "#E79439", "#EFC631", "#6BA54A", "#4A7B8C", "#3984C6", "#634AA5", "#A54A7B"],
                    ["#9C0000", "#B56308", "#BD9400", "#397B21", "#104A5A", "#085294", "#311873", "#731842"],
                    ["#630000", "#7B3900", "#846300", "#295218", "#083139", "#003163", "#21104A", "#4A1031"]
                ],
                r = function (b) {
                    b.find(".note-color-palette").each(function () {
                        for (var b = a(this), c = b.attr("data-target-event"), d = [], e = 0, f = q.length; f > e; e++) {
                            for (var g = q[e], h = [], i = 0, j = g.length; j > i; i++) {
                                var k = g[i];
                                h.push(['<button type="button" class="note-color-btn" style="background-color:', k, ';" data-event="', c, '" data-value="', k, '" title="', k, '" data-toggle="button" tabindex="-1"></button>'].join(""))
                            }
                            d.push("<div>" + h.join("") + "</div>")
                        }
                        b.html(d.join(""))
                    })
                };
            this.createLayout = function (c, i) {
                var j = c.next();
                if (!j || !j.hasClass("note-editor")) {
                    var k = a('<div class="note-editor"></div>');
                    i.width && k.width(i.width), i.height > 0 && a('<div class="note-statusbar">' + h() + "</div>").prependTo(k);
                    var l = !c.is(":disabled"),
                        m = a('<div class="note-editable" contentEditable="' + l + '"></div>').prependTo(k);
                    i.height && m.height(i.height), i.direction && m.attr("dir", i.direction), m.html(f.html(c) || f.emptyPara), a('<textarea class="note-codable"></textarea>').prependTo(k);
                    for (var n = a.summernote.lang[i.lang], o = "", q = 0, s = i.toolbar.length; s > q; q++) {
                        var t = i.toolbar[q];
                        o += '<div class="note-' + t[0] + ' btn-group">';
                        for (var u = 0, v = t[1].length; v > u; u++) o += b[t[1][u]](n);
                        o += "</div>"
                    }
                    o = '<div class="note-toolbar btn-toolbar">' + o + "</div>";
                    var w = a(o).prependTo(k);
                    r(w), p(w, "bottom");
                    var x = a(d(n)).prependTo(k);
                    p(x), a(e()).prependTo(k);
                    var y = a(g(n, i)).prependTo(k);
                    y.find("button.close, a.modal-close").click(function () {
                        a(this).closest(".modal").modal("hide")
                    }), a('<div class="note-dropzone"><div class="note-dropzone-message"></div></div>').prependTo(k), k.insertAfter(c), c.hide()
                }
            }, this.layoutInfoFromHolder = function (a) {
                var b = a.next();
                if (b.hasClass("note-editor")) {
                    var c = f.buildLayoutInfo(b);
                    for (var d in c) c.hasOwnProperty(d) && (c[d] = c[d].call());
                    return c
                }
            }, this.removeLayout = function (a) {
                var b = this.layoutInfoFromHolder(a);
                b && (a.html(b.editable.html()), b.editor.remove(), a.show())
            }
        };
    a.summernote = a.summernote || {}, a.extend(a.summernote, g);
    var u = new t,
        v = new s;
    a.fn.extend({
        summernote: function (b) {
            if (b = a.extend({}, a.summernote.options, b), this.each(function (c, d) {
                    var e = a(d);
                    u.createLayout(e, b);
                    var g = u.layoutInfoFromHolder(e);
                    v.attach(g, b), f.isTextarea(e[0]) && e.closest("form").submit(function () {
                        e.html(e.code())
                    })
                }), this.first() && b.focus) {
                var c = u.layoutInfoFromHolder(this.first());
                c.editable.focus()
            }
            return this.length > 0 && b.oninit && b.oninit(), this
        },
        code: function (b) {
            if (void 0 === b) {
                var d = this.first();
                if (0 === d.length) return;
                var e = u.layoutInfoFromHolder(d);
                if (e && e.editable) {
                    var f = e.editor.hasClass("codeview");
                    return f && c.bCodeMirror && e.codable.data("cmEditor").save(), f ? e.codable.val() : e.editable.html()
                }
                return d.html()
            }
            return this.each(function (c, d) {
                var e = u.layoutInfoFromHolder(a(d));
                e && e.editable && e.editable.html(b)
            }), this
        },
        destroy: function () {
            return this.each(function (b, c) {
                var d = a(c),
                    e = u.layoutInfoFromHolder(d);
                e && e.editable && (v.dettach(e), u.removeLayout(d))
            }), this
        }
    })
});