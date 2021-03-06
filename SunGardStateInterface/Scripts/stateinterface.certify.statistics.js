﻿$(function () {
    var vm = new myApp.vm(initialData);

    $(function () {
        $("#datepicker").datepicker();
    });
    vm.average = ko.observable();
    vm.date = ko.observable();
    vm.estimatedDate = ko.observable().extend({ initializeValidation: '', validateIsDate: '', validateIsFutureDate: '' });
    vm.estimatedAverage = ko.observable().extend({ initializeValidation: '', validateIsPostiveNumber: '' });
    vm.isCertification = ko.observable("true");
    vm.isAverageInput = ko.observable(false);

    vm.estimatedDate.subscribe(function (newValue) {
        vm.average(null);
    });

    vm.estimatedAverage.subscribe(function (newValue) {
        vm.date(null);
    });

    vm.isCertification.subscribe(function (newValue) {
        vm.date(null);
        vm.average(null);
    });

    vm.calculateDate = function () {
        if ((typeof vm.estimatedAverage() === "undefined") || vm.estimatedAverage() === '') {
            vm.estimatedAverage.hasError(true);
            vm.estimatedAverage.validationMessage('Must enter a value');
        }

        if (!vm.estimatedAverage.hasError()) {
            vm.isAverageInput(true);
            vm.calculationSendToServer();
        }
    }

    vm.calculateAverage = function () {
        if ((typeof vm.estimatedDate() === "undefined") || (vm.estimatedDate() === '')) {
            vm.estimatedDate.hasError(true);
            vm.estimatedDate.validationMessage('Must enter a value');
        }

        if (!vm.estimatedDate.hasError()) {
            vm.isAverageInput(false);
            vm.calculationSendToServer();
        }
    }

    vm.calculationSendToServer = function () {
        var count = 0;
        if (vm.isCertification() == "true") {
            count = initialData.Statistics.CountUnCertifiedTestCases + initialData.Statistics.CountUnVerifiedTestCases;
        } else {
            count = initialData.Statistics.CountUnVerifiedTestCases;
        }

        var param = {
            RecordsCenter: initialData.RecordsCenter.Name,
            CompletedDate: vm.estimatedDate(),
            Average: vm.estimatedAverage(),
            TestCases: count,
            IsAverageInput: vm.isAverageInput()
        };

        vm.services.postToServer(JSON.stringify(param), function (result) {
            if (vm.isAverageInput()) {
                vm.date(result);
            } else {
                vm.average(result);
            }
        }, vm.GetAverageUrl());
    };

    ko.applyBindings(vm);
});
