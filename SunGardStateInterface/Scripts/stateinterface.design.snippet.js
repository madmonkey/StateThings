/*global ko*/

$(function () {
    var vm = new myApp.vm(initialData);

    vm.snippetsAreLoading = ko.observable(false);
    vm.showNoSnippetFound = ko.observable(false);
    vm.catalogItemsSplit = ko.observable(Math.ceil(vm.CatalogItems().length / 2));

    vm.evaluateShowNoSnippetsMessage = function () {
        if (vm.CatalogItems().length === 0) {
            vm.showNoSnippetFound(true);
        }
        else {
            vm.showNoSnippetFound(false);
        }
    };

    vm.snippetModal = ko.observable(false);
    vm.SnippetParameters.Name = ko.observable().extend({ initializeValidation: '', validateNonEmpty: '*' });
    vm.SnippetParameters.Description = ko.observable().extend({ initializeValidation: '', validateNonEmpty: '*' });

    vm.isSnippetError = ko.computed(function () {
        return vm.SnippetParameters.Name.hasError() || vm.SnippetParameters.Description.hasError();
    });

    vm.initializeSnippet = function() {
        vm.SnippetParameters.Name('');
        vm.SnippetParameters.Description('');
    };

    vm.addSnippet = function() {
        vm.initializeSnippet();
        vm.snippetModal(vm);
    };

    vm.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
        vm.getSnippets(newValue);
    });

    vm.getSnippets = function (recordsCenterName) {
        vm.snippetsAreLoading(true);
        vm.SnippetsParameters.RecordsCenterName(recordsCenterName);
        var params = ko.toJSON(vm.SnippetsParameters);
        vm.services.postToServer(params, function (data) {
            ko.mapping.fromJS(data, {}, vm.CatalogItems);
            vm.snippetsAreLoading(false);
            vm.catalogItemsSplit(Math.ceil(vm.CatalogItems().length / 2));
        }, vm.GetSnippetsUrl());
    };

    vm.isError = ko.observable(false);
    vm.errorMessage = ko.observable('');
    vm.createSnippet = function () {
        vm.isError(false);
        vm.errorMessage('');
        //var newTab = window.open('', '_blank');
        vm.SnippetParameters.RecordsCenterName(vm.RecordsCenterSelector.SelectedRecordsCenterName());
        vm.services.postToServer(
            ko.toJSON(vm.SnippetParameters),
            function (data) {
                if (!vm.services.isError()) {
                    //newTab.location = data.DetailsUrl;
                    window.location = data.DetailsUrl;
                }
            },
            vm.CreateSnippetUrl());
    };

    vm.evaluateShowNoSnippetsMessage();
    ko.applyBindings(vm);
});