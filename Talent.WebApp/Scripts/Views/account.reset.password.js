$(function () {

    $.fn.form.settings.rules.myPasswordRule = function (param) {

        if (param.length < 8)
            return false;
        if (param.search(/[0-9]/) == -1)
            return false;
        if (param.search(/[a-z]/) == -1)
            return false;
        if (param.search(/[A-Z]/) == -1)
            return false;
        if (param.search(/[!\@\#\$\%\^\&\(\)\_\+\.\,\;\:]/) == -1)
            return false;
        return true;
    }

    $.fn.form.settings.rules.matchPassword = function (param) {

        var pass = $('[name="NewPassword"]').val();
        if (param != pass)
            return false;
        return true;
    }

    $('.ui.form')
        .form({
            inline: true,
            //on: 'change',
            fields: {
                password: {
                    identifier: 'NewPassword',
                    rules: [
                        {
                            type: 'myPasswordRule[]',
                            prompt: 'Your password must be at least 8 characters. <br />With at least one lowercase letter <br />With at least one uppercase letter, <br />With at least one number <br />With at least one special character (Not "`,*,~")'
                        },
                    ]
                },
                confirmPassword: {
                    identifier: 'ConfirmPassword',
                    rules: [
                        {
                            type: 'empty',
                            prompt: 'Please enter a password'
                        },
                        {
                            type: 'matchPassword[]',
                            prompt: 'The password does not match.'
                        },
                    ]
                },

            }
        });
});