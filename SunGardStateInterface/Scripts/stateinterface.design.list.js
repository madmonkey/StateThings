/*global ko*/

$(function () {

    var services = new myApp.services();

    var vm = new myApp.vm(initialData);
    vm.listsAreLoading = ko.observable(false);
    vm.showNoListsFound = ko.observable(false);

    vm.evaluateShowNoListsMessage = function () {
        if (vm.Lists().length === 0) {
            vm.showNoListsFound(true);
        }
        else {
            vm.showNoListsFound(false);
        }
    };

    vm.evaluateShowNoListsMessage();
    
    vm.listsSplit = ko.observable(Math.ceil(vm.Lists().length / 2));
    
    vm.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
        vm.getLists(newValue);
    });

    vm.getLists = function (recordsCenterName) {
        vm.listsAreLoading(true);

        vm.GetListsParameters.RecordsCenterName(recordsCenterName);

        var params = ko.toJSON(vm.GetListsParameters);

        services.postToServer(params, function (data) {
            ko.mapping.fromJS(data, {}, vm.Lists);

            vm.evaluateShowNoListsMessage();

            vm.listsAreLoading(false);

            vm.listsSplit(Math.ceil(vm.Lists().length / 2));

        }, vm.GetListsUrl());
    };

    ko.applyBindings(vm);

});