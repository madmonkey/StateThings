/*global ko*/

$(function () {
    var services = new myApp.services();
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

    vm.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
        vm.getSnippets(newValue);
    });

    vm.getSnippets = function (recordsCenterName) {
        vm.snippetsAreLoading(true);
        vm.SnippetsParameter.RecordsCenterName(recordsCenterName);
        var params = ko.toJSON(vm.SnippetsParameter);

        services.postToServer(params, function (data) {
            ko.mapping.fromJS(data, {}, vm.CatalogItems);


            vm.snippetsAreLoading(false);

            vm.catalogItemsSplit(Math.ceil(vm.CatalogItems().length / 2));

        }, vm.GetSnippetsUrl());
    };

    vm.snippetModal = ko.observable(false);
    vm.SnippetRequest.Name = ko.observable().extend({initializeValidation: '', validateNonEmpty: '*' });
    vm.SnippetRequest.Description = ko.observable().extend({ initializeValidation: '', validateNonEmpty: '*' });

    vm.isSnippetError = ko.computed(function () {
        return vm.SnippetRequest.Name.hasError() || vm.SnippetRequest.Description.hasError();
    });

    vm.initializeSnippet = function() {
        vm.SnippetRequest.Name('');
        vm.SnippetRequest.Description('');
    };

    vm.addSnippet = function() {
        vm.initializeSnippet();
        vm.snippetModal(vm);
    };

    vm.createSnippet = function () {
        var newTab = window.open('', '_blank');
        vm.SnippetRequest.RecordsCenterName(vm.RecordsCenterSelector.SelectedRecordsCenterName());
        services.postToServer(ko.toJSON(vm.SnippetRequest), function(data) {
            newTab.location = data.SnippetDetailsUrl;
        }, vm.CreateSnippetUrl()); 
    };

    vm.evaluateShowNoSnippetsMessage();
    ko.applyBindings(vm);
});