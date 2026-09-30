"use strict";
(function ($) {
    $(document).ready(function () {

        var $webContainer = $(".web-container"),
            $rightSideContainer = $(".rightSide-content"),
            $entityDetailsIdLink = $(".linkDisabled"),
            $entityDetailsNameParagraph = $(".header-paragraph").text(),
            $detailsDeleteButton = $(".details-delete-button"),
            $invoicesGrid = $('#invoicesGrid').data("kendoGrid"),
            $invoicesGridDataSource = $invoicesGrid.dataSource,
            $tracksGrid = $('#tracksGrid').data("kendoGrid"),
            $tracksGridDataSource = $tracksGrid.dataSource,
            $invoiceSearchTextBox = $("#invoice-search-text-box"),
            $trackSearchTextBox = $("#track-search-text-box"),
            $invoiceFilterSelect = $(".invoice-filter-select").multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Choose to search by'
            }),
            $trackFilterSelect = $(".track-filter-select").multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Choose to search by'
            }),
            $invoicesDeleteDropdown = $(".invoices-index-delete-button"),
            $tracksDeleteDropdown = $(".tracks-index-delete-button"),
            customerDeleteUrl = "/Customer/Delete",
            invoiceDeleteUrl = "/Invoice/Delete",
            trackDeleteUrl = "/Track/Delete";

        $(".sideBar-button:nth-child(2)").addClass("selected-button");
        $invoiceFilterSelect.multiselect('selectAll', false);
        $invoiceFilterSelect.multiselect('updateButtonText');
        $trackFilterSelect.multiselect('selectAll', false);
        $trackFilterSelect.multiselect('updateButtonText');

        if ($rightSideContainer.height() > $webContainer.height()) {
            $webContainer.css({
                "height": $rightSideContainer.height()
            })
        }

        function setInvoicesGridUserOptions() {
            var optionsJson = sessionStorage["customerInvoicesGridOptions"];
            if (optionsJson) {
                var options = JSON.parse(optionsJson);

                $invoicesGridDataSource.pageSize(options.dataSource.pageSize);
                $invoicesGridDataSource.page(options.dataSource.page);
                $invoicesGridDataSource.sort(options.dataSource.sort);
            }
        }
        function setTracksGridUserOptions() {
            var optionsJson = sessionStorage["customerTracksGridOptions"];
            if (optionsJson) {
                var options = JSON.parse(optionsJson);

                $tracksGridDataSource.pageSize(options.dataSource.pageSize);
                $tracksGridDataSource.page(options.dataSource.page);
                $tracksGridDataSource.sort(options.dataSource.sort);
            }
        }

        //Checking the customer details environment
        var urlParameterValue = getURLParameter('customerId');
        var lastGridPage = getURLParameter('lastGridPage');

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

        if (lastGridPage) {
            $invoicesGrid.one("dataBound", function (e) {
                var items = $invoicesGridDataSource.total(),
                    pageSize = $invoicesGridDataSource.pageSize(),
                    pageNum = parseInt(items / pageSize) + 1;

                $invoicesGridDataSource.page(pageNum);
            });
        } else {
            $invoicesGrid.one("dataBound", function (e) {
                setInvoicesGridUserOptions();
            })
        }

        $tracksGrid.one("dataBound", function (e) {
            setTracksGridUserOptions();
        })

        var tabToSelect = sessionStorage["customerActiveTab"];
        if (tabToSelect) {
            var tab = JSON.parse(tabToSelect);
            $("#" + tab).click();
        }

        // Event listeners
        $detailsDeleteButton.on("click", openCustomerDeleteModal);
        $invoiceSearchTextBox.on("input", onInvoiceSearch);
        $trackSearchTextBox.on("input", onTrackSearch);
        $invoiceFilterSelect.on("change", onInvoiceSearch)
        $trackFilterSelect.on("change", onTrackSearch)
        $invoicesGrid.bind("change", onInvoiceCheckBoxSelected);
        $tracksGrid.bind("change", onTrackCheckBoxSelected);
        $invoicesDeleteDropdown.on("click", openInvoicesDeleteModal);
        $tracksDeleteDropdown.on("click", openTracksDeleteModal);
        $invoicesGrid.bind('dataBound', onInvoicesPageSizeChange);
        $tracksGrid.bind('dataBound', onTracksPageSizeChange);

        $(".nav-item").on("click", function (e) {
            var activeTabId = $(this).attr("id");
            sessionStorage["customerActiveTab"] = JSON.stringify(activeTabId);
        });

        $("#nav-invoices-tab").on("click", function () {
            setTimeout(function () {
                $invoicesGridDataSource.read();
                $invoicesGrid.refresh();
            }, 50)
        })
        $("#nav-tracks-tab").on("click", function () {
            setTimeout(function () {
                $tracksGridDataSource.read();
                $tracksGrid.refresh();
            }, 50)
        })

        //Event listeners functions
        function openCustomerDeleteModal() {

            var modal = new DeleteModal({
                $container: $(".delete-plugin-container"),
                selectedEntitiesIds: getSelectedEntityId(),
                selectedEntitiesNames: getSelectedEntityName(),
                entityType: "Customer",
                paragraphsInfo: "Customer",
                deleteUrl: customerDeleteUrl
            });

            modal.Open();
        };

        function getSelectedEntityId() {
            return [$entityDetailsIdLink.text()];
        }

        function getSelectedEntityName() {
            return $entityDetailsNameParagraph;
        }

        $entityDetailsIdLink.on("click", function () {
            return false;
        })

        function onInvoiceSearch() {
            var searchValue = $.trim($invoiceSearchTextBox.val()),
                filterSelectedOptions = $invoiceFilterSelect.val(),
                gridFilters = [];

            if ($.inArray("invoiceId", filterSelectedOptions) >= 0) {
                if (/^[0-9]+$/.test(searchValue)) {
                    gridFilters.push({ field: "InvoiceId", operator: "eq", value: parseInt(searchValue) });
                }
            }

            if ($.inArray("invoiceDate", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "InvoiceDate", operator: "contains", value: searchValue });
            }

            if ($.inArray("billingAddress", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "BillingAddress", operator: "contains", value: searchValue });
            }

            if ($.inArray("billingCity", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "BillingCity", operator: "contains", value: searchValue });
            }

            if ($.inArray("billingCountry", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "BillingCountry", operator: "contains", value: searchValue });
            }

            if ($.inArray("billingPostalCode", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "BillingPostalCode", operator: "contains", value: searchValue });
            }

            if ($.inArray("total", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "Total", operator: "contains", value: searchValue });
            }

            $invoicesGridDataSource.filter({
                logic: "or",
                filters: gridFilters
            });
        }

        function onTrackSearch() {
            var searchValue = $.trim($trackSearchTextBox.val()),
                filterSelectedOptions = $trackFilterSelect.val(),
                gridFilters = [];

            if ($.inArray("trackId", filterSelectedOptions) >= 0) {
                if (/^[0-9]+$/.test(searchValue)) {
                    gridFilters.push({ field: "TrackId", operator: "eq", value: parseInt(searchValue) });
                }
            }

            if ($.inArray("name", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "Name", operator: "contains", value: searchValue });
            }

            if ($.inArray("album", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "AlbumTitle", operator: "contains", value: searchValue });
            }

            if ($.inArray("composer", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "Composer", operator: "contains", value: searchValue });
            }

            if ($.inArray("bytes", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "Bytes", operator: "contains", value: searchValue });
            }

            if ($.inArray("unitPrice", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "UnitPrice", operator: "contains", value: searchValue });
            }

            $tracksGridDataSource.filter({
                logic: "or",
                filters: gridFilters
            });
        }

        function onInvoiceCheckBoxSelected() {
            var ids = getSelectedInvoiceEntitiesIds();

            if (ids.length > 0) {
                $invoicesDeleteDropdown.prop("disabled", false);
            } else {
                $invoicesDeleteDropdown.prop("disabled", true);
            }
        }

        function onTrackCheckBoxSelected() {
            var ids = getSelectedTrackEntitiesIds();

            if (ids.length > 0) {
                $tracksDeleteDropdown.prop("disabled", false);
            } else {
                $tracksDeleteDropdown.prop("disabled", true);
            }
        }

        function openInvoicesDeleteModal() {
            var InvoiceDeleteRequest = {
                InvoiceIdsForDeletion: getSelectedInvoiceEntitiesIds(),
                CustomerId: urlParameterValue,
                IsFromCustomer: true
            }

            var modal = new DeleteModal({
                $container: $(".delete-plugin-container"),
                selectedEntitiesIds: InvoiceDeleteRequest,
                selectedEntitiesNames: getSelectedInvoiceEntitiesNames(),
                entityType: "Invoices",
                paragraphsInfo: "Invoices with id",
                deleteUrl: invoiceDeleteUrl
            });

            modal.Open();
        };

        function openTracksDeleteModal() {

            var modal = new DeleteModal({
                $container: $(".delete-plugin-container"),
                selectedEntitiesIds: getSelectedTrackEntitiesIds(),
                selectedEntitiesNames: getSelectedTrackEntitiesNames(),
                entityType: "Tracks",
                paragraphsInfo: "Tracks with id",
                deleteUrl: trackDeleteUrl
            });

            modal.Open();
        };

        function getSelectedInvoiceEntitiesIds() {
            return $invoicesGrid.selectedKeyNames();
        }

        function getSelectedInvoiceEntitiesNames() {
            var ids = getSelectedInvoiceEntitiesIds(),
                selectedInvoiceEntitiesNames = ids;

            return selectedInvoiceEntitiesNames;
        }

        function getSelectedTrackEntitiesIds() {
            return $tracksGrid.selectedKeyNames();
        }

        function getSelectedTrackEntitiesNames() {
            var ids = getSelectedTrackEntitiesIds(),
                selectedTrackEntitiesNames = ids;

            return selectedTrackEntitiesNames;
        }

        function onInvoicesPageSizeChange() {
            $invoicesDeleteDropdown.prop("disabled", true);

            sessionStorage["customerInvoicesGridOptions"] = kendo.stringify($invoicesGrid.getOptions());
        }

        function onTracksPageSizeChange() {
            $tracksDeleteDropdown.prop("disabled", true);

            sessionStorage["customerTracksGridOptions"] = kendo.stringify($tracksGrid.getOptions());
        }
    });
})(jQuery)