"use strict";
(function ($) {
    $(document).ready(function () {
        $("#countries").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select a country",
                value: null
            }
        });
        $("#supportRep-input").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select an employee",
                value: null
            }
        });
        var $supportRepDropDown = $("#supportRep-input").data("kendoDropDownList"),
            $supportRepTextBox = $("#supportRep-input"),
            $countriesDropDown = $("#countries").data("kendoDropDownList"),
            $countriesTextBox = $("#countries"),
            $form = $(".form-content"),
            $formSubmitButton = $(".form-submitButton"),
            $validator = $(".form-content").data("validator"),
            employees = [],
            countries = [];

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
            url: "/Employee/ListEmployees",
            type: "GET",
            beforeSend: function () {
                kendo.ui.progress($("#supportRep-input").parent(), true);
            },
            success: function (response) {

                $.each(response, function (i, employee) {
                    employees.push({ text: employee.FullName, value: employee.EmployeeId.toString() })
                });
                $supportRepDropDown.setDataSource(employees)
                $supportRepDropDown.value($supportRepTextBox.attr("value"));
                kendo.ui.progress($("#supportRep-input").parent(), false);
            }
        });

        $.ajax({
            url: "/Employee/ListCountries",
            type: "GET",
            beforeSend: function () {
                kendo.ui.progress($("#countries").parent(), true);
            },
            success: function (response) {

                $.each(response, function (i, country) {
                    countries.push({ text: country, value: country })
                });
                $countriesDropDown.setDataSource(countries)
                $countriesDropDown.value($countriesTextBox.attr("value"));
                kendo.ui.progress($("#countries").parent(), false);
            }
        });

        //Checking the customer environment
        var urlParameterValue = getURLParameter('customerId');

        if (urlParameterValue !== undefined) {
            var $emailInput = $("#CustomerUpdateRequest_Email"),
                request = {
                    EntityId: urlParameterValue,
                    Email: $emailInput.val()
                },
                rules = $emailInput.rules("add",
                    {
                        "remote":
                        {
                            url: "/Customer/ValidateEmailAddress",
                            type: "POST",
                            data: request
                        }
                    });

            $emailInput.on("keyup", function () {
                rules.remote.data = requestCreate();
            });

            function requestCreate() {
                request = {
                    EntityId: urlParameterValue,
                    Email: $emailInput.val()
                }
                return request;
            }
        }

        function getURLParameter(sParam) {
            var sPageURL = window.location.search.substring(1),
                sURLVariables = sPageURL.split('&');

            for (var i = 0; i < sURLVariables.length; i++) {
                var sParameterName = sURLVariables[i].split('=');
                if (sParameterName[0] == sParam) {
                    return sParameterName[1];
                }
            }
        };

        // Event listeners
        $form.on("submit", onFormSubmit);

        //Event listeners functions
        function onFormSubmit() {
            // .valid() dont wait for the ajax response for remote rules so a check for pending requests was needed
            if ($form.valid() && $form.validate().pendingRequest === 0) {
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