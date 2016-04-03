'use strict';

function QuoteDtlCtrl($stateParams, $q, $filter, previousState, optionData, quoteData) {
	var self = this;

	self.previousState = previousState;

	var promises = [
		quoteData.get($stateParams.id),
		optionData.get('BillingDay'),
		optionData.get('Month'),
		optionData.get('QuoteStatus'),
		optionData.get('QuoteType'),
		optionData.get('Season')
	];

	$q.all(promises).then(function(results) {
		self.quote = results[0].data;
		self.billingDays = results[1].data;
		self.months = results[2].data;
		self.statuses = results[3].data;
		self.types = results[4].data;
		self.seasons = results[5].data;
	});

	this.cancel = function() {
		return quoteData.get($stateParams.id);
	};

	this.save = function() {
		return quoteData.save(self.quote);
	};

	this.getPreviousState = function() {
		return $q.when(self.previousState);
	};
}

QuoteDtlCtrl.$inject = ['$stateParams', '$q', '$filter', 'previousState', 'optionData', 'quoteData'];
app.controller('QuoteDtlCtrl', QuoteDtlCtrl);