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
            deleteUrl = "/Album/Delete";

        $(".sideBar-button:nth-child(6)").addClass("selected-button");
        $filterSelect.multiselect('selectAll', false);
        $filterSelect.multiselect('updateButtonText');

        function setGridUserOptions() {
            var optionsJson = sessionStorage["albumGridOptions"];
            if (optionsJson) {
                var options = JSON.parse(optionsJson);

                $gridDataSource.pageSize(options.dataSource.pageSize);
                $gridDataSource.page(options.dataSource.page);
                $gridDataSource.sort(options.dataSource.sort);
            }
        }

        //Checking the album environment
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

            if ($.inArray("albumId", filterSelectedOptions) >= 0) {
                if (/^[0-9]+$/.test(searchValue)) {
                    gridFilters.push({ field: "AlbumId", operator: "eq", value: parseInt(searchValue) });
                }
            }

            if ($.inArray("title", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "Title", operator: "contains", value: searchValue });
            }

            if ($.inArray("artist", filterSelectedOptions) >= 0) {
                gridFilters.push({ field: "ArtistName", operator: "contains", value: searchValue });
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
                entityType: "Albums",
                paragraphsInfo: "Albums",
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
                var albumName = $gridDataSource.get(ids[i]).Title;
                selectedEntitiesNames.push(" " + albumName);
            }

            return selectedEntitiesNames;
        }

        function onPageSizeChange() {
            $deleteDropdown.prop("disabled", true);

            sessionStorage["albumGridOptions"] = kendo.stringify($grid.getOptions());
        }
    });
})(jQuery)