"use strict";
(function ($) {
    $(document).ready(function () {

        var $webContainer = $(".web-container"),
            $rightSideContainer = $(".rightSide-content"),
            $entityDetailsIdLink = $(".linkDisabled"),
            $entityDetailsNameParagraph = $(".header-paragraph").text(),
            $detailsDeleteButton = $(".details-delete-button"),
            deleteUrl = "/Employee/Delete";

        $(".sideBar-button:nth-child(1)").addClass("selected-button");

        if ($rightSideContainer.height() > $webContainer.height()) {
            $webContainer.css({
                "height": $rightSideContainer.height()
            })
        }

        // Event listeners
        $detailsDeleteButton.on("click", openEmployeeDeleteModal);

        //Event listeners functions
        function openEmployeeDeleteModal() {

            var modal = new DeleteModal({
                $container: $(".delete-plugin-container"),
                selectedEntitiesIds: getSelectedEntityId(),
                selectedEntitiesNames: getSelectedEntityName(),
                entityType: "Employee",
                paragraphsInfo: "Employee",
                deleteUrl: deleteUrl
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
    });
})(jQuery)