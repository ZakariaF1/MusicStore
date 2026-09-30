"use strict";
(function ($) {
    $(document).ready(function () {
        var $form = $(".form-content"),
            $formSubmitButton = $(".form-submitButton"),
            $validator = $(".form-content").data("validator");

        if ($validator) {
            $validator.settings.onkeyup = false;
        }

        // Prevent errors while adding before and after text whitespaces
        $.each($.validator.methods, function (key, value) {
            $.validator.methods[key] = function () {
                if (arguments.length > 0) {
                    arguments[0] = $.trim(arguments[0]);
                }

                return value.apply(this, arguments);
            };
        });

        // Prevent errors while adding before and after text whitespaces
        $.each($.validator.methods, function (key, value) {
            $.validator.methods[key] = function () {
                if (arguments.length > 0) {
                    arguments[0] = $.trim(arguments[0]);
                }

                return value.apply(this, arguments);
            };
        });

        // Event listeners
        $form.on("submit", onFormSubmit);

        //Event listeners functions
        function onFormSubmit() {
            if ($form.valid()) {
                $(".form-content :input").each(function () {
                    var input = $(this);
                    var trimmedValue = $.trim(input.val());
                    input.val(trimmedValue);
                });

                $formSubmitButton.prop("disabled", true)
                return true
            }
        }
    });
})(jQuery)