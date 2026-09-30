"use strict";
(function ($) {
    $(document).ready(function () {

        var $grid = $('#grid').data("kendoGrid"),
            $gridDataSource = $grid.dataSource,
            $deleteDropdown = $(".index-delete-button"),
            $searchTextBox = $(".search-text-box"),
            $filterSelect = $(".filter-select").multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Choose to search by'
            }),
            deleteUrl = "/Track/Delete";

        $(".sideBar-button:nth-child(4)").addClass("selected-button");
        $filterSelect.multiselect('selectAll', false);
        $filterSelect.multiselect('updateButtonText');

        function setGridUserOptions() {
            var optionsJson = sessionStorage["trackGridOptions"];
            if (optionsJson) {
                var options = JSON.parse(optionsJson);

                $gridDataSource.pageSize(options.dataSource.pageSize);
                $gridDataSource.page(options.dataSource.page);
                $gridDataSource.sort(options.dataSource.sort);
            }
        }

        //Checking the track environment
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
            $grid.one("dataBound", function (e) {
                var items = $gridDataSource.total(),
                    pageSize = $gridDataSource.pageSize(),
                    pageNum = parseInt(items / pageSize) + 1;

                $gridDataSource.page(pageNum);
            });
        } else {
            $grid.one("dataBound", function (e) {
                setGridUserOptions();
            })
        }

        // Event listeners
        $searchTextBox.on("input", gridSearch)
        $filterSelect.on("change", gridSearch)
        $grid.bind("change", onCheckBoxSelected);
        $deleteDropdown.on("click", openDeleteModal);
        $grid.bind('dataBound', onPageSizeChange);

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

            if ($.inArray("name", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "Name", operator: "contains", value: searchValue });
            }

            if ($.inArray("album", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "AlbumTitle", operator: "contains", value: searchValue });
            }

            if ($.inArray("mediaType", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "MediaTypeName", operator: "contains", value: searchValue });
            }

            if ($.inArray("genre", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "GenreName", operator: "contains", value: searchValue });
            }

            if ($.inArray("composer", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "Composer", operator: "contains", value: searchValue });
            }

            if ($.inArray("milliseconds", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "Milliseconds", operator: "contains", value: searchValue });
            }

            if ($.inArray("bytes", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "Bytes", operator: "contains", value: searchValue });
            }

            if ($.inArray("unitPrice", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "UnitPrice", operator: "contains", value: searchValue });
            }

            $gridDataSource.filter({
                logic: "or",
                filters: gridFilters
            });
        }

        function onCheckBoxSelected() {
            var ids = getSelectedEntitiesIds();

            if (ids.length > 0) {
                $deleteDropdown.prop("disabled", false);
            } else {
                $deleteDropdown.prop("disabled", true);
            }
        }

        function openDeleteModal() {

            var modal = new DeleteModal({
                $container: $(".delete-plugin-container"),
                selectedEntitiesIds: getSelectedEntitiesIds(),
                selectedEntitiesNames: getSelectedEntitiesNames(),
                entityType: "Tracks",
                paragraphsInfo: "Tracks",
                deleteUrl: deleteUrl
            });

            modal.Open();
        };

        function getSelectedEntitiesIds() {
            return $grid.selectedKeyNames();
        }

        function getSelectedEntitiesNames() {
            var ids = getSelectedEntitiesIds(),
                selectedEntitiesNames = [];

            for (var i = 0; i < ids.length; i++) {
                var trackName = $gridDataSource.get(ids[i]).Name;
                selectedEntitiesNames.push(" " + trackName);
            }

            return selectedEntitiesNames;
        }

        function onPageSizeChange() {
            $deleteDropdown.prop("disabled", true);

            sessionStorage["trackGridOptions"] = kendo.stringify($grid.getOptions());
        }
    });
})(jQuery)