$(function () {

    //#region knockout extenders
    ko.extenders.numeric = function (target, overrideMessage) {
        target.hasError = ko.observable();
        target.validationMessage = ko.observable();

        // subscribe to the observable so we can intercept the value and do our custom processing.
        target.subscribe(function (newValue) {
            // this will strip out any non numeric characters
            target(newValue.replace(/[^0-9]+/g, '')); //[^0-9\.]/g - allows decimals
            target.hasError(false);
            target.validationMessage(overrideMessage);
        }, this);

        return target;
    };

    ko.extenders.mask = function(target, mask) {
        target.hasError = ko.observable();
        target.validationMessage = ko.observable();

        target.mask = mask;
        target.subscribe(function(newValue) {
            target.hasError(newValue.length != mask.length && newValue.length > 0);
            target.validationMessage(target.hasError ? 'Invalid format' : '');
        }, this);
        return target;
    };
    //#endregion knockout extenders

    //#region knockout binding handlers
    var orgValueInit = ko.bindingHandlers.value.init;
    ko.bindingHandlers.value.init = function(element, valueAccessor) {
        var mask = valueAccessor().mask;
        if (mask) {
            $(element).mask('?' + mask); // make entire field optional - so as to preserve user-input
        }

        orgValueInit.apply(this, arguments);
    };

    //#endregion knockout binding handlers

    //#region knockout validation
    var knockoutValidationSettings = {
        insertMessages: false,
        decorateElement: true,
        errorElementClass: 'err'
    };
    ko.validation.init(knockoutValidationSettings);
    //#endregion knockout validation

});