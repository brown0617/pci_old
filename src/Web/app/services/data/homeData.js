'use strict';

function homeData($http) {
	var uri = 'http://localhost:32150/api/home';

	//	fetch orders summary by contract year
	this.getOrdersSummary = function(contractYear) {
		return $http.get(uri + '/ordersSummary/' + contractYear);
	};
};

homeData.$inject = ['$http'];
app.service('homeData', homeData);