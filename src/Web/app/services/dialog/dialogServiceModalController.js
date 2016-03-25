'use strict';

function DialogServiceModalCtrl($uibModalInstance, params) {
	var self = this;

	self.params = params;

	this.Respond = function (response) {
		$uibModalInstance.close(response);
	};
};

DialogServiceModalCtrl.$inject = ['$uibModalInstance', 'params'];
app.controller('DialogServiceModalCtrl', DialogServiceModalCtrl);