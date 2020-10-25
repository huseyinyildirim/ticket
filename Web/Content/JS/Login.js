function LoginBegin(e) {
    $('#login button').attr('disabled', 'disabled');
}

function LoginSuccess(e) {
    e = e.ro;
    switch (e.Code) {
    case 0:
        Redirect(e.Url);
        break;
    case 1:
        MessageBox("Hata", e.Message);
        break;
    default:
        break;
    }
    $('#login button').removeAttr('disabled');
}

function reSize() {
    var login = $('#login');
    var windowHeight = $(window).height();
    var loginHeight = login.height();
    login.animate({ marginTop: Math.round((windowHeight - loginHeight) / 2) + 'px' });
}

$(document).ready(function() {

    reSize();

});

$(window).resize(reSize);