
$(function () {

    $('.ui.form')
       .form({
           inline: true,
           //on: 'change',
           fields: {

               Username: {
                   identifier: 'Username',
                   rules: [
                     {
                         type: 'email',
                         prompt: 'Please enter a valid email'
                     }
                   ]
               },
               Password: {
                   identifier: 'Password',
                   rules: [
                     {
                         type: 'empty',
                         prompt: 'Please enter a password'
                     },
                     //{
                     //    type: 'minLength[8]',
                     //    prompt: 'Your password must be at least {ruleValue} characters'
                     //}
                   ]
               },
           }
       });

    $('#submit-btn').click(function () {
        $('form').form('validate form');
        $('form').form('is valid', function () {
            $('form').form('submit');
        });

    });
});