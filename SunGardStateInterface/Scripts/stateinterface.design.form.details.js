﻿/*global ko*/

$(function () {

    var services = new myApp.services();

    var vm = new myApp.vm(initialData);

    vm.applicationAssociationModal = ko.observable();

    vm.updateApplications = function (item) {
        services.copyArray(vm.Applications, vm.ApplicationsForEdit);
        vm.applicationAssociationModal(item);
    };

    vm.saveApplications = function () {
        var param = {
            RecordsCenterId: vm.RecordsCenterId,
            FormId: vm.FormId,
            Applications: vm.ApplicationsForEdit
        };

        services.postToServer(ko.toJSON(param), function (data) {
            vm.Applications(data.Applications);
        }, vm.UpdateApplicationsAssociationUrl());
    };

    ko.applyBindings(vm);
});