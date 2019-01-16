$(function () {

    $('#updateProfileForm')
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
               phone: {
                   identifier: 'Phone',
                   rules: [
                     {
                         type: 'empty',
                         prompt: 'Please enter your mobile number'
                     }
                   ]
               },
               
           }
       });
});