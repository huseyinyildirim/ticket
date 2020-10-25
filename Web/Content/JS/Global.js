function Redirect(url) { window.location.href = url; }
function RedirectLogin() { window.location.href = (window.location.href).replace(window.location.origin, ''); }

String.prototype.turkishToUpper = function () { var string = this; var letters = { "i": "İ", "ş": "Ş", "ğ": "Ğ", "ü": "Ü", "ö": "Ö", "ç": "Ç", "ı": "I" }; string = string.replace(/(([iışğüçö]))/g, function (letter) { return letters[letter]; }); return string.toUpperCase(); };
String.prototype.turkishToLower = function () { var string = this; var letters = { "İ": "i", "I": "ı", "Ş": "ş", "Ğ": "ğ", "Ü": "ü", "Ö": "ö", "Ç": "ç" }; string = string.replace(/(([İIŞĞÜÇÖ]))/g, function (letter) { return letters[letter]; }); return string.toLowerCase(); };

function MessageBox(title, message) {
    var body = $("<div class=\"message-box-overlay notSelect\"><div id=\"message-box\" class=\"message-box\">" +
        "<span class=\"x fa fa-times\">&nbsp;</span>" +
        "<p class=\"title\">" + title + "</p>" +
        "<p class=\"message\">" + message + "</p>" +
        "</div></div>").fadeIn();

    $('body').prepend(body);
}

$(document).ready(function () {
    
    $('input.aloneText, textarea.aloneText').keyup(function () { if (this.value.match(/[^a-zA-ZöğşçıüÖĞŞÇİÜ]/g)) { this.value = this.value.replace(/[^a-zA-ZöğşçıüÖĞŞÇİÜ]/g, ''); } });
    $('input').attr('autocomplete', 'off');

    $(document).on('click', '.message-box .x', function () { $(this).parents('.message-box-overlay').fadeOut(function () { $(this).remove(); }); })
               .on('click', '.delete', function () { return confirm('Silmek istediğinize emin misiniz?'); })
               .on('click', '.change', function () { return confirm('Değiştirmek istediğinize emin misiniz?'); })
               .on('click', '[click-func]', function () { var func = $(this).attr('click-func'); eval(func); })
               .on('dblclick', '[dblclick-func]', function () { var func = $(this).attr('dblclick-func'); eval(func); })
               .on('click', '[click]', function () { var url = $(this).attr('click'); Redirect(url); })
               .on('dblclick', '[dblclick]', function () { var url = $(this).attr('dblclick'); Redirect(url); })
               .on('click', '[form-id]', function () {
                   var formId = $(this).attr('form-id');
                   var form = $('#' + formId);
                   $('.addEditFormArea').removeClass('open');
                   $(form).toggleClass('open', '');

               })
               .on('click', '.addEditFormArea .close-form', function () { $(this).parent().toggleClass('open', ''); })
    ;


    // +++ TAB PLUG-IN +++

    $(document).find('.tab:not([disabled=disabled]) > .panes > .pane').eq(0).addClass('active');
    $(document).find('.tab:not([disabled=disabled]) > ul > li').eq(0).addClass('active');
    $(document).on('click', '.tab > ul > li', function () {
        var _this = $(this);
        var _thisDisabled = _this.parents('.tab').attr('disabled');
        if (_thisDisabled == undefined) {
            var _thisIndex = _this.index();
            _this.parent().find('li').removeClass('active');
            _this.addClass('active');
            _this.parents('.tab').find('.panes .pane').hide().eq(_thisIndex).show();
        }
    });

    // --- TAB PLUG-IN ---

});

$(window).keydown(function (e) {

    if (e.keyCode == 27) {
        e.preventDefault();
        $('.addEditFormArea').removeClass('open');
    }

});