// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document)
    .ready(function () {
        $('.checkbox').checkbox();
        $('#account')
            .form({
                fields: {
                    email: {
                        identifier: 'Input_Email',
                        rules: [
                            {
                                type: 'empty',
                                prompt: 'Please enter your e-mail'
                            },
                            {
                                type: 'email',
                                prompt: 'Please enter a valid e-mail'
                            }
                        ]
                    },
                    password: {
                        identifier: 'Input_Password',
                        rules: [
                            {
                                type: 'empty',
                                prompt: 'Please enter your password'
                            },
                            {
                                type: 'length[6]',
                                prompt: 'Your password must be at least 6 characters'
                            }
                        ]
                    }
                }
            })
        ;
        $('#register')
            .form({
                fields: {
                    email: {
                        identifier: 'Input_Email',
                        rules: [
                            {
                                type: 'empty',
                                prompt: 'Please enter your e-mail'
                            },
                            {
                                type: 'email',
                                prompt: 'Please enter a valid e-mail'
                            }
                        ]
                    },
                    password1: {
                        identifier: 'Input_Password',
                        rules: [
                            {
                                type: 'empty',
                                prompt: 'Please enter your password'
                            },
                            {
                                type: 'length[6]',
                                prompt: 'Your password must be at least 6 characters'
                            }
                        ]
                    },
                    password2: {
                        identifier: 'Input_ConfirmPassword',
                        rules: [
                            {
                                type: 'empty',
                                prompt: 'Please enter your password'
                            },
                            {
                                type: 'length[6]',
                                prompt: 'Your password must be at least 6 characters'
                            }
                        ]
                    }
                }
            })
        ;
        $('#logoutItem').on('click', function () {
            $('#logoutForm').submit();
        })
    })
;
