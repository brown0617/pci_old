'use strict';

function propertyData($http) {
	var uri = 'http://localhost:32150/api/properties';

	//	fetch all properties
	this.getAll = function() {
		return $http.get(uri);
	};

	//	fetch a property by id
	this.get = function (id) {
		return $http.get(uri + '/' + id);
	};

	//	fetch a property by customer id
	this.getByCustomerId = function (customerId) {
		return $http.get(uri + '/customer/' + customerId);
	};

	//	fetch a new property
	this.new = function() {
		return $http.get(uri + '/new');
	};

	//	save a property
	this.save = function(property) {
		return $http.put(uri, property);
	};
};

propertyData.$inject = ['$http'];
app.service('propertyData', propertyData);