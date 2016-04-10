'use strict';

function optionData($http) {
	var uri = 'http://localhost:32150/api/options';

	//	fetch BillingDay options
	this.getBillingDayOptions = function() {
		return $http.get(uri + '/billingDay');
	};
	//	fetch Month options
	this.getMonthOptions = function() {
		return $http.get(uri + '/month');
	};
	//	fetch QuoteStatus options
	this.getQuoteStatusOptions = function() {
		return $http.get(uri + '/quoteStatus');
	};
	//	fetch QuoteType options
	this.getQuoteTypeOptions = function() {
		return $http.get(uri + '/quoteType');
	};
	//	fetch Season options
	this.getSeasonOptions = function() {
		return $http.get(uri + '/season');
	};
};

optionData.$inject = ['$http'];
app.service('optionData', optionData);