$(function () {
    // Option 1: Mapping plugin with observables
    //var vm = new myApp.vm(initialData);
   
    // Option 2: Mapping plugin with no observables (Same performance as Option 1)
    //var mappingOptions = {
    //    'observe': [""]
    //}
    //var vm = ko.mapping.fromJS(initialData, mappingOptions);

    // Option 3: Hand rolled view model (Best performance)
    var vm = {
        Id: initialData.Id,
        RecordsCenterName: initialData.RecordsCenterName,
        ListName: initialData.ListName,
        LastUpdated: initialData.LastUpdated,
        OptionListTiers: initialData.OptionListTiers,
        OptionListItems: initialData.OptionListItems,
        CanDesignManage: initialData.CanDesignManage,
        FormFieldsUsing: initialData.FormFieldsUsing,
        DesignHomeUrl: initialData.DesignHomeUrl,
        ListsHomeUrl: initialData.ListsHomeUrl,
        ListHelpUrl: initialData.ListHelpUrl
    };

    ko.applyBindings(vm);
});