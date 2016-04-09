'use strict';

function QuoteItemModalCtrl($uibModalInstance, item) {
	var self = this;
	self.item = item;

	this.save = function() {
		$uibModalInstance.close(self.item);
	};

	this.cancel = function() {
		$uibModalInstance.dismiss();
	};
};

QuoteItemModalCtrl.$inject = ['$uibModalInstance', 'item'];
app.controller('QuoteItemModalCtrl', QuoteItemModalCtrl);