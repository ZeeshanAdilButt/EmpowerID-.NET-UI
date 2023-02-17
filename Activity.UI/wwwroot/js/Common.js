


function FilterPaginationData(url) {
    $.ajax({
        url: url,
        success: function (data) {
            $(".renderPartial").html('');
            $(".renderPartial").html(data);
        },
        cache: false,
        contentType: false,
        processData: false
    });
}

$(document).ready(function () {

    debugger;

    $(".addr").focusout(function () {
        debugger;

        console.log("address out");
    }

   

    $('input.phone').attr('maxlength', "10").attr('placeholder', "Phone number without dashes");
    $('input.phone').attr('minlength', "10");
    $('input.zipcode').attr('maxlength', "5").attr('placeholder', "5 digits");
    $('input.zipcode').attr('minlength', "5");
    $('input.npi').attr('maxlength', "10").attr('placeholder', "10 digits");
    $('input.practicecode').attr('maxlength', "4");
    $('input.practicecode').attr('minlength', "4");

    $(document).on('keydown paste', 'input.phone', allowOnlyDigits);
    $(document).on('keydown paste', 'input.zipcode', allowOnlyDigits);
    $(document).on('keydown paste', 'input.npi', allowOnlyDigits);


    $(".select2").select2({
        theme: 'bootstrap4'
    });
});
//Ajax error handler

$(document).ajaxError(function (event, XMLHttpRequest, ajaxOptions) {
    try {
        var response = XMLHttpRequest.responseJSON;
        if (response.success == false) {
            ErrorMessage("Error", response.message);
            $(".renderPartial").removeClass('dim'); // remove dim class on tables in case of return type partial views
        }
        if (response.errorCode == 401) {
            //$("#logout")[0].click();
            window.location.href = '/Auth/Logout';
        }
    }
    catch (ex) { console.log("Ajax complete method failed") }
});