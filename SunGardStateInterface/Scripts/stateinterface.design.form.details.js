$(function () {
    var vm = new myApp.vm(initialData);
    
    vm.applicationAssociationModal = ko.observable();
    vm.categoryAssociationModal = ko.observable();
    vm.categoriesItemsSplit = ko.observable(Math.ceil(vm.Categories().length / 2));
    vm.areSelectedApplications = ko.observable(false);
    vm.areSelectedCategories = ko.observable(false)

    vm.updateApplications = function (item) {
        vm.services.copyArray(vm.Applications, vm.ApplicationsForEdit);
        vm.applicationAssociationModal(item);
    };

    vm.updateCategories = function (item) {
        vm.services.copyArray(vm.Categories, vm.CategoriesForEdit);
        vm.categoryAssociationModal(item);
    };

    vm.saveApplications = function () {
        var param = {
            RecordsCenterName: vm.RecordsCenterName,
            FormId: vm.FormId,
            Applications: vm.ApplicationsForEdit
        };

        vm.services.postToServer(ko.toJSON(param), function (data) {
            ko.mapping.fromJS(data.Applications, {}, vm.Applications);
            vm.setAreSelectedApplications();
        }, vm.UpdateApplicationsAssociationUrl());
    };

    vm.saveCategories = function () {
        var param = {
            RecordsCenterName: vm.RecordsCenterName,
            FormId: vm.FormId,
            Categories: vm.CategoriesForEdit
        };

        vm.services.postToServer(ko.toJSON(param), function (data) {
            ko.mapping.fromJS(data.Categories, {}, vm.Categories);
            vm.setAreSelectedCategories();
        }, vm.UpdateCategoriesAssociationUrl());
    };

    vm.setAreSelectedApplications = function () {
        var areSelected = false;
        vm.Applications().forEach(function (app) {
            if (app.IsSelected() === true) {
                areSelected = true;
            }
        });

        vm.areSelectedApplications(areSelected);
    };

    vm.setAreSelectedCategories = function () {
        var areSelected = false;

        vm.Categories().forEach(function (app) {
            if (app.IsSelected() === true) {
                areSelected = true;
            }
        });

        vm.areSelectedCategories(areSelected);
    };

    vm.setAreSelectedApplications();
    vm.setAreSelectedCategories();
    ko.applyBindings(vm);
});