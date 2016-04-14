'use strict';

function PropertyDtlCtrl($stateParams, $q, $filter, previousState, propertyData) {
	var self = this;
	self.previousState = previousState;

	var promises = [
		propertyData.get($stateParams.id)
	];

	$q.all(promises).then(function(results) {
		self.property = results[0].data;
	});

	this.cancel = function() {
		return propertyData.get($stateParams.id);
	};

	this.save = function() {
		return propertyData.save(self.property);
	};

	this.getPreviousState = function() {
		return $q.when(self.previousState);
	};

}

PropertyDtlCtrl.$inject = ['$stateParams', '$q', '$filter', 'previousState', 'propertyData'];
app.controller('PropertyDtlCtrl', PropertyDtlCtrl);