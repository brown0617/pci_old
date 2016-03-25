'use strict';

function apiInterceptor($rootScope) {
	var interceptor = this;
	var responseMsg = { type: '', text: '', notify: false, autoDismiss: false };

	// handle errors from api
	interceptor.responseError = function (response) {
		responseMsg.type = 'danger';

		if (response.config.method === 'GET') {
			responseMsg.text = 'Unable to retrieve data.';
		} else {
			responseMsg.text = 'Unable to save data.';
		}
		responseMsg.notify = true;
		responseMsg.autoDismiss = false;

		$rootScope.$broadcast('ApiResponse', responseMsg);
		return response;
	};

	// handle successful api calls
	interceptor.response = function(response) {
		if (response.config.method === 'GET') {
			return response;
		}
		responseMsg.type = 'success';
		responseMsg.text = 'Data saved successfully!';
		responseMsg.notify = true;
		responseMsg.autoDismiss = true;

		$rootScope.$broadcast('ApiResponse', responseMsg);
		return response;
	};
};

app.service('apiInterceptor', apiInterceptor);