'use strict';

function serviceData($http) {
	var uri = 'http://localhost:32150/api/services';

	//	fetch all services
	this.getAll = function() {
		return $http.get(uri);
	};
};

serviceData.$inject = ['$http'];
app.service('serviceData', serviceData);