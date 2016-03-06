'use strict';

function apiInterceptor($rootScope) {
	var interceptor = this;

	interceptor.responseError = function(response) {
		$rootScope.$broadcast('Error', response.data);
		return response;
	};
};

angular
	.module('PCI')
	.service('apiInterceptor', apiInterceptor);