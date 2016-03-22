'use strict';

function PciContact(personData, contactService) {
	return {
		restrict: 'E',
		require: 'ngModel',
		scope: {
			label: '@'
		},
		template: '<div class="form-group">' +
			'<label class="control-label">{{label}}</label>' +
			'<div class="input-group"><select class="form-control" ng-model="ngModel" ng-options="contact.Id as contact.FullName for contact in contacts">'+
			'<option value="">Select a contact</option>' +
			'</select>' +
			'<div class="input-group-btn"><a class="btn" ng-click="editContact(ngModel)"><i class="fa fa-pencil-square-o fa-2x"></i></a>' +
			'</div></div>',
		link: function (scope, elem, attrs, ngModel) {
			personData.getAll().then(function (result) {
				scope.contacts = result.data;
			});

			scope.editContact = function (id) {
				//var id = ngModel.$viewValue;
				contactService.edit(id);
			};
		}
	};
};

angular
	.module('PCI')
	.directive('pciContact', PciContact);