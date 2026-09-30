"use strict";
(function ($) {
    $(document).ready(function () {
        $("#artistId-input").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select an artist",
                value: null
            }
        });

        var $artistsDropDown = $("#artistId-input").data("kendoDropDownList"),
            $artistsTextBox = $("#artistId-input"),
            $form = $(".form-content"),
            $artistIdSpan = $(".artistId-validation-error"),
            $formSubmitButton = $(".form-submitButton"),
            $validator = $(".form-content").data("validator"),
            artists = [];

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

        $.ajax({
            url: "/Artist/ListArtists",
            type: "GET",
            beforeSend: function () {
                kendo.ui.progress($("#artistId-input").parent(), true);
            },
            success: function (response) {
                $.each(response, function (i, artist) {
                    artists.push({ text: artist.Name, value: artist.ArtistId.toString() })
                });
                $artistsDropDown.setDataSource(artists)
                $artistsDropDown.value($artistsTextBox.attr("value"));
                kendo.ui.progress($("#artistId-input").parent(), false);
            }
        });

        // Event listeners
        $('#artistId-input').on("blur", artistValidate);
        $form.on("submit", onFormSubmit);

        //Event listeners functions
        function artistValidate() {
            var $artistInput = $('#artistId-input'),
                $artistDropdown = $("#artistId-input").data("kendoDropDownList");

            if ($artistDropdown.value() === "") {
                $artistIdSpan.text("The artist Id field is required");
                $artistIdSpan.css({
                    "display": "block"
                })
                $artistInput.prev().css({
                    "border-color": "red"
                })
                return false
            } else {
                $artistIdSpan.css({
                    "display": "none"
                })
                $artistInput.prev().css({
                    "border-color": ""
                })
            }
            return true
        }

        function onFormSubmit() {
            var form = {
                isFormValid: undefined,
                isArtistValid: undefined
            }
            if ($form.valid()) {
                form.isFormValid = true;
            } else {
                form.isFormValid = false;
            }
            if (artistValidate()) {
                form.isArtistValid = true;
            } else {
                form.isArtistValid = false;
            }

            if (validate(form)) {
                $(".form-content :input").each(function () {
                    var input = $(this);
                    var trimmedValue = $.trim(input.val());
                    input.val(trimmedValue);
                });

                $formSubmitButton.prop("disabled", true);
                return true
            } else {
                return false
            }

            function validate(obj) {
                for (var property in obj)
                    if (!obj[property]) {
                        return false
                    }
                return true;
            }
        }
    });
})(jQuery)