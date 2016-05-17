'use strict';

function workOrderData($http) {
	var uri = 'http://localhost:32150/api/workOrders';

	//	fetch all work orders
	this.getAll = function() {
		return $http.get(uri);
	};

	//	fetch a work order by id
	this.get = function(id) {
		return $http.get(uri + '/' + id);
	};


	//	save a work order
	this.save = function(quote) {
		return $http.put(uri, quote);
	};
};

workOrderData.$inject = ['$http'];
app.service('workOrderData', workOrderData);