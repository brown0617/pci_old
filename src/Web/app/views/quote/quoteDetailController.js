'use strict';

function QuoteDtlCtrl($stateParams, $q, $filter, previousState, optionData, quoteData) {
	var self = this;

	self.previousState = previousState;

	var promises = [
		quoteData.get($stateParams.id),
		optionData.get('Season')
	];

	$q.all(promises).then(function(results) {
		self.quote = results[0].data;
		self.seasons = results[1].data;
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