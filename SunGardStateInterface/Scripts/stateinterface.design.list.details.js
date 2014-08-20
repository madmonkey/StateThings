/*global ko*/

$(function () {

    var services = new myApp.services();

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
        RecordsCenterId: initialData.RecordsCenterId,
        ListName: initialData.ListName,
        Created: initialData.Created,
        Updated: initialData.Updated,
        OptionListTiers: initialData.OptionListTiers,
        OptionListItems: initialData.OptionListItems,
        CanDesignManage: initialData.CanDesignManage,
        FormFieldsUsing: initialData.FormFieldsUsing
    };

    ko.applyBindings(vm);

});