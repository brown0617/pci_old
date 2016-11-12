'use strict';

function quoteData($http) {
	var uri = 'http://localhost:32150/api/quotes';

	//	fetch all quotes
	this.getAll = function() {
		return $http.get(uri);
	};

	//	fetch all active quotes
	this.getAllActive = function() {
		return $http.get(uri + '/active');
	};

	//	fetch a quote by id
	this.get = function(id) {
		return $http.get(uri + '/' + id);
	};

	//	fetch a quote by property id
	this.getByPropertyId = function(propertyId) {
		return $http.get(uri + '/property/' + propertyId);
	};

	//	fetch a new quote
	this.new = function() {
		return $http.get(uri + '/new');
	};

	//	save a quote
	this.save = function(quote) {
		return $http.put(uri, quote);
	};

	// close a quote
	this.close = function(quote, createOrder) {
		return $http.put(uri + '/close/?createOrder=' + createOrder, quote);
	};
};

quoteData.$inject = ['$http'];
app.service('quoteData', quoteData);