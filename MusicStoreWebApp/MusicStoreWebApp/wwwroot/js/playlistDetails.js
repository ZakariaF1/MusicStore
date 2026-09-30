"use strict";
(function ($) {
    $(document).ready(function () {

        var $webContainer = $(".web-container"),
            $rightSideContainer = $(".rightSide-content"),
            $entityDetailsIdLink = $(".linkDisabled"),
            $entityDetailsNameParagraph = $(".header-paragraph").text(),
            $detailsDeleteButton = $(".details-delete-button"),
            $grid = $('#grid').data("kendoGrid"),
            $gridDataSource = $grid.dataSource,
            $searchTextBox = $(".search-text-box"),
            $filterSelect = $(".filter-select").multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Choose to search by'
            }),
            $deleteDropdown = $(".index-delete-button"),
            $linkTrackButton = $(".create-button"),
            $linkModalWrapper = $(".track-modal-wrapper"),
            $linkModalLinkButton = $(".trackModal-mainButton"),
            $linkModalCancelButton = $(".trackModal-secondaryButton"),
            playlistDeleteUrl = "/Playlist/Delete",
            playlistTrackDeleteUrl = "/Playlist/DeletePlaylistTracks",
            trackDetails,
            tracks = [];

        $(".sideBar-button:nth-child(5)").addClass("selected-button");
        $filterSelect.multiselect('selectAll', false);
        $filterSelect.multiselect('updateButtonText');

        if ($rightSideContainer.height() > $webContainer.height()) {
            $webContainer.css({
                "height": $rightSideContainer.height()
            })
        }

        var tabToSelect = sessionStorage["playlistActiveTab"];
        if (tabToSelect) {
            var tab = JSON.parse(tabToSelect);
            $("#" + tab).click();
        }

        function setGridUserOptions() {
            var optionsJson = sessionStorage["playlistTrackGridOptions"];
            if (optionsJson) {
                var options = JSON.parse(optionsJson);

                $gridDataSource.pageSize(options.dataSource.pageSize);
                $gridDataSource.page(options.dataSource.page);
                $gridDataSource.sort(options.dataSource.sort);
            }
        }

        //Checking the playlist environment
        var urlParameterValue = getURLParameter('playlistId'),
            lastGridPage = getURLParameter('lastGridPage');

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
        $detailsDeleteButton.on("click", openPlaylistDeleteModal);
        $searchTextBox.on("input", gridSearch);
        $filterSelect.on("change", gridSearch)
        $grid.bind("change", onCheckBoxSelected);
        $deleteDropdown.on("click", openPlaylistTrackDeleteModal);
        $linkTrackButton.on("click", openLinkModal);
        $('#track-input').on("change", getTrackDetails);
        $linkModalLinkButton.on("click", function () {
            linkTrack(trackDetails);
        });
        $linkModalCancelButton.on("click", closeLinkModal);
        $grid.bind('dataBound', onPageSizeChange);

        $(".nav-item").on("click", function (e) {
            var activeTabId = $(this).attr("id");
            sessionStorage["playlistActiveTab"] = JSON.stringify(activeTabId);
        });

        $("#nav-tracks-tab").on("click", function () {
            setTimeout(function () {
                $gridDataSource.read();
                $grid.refresh();
            }, 100)
        });

        $("#track-input").kendoDropDownList({});

        $.ajax({
            url: "/Track/ListTracks",
            type: "GET",
            beforeSend: function () {
                kendo.ui.progress($("#track-input").parent(), true);
            },
            success: function (response) {

                var playlistTracks = [],
                    gridPlaylistTracks = $gridDataSource.view(),
                    gridPlaylistTracksLength = $gridDataSource.view().length;

                for (var i = 0; i < gridPlaylistTracksLength; i++) {
                    playlistTracks.push(gridPlaylistTracks[i].TrackId);
                }

                $.each(response, function (i, track) {
                    if ($.inArray(track.TrackId, playlistTracks) >= 0) {
                        tracks.push({ text: track.Name, value: track.TrackId.toString(), isNotAvailable: true })
                    } else {
                        tracks.push({ text: track.Name, value: track.TrackId.toString(), isNotAvailable: false })
                    }
                });
                $("#track-input").kendoDropDownList({
                    dataTextField: "text",
                    dataValueField: "value",
                    dataSource: tracks,
                    optionLabel: {
                        text: "Select a track",
                        value: null
                    },
                    select: function (e) {
                        if (e.dataItem.isNotAvailable) {
                            e.preventDefault();
                        }
                    },
                    template: kendo.template($("#template").html())
                });
                kendo.ui.progress($("#track-input").parent(), false);
            }
        });

        //Event listeners functions
        function openPlaylistDeleteModal() {

            var modal = new DeleteModal({
                $container: $(".delete-plugin-container"),
                selectedEntitiesIds: getSelectedEntityId(),
                selectedEntitiesNames: getSelectedEntityName(),
                entityType: "Playlist",
                paragraphsInfo: "Playlist",
                deleteUrl: playlistDeleteUrl
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

        function openPlaylistTrackDeleteModal() {

            var data = []
            data.push([urlParameterValue], getSelectedEntitiesIds())

            var modal = new DeleteModal({
                $container: $(".delete-plugin-container"),
                selectedEntitiesIds: data,
                selectedEntitiesNames: getSelectedEntitiesNames(),
                entityType: "Playlist Tracks",
                paragraphsInfo: "Playlist Tracks with id",
                deleteUrl: playlistTrackDeleteUrl
            });

            modal.Open();
        };

        function getSelectedEntitiesIds() {
            return $grid.selectedKeyNames();
        }

        function getSelectedEntitiesNames() {
            var ids = getSelectedEntitiesIds(),
                selectedEntitiesNames = ids;

            return selectedEntitiesNames;
        }

        function openLinkModal() {
            $linkModalWrapper.css({
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

                        $linkModalLinkButton.prop("disabled", false);
                    }
                });
            } else {
                $("div").remove(".added-form");
                $linkModalLinkButton.prop("disabled", true);
            }
        }

        function linkTrack(trackDetails) {
            var data = [urlParameterValue, trackDetails.TrackId]

            $.ajax({
                url: "/Playlist/CreatePlaylistTrack",
                type: "POST",
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json',
                success: function (response) {
                    window.location.href = response.redirectToUrl;
                }
            });
        }

        function closeLinkModal() {
            $linkModalWrapper.css({
                "display": "none"
            })
            $("#track-input").data("kendoDropDownList").select(0);
            $("div").remove(".added-form");
            $linkModalLinkButton.prop("disabled", true);
        }

        function onPageSizeChange() {
            $deleteDropdown.prop("disabled", true);

            sessionStorage["playlistTrackGridOptions"] = kendo.stringify($grid.getOptions());
        }
    });
})(jQuery)