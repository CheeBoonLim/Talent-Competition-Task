
$(function () {

    $('.ui.form')
        .form({
            inline: true,
            on: 'change',
            fields: {
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
                    Email: {
                        identifier: 'Email',
                        rules: [
                            {
                                type: 'email',
                                prompt: 'Please enter a valid email'
                            }
                        ]
                    },
                    Subject: {
                        identifier: 'Subject',
                        rules: [
                            {
                                type: 'empty',
                                prompt: 'Please enter your subject'
                            },
                        ]
                    },
                    Message: {
                        identifier: 'Message',
                        rules: [
                            {
                                type: 'empty',
                                prompt: 'Please enter your message'
                            },
                        ]
                    },
                }
            }
        });

    $('#submit-btn').click(function () {
        $('#contactForm').form('validate form');
        $('#contactForm').form('is valid', function () {
            $('#contactForm').form('submit');
        });
    });
});