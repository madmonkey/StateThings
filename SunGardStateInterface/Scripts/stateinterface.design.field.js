/*global ko*/

$(function () {
    var services = new myApp.services();
    var vm = new myApp.vm(initialData);

    vm.fieldsAreLoading = ko.observable(false);
    vm.showNoFieldsFound = ko.observable(false);
    vm.catalogItemsSplit = ko.observable(Math.ceil(vm.CatalogItems().length / 2));

    vm.evaluateShowNoFieldsMessage = function () {
        if (vm.CatalogItems().length === 0) {
            vm.showNoFieldsFound(true);
        }
        else {
            vm.showNoFieldsFound(false);
        }
    };

    vm.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
        vm.getFields(newValue);
    });

    vm.getFields = function (recordsCenterName) {
        vm.fieldsAreLoading(true);
        vm.GetFieldsParameters.RecordsCenterName(recordsCenterName);
        var params = ko.toJSON(vm.GetFieldsParameters)

        services.postToServer(params, function (data) {
            ko.mapping.fromJS(data, {}, vm.CatalogItems);

            vm.evaluateShowNoFieldsMessage();

            vm.fieldsAreLoading(false);

            vm.catalogItemsSplit(Math.ceil(vm.CatalogItems().length / 2));

        }, vm.GetFieldsUrl());
    };

    vm.evaluateShowNoFieldsMessage();
    ko.applyBindings(vm);
});