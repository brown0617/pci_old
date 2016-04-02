'use strict';

function optionData($http) {
	var uri = 'http://localhost:32150/api/options';

	//	fetch a options by enum type name
	this.get = function(tEnum) {
		return $http.get(uri + '/' + tEnum);
	};
};

optionData.$inject = ['$http'];
app.service('optionData', optionData);