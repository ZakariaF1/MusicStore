"use strict";
(function ($) {
    $(document).ready(function () {
        $("#reportsTo-input").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select an employee",
                value: null
            },
            select: function (e) {
                if (e.dataItem.isNotAvailable) {
                    e.preventDefault();
                }
            },
            template: kendo.template($("#template").html())
        });
        $("#countries").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select a country",
                value: null
            }
        });

        var $reportsToDropDown = $("#reportsTo-input").data("kendoDropDownList"),
            $countriesDropDown = $("#countries").data("kendoDropDownList"),
            $reportsToTextBox = $("#reportsTo-input"),
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

        var $birthDateInput = $(".birthDate-input"),
            $hireDateInput = $(".hireDate-input"),
            currentDate = new Date(),
            currentYear = currentDate.getFullYear().toString(),
            currentMonth = currentDate.getMonth() + 1,
            currentDay = currentDate.getDate().toString(),
            hireDateMaxMonth = currentMonth + 2;

        currentMonth = currentMonth.toString();
        hireDateMaxMonth = hireDateMaxMonth.toString();


        if (currentMonth.length == 1) {
            currentMonth = "0" + currentMonth;
        }
        if (hireDateMaxMonth.length == 1) {
            hireDateMaxMonth = "0" + hireDateMaxMonth;
        }
        if (currentDay.length == 1) {
            currentDay = "0" + currentDay;
        }

        var minDate = `${currentYear - 100}-${currentMonth}-${currentDay}`,
            birthMaxDate = `${currentYear - 16}-${currentMonth}-${currentDay}`,
            hireMaxDate = `${currentYear}-${hireDateMaxMonth}-${currentDay}`

        $birthDateInput.attr({
            "max": birthMaxDate,
            "min": minDate
        })

        $hireDateInput.attr({
            "max": hireMaxDate
        })

        $.ajax({
            url: "/Employee/ListEmployees",
            type: "GET",
            beforeSend: function () {
                kendo.ui.progress($("#reportsTo-input").parent(), true);
            },
            success: function (response) {

                $.each(response, function (i, employee) {
                    if (employee.EmployeeId == urlParameterValue) {
                        employees.push({ text: employee.FullName, value: employee.EmployeeId.toString(), isNotAvailable: true })
                    } else {
                        employees.push({ text: employee.FullName, value: employee.EmployeeId.toString(), isNotAvailable: false })
                    }
                });
                $reportsToDropDown.setDataSource(employees)
                $reportsToDropDown.value($reportsToTextBox.attr("value"));
                kendo.ui.progress($("#reportsTo-input").parent(), false);
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

        //Checking the employee environment
        var urlParameterValue = getURLParameter('employeeId');

        if (urlParameterValue !== undefined) {
            var $emailInput = $("#EmployeeUpdateRequest_Email");

            $.each($(".dateTime"), function (i, input) {
                var string = "",
                    date = $(input).attr("value").split(" ");

                if (date.length === 3) {
                    var splitedDate = date[0].split("/"),
                        month = splitedDate[0],
                        day = splitedDate[1],
                        year = splitedDate[2];


                    if (month.length == 1) {
                        month = "0" + month;
                    }
                    if (day.length == 1) {
                        day = "0" + day;
                    }

                    string = `${year}-${month}-${day}`

                    $(input).val(string);
                }
            })
            if ($birthDateInput.val()) {
                $hireDateInput.attr("disabled", false);
            }

            var request = {
                EntityId: urlParameterValue,
                Email: $emailInput.val()
            },
                rules = $emailInput.rules("add",
                    {
                        "remote":
                        {
                            url: "/Employee/ValidateEmailAddress",
                            type: "POST",
                            data: request
                        }
                    });

            $emailInput.on("focusout", function () {
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
        $(".dateTime ").on("focusout", invalidDateHandler);
        $(".birthDate-input ").on("focusout", birthDateValidate);
        $form.on("submit", onFormSubmit);

        function invalidDateHandler() {
            if ($(this).val() == "") {
                $(this).val("")
            }
        }

        function birthDateValidate() {
            if ($(this).val() && $(this).valid()) {
                var birthYear = new Date($(this).val()).getFullYear() + 16,
                    birthMonth = new Date($(this).val()).getMonth() + 1,
                    birthDay = new Date($(this).val()).getDate().toString();

                birthMonth = birthMonth.toString();
                birthYear = birthYear.toString();

                if (birthMonth.length == 1) {
                    birthMonth = "0" + birthMonth;
                }
                if (birthDay.length == 1) {
                    birthDay = "0" + birthDay;
                }

                var hireMinDate = `${birthYear}-${birthMonth}-${birthDay}`;

                $hireDateInput.attr({
                    "disabled": false,
                    "min": hireMinDate
                });
            } else {
                $hireDateInput.attr({
                    "disabled": true,
                    "min": ""
                });
                $hireDateInput.val("");
            }
        }

        function onFormSubmit() {
            // .valid() dont wait for the ajax response for remote rules so a check for pending requests was needed
            if ($form.valid() && $form.validate().pendingRequest === 0) {
                console.log("valid");
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
})(jQuery);