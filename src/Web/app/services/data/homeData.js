'use strict';

function homeData($http) {
	var uri = 'http://localhost:32150/api/home';

	//	fetch sales summary by contract year
	this.getOrdersSummary = function(contractYear) {
		return $http.get(uri + '/salesSummary/' + contractYear);
	};
};

homeData.$inject = ['$http'];
app.service('homeData', homeData);