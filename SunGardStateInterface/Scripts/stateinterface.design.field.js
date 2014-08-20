/*global ko*/

$(function () {
    var services = new myApp.services();
    var vm = new myApp.vm(initialData);

    vm.fieldsAreLoading = ko.observable(false);
    vm.showNoFieldsFound = ko.observable(false);

    vm.evaluateShowNoFieldsMessage = function () {
        if (vm.Fields().length === 0) {
            vm.showNoFieldsFound(true);
        }
        else {
            vm.showNoFieldsFound(false);
        }
    };

    vm.fieldsSplit = ko.observable(Math.ceil(vm.Fields().length / 2));

    vm.evaluateShowNoFieldsMessage();

    vm.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
        vm.getFields(newValue);
    });

    vm.getFields = function (recordsCenterName) {
        vm.fieldsAreLoading(true);
        vm.GetFieldsParameters.RecordsCenterName(recordsCenterName);
        services.postToServer(ko.toJSON(vm.GetFieldsParameters),
            function (data) {
                ko.mapping.fromJS(data, {}, vm.Fields);

                vm.evaluateShowNoFieldsMessage();

                vm.fieldsAreLoading(false);

                vm.fieldsSplit(Math.ceil(vm.Fields().length / 2));

            }, vm.GetFieldsUrl());
    };

    ko.applyBindings(vm);
});