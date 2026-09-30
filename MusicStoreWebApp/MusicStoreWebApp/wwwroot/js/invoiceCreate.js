"use strict";
(function ($) {
    $(document).ready(function () {

        $("#grid").kendoGrid({
            height: 409,
            autoBind: false,
            columns: [
                { field: "TrackId", title: "Track Id" },
                { field: "TrackName", title: "Track Name" },
                { field: "UnitPrice", title: "Unit Price", format: "{0:c}"},
                { field: "Quantity" },
                { selectable: true, width: "70px" },
            ],
            dataSource: {
                data: [],
                schema: {
                    model: { id: "TrackId" }
                },
                pageSize: 10
            },
            sortable: {
                showIndexes: true,
                mode: "multiple"
            },
            pageable: {
                pageSizes: [5, 10, 20]
            },
        });

        $("#customerId-input").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select a customer",
                value: null
            }
        });
        $("#countries").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select a country",
                value: null
            }
        });
        $("#track-input").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            optionLabel: {
                text: "Select a track",
                value: null
            }
        });

        var $grid = $('#grid').data("kendoGrid"),
            $gridDataSource = $grid.dataSource,
            $customersDropdown = $('#customerId-input').data("kendoDropDownList"),
            $countriesDropdown = $('#countries').data("kendoDropDownList"),
            $countriesTextBox = $('#countries'),
            $tracksDropdown = $('#track-input').data("kendoDropDownList"),
            $tracksTextBox = $('#track-input'),
            $searchTextBox = $("#search-text-box"),
            $filterSelect = $(".filter-select").multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Choose to search by'
            }),
            $addTrackButton = $(".create-button"),
            $removeDropdown = $(".index-delete-button"),
            $addModalWrapper = $(".track-modal-wrapper"),
            $removeModalWrapper = $(".remove-modal-wrapper"),
            $unitPriceTextbox = $(".unit-price-textBox"),
            $quantityTextbox = $(".quantity-textBox"),
            $addModalAddButton = $(".trackModal-mainButton"),
            $addModalCancelButton = $(".trackModal-secondaryButton"),
            $removeModalRemoveButton = $(".removeModal-mainButton"),
            $removeModalCancelButton = $(".removeModal-secondaryButton"),
            $customerIdTextBox = $("#customerId-input"),
            $customerSpan = $(".customerId-validation-error"),
            $invoiceDateTextBox = $(".invoice-invoiceDate"),
            $billingAddressTextBox = $(".invoice-billingAddress"),
            $billingCityTextBox = $(".invoice-billingCity"),
            $billingStateTextBox = $(".invoice-billingState"),
            $billingPostalCodeTextBox = $(".invoice-billingPostalCode"),
            $totalPriceTextBox = $(".total-price"),
            $formSubmitButton = $(".form-submitButton"),
            $formCancelButton = $(".form-cancelButton"),
            $errorMessageContainer = $(".error-message-container"),
            $errorMessageCloseButton = $(".fa-times"),
            $invoiceFormValidator = $("#main-form").data("validator"),
            $invoiceItemFormValidator = $("#secondary-form").data("validator"),
            trackDetails,
            countries = [],
            tracks = [],
            request = {
                InvoiceCreateRequest: undefined,
                InvoiceItemCreateRequests: undefined,
                CustomerId: undefined,
                IsFromCustomer: undefined
            };

        if ($invoiceFormValidator) {
            $invoiceFormValidator.settings.onkeyup = false;
        }
        if ($invoiceItemFormValidator) {
            $invoiceItemFormValidator.settings.onkeyup = false;
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

        $filterSelect.multiselect('selectAll', false);
        $filterSelect.multiselect('updateButtonText');

        var $invoiceDateInput = $(".invoice-invoiceDate"),
            currentDate = new Date(),
            currentYear = currentDate.getFullYear().toString(),
            currentMonth = (currentDate.getMonth() + 1).toString(),
            currentDay = currentDate.getDate().toString();


        if (currentMonth.length == 1) {
            currentMonth = "0" + currentMonth;
        }
        if (currentDay.length == 1) {
            currentDay = "0" + currentDay;
        }

        var minDate = `${currentYear - 50}-${currentMonth}-${currentDay}`,
            maxDate = `${currentYear}-${currentMonth}-${currentDay}`

        $invoiceDateInput.attr({
            "max": maxDate,
            "min": minDate
        })

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
                $countriesDropdown.setDataSource(countries)
                $countriesDropdown.value($countriesTextBox.attr("value"));
                kendo.ui.progress($("#countries").parent(), false);
            }
        });

        $.ajax({
            url: "/Track/ListTracks",
            type: "GET",
            beforeSend: function () {
                kendo.ui.progress($("#track-input").parent(), true);
            },
            success: function (response) {
                $.each(response, function (i, track) {
                    tracks.push({ text: track.Name, value: track.TrackId.toString() })
                });
                $tracksDropdown.setDataSource(tracks);
                $tracksDropdown.value($tracksTextBox.attr("value"));
                kendo.ui.progress($("#track-input").parent(), false);
            }
        });

        //Checking the invoiceCreate environment
        var urlParameterValue = getURLParameter('customerId'),
            customers = [];

        if (urlParameterValue === undefined) {
            $.ajax({
                url: "/Customer/ListCustomers",
                type: "GET",
                beforeSend: function () {
                    kendo.ui.progress($("#customerId-input").parent(), true);
                },
                success: function (response) {
                    $.each(response, function (i, customer) {
                        customers.push({ text: customer.FullName, value: customer.CustomerId.toString() })
                    });
                    $customersDropdown.setDataSource(customers)
                    $customersDropdown.value($customerIdTextBox.attr("value"));
                    kendo.ui.progress($("#customerId-input").parent(), false);
                }
            });
            request.CustomerId = null;
            request.IsFromCustomer = false
            $formCancelButton.on("click", function () {
                onFormCancel(false);
            });
        } else {
            $.ajax({
                url: "/Customer/ListCustomers",
                type: "GET",
                beforeSend: function () {
                    kendo.ui.progress($("#customerId-input").parent(), true);
                },
                success: function (response) {
                    $.each(response, function (i, customer) {
                        customers.push({ text: customer.FullName, value: customer.CustomerId.toString() })
                    });
                    $customersDropdown.setDataSource(customers)
                    $customersDropdown.value(urlParameterValue);
                    $customersDropdown.enable(false);
                    kendo.ui.progress($("#customerId-input").parent(), false);
                }
            });
            request.CustomerId = urlParameterValue;
            request.IsFromCustomer = true
            $formCancelButton.on("click", function () {
                onFormCancel(true);
            });
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

        function setGridUserOptions() {
            var optionsJson = sessionStorage["invoiceCreateGridOptions"];
            if (optionsJson) {
                var options = JSON.parse(optionsJson);

                $gridDataSource.pageSize(options.dataSource.pageSize);
                $gridDataSource.page(options.dataSource.page);
                $gridDataSource.sort(options.dataSource.sort);
            }
        }

        setGridUserOptions();

        // Event listeners
        $searchTextBox.on("input", gridSearch)
        $filterSelect.on("change", gridSearch)
        $grid.bind("change", onCheckBoxSelected);
        $grid.bind('dataBound', onPageSizeChange);
        $addTrackButton.on("click", openAddModal);
        $removeDropdown.on("click", openRemoveModal);
        $('#track-input').on("change", getTrackDetails);
        $addModalAddButton.on("click", onTrackAdd);
        $addModalCancelButton.on("click", closeAddModal);
        $removeModalRemoveButton.on("click", onTrackRemove);
        $removeModalCancelButton.on("click", closeRemoveModal);
        $('#customerId-input').on("blur", customerValidate);
        $formSubmitButton.on("click", onFormSubmit);
        $errorMessageCloseButton.on("click", closeErrorMessage);
        $(".dateTime ").on("focusout", invalidDateHandler);


        //Event listeners functions
        function gridSearch() {
            var searchValue = $.trim($searchTextBox.val()),
                filterSelectedOptions = $filterSelect.val(),
                gridFilters = [];

            if ($.inArray("trackId", filterSelectedOptions) >= 0) {
                if (/^[0-9]+$/.test(searchValue)) {
                    gridFilters.push({ field: "TrackId", operator: "eq", value: parseInt(searchValue) });
                }
            }

            if ($.inArray("trackName", filterSelectedOptions) >= 0) {
                    gridFilters.push({ field: "TrackName", operator: "contains", value: searchValue });
            }

            if ($.inArray("unitPrice", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "UnitPrice", operator: "contains", value: searchValue });
            }

            if ($.inArray("quantity", filterSelectedOptions) >= 0) {
                if (/^[0-9]+$/.test(searchValue)) {
                    gridFilters.push({ field: "Quantity", operator: "eq", value: parseInt(searchValue) });
                }
            }

            $gridDataSource.filter({
                logic: "or",
                filters: gridFilters
            });
        }

        function onCheckBoxSelected() {
            var ids = getSelectedEntitiesIds();

            if (ids.length > 0) {
                $removeDropdown.prop("disabled", false);
            } else {
                $removeDropdown.prop("disabled", true);
            }
        }

        function openRemoveModal() {
            $removeModalWrapper.css({
                "display": "block"
            })
            $(".removeModal-header-paragraph").text(`Remove Tracks with Id ${getSelectedEntitiesNames()}`);
            $(".removeModal-content-paragraph").text(`WARNING: Tracks with Id ${getSelectedEntitiesNames()} will be removed. You can add them again if you wanted`);
        };

        function getSelectedEntitiesIds() {
            return $grid.selectedKeyNames();
        }

        function getSelectedEntitiesNames() {
            var ids = getSelectedEntitiesIds(),
                selectedEntitiesNames = ids;

            return selectedEntitiesNames;
        }

        function onPageSizeChange() {
            $removeDropdown.prop("disabled", true);

            sessionStorage["invoiceCreateGridOptions"] = kendo.stringify($grid.getOptions());
        }

        function openAddModal() {
            $addModalWrapper.css({
                "display": "block"
            })
        }

        function getTrackDetails() {
            var $trackDropdown = $("#track-input").data("kendoDropDownList");

            if ($trackDropdown.value() !== "") {
                $.ajax({
                    url: "/Track/GetTrack",
                    type: "POST",
                    data: JSON.stringify($trackDropdown.value()),
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (response) {

                        trackDetails = response;

                        $("div").remove(".added-form");
                        var $formGroupContainer = $("<div/>", {
                            class: "form-group added-form"
                        });

                        var $firstFormGroup = $(".first-form-group"),
                            $trackDetailsLabel = $("<div/>", {
                                class: "trackDetails-label",
                                text: "Track Details"
                            }),
                            $trackIdLabel = $("<div/>", {
                                class: "trackId-label track-metaData"
                            }),
                            $albumIdLabel = $("<div/>", {
                                class: "albumId-label track-metaData"
                            }),
                            $mediaTypeIdLabel = $("<div/>", {
                                class: "mediaTypeId-label track-metaData"
                            }),
                            $genreIdLabel = $("<div/>", {
                                class: "genreId-label track-metaData"
                            }),
                            $unitPriceLabel = $("<div/>", {
                                class: "unitPrice-label track-metaData"
                            });

                        $formGroupContainer.insertAfter($firstFormGroup);
                        $formGroupContainer.append($trackDetailsLabel, $trackIdLabel, $albumIdLabel, $mediaTypeIdLabel, $genreIdLabel, $unitPriceLabel)

                        $trackIdLabel.text("Track Id: " + response.TrackId);
                        $albumIdLabel.text(response.Album === null ? "Album: -" : "Album: " + response.Album.Title);
                        $mediaTypeIdLabel.text("Media Type: " + response.MediaType.Name);
                        $genreIdLabel.text(response.Genre === null ? "Genre: -" : "Genre: " + response.Genre.Name);
                        $unitPriceLabel.text("Unit Price: " + response.UnitPrice);

                        $unitPriceTextbox.attr("placeholder", response.UnitPrice)
                        $addModalAddButton.prop("disabled", false);
                    }
                });
            } else {
                $("div").remove(".added-form");
                $unitPriceTextbox.removeAttr("placeholder")
                $addModalAddButton.prop("disabled", true);
            }
        }

        function onTrackAdd() {

            if ($("#secondary-form").valid()) {
                addTrack(trackDetails);
                closeAddModal();
            } else {
                return false
            }
        }

        function addTrack(trackDetails) {

            var invoiceItems = [],
                gridinvoiceItems = $grid.dataSource.view(),
                gridinvoiceItemsLength = $grid.dataSource.view().length;

            for (var i = 0; i < gridinvoiceItemsLength; i++) {
                var invoiceItem = {
                    TrackId: gridinvoiceItems[i].TrackId,
                    UnitPrice: gridinvoiceItems[i].UnitPrice,
                    Quantity: gridinvoiceItems[i].Quantity
                }
                invoiceItems.push(invoiceItem);
            }

            if ($unitPriceTextbox.val() == "") {
                if (gridinvoiceItemsLength > 0) {
                    $.each(gridinvoiceItems, function (index, data) {
                        if (trackDetails.TrackId == data.TrackId && trackDetails.UnitPrice == data.UnitPrice) {
                            data.set("Quantity", parseInt(data.Quantity) + parseInt($quantityTextbox.val()));
                            return false
                        }
                        if (index + 1 == gridinvoiceItemsLength) {
                            $grid.dataSource.add({
                                "TrackId": trackDetails.TrackId,
                                "TrackName": trackDetails.Name,
                                "UnitPrice": parseFloat(trackDetails.UnitPrice),
                                "Quantity": parseInt($quantityTextbox.val())
                            });
                        }
                    });
                } else {
                    $grid.dataSource.add({
                        "TrackId": trackDetails.TrackId,
                        "TrackName": trackDetails.Name,
                        "UnitPrice": parseFloat(trackDetails.UnitPrice),
                        "Quantity": parseInt($quantityTextbox.val())
                    });
                }
            } else {
                if (gridinvoiceItemsLength > 0) {
                    $.each(gridinvoiceItems, function (index, data) {
                        if (trackDetails.TrackId == data.TrackId && $unitPriceTextbox.val() == data.UnitPrice) {
                            data.set("Quantity", parseInt(data.Quantity) + parseInt($quantityTextbox.val()));
                            return false
                        }
                        if (index + 1 == gridinvoiceItemsLength) {
                            $grid.dataSource.add({
                                "TrackId": trackDetails.TrackId,
                                "TrackName": trackDetails.Name,
                                "UnitPrice": parseFloat($unitPriceTextbox.val()),
                                "Quantity": parseInt($quantityTextbox.val())
                            });
                        }
                    })
                } else {
                    $grid.dataSource.add({
                        "TrackId": trackDetails.TrackId,
                        "TrackName": trackDetails.Name,
                        "UnitPrice": parseFloat($unitPriceTextbox.val()),
                        "Quantity": parseInt($quantityTextbox.val())
                    });
                }
            }
            calculateTotalPrice();
            checkForm()
        }

        function closeAddModal() {
            $(".myModal-wrapper").css({
                "display": "none"
            })
            $("span[data-valmsg-for='unit-price'], span[data-valmsg-for='quantity']").removeClass("field-validation-error");
            $(".unit-price-textBox, .quantity-textBox").removeClass("input-validation-error");
            $("#unit-price-error, #quantity-error").remove();
            $("#track-input").data("kendoDropDownList").select(0);
            $("div").remove(".added-form");
            $unitPriceTextbox.val("");
            $unitPriceTextbox.removeAttr("placeholder");
            $quantityTextbox.val("");
            $addModalAddButton.prop("disabled", true);
        }

        function onTrackRemove() {
            var selectedRows = $grid.select(),
                rowsUids = [];

            $.each(selectedRows, function () {
                var row = $grid.dataItem(this),
                    rowUid = row.uid;

                rowsUids.push(rowUid)
            });

            $.each(rowsUids, function (i, uid) {
                var dataRow = $grid.dataSource.getByUid(uid);
                $grid.dataSource.remove(dataRow);
            });
            closeAddModal();
            calculateTotalPrice()
            checkForm()
        }

        function closeRemoveModal() {
            $removeModalWrapper.css({
                "display": "none"
            })
        }

        function calculateTotalPrice() {
            var $gridDataSourceData = $grid.dataSource.data();

            var totalPrice = 0;

            if ($gridDataSourceData.length > 0) {
                $.each($gridDataSourceData, function (i, item) {
                    totalPrice += item.UnitPrice * item.Quantity;
                });
                totalPrice = parseFloat(totalPrice.toFixed(2));
            } else {
                totalPrice = 0;
            }
            $totalPriceTextBox.val(totalPrice);
        }

        function customerValidate() {
            var $customerDropdown = $("#customerId-input").data("kendoDropDownList");

            if ($customerDropdown.value() === "") {
                $customerSpan.text("The customer Id field is required");
                $customerSpan.css({
                    "display": "block",
                })
                $customerIdTextBox.prev().css({
                    "border-color": "red",
                })
                return false
            } else {
                $customerSpan.css({
                    "display": "none",
                })
                $customerIdTextBox.prev().css({
                    "border-color": ""
                })
            }
            return true
        }

        function checkForm() {
            var gridinvoiceItemsLength = $grid.dataSource.data().length;
            if (gridinvoiceItemsLength > 0) {
                $formSubmitButton.prop("disabled", false);
            } else {
                $formSubmitButton.prop("disabled", true);
            }
        }

        function gridValidate() {
            var gridRows = $grid.tbody.find("tr"),
                gridIsValid = true;

            $.each(gridRows, function (i, row) {
                var rowItem = $grid.dataItem($(this));

                if (rowItem.Quantity > 10) {
                    $(row).css({
                        "background-color": "rgba(255, 0, 0, 0.76)"
                    })
                    gridIsValid = false;
                }
            });

            if (!gridIsValid) {
                $errorMessageContainer.css({
                    "display": "block"
                })
                setTimeout(function () {
                    $errorMessageContainer.fadeOut();
                }, 5000);
                return false;
            }
            return true;
        }

        function closeErrorMessage() {
            $errorMessageContainer.css({
                "display": "none"
            })
        }

        function invalidDateHandler() {
            if ($(this).val() == "") {
                $(this).val("")
            }
        }

        function onFormSubmit() {
            $("#main-form").valid();

            if (urlParameterValue === undefined) {
                gridValidate();
                if (!customerValidate() || !gridValidate()) {
                    return false
                }
            } else {
                if (!gridValidate()) {
                    return false
                }
            }

            var invoiceItems = [],
                gridinvoiceItems = $grid.dataSource.data(),
                gridinvoiceItemsLength = $grid.dataSource.data().length;

            for (var i = 0; i < gridinvoiceItemsLength; i++) {
                var invoiceItem = {
                    TrackId: gridinvoiceItems[i].TrackId,
                    UnitPrice: gridinvoiceItems[i].UnitPrice,
                    Quantity: gridinvoiceItems[i].Quantity
                }
                invoiceItems.push(invoiceItem);
            }

            request.InvoiceCreateRequest = {
                CustomerId: $customerIdTextBox.val(),
                InvoiceDate: $invoiceDateTextBox.val(),
                BillingAddress: $billingAddressTextBox.val(),
                BillingCity: $billingCityTextBox.val(),
                BillingState: $billingStateTextBox.val(),
                BillingCountry: $("#countries").data("kendoDropDownList").value(),
                BillingPostalCode: $billingPostalCodeTextBox.val()
            }
            request.InvoiceItemCreateRequests = invoiceItems;

            if ($("#main-form").valid()) {
                $.each(request.InvoiceCreateRequest, function (name, value) {
                    var property = name

                    if (value == "") {
                        request.InvoiceCreateRequest[property] = undefined;
                    } else {
                        var trimmedValue = $.trim(value);
                        request.InvoiceCreateRequest[property] = trimmedValue;
                    }
                });

                $.ajax({
                    url: "/Invoice/Create",
                    type: "POST",
                    traditional: true,
                    data: JSON.stringify(request),
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (response) {
                        window.location.href = response.redirectToUrl;
                    }
                });
                $formSubmitButton.prop("disabled", true)
            }
        }

        function onFormCancel(isFromCustomer) {
            if (isFromCustomer) {
                window.location.href = `/Customer/Details?customerId=${urlParameterValue}`;
            } else {
                window.location.href = "/Invoice";
            }
        }
    });
})(jQuery)