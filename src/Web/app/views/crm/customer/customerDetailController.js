'use strict';

function CustomerDtlCtrl(customerData, $stateParams) {
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
}

CustomerDtlCtrl.$inject = ['customerData', '$stateParams'];
angular
	.module('PCI')
	.controller('CustomerDtlCtrl', CustomerDtlCtrl);