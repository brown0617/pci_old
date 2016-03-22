'use strict';

function customerData($http) {
	var uri = 'http://localhost:32150/api/customers';

	//	fetch all customers
	this.getAll = function() {
		return $http.get(uri);
	};

	//	fetch a customer by id
	this.get = function(id) {
		return $http.get(uri + '/' + id);
	};
};

customerData.$inject = ['$http'];
angular
	.module('PCI')
	.service('customerData', customerData);