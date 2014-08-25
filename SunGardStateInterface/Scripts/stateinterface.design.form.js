$(function () {
    var vm = new myApp.vm(initialData);

    vm.formsAreLoading = ko.observable(false);
    vm.showNoFormsFound = ko.observable(false);
    vm.catalogItemsSplit = ko.observable(Math.ceil(vm.CatalogItems().length / 2));

    vm.evaluateShowNoFormsMessage = function () {
        if (vm.CatalogItems().length === 0) {
            vm.showNoFormsFound(true);
        }
        else {
            vm.showNoFormsFound(false);
        }
    };

    vm.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
        vm.getForms(newValue);
    });

    vm.getForms = function (recordsCenterName) {
        vm.formsAreLoading(true);
        vm.FormsParameters.RecordsCenterName(recordsCenterName);
        var params = ko.toJSON(vm.FormsParameters);
        vm.services.postToServer(params, function (data) {
            ko.mapping.fromJS(data, {}, vm.CatalogItems);
            vm.evaluateShowNoFormsMessage();
            vm.formsAreLoading(false);
            vm.catalogItemsSplit(Math.ceil(vm.CatalogItems().length / 2));
        }, vm.GetFormsUrl());
    };

    vm.evaluateShowNoFormsMessage();
    ko.applyBindings(vm);
});