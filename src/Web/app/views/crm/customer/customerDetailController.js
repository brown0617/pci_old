'use strict';

function CustomerDtlCtrl(customerData, $stateParams) {
	var self = this;

	customerData.get($stateParams.id).then(function(result) {
		self.customer = result.data;
	});
}

CustomerDtlCtrl.$inject = ['customerData', '$stateParams'];
angular
	.module('PCI')
	.controller('CustomerDtlCtrl', CustomerDtlCtrl);