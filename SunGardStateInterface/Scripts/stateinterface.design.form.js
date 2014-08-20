/*global ko*/

$(function () {

    var services = new myApp.services();

    var vm = new myApp.vm(initialData);

    vm.formsAreLoading = ko.observable(false);
    vm.showNoFormsFound = ko.observable(false);

    vm.evaluateShowNoFormsMessage = function () {
        if (vm.RequestForms().length === 0) {
            vm.showNoFormsFound(true);
        }
        else {
            vm.showNoFormsFound(false);
        }
    };

    vm.requestFormsSplit = ko.observable(Math.ceil(vm.RequestForms().length / 2));

    vm.evaluateShowNoFormsMessage();

    vm.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
        vm.getForms(newValue);
    });

    vm.getForms = function (recordsCenterName) {
        vm.formsAreLoading(true);

        vm.GetFormsParameters.RecordsCenterName(recordsCenterName);
        var params = ko.toJSON(vm.GetFormsParameters);

        services.postToServer(params, function (data) {
            ko.mapping.fromJS(data, {}, vm.RequestForms);

            vm.evaluateShowNoFormsMessage();

            vm.formsAreLoading(false);

            vm.requestFormsSplit(Math.ceil(vm.RequestForms().length / 2));

        }, vm.GetFormsUrl());
    };

    ko.applyBindings(vm);
});