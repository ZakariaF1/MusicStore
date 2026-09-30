"use strict";
(function ($) {
    $(document).ready(function () {
        $("#albumId-input").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select an album",
                value: null
            }
        });
        $("#mediaTypeId-input").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select a media type",
                value: null
            }
        });
        $("#genreId-input").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select a genre",
                value: null
            }
        });

        var $albumsDropdown = $('#albumId-input').data("kendoDropDownList"),
            $albumsTextBox = $('#albumId-input'),
            $mediaTypesDropdown = $('#mediaTypeId-input').data("kendoDropDownList"),
            $mediaTypesTextBox = $('#mediaTypeId-input'),
            $genresDropdown = $('#genreId-input').data("kendoDropDownList"),
            $genresTextBox = $('#genreId-input'),
            $form = $(".form-content"),
            $formSubmitButton = $(".form-submitButton"),
            $validator = $(".form-content").data("validator"),
            $mediaTypeSpan = $(".mediaTypeId-validation-error"),
            albums = [],
            mediaTypes = [],
            genres = [];

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
            url: "/Album/ListAlbums",
            type: "GET",
            beforeSend: function () {
                kendo.ui.progress($("#albumId-input").parent(), true);
            },
            success: function (response) {
                $.each(response, function (i, album) {
                    albums.push({ text: album.Title, value: album.AlbumId.toString() })
                });
                $albumsDropdown.setDataSource(albums)
                $albumsDropdown.value($albumsTextBox.attr("value"));
                kendo.ui.progress($("#albumId-input").parent(), false);
            }
        });

        $.ajax({
            url: "/MediaType/ListMediaTypes",
            type: "GET",
            beforeSend: function () {
                kendo.ui.progress($("#mediaTypeId-input").parent(), true);
            },
            success: function (response) {
                $.each(response, function (i, mediaType) {
                    mediaTypes.push({ text: mediaType.Name, value: mediaType.MediaTypeId.toString() })
                });
                $mediaTypesDropdown.setDataSource(mediaTypes)
                $mediaTypesDropdown.value($mediaTypesTextBox.attr("value"));
                kendo.ui.progress($("#mediaTypeId-input").parent(), false);
            }
        });

        $.ajax({
            url: "/Genre/ListGenres",
            type: "GET",
            beforeSend: function () {
                kendo.ui.progress($("#genreId-input").parent(), true);
            },
            success: function (response) {
                $.each(response, function (i, genre) {
                    genres.push({ text: genre.Name, value: genre.GenreId.toString() })
                });
                $genresDropdown.setDataSource(genres)
                $genresDropdown.value($genresTextBox.attr("value"));
                kendo.ui.progress($("#genreId-input").parent(), false);
            }
        });

        // Event listeners
        $('#mediaTypeId-input').on("blur", mediaTypeValidate);
        $form.on("submit", onFormSubmit);

        //Event listeners functions
        function mediaTypeValidate() {
            var $mediaTypeInput = $('#mediaTypeId-input'),
                $mediaTypeDropdown = $("#mediaTypeId-input").data("kendoDropDownList");

            if ($mediaTypeDropdown.value() === "") {
                $mediaTypeSpan.text("The media type Id field is required");
                $mediaTypeSpan.css({
                    "display": "block",
                })
                $mediaTypeInput.prev().css({
                    "border-color": "red",
                })
                return false
            } else {
                $mediaTypeSpan.css({
                    "display": "none",
                })
                $mediaTypeInput.prev().css({
                    "border-color": ""
                })
            }
            return true
        }

        function onFormSubmit() {
            var form = {
                isFormValid: undefined,
                isMediaTypeValid: undefined
            }
            if ($form.valid()) {
                form.isFormValid = true;
            } else {
                form.isFormValid = false;
            }
            if (mediaTypeValidate()) {
                form.isMediaTypeValid = true;
            } else {
                form.isMediaTypeValid = false;
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