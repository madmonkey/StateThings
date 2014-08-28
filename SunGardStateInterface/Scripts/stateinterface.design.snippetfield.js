$(function () {
    var vm = new myApp.vm(initialData);

    vm.MaxIntegerValue = 2147483647;
    vm.snippetFieldModal = ko.observable();
    vm.snippetPropertiesModal = ko.observable();
    vm.confirmDeleteModal = ko.observable();
    vm.SelectedField.TagName.extend({ initializeValidation: '', validateNonEmpty: ''});
    vm.SnippetForEdit.Name.extend({ initializeValidation: '', validateNonEmpty: '*' });
    vm.SnippetForEdit.Description.extend({ initializeValidation: '', validateNonEmpty: '*' });
    vm.SelectedField.Length.extend({ initializeValidation: '', validateIsPostiveInteger: '', validateIsMin: 0, validateIsMax: vm.MaxIntegerValue });
    vm.SelectedField.TrimInputToLength.extend({ initializeValidation: '', validateIsPostiveInteger: '', validateIsMin: 0, validateIsMax: vm.MaxIntegerValue });
    vm.SelectedField.Frequency.extend({ initializeValidation: '', validateIsPostiveInteger: '', validateIsMin: 0, validateIsMax: vm.MaxIntegerValue });
    vm.SelectedField.PadCharacterDec.extend({ initializeValidation: '', validateIsPostiveInteger: '', validateIsMin: 0, validateIsMax: 255 });

    vm.propertiesAreLoading = ko.observable(false);

    vm.isSnippetFieldError = ko.computed(function () {
        return vm.SelectedField.TagName.hasError() || vm.SelectedField.Length.hasError() ||
            vm.SelectedField.TrimInputToLength.hasError() || vm.SelectedField.Frequency.hasError() || vm.SelectedField.PadCharacterDec.hasError();
    });

    vm.isSnippetError = ko.computed(function () {
        return vm.SnippetForEdit.Name.hasError() || vm.SnippetForEdit.Description.hasError();
    });

    vm.isList = ko.observable(false);
    vm.isTransform = ko.observable(false);
    vm.isUpper = ko.observable(false);
    vm.isCarriageReturn = ko.observable(false);
    vm.isLength = ko.observable(false);
    vm.isFrequency = ko.observable(false);
    vm.isSeparator = ko.observable(false);
    vm.isDefaultValue = ko.observable(false);
    vm.isTrimInput = ko.observable(false);
    vm.isPad = ko.observable(false);
    
    vm.isTransformLabel = ko.computed(function () {
        return (vm.isList() || vm.isTransform());
    });

    ko.subscribable.fn.subscribeChanged = function (callback) {
        var oldValue;
        this.subscribe(function (_oldValue) {
            oldValue = _oldValue;
        }, this, 'beforeChange');

        this.subscribe(function (newValue) {
            callback(newValue, oldValue);
        });
    };

    var frequencySubscription = vm.SelectedField.Frequency.subscribeChanged(function (newValue, oldValue) {
        if (newValue > 1) {
            vm.isSeparator(true);
        }
        else {
            vm.isSeparator(false);
            vm.SelectedField.Separator('');
        }
    });

    var typeSubscription = vm.SelectedField.Field.subscribeChanged(function (newValue, oldValue) {
        switch (newValue) {
            case "Date": {
                vm.isList(true);
                vm.isTransform(false);
                vm.isUpper(false);
                vm.isCarriageReturn(false);
                vm.isLength(false);
                vm.isFrequency(true);
                vm.isSeparator(true);
                vm.isDefaultValue(false);
                vm.isTrimInput(false);
                vm.isPad(false);
                vm.services.copyArray(vm.AvailableDateFormats, vm.AvailableOptions);
                break;
            }
            case "Name": {
                vm.isList(true);
                vm.isTransform(false);
                vm.isUpper(true);
                vm.isCarriageReturn(false);
                vm.isLength(true);
                vm.isFrequency(true);
                vm.isSeparator(true);
                vm.isDefaultValue(false);
                vm.isTrimInput(true);
                vm.isPad(true);
                vm.services.copyArray(vm.AvailableNameFormats, vm.AvailableOptions);
                break;
            }
            case "Numeric": {
                vm.isList(false);
                vm.isTransform(false);
                vm.isUpper(false);
                vm.isCarriageReturn(false);
                vm.isLength(true);
                vm.isFrequency(true);
                vm.isSeparator(true);
                vm.isDefaultValue(true);
                vm.isTrimInput(false);
                vm.isPad(true);
                break;
            }
            case "SSN": {
                vm.isList(false);
                vm.isTransform(false);
                vm.isUpper(false);
                vm.isCarriageReturn(false);
                vm.isLength(false);
                vm.isFrequency(true);
                vm.isSeparator(true);
                vm.isDefaultValue(false);
                vm.isTrimInput(false);
                vm.isPad(true);
                break;
            }
            case "Counter": {
                vm.isList(false);
                vm.isTransform(false);
                vm.isUpper(false);
                vm.isCarriageReturn(false);
                vm.isLength(true);
                vm.isFrequency(false);
                vm.isSeparator(false);
                vm.isDefaultValue(false);
                vm.isTrimInput(false);
                vm.isPad(true);
                break;
            }
            case "Convert": {
                vm.isList(false);
                vm.isTransform(true);
                if ((oldValue === 'Date') || (oldValue === 'Name')) {
                    vm.SelectedField.TransformFormat('');
                }
                vm.isUpper(true);
                vm.isCarriageReturn(true);
                vm.isLength(true);
                vm.isFrequency(true);
                vm.isSeparator(true);
                vm.isDefaultValue(true);
                vm.isTrimInput(false);
                vm.isPad(true);
                break;
            }
            case "Text": {
                vm.isList(false);
                vm.isTransform(false);
                vm.isUpper(true);
                vm.isCarriageReturn(true);
                vm.isLength(true);
                vm.isFrequency(true);
                vm.isSeparator(true);
                vm.isDefaultValue(true);
                vm.isTrimInput(true);
                vm.isPad(true);
                break;
            }
            case "SystemDate": {
                vm.isList(false);
                vm.isTransform(true);
                if ((oldValue === 'Date') || (oldValue === 'Name')) {
                    vm.SelectedField.TransformFormat('');
                }
                vm.isUpper(false);
                vm.isCarriageReturn(false);
                vm.isLength(true);
                vm.isFrequency(false);
                vm.isSeparator(false);
                vm.isDefaultValue(false);
                vm.isTrimInput(false);
                vm.isPad(false);
                break;
            }
            case "StateProvinceRegion": {
                vm.isList(false);
                vm.isTransform(true);
                if ((oldValue === 'Date') || (oldValue === 'Name')) {
                    vm.SelectedField.TransformFormat('');
                }
                vm.isUpper(true);
                vm.isCarriageReturn(false);
                vm.isLength(true);
                vm.isFrequency(true);
                vm.isSeparator(true);
                vm.isDefaultValue(true);
                vm.isTrimInput(false);
                vm.isPad(true);
                break;
            }
            default: { // set to Text
                vm.isList(false);
                vm.isTransform(false);
                vm.isUpper(true);
                vm.isCarriageReturn(true);
                vm.isLength(true);
                vm.isFrequency(true);
                vm.isSeparator(true);
                vm.isDefaultValue(true);
                vm.isTrimInput(true);
                vm.isPad(true);
                break;
            }
        }
    });

    vm.finalizeSelectedField = function () {
        switch (vm.SelectedField.Field()) {
            case "Date": {
                vm.SelectedField.MakeUpperCase(null);
                vm.SelectedField.AcceptCarriageReturns(null);
                vm.SelectedField.Length(null);
                vm.SelectedField.DefaultValue('');
                vm.SelectedField.TrimInputToLength(null);
                vm.SelectedField.PadCharacterDec(null);
                break;
            }
            case "Name": {
                vm.SelectedField.AcceptCarriageReturns(null);
                vm.SelectedField.DefaultValue('');
                break;
            }
            case "Numeric": {
                vm.SelectedField.MakeUpperCase(null);
                vm.SelectedField.AcceptCarriageReturns(null);
                vm.SelectedField.TrimInputToLength(null);
                break;
            }
            case "SSN": {
                vm.SelectedField.TransformFormat('');
                vm.SelectedField.MakeUpperCase(null);
                vm.SelectedField.AcceptCarriageReturns(null);
                vm.SelectedField.Length(null);
                vm.SelectedField.DefaultValue('');
                vm.SelectedField.TrimInputToLength(null);
                break;
            }
            case "Counter": {
                vm.SelectedField.TransformFormat('');
                vm.SelectedField.MakeUpperCase(null);
                vm.SelectedField.AcceptCarriageReturns(null);
                vm.SelectedField.Frequency(null);
                vm.SelectedField.Separator('');
                vm.SelectedField.DefaultValue('');
                vm.SelectedField.TrimInputToLength(null);
                break;
            }
            case "Convert": {
                vm.SelectedField.TrimInputToLength(null);
                break;
            }
            case "Text": {
                vm.SelectedField.TransformFormat('');
                break;
            }
            case "SystemDate": {
                vm.SelectedField.MakeUpperCase(null);
                vm.SelectedField.AcceptCarriageReturns(null);
                vm.SelectedField.Frequency(null);
                vm.SelectedField.Separator('');
                vm.SelectedField.DefaultValue('');
                vm.SelectedField.TrimInputToLength(null);
                vm.SelectedField.PadCharacterDec(null);
                break;
            }
            case "StateProvinceRegion": {
                vm.SelectedField.AcceptCarriageReturns(null);
                vm.SelectedField.TrimInputToLength(null);
                break;
            }
            default: { // default to Text
                vm.SelectedField.TransformFormat('');
                break;
            }
        }
    };

    vm.copyToSelectedField = function (item) {
        vm.transform(item.TransformFormat());
        vm.SelectedField.RecordsCenterName(vm.RecordsCenterName()),
        vm.SelectedField.SnippetId(vm.Id()),
        vm.SelectedField.Id(item.Id());
        vm.SelectedField.TagName(item.TagName());
        vm.SelectedField.Prefix(item.Prefix());
        vm.SelectedField.Suffix(item.Suffix());
        vm.SelectedField.ToolTip(item.ToolTip());
        vm.SelectedField.MakeUpperCase(item.MakeUpperCase());
        vm.SelectedField.Length(item.Length());
        vm.SelectedField.PadCharacterDec(item.PadCharacterDec());
        vm.SelectedField.TrimInputToLength(item.TrimInputToLength());
        vm.SelectedField.DefaultValue(item.DefaultValue());
        vm.SelectedField.Field(item.Field());
        vm.SelectedField.Frequency(item.Frequency());
        vm.SelectedField.Separator(item.Separator());
        vm.SelectedField.AcceptCarriageReturns(item.AcceptCarriageReturns());
        if (vm.SelectedField.TrimInputToLength() == null) {
            vm.SelectedField.TrimInputToLength('');
        }
        if (vm.SelectedField.PadCharacterDec() == null) {
            vm.SelectedField.PadCharacterDec('');
        }
    };

    vm.copyNewToSelectedField = function () {
        vm.transform('');
        vm.SelectedField.RecordsCenterName(vm.RecordsCenterName()),
        vm.SelectedField.SnippetId(vm.Id()),
        vm.SelectedField.Id(0);
        vm.SelectedField.TagName('');
        vm.SelectedField.Prefix('');
        vm.SelectedField.Suffix('');
        vm.SelectedField.ToolTip('');
        vm.SelectedField.MakeUpperCase(true);
        vm.SelectedField.Length('');
        vm.SelectedField.PadCharacterDec('');
        vm.SelectedField.TrimInputToLength('');
        vm.SelectedField.DefaultValue('');
        vm.SelectedField.TransformFormat('');
        vm.SelectedField.Field('Text');
        vm.SelectedField.Frequency('');
        vm.SelectedField.Separator('');
        vm.SelectedField.AcceptCarriageReturns(false);
        vm.isUpper(true);
        vm.isCarriageReturn(true);
        vm.isLength(true);
        vm.isFrequency(true);
        vm.isSeparator(true);
        vm.isDefaultValue(true);
        vm.isTrimInput(true);
        vm.isPad(true);
    };

    vm.copyToSnippetForEdit = function (item) {
        vm.SnippetForEdit.RecordsCenterName(item.RecordsCenterName);
        vm.SnippetForEdit.Id(item.Id());
        vm.SnippetForEdit.Name(item.TokenName());
        vm.SnippetForEdit.Description(item.Description());
        vm.SnippetForEdit.Definition(item.TransactionDefinition());
        vm.SnippetForEdit.Criteria(item.Criteria());
        vm.SnippetForEdit.IncludePrefixAndSuffix(item.IncludePrefixAndSuffix());
    };

    vm.transform = ko.observable('');
    vm.setSelectedOption = function () {
        vm.SelectedField.TransformFormat(vm.transform());
    };

    vm.indexId = ko.observable();
    vm.checkForDuplication = function () {
        for (var i = 0; i < vm.TransactionSnippetFields().length ; i++) {
            var name = vm.TransactionSnippetFields()[i].TagName();
            if (vm.TransactionSnippetFields()[i].TagName() === vm.SelectedField.TagName()) {
                if (vm.TransactionSnippetFields()[i].Id != vm.indexId()) {
                    vm.SelectedField.TagName.hasError(true);
                    vm.SelectedField.TagName.validationMessage('Name already exists')
                }
            }
        }
    };

    var duplicateSubscription = vm.SelectedField.TagName.subscribe(function (newValue) {
        vm.checkForDuplication();
    });

    vm.fieldNameWarning = ko.observable(false);
    var warningSubscription = vm.SelectedField.TagName.subscribe(function (newValue) {
        vm.fieldNameWarning(true);
    });

    vm.snippetNameWarning = ko.observable(false);
    vm.SnippetForEdit.Name.subscribe(function (newValue) {
        vm.snippetNameWarning(true);
    });

    vm.editField = function (item) {
        vm.services.isError(false);
        vm.indexId(item.Id);
        vm.fieldNameWarning(false);
        warningSubscription.dispose();
        duplicateSubscription.dispose();
        vm.copyNewToSelectedField();
        vm.copyToSelectedField(item);
        vm.snippetFieldModal(item);
        warningSubscription = vm.SelectedField.TagName.subscribe(function (newValue) {
            vm.fieldNameWarning(true);
        });
        duplicateSubscription = vm.SelectedField.TagName.subscribe(function (newValue) {
            vm.checkForDuplication();
        });
    };

    vm.addField = function () {
        vm.services.isError(false);
        vm.fieldNameWarning(false);
        warningSubscription.dispose();
        vm.copyNewToSelectedField();
        vm.snippetFieldModal(vm.SelectedField);
    };

    vm.deleteField = function (item) {
        vm.services.isError(false);
        vm.copyToSelectedField(item);
        vm.confirmDeleteModal(item);
    };

    vm.editSnippet = function () {
        vm.services.isError(false);
        vm.copyToSnippetForEdit(vm);
        vm.snippetPropertiesModal(vm);
        vm.snippetNameWarning(false);
    };

    vm.updateSnippetField = function () {
        vm.finalizeSelectedField();
        vm.services.postToServer(ko.toJSON(vm.SelectedField), function (data) {
            if (!vm.services.isError()) {
                $(".modal").modal('hide');
            }
            ko.mapping.fromJS(data, {}, vm);
        }, vm.UpdateSnippetFieldUrl());
    };

    vm.deleteSnippetField = function () {
        vm.services.postToServer(ko.toJSON(vm.SelectedField), function (data) {
            if (!vm.services.isError()) {
                $(".modal").modal('hide');
            }
            ko.mapping.fromJS(data, {}, vm);
        }, vm.DeleteSnippetFieldUrl());
    };

    vm.updateSnippet = function () {
        vm.propertiesAreLoading(true);

        vm.services.postToServer(ko.toJSON(vm.SnippetForEdit), function (data) {
            if (!vm.services.isError()) {
                $(".modal").modal('hide');
            }
            ko.mapping.fromJS(data, {}, vm);
        }, vm.UpdateSnippetUrl());

        vm.propertiesAreLoading(false);
    };

    ko.applyBindings(vm);
});