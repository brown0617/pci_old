'use strict';

function personData($http) {
	var uri = 'http://localhost:32150/api/people';

	//	fetch all people
	this.getAll = function() {
		return $http.get(uri);
	};

	//	fetch a person by id
	this.get = function(id) {
		return $http.get(uri + '/' + id);
	};

	//	fetch an person
	this.new = function() {
		return $http.get(uri + '/new');
	};

	//	save a person
	this.save = function(person) {
		return $http.put(uri, person);
	};
};

personData.$inject = ['$http'];
app.service('personData', personData);