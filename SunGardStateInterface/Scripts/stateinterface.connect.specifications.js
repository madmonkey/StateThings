$(function () {
    var vm = new myApp.vm(initialData);

    vm.formsAreLoading = ko.observable(false);
    vm.showNoFormsFound = ko.observable(false);

    vm.getForms = function (data) {
        vm.formsAreLoading(true);
        var params = ko.toJSON(vm.FormsRequestParameters);
        vm.services.postToServer(params, function (data) {
            ko.mapping.fromJS(data, {}, vm.Categories);

            if (vm.Categories().length === 0) {
                vm.showNoFormsFound(true);
            }
            else {
                vm.showNoFormsFound(false);
            }
            vm.formsAreLoading(false);
        }, 'Specifications/GetForms');
    };

    ko.applyBindings(vm);
});