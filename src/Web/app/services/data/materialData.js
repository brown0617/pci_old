'use strict';

function materialData($http) {
	var uri = 'http://localhost:32150/api/materials';

	//	fetch all services
	this.getMaterialsByName = function(name) {
		return $http.get(uri + '/' + name).then(function(result) {
			return result.data;
		});
	};
};

materialData.$inject = ['$http'];
app.service('materialData', materialData);