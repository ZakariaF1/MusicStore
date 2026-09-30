"use strict";
var DeleteModal = (function () {
    function DeleteModal(options) {
        var self = this;

        // options variables
        self.$container = options.$container;
        self.selectedEntitiesIds = options.selectedEntitiesIds;
        self.selectedEntitiesNames = options.selectedEntitiesNames;
        self.entityType = options.entityType;
        self.paragraphsInfo = options.paragraphsInfo;
        self.deleteUrl = options.deleteUrl;

        // Creating modal elements
        self.$deleteModalWrapper = $("<div/>", {
            class: "myModal-wrapper delete-modal-wrapper",
        });
        self.$headerParagraph = $("<div/>", {
            class: "myModal-header-paragraph deleteModal-header-paragraph"
        });
        self.$contentParagraph = $("<div/>", {
            class: "myModal-content-paragraph deleteModal-content-paragraph"
        });
        self.$deleteButton = $("<button/>", {
            class: "btn btn-danger myModal-button myModal-mainButton",
            text: "Delete"
        });
        var $deleteModal = $("<div/>", {
            class: "myModal delete-modal"
        }),
            $modalHeader = $("<div/>", {
                class: "myModal-header deleteModal-header"
            }),
            $contentFooterContainer = $("<div/>", {
                class: "content-footer-container"
            }),
            $modalContent = $("<div/>", {
                class: "myModal-content deleteModal-content"
            }),
            $modalBreakLine = $("<hr/>", {
                class: "break-line"
            }),
            $modalFooter = $("<div/>", {
                class: "myModal-footer deleteModal-footer"
            }),
            $cancelButton = $("<button/>", {
                class: "btn btn-danger myModal-button myModal-secondaryButton",
                text: "Cancel"
            });

        // Appending modal elements
        self.$container.append(self.$deleteModalWrapper);
        self.$deleteModalWrapper.append($deleteModal);
        $deleteModal.append($modalHeader, $contentFooterContainer);
        $modalHeader.append(self.$headerParagraph);
        $contentFooterContainer.append($modalContent, $modalBreakLine, $modalFooter);
        $modalContent.append(self.$contentParagraph);
        $modalFooter.append($cancelButton, self.$deleteButton);


        $cancelButton.on("click", function () {
            self.Cancel.apply(self);
        });

        self.$deleteButton.on("click", function () {
            self.Delete();
        });
    };

    DeleteModal.prototype.Open = function () {
        var self = this;

        self.$deleteModalWrapper.css({
            "display": "block"
        });

        self.$headerParagraph.text(`Delete ${self.paragraphsInfo} ${self.selectedEntitiesNames}`);
        self.$contentParagraph.text(`WARNING: ${self.paragraphsInfo} ${self.selectedEntitiesNames} will be deleted. There is no way to recover the ${self.entityType.toLowerCase()} after deletion`);
    }

    DeleteModal.prototype.Cancel = function () {
        var self = this;

        self.$deleteModalWrapper.remove();
    }

    DeleteModal.prototype.Delete = function () {
        var self = this;

        $.ajax({
            url: self.deleteUrl,
            type: "Delete",
            traditional: true,
            data: JSON.stringify(self.selectedEntitiesIds),
            dataType: 'json',
            contentType: 'application/json',
            success: function (response) {
                window.location.href = response.redirectToUrl;
            }
        });

        self.$deleteButton.prop("disabled", true);
    }
    return DeleteModal;
}());