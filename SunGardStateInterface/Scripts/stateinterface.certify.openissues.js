/*global ko*/

$(function () {
    var services = new myApp.services();

    var vm = new myApp.vm(initialData);
    ko.applyBindings(vm);
});