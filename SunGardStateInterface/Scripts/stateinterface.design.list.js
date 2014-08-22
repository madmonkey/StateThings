/*global ko*/

$(function () {
    var services = new myApp.services();
    var vm = new myApp.vm(initialData);

    vm.listsAreLoading = ko.observable(false);
    vm.showNoListsFound = ko.observable(false);
    vm.catalogItemsSplit = ko.observable(Math.ceil(vm.CatalogItems().length / 2));

    vm.evaluateShowNoListsMessage = function () {
        if (vm.CatalogItems().length === 0) {
            vm.showNoListsFound(true);
        }
        else {
            vm.showNoListsFound(false);
        }
    };

    vm.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
        vm.getLists(newValue);
    });

    vm.getLists = function (recordsCenterName) {
        vm.listsAreLoading(true);
        vm.GetListsParameters.RecordsCenterName(recordsCenterName);
        var params = ko.toJSON(vm.GetListsParameters);

        services.postToServer(params, function (data) {
            ko.mapping.fromJS(data, {}, vm.CatalogItems);

            vm.evaluateShowNoListsMessage();

            vm.listsAreLoading(false);

            vm.catalogItemsSplit(Math.ceil(vm.CatalogItems().length / 2));

        }, vm.GetListsUrl());
    };

    vm.evaluateShowNoListsMessage();
    ko.applyBindings(vm);
});