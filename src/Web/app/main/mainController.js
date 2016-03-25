'use strict';

function MainCtrl($rootScope) {
	var self = this;
	var defaultAlert = { type: '', text: '', show: false, dismissOnTimeout: 0 };

	function buildAlert(msg) {
		if (!msg.notify) {
			return defaultAlert;
		}

		return {
			type: msg.type,
			text: msg.text,
			show: true,
			dismissOnTimeout: msg.autoDismiss ? 0 : 3000
		};
	}

	this.closeAlert = function() {
		self.alert = defaultAlert;
	};

	$rootScope.$on('ApiResponse', function(event, responseMsg) {
		self.alert = buildAlert(responseMsg);
	});
};

MainCtrl.$inject = ['$rootScope'];
app.controller('MainCtrl', MainCtrl);