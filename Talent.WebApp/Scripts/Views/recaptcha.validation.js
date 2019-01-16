
$(function () {

    $('#skin-recaptcha-error').hide();

    $("form").validate({
        submitHandler: function (form) {

            var response = grecaptcha.getResponse();

            //recaptcha failed validation
            if (response.length == 0) {
                $('#skin-recaptcha-error').html("reCaptcha is required");
                $('#skin-recaptcha-error').show();
                return false;
            }
            //recaptcha passed validation
            else {
                $('#skin-recaptcha-error').empty();
                $('#skin-recaptcha-error').hide();
                return true;
            }
        }
    });
});