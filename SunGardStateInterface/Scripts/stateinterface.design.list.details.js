$(function () {
    // Option 1: Mapping plugin with observables
    var vm = new myApp.vm('');

    // Option 2: Mapping plugin with no observables (Same performance as Option 1)
    //var mappingOptions = {
    //    'observe': [""]
    //}
    //var vm = ko.mapping.fromJS(initialData, mappingOptions);

    // Option 3: Hand rolled view model (Best performance)
    vm.Id = initialData.Id;
    vm.RecordsCenterName = initialData.RecordsCenterName;
    vm.ListName = initialData.ListName;
    vm.LastUpdated = initialData.LastUpdated;
    vm.OptionListTiers = initialData.OptionListTiers;
    vm.OptionListItems = initialData.OptionListItems;
    vm.CanDesignManage = initialData.CanDesignManage;
    vm.FormFieldsUsing = initialData.FormFieldsUsing;
    vm.DesignHomeUrl = initialData.DesignHomeUrl;
    vm.ListsHomeUrl = initialData.ListsHomeUrl;
    vm.ListHelpUrl = initialData.ListHelpUrl;

    ko.applyBindings(vm);
});