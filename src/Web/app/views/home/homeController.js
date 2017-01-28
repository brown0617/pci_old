'use strict';

function HomeCtrl($state, $stateParams, homeData) {
	var self = this;

	self.contractYear = new Date().getFullYear().toString();
	// TODO:  remove next line for prod deployment
	self.contractYear = "2016";

	homeData.getOrdersSummary(self.contractYear)
		.then(function(results) {
			self.ordersSummary = results.data;
		});
}

HomeCtrl.$inject = ['$state', '$stateParams', 'homeData'];
app.controller('HomeCtrl', HomeCtrl);