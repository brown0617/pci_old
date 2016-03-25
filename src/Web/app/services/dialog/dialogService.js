'use strict';

function dialogService($uibModal) {
	return {
		message: '',
		title: '',
		btnYesNo: false,
		btnOk: false,
		btnCancel: false,
		open: function () {
			var params = {
				message: this.message,
				title: this.title,
				btnYesNo: this.btnYesNo,
				btnOk: this.btnOk,
				btnCancel: this.btnCancel
			};

			var modalInstance = $uibModal.open({
				templateUrl: '../services/dialog/dialogServiceModal.html',
				backdrop: 'static',
				keyboard: false,
				windowClass: 'sm',
				controller: 'DialogServiceModalCtrl',
				controllerAs: 'ctrl',
				resolve: {
					params: function() {
						return params;
					}
				}
			});

			return modalInstance.result;
		}
	};
}

dialogService.$inject = ['$uibModal'];
app.service('dialogService', dialogService);