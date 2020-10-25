$.validator.setDefaults({ ignore: '.ignore' });
$.validator.methods["date"] = function (value, element) { return true; };
$.validator.methods.range = function(value, element, param) {
    var globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
};

$.validator.methods.number = function(value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
};

$.fx.off = true;

var DateDiff = {
    inDays: function(d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000));
    },

    inWeeks: function(d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000 * 7));
    },

    inMonths: function(d1, d2) {
        var d1Y = d1.getFullYear();
        var d2Y = d2.getFullYear();
        var d1M = d1.getMonth();
        var d2M = d2.getMonth();

        return (d2M + 12 * d2Y) - (d1M + 12 * d1Y);
    },

    inYears: function(d1, d2) {
        return d2.getFullYear() - d1.getFullYear();
    }
};


function sessionState() { setInterval(function () { $.ajax({ cache: false, url: '/SessionState', method: 'GET' }); }, 3000); }

$(document).ready(function () {

    sessionState();

    $.datepicker.setDefaults($.datepicker.regional['tr']);
    $.validator.messages = {
        required: "*",
        remote: "*",
        email: "*",
        url: "*",
        date: "*",
        dateISO: "*",
        number: "*",
        digits: "*",
        creditcard: "*",
        equalTo: "*"
    };

    $('.scroll').perfectScrollbar();
    $('textarea').autogrow();
    $('.tc').masked('99999999999');
    $('.cc').masked('9999-9999-9999-9999');
    $('.cvc').masked('999');
    $('.cc_date').masked('99/9999');
    $('.phone').masked('0(999)-999-99-99');
    $('.time').masked('99:99');
    $('.postCode').mask('99999');
    $('.date').mask('99.99.9999');
    $('.dateTime').mask('99.99.9999 99:99:99');
    $('.date').datepicker();
    $('.dateTime').datepicker();
    $('input.numeric').numeric({ decimal: "," });
    $('select').chosen({ allow_single_deselect: true, no_results_text: 'Bulunamad&#305;:' });
    $('#startDateTime, .startDateTime').datepicker({ numberOfMonths: 3 });
    $('#finishDateTime, .finishDateTime').datepicker({ numberOfMonths: 3 });
    $('[title]').tipsy();

    $('input').on('blur focusOut', function () { $(this).val($(this).val().trim()); });

    var noValidate = $('.notvalidate').validate();
    if (noValidate != undefined) { noValidate.settings.rules = null; }

    $(document).on('dblclick', '.permListBox .permItem[data], .permListBox .permTitle[data]', function () {
        var _this = $(this);
        var data = $.parseJSON(_this.attr('data'));
        var permissionId = data.permId;
        var USERID = data.userId;
        var sendData = { USERID: USERID, permissionId: permissionId };


        $.ajax({ url: '/Users/PermissionChange/', data: sendData, cache: false, method: 'POST', dataType: "json" }).done(function (e) {
            var returnPerms = e.returnPerms;
            
            for (var i = 0; i<returnPerms.length; i++) {
                var permId = returnPerms[i];
                $('.permListBox .permItem[data], .permListBox .permTitle[data]').each(function () {
                    var _thisPerm = $(this);
                    var thisPermId = ($.parseJSON(_thisPerm.attr('data'))).permId;
                    if (thisPermId == permId) {
                        _thisPerm.toggleClass('disable', '');
                    }
                });
            }



        });
    });

    // +++ CASCADE COMBOBOXLAR İÇİN İl > İlçe  Okul > Sınıf +++
    $('.CASCADE_CITY_ID').on('change', function () {
        var val = $(this).val();
        $('.CASCADE_COUNTY_ID').load('/CP/GetCountys?id=' + val, function () {
            $('.CASCADE_COUNTY_ID').trigger("chosen:updated");
        });
    });

    $('.CASCADE_SCHOOL_ID').on('change', function () {
        var val = $(this).val();
        $('.CASCADE_SCHOOL_CLASS_ID').load('/CP/GetSchoolClass?id=' + val, function () {
            $('.CASCADE_SCHOOL_CLASS_ID').trigger("chosen:updated");
        });
    });
    // --- NORMAL ---
    

    $('#selectCoordinates').on('click', function() {
        var dataId = $(this).attr('data-id');
        window.open('/Persons/Map/' + dataId, 'Yer seçimi', 'location=no,fullscreen=yes,width=2000,height=2000');
    });

    $(document).on('click', '.selectedList-item .x', function () {
        var id = $(this).parent().attr('id');
        $('#' + id + '-value').remove();
        $('#' + id).remove();
    });
    

});

function cFloat(val) {
    if (val != "") { val = val.toString().replace(',', '.'); }
    var x = parseFloat(0.00).toFixed(2);
    try {
        x = parseFloat(val).toFixed(2);
    } catch(e) {
        x = x;
    }
    if (isNaN(x)) { x = parseFloat(0.00).toFixed(2); }
    return x;
}

function Form_Success(e) {
    e = e.ro;
    if (e == undefined) {RedirectLogin();}
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
        
    if (e.Object != null) { eval(e.Object); }
        
}
    
function Form_Begin() {
    
}

function SelectBrand(id) {
    $.ajax({
        url: '/Promotions/BrandModels',
        data: 'id=' + id,
        method: 'POST',
        cache: false,
        success: function (e) { $('#grdModels tbody').html(e); }
    });
}

function SelectModel(id, name) {
    var body = '<div id="item-' + id + '" class="selectedList-item">' + name + ' <a href="javascript:;" class="x iconlink-gray"><i class="fa fa-times fs11px"></i></a></div><input id="item-' + id + '-value" type="hidden" name="selectedModels" value="' + id + '" />';
    var control = $('#item-' + id).length;
    if (control == 0) {
        $('#lstSelectedModels').append(body);
    }
}

function GetCoordinates(la, lo) {
    $('#STUDENT_LAT').val(la.toString().replace('.', ','));
    $('#STUDENT_LONG').val(lo.toString().replace('.', ','));
    $('#selectCoordinates').attr('title', 'Yerini Değiştir');
}

function PopUp(url, title, width, height) {
    window.open(url, title, 'location=no,fullscreen=yes,width=' + width + ',height=' + height);
}