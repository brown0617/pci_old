'use strict';

function PropertyCtrl($state, $stateParams, $q, $filter, previousState, propertyData) {
	var self = this;
	self.previousState = previousState;

	propertyData.get($stateParams.id)
		.then(function(results) {
			self.property = results.data;
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

PropertyCtrl.$inject = ['$state', '$stateParams', '$q', '$filter', 'previousState', 'propertyData'];
app.controller('PropertyCtrl', PropertyCtrl);