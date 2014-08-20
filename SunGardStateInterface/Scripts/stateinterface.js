/*global ko*/
var myApp = myApp || {};

$(function () {

    myApp.services = function () {
        var self = this;

        self.postToServer = function (data, callBack, url) {
            self.submitToServer('POST', data, callBack, url);
        };

        self.putToServer = function (data, callBack, url) {
            self.submitToServer('PUT', data, callBack, url);
        };

        self.deleteFromServer = function (data, callBack, url) {
            self.submitToServer('DELETE', data, callBack, url);
        };

        self.getFromServer = function (data, callBack, url) {
            self.submitToServer('GET', data, callBack, url);
        }

        self.submitToServer = function (verb, data, callBack, url) {
            $.ajax({
                url: url,
                type: verb,
                data: data,
                datatype: "json",
                contentType: "application/json charset=utf-8",
                success: function (result) {
                    callBack(result);
                },
                error: function (result) { alert("error"); }
            });
        };

        self.copyArray = function (fromArray, toArray) {
            toArray.splice(0, toArray().length);
            for (var i = 0; i < fromArray().length; i++) {
                var temp = ko.toJS(fromArray()[i]);
                var copy = ko.mapping.fromJS(temp);
                toArray.push(copy);
            }
        }

        $(".modal").on('shown.bs.modal', function () {
            $(this).find("[autofocus]:first").focus();
        });

    };

    myApp.vm = function (data) {
        var self = this;

        ko.bindingHandlers.bootstrapShowModal = {
            init: function (element, valueAccessor) {
                $(element).on('hidden', function () {
                    var value = valueAccessor();
                    value(false);
                });
                $(element).on('shown'), function () {
                    var value = valueAccessor();
                    value(true);
                };
            },
            update: function (element, valueAccessor) {
                var value = valueAccessor();
                if (ko.utils.unwrapObservable(value)) {
                    $(element).modal('show');
                    // this is to focus input field inside dialog
                    $("input", element).focus();
                } else {
                    $(element).modal('hide');
                }
            }
        };

        ko.extenders.validateIsPostiveNumber = function (target, overrideMessage) {
            //add some sub-observables to our observable
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();

            //define a function to do validation
            function validate(newValue) {
                var expression = /[^0-9.]/;
                target.hasError(expression.test(newValue));
                target.validationMessage(overrideMessage || "Input must be a positive number");
            }

            //initial validation - on page load
            //validate(target());

            //validate whenever the value changes
            target.subscribe(validate);

            //return the original observable
            return target;
        };

        ko.extenders.validateIsPostiveInteger = function (target, overrideMessage) {
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();

            function validate(newValue) {
                console.log(newValue + ':' + target.value);
                var expression = /[^0-9]/;
                target.hasError(expression.test(newValue));
                console.log(expression.test(newValue));
                target.validationMessage(overrideMessage || "Input must be a positive digit.");
            }

            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsMin = function (target, number, overrideMessage) {
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();
            target.minimumValue = ko.observable(parseInt(number));

            function validate(newValue) {
                target.hasError(parseInt(newValue) <= target.minimumValue());
                target.validationMessage(overrideMessage || "Input must be greater than " + target.minimumValue());
            }

            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsMax = function (target, number, overrideMessage) {
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();
            target.minimumValue = ko.observable(parseInt(number));

            function validate(newValue) {
                target.hasError(parseInt(newValue) >= target.minimumValue());
                target.validationMessage(overrideMessage || "Input must be less than " + target.minimumValue());
            }

            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsWholeNumberInRange = function (target, parameters) {
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();
            target.minimumValue = ko.observable(parseInt(parameters.minimumValue));
            target.maximumValue = ko.observable(parseInt(parameters.maximumValue));            

            function validate(newValue) {
                var expression = /[^0-9]/;
                if (!!newValue) {
                    if (!expression.test(newValue)) {
                        if (parseInt(newValue) >= target.minimumValue()) {
                            if (parseInt(newValue) > target.maximumValue()) {
                                target.hasError(true);
                                target.validationMessage("Input must be less than or equal to " + target.maximumValue());
                            }
                            else {
                                target.hasError(false);
                            }
                        }
                        else {
                            target.hasError(true);
                            target.validationMessage("Input must be greater than or equal to " + target.minimumValue());
                        }
                    }
                    else {
                        target.hasError(true);
                        target.validationMessage("Input must be a whole number");
                    }
                }
                else {
                    target.hasError(false);
                }
            }
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsDate = function (target, overrideMessage) {
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();

            function validate(newValue) {
                if (newValue != '') {
                    var isValidFormat = moment(newValue, 'MM/DD/YYYY', true).isValid();
                    target.hasError(!isValidFormat);
                    target.validationMessage(overrideMessage || "Input must be a date");
                } else {
                    target.hasError(newValue ? true : false);
                }
            }
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsFutureDate = function (target, overrideMessage) {
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();

            function validate(newValue) {
                if (moment(newValue, 'MM/DD/YYYY', true).isValid()) {
                    target.hasError(moment(newValue).isBefore(moment()));
                    target.validationMessage(overrideMessage || "Input must be a future date");
                }
            }
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateNonEmpty = function (target, overrideMessage) {
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();

            function validate(newValue) {
                target.hasError(newValue ? false : true);
                target.validationMessage(overrideMessage || "Input must not be blank");
            }
            validate(target());
            target.subscribe(validate);
            return target;
        };

        ko.mapping.fromJS(data, {}, this);

        if (typeof self.RecordsCenterSelector != 'undefined') {
            var services = new myApp.services();
            self.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
                var request = {
                    RecordsCenterName: newValue
                };
                var params = JSON.stringify(request);
                services.postToServer(params, function (data) {

                }, self.RecordsCenterSelector.SetRecordsCenterUrl());
            });
        }
    };
});