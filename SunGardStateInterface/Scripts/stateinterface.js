﻿/*global ko*/
var myApp = myApp || {};

$(function () {
    myApp.vm = function (data) {
        var self = this;
        self.services = new function () {
            var self = this;
            self.errorModal = ko.observable(false);
            self.isError = ko.observable(false);
            self.errorMessage = ko.observable('');
            self.isSystemError = ko.observable(false);
            self.systemErrorMessage = ko.observable('');
            
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
                self.isError(false);
                self.errorMessage('');
                self.isSystemError(false);
                self.systemErrorMessage('');
                $.ajax({
                    url: url,
                    type: verb,
                    data: data,
                    datatype: "json",
                    contentType: "application/json charset=utf-8",
                    success: function (data) {
                        var message = '';
                        self.isError(false);
                        self.errorMessage('');
                        for (var i = 0; i < data.Information.length; i++) {
                            if (data.Information[i].IsError) {
                                message += data.Information[i].Message;
                                message += "<br>";
                                self.isError(true);
                            }
                        }
                        if (self.isError) {
                            self.errorMessage(message);
                        }
                        callBack(data.Result);
                    },
                    error: function (error) {                                                
                        var errorObject = ko.utils.parseJson(error.statusText);
                        var message = '';
                        self.isSystemError(false);
                        self.systemErrorMessage('');
                        for (var i = 0; i < errorObject.Information.length; i++) {
                            if (errorObject.Information[i].IsError) {
                                message += errorObject.Information[i].Message;
                                message += "<br>";
                                self.isSystemError(true);
                            }
                        }
                        if (self.isSystemError) {
                            self.systemErrorMessage(message);
                            self.errorModal(self);
                        }
                    }
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

        ko.extenders.initializeValidation = function (target) {
            target.hasError = ko.observable(false);
            target.validationMessage = ko.observable('');

            function validate(newValue) {
                target.hasError(false);
                target.validationMessage('');
            }
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsPostiveNumber = function (target, overrideMessage) {
            function validate(newValue) {
                var expression = /[^0-9.]/;
                target.hasError(expression.test(newValue));
                target.validationMessage(overrideMessage || "Input must be a positive number");
            }
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsPostiveInteger = function (target, overrideMessage) {
            function validate(newValue) {
                var expression = /[^0-9]/;
                if (!target.hasError()) {
                    target.hasError(expression.test(newValue));
                    target.validationMessage(overrideMessage || "Input must be a positive digit.");
                }
            }
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsDate = function (target, overrideMessage) {
            function validate(newValue) {
                if (!target.hasError()) {
                    if (newValue != '') {
                        var isValidFormat = moment(newValue, 'MM/DD/YYYY', true).isValid();
                        target.hasError(!isValidFormat);
                        target.validationMessage(overrideMessage || "Input must be a date");
                    } else {
                        target.hasError(newValue ? true : false);
                    }
                }
            }
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsFutureDate = function (target, overrideMessage) {
            function validate(newValue) {
                if (!target.hasError()) {
                    if (moment(newValue, 'MM/DD/YYYY', true).isValid()) {
                        target.hasError(moment(newValue).isBefore(moment()));
                        target.validationMessage(overrideMessage || "Input must be a future date");
                    }
                }
            }
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateNonEmpty = function (target, overrideMessage) {
            function validate(newValue) {
                if (!target.hasError()) {
                    target.hasError(newValue ? false : true);
                    target.validationMessage(overrideMessage || "This field is required");
                }
            }
            validate(target());
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsMin = function (target, number, overrideMessage) {
            target.minimumValue = ko.observable(parseInt(number));

            function validate(newValue) {
                if (!target.hasError()) {
                    target.hasError(parseInt(newValue) < target.minimumValue());
                    target.validationMessage(overrideMessage || "Input is less than " + target.minimumValue());
                }
            }
            target.subscribe(validate);
            return target;
        };

        ko.extenders.validateIsMax = function (target, number, overrideMessage) {
            target.maximumValue = ko.observable(parseInt(number));

            function validate(newValue) {
                if (!target.hasError()) {
                    target.hasError(parseInt(newValue) > target.maximumValue());
                    target.validationMessage(overrideMessage || "Input is greater than " + target.maximumValue());
                }
            }
            target.subscribe(validate);
            return target;
        };

        ko.mapping.fromJS(data, {}, this);

        if (typeof self.RecordsCenterSelector != 'undefined') {
            self.RecordsCenterSelector.SelectedRecordsCenterName.subscribe(function (newValue) {
                var request = {
                    RecordsCenterName: newValue
                };
                var params = JSON.stringify(request);
                self.services.postToServer(params, function (data) {

                }, self.RecordsCenterSelector.SetRecordsCenterUrl());
            });
        }
    };
});