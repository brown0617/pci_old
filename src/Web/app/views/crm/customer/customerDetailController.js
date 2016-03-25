'use strict';

function CustomerDtlCtrl(customerData, $stateParams, previousState) {
	var self = this;

	self.customer = {};

	customerData.get($stateParams.id).then(function(result) {
		self.customer = result.data;
	});

	this.cancel = function() {
		return customerData.get($stateParams.id);
	};

	this.save = function() {
		return customerData.save(self.customer);
	};

	this.previousState = previousState;
}

CustomerDtlCtrl.$inject = ['customerData', '$stateParams', 'previousState'];
angular
	.module('PCI')
	.controller('CustomerDtlCtrl', CustomerDtlCtrl);