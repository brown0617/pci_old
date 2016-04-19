'use strict';

function optionData($http) {
	var uri = 'http://localhost:32150/api/options';

	//	fetch BillingDay options
	this.getBillingDayOptions = function() {
		return $http.get(uri + '/billingDay', { cache: true });
	};
	//	fetch Billing Method options
	this.getBillingMethodOptions = function() {
		return $http.get(uri + '/billingMethod', { cache: true });
	};
	//	fetch Month options
	this.getMonthOptions = function() {
		return $http.get(uri + '/month', { cache: true });
	};
	//	fetch QuoteStatus options
	this.getQuoteStatusOptions = function() {
		return $http.get(uri + '/quoteStatus', { cache: true });
	};
	//	fetch QuoteType options
	this.getQuoteTypeOptions = function() {
		return $http.get(uri + '/quoteType', { cache: true });
	};
	//	fetch Season options
	this.getSeasonOptions = function() {
		return $http.get(uri + '/season', { cache: true });
	};
	//	fetch Service Frequency options
	this.getServiceFrequencyOptions = function() {
		return $http.get(uri + '/serviceFrequency', { cache: true });
	};
};

optionData.$inject = ['$http'];
app.service('optionData', optionData);