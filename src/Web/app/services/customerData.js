'use strict';

function customerData($http) {
	var resourcePath = 'http://localhost:32150/api/customers';

	//	fetch all customers
	this.getAll = function() {
		return $http.get(resourcePath);
	};
};

angular
	.module('PCI')
	.service('customerData', customerData);