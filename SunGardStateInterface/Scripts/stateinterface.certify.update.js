/*global ko*/

$(function () {

    var services = new myApp.services();
    var vm = new myApp.vm(initialData);

    vm.showTestHistory = ko.observable(false);
    vm.formsAreLoading = ko.observable(false);
    vm.showNoFormsFound = ko.observable(false);
    vm.showPassFailCommands = ko.observable(false);

    vm.toggleTestHistory = function (item) {
        vm.showTestHistory(!vm.showTestHistory());
    };

    vm.getForms = function () {
        vm.formsAreLoading(true);

        vm.CertificationParameters.RecordsCenterName(vm.RecordsCenterSelector.SelectedRecordsCenterName());
        var params = ko.toJSON(vm.CertificationParameters);

        services.postToServer(params, function (data) {
            ko.mapping.fromJS(data, {}, vm.Applications);

            if (vm.Applications().length === 0) {
                vm.showNoFormsFound(true);
            }
            else {
                vm.showNoFormsFound(false);
            }
            vm.formsAreLoading(false);
        }, vm.GetFormsUrl());
    };

    ko.applyBindings(vm);
});