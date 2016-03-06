'use strict';

function CustomerCtrl(customerData) {
	var self = this;

	customerData.getAll().then(function(result) {
		self.customers = result.data;
	});
}

angular
	.module('PCI')
	.controller('CustomerCtrl', CustomerCtrl);