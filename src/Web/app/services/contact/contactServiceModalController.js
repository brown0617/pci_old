'use strict';

function ContactServiceModalCtrl($uibModalInstance, contact) {
	var self = this;
	self.contact = contact;

	this.save = function() {
		$uibModalInstance.close(self.contact);
	};

	this.cancel = function() {
		$uibModalInstance.dismiss();
	};
};

ContactServiceModalCtrl.$inject = ['$uibModalInstance', 'contact'];
app.controller('ContactServiceModalCtrl', ContactServiceModalCtrl);