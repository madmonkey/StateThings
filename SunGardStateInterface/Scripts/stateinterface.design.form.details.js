$(function () {
    var vm = new myApp.vm(initialData);
    
    vm.applicationAssociationModal = ko.observable();

    vm.updateApplications = function (item) {
        vm.services.copyArray(vm.Applications, vm.ApplicationsForEdit);
        vm.applicationAssociationModal(item);
    };

    vm.saveApplications = function () {
        var param = {
            RecordsCenterName: vm.RecordsCenterName,
            FormId: vm.FormId,
            Applications: vm.ApplicationsForEdit
        };

        vm.services.postToServer(ko.toJSON(param), function (data) {
            vm.Applications(data.Applications);
        }, vm.UpdateApplicationsAssociationUrl());
    };

    ko.applyBindings(vm);
});