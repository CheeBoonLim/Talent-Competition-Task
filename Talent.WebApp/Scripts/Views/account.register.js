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

        var pass = $('[name="password"]').val();
        if (param != pass)
            return false;
        return true;
    }

    $('.ui.form')
       .form({
           inline: true,
           on: 'change',
           fields: {
               FirstName: {
                   identifier: 'FirstName',
                   rules: [
                     {
                         type: 'empty',
                         prompt: 'Please enter your first name'
                     }
                   ]
               },
               LastName: {
                   identifier: 'LastName',
                   rules: [
                     {
                         type: 'empty',
                         prompt: 'Please enter your last name'
                     }
                   ]
               },
               MobilePhone: {
                   identifier: 'MobilePhone',
                   rules: [
                     {
                         type: 'empty',
                         prompt: 'Please enter your mobile number'
                     }
                   ]
               },
               Username: {
                   identifier: 'Username',
                   rules: [
                     {
                         type: 'email',
                         prompt: 'Please enter a valid email'
                     }
                   ]
               },
               password: {
                   identifier: 'Password',
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
               terms: {
                   identifier: 'terms',
                   rules: [
                     {
                         type: 'checked',
                         prompt: 'You must agree to the terms and conditions'
                     }
                   ]
               }
           }
       });

    // Why do you need to call it again, it is called on the page already!
    //$('#submit-btn').click(function () {
    //    $('form').form('validate form');
    //    $('form').form('is valid', function () {
    //        $('form').form('submit');
    //    });
    //});
});