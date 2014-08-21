$(function () {
    var services = new myApp.services();
    var vm = new myApp.vm(initialData);

    vm.snippetsAreLoading = ko.observable(false);
    vm.showNoSnippetFound = ko.observable(false);

    vm.evaluateShowNoSnippetsMessage = function () {
        if (vm.TransactionSnippets().length === 0) {
            vm.showNoSnippetFound(true);
        }
        else {
            vm.showNoSnippetFound(false);
        }
    };

    vm.evaluateShowNoSnippetsMessage();

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

    vm.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
        vm.getSnippets(newValue);
    });

    vm.getSnippets = function (recordsCenterName) {
        vm.snippetsAreLoading(true);

        vm.SnippetsRequest.RecordsCenterName(recordsCenterName);
        var param = ko.toJSON(vm.SnippetsRequest);

        services.postToServer(param, function (data) {
            ko.mapping.fromJS(data, {}, vm.TransactionSnippets);

            vm.evaluateShowNoSnippetsMessage();

            vm.snippetsAreLoading(false);

        }, vm.GetSnippetsUrl());
    };

    vm.createSnippet = function() {
        var newTab = window.open('', '_blank');
        vm.SnippetRequest.RecordsCenterName(vm.RecordsCenterSelector.SelectedRecordsCenterName());
        services.postToServer(ko.toJSON(vm.SnippetRequest), function(data) {
            newTab.location = data.SnippetDetailsUrl;
        }, vm.CreateSnippetUrl());
    };

    ko.applyBindings(vm);
});