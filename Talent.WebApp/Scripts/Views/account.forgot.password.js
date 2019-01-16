
$(function () {

    $('.ui.form')
       .form({
           inline: true,
           //on: 'change',
           fields: {

               Username: {
                   identifier: 'Email',
                   rules: [
                     {
                         type: 'email',
                         prompt: 'Please enter a valid email'
                     }
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