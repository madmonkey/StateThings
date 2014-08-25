$(function () {
    var vm = new myApp.vm(initialData);

    vm.showTestHistory = ko.observable(false);
    vm.formIsLoading = ko.observable(false);
    vm.showPassFailCommands = ko.observable(true);
    vm.queryUpdateTestCaseModal = ko.observable();
    vm.queryResetTestCaseModal = ko.observable();

    vm.parseName = function (name) {
        return name.replace(/ /g, "-");
    };

    vm.goToTab = function () {
        if (location.hash) {
            var hash = location.hash.split('tc_');
            $('#appTab a[href="' + hash[0] + '"]').tab('show');
        }
    }

    vm.toggleTestHistory = function (item) {
        vm.formIsLoading(true);
        vm.showTestHistory(!vm.showTestHistory());
        vm.formIsLoading(false);
    };

    vm.queryPassTestCase = function (item) {
        vm.copyToSelectedTestCase(item, true);
        vm.queryUpdateTestCaseModal(item);
    };

    vm.queryFailTestCase = function (item) {
        vm.copyToSelectedTestCase(item, false);
        vm.queryUpdateTestCaseModal(item);
    };

    vm.queryResetTestCase = function (item) {
        vm.copyToSelectedTestCase(item, null);
        vm.queryUpdateTestCaseModal(item);
    };

    vm.copyToSelectedTestCase = function (item, hasPassed) {
        vm.SelectedTestCase.CurrentStage(item.CurrentStage());
        vm.SelectedTestCase.FormId(item.FormId());
        vm.SelectedTestCase.CriteriaId(item.CriteriaId());
        vm.SelectedTestCase.TestCaseId(item.TestCaseId());
        vm.SelectedTestCase.Note('');
        vm.SelectedTestCase.HasPassed(hasPassed);
    };

    vm.applyformQAStatusUpdate = function (data) {
        ko.mapping.fromJS(data, {}, vm.RequestForm.QAStatus);
    };

    vm.activeTabId = ko.observable();

    vm.applyTestCaseUpdate = function (data) {
        ko.mapping.fromJS(data, {}, vm.RequestForm);
        vm.hookupEventHandlers();
        $('#appTab a[href="' + vm.activeTabId() + '"]').tab('show')
    };

    vm.hookupEventHandlers = function () {
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            vm.activeTabId(e.target.hash);
        });
    };

    vm.updateTestCase = function () {
        vm.formIsLoading(true);
        var params = ko.toJSON(vm.SelectedTestCase);
        if (vm.SelectedTestCase.HasPassed() === null) {
            vm.services.postToServer(params, function (data) {
                vm.applyTestCaseUpdate(data);
                vm.formIsLoading(false);
            }, vm.ResetTestCaseUrl());
        }
        else {
            vm.services.postToServer(params, function (data) {
                vm.applyTestCaseUpdate(data);
                vm.formIsLoading(false);
            }, vm.UpdateTestCaseUrl());
        }
    };

    vm.getCurrentFormQAState = function () {
        formIsLoading(true);
        var request = {
            RecordsCenterId: vm.RecordsCenterId(),
            FormId: vm.SelectedTestCase.FormId()
        };

        var params = JSON.stringify(request)
        vm.services.postToServer(params, function (data) {
            vm.applyformQAStatusUpdate(data);
            vm.formIsLoading(false);
        }, vm.GetFormQAStateUrl());
    };

    ko.applyBindings(vm);

    vm.hookupEventHandlers();
    vm.goToTab();
});