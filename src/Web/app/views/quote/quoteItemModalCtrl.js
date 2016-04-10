'use strict';

function QuoteItemModalCtrl($uibModalInstance, serviceData, item) {
	var self = this;

	serviceData.getAll().then(function(result) {
		self.services = result.data;
		self.item = item;
	});

	self.updatePrice = function() {
		self.item.ServiceUnitPrice = self.item.Service.Price;
		this.calcServicePrice();
	}

	self.calcServicePrice = function() {
		self.item.ServicePrice = self.item.ServiceUnitPrice * self.item.ServiceQuantity;
	}

	this.save = function() {
		$uibModalInstance.close(self.item);
	};

	this.cancel = function() {
		$uibModalInstance.dismiss();
	};
};

QuoteItemModalCtrl.$inject = ['$uibModalInstance', 'serviceData', 'item'];
app.controller('QuoteItemModalCtrl', QuoteItemModalCtrl);