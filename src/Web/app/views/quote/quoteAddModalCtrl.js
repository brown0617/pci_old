'use strict';

function QuoteAddModalCtrl($q, $uibModalInstance, optionData, propertyData, customerData, quoteToAdd) {
	var self = this;
	self.quote = quoteToAdd;

	var promises = [customerData.getAll(), optionData.getQuoteTypeOptions(), optionData.getSeasonOptions()];
	$q.all(promises).then(function(results) {
		self.customers = results[0].data;
		self.quoteTypes = results[1].data;
		self.seasons = results[2].data;
	});

	// aynch call to get materials list
	self.getPropertiesByName = function (name) {
		return propertyData.getPropertiesByName(name);
	};

	self.propertySelected = function(property) {
		if (property) {
			self.quote.PropertyId = property.Id;
		} else {
			// new property

		}
	};

	this.save = function() {
		$uibModalInstance.close(self.quote);
	};

	this.cancel = function() {
		$uibModalInstance.dismiss();
	};
};

QuoteAddModalCtrl.$inject = ['$q', '$uibModalInstance', 'optionData', 'propertyData', 'customerData', 'quoteToAdd'];
app.controller('QuoteAddModalCtrl', QuoteAddModalCtrl);