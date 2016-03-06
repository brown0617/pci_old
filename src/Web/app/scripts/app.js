'use strict';

var app = angular.module('PCI', [
	'angular-storage',
	'ui.router'
]);

app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
	$urlRouterProvider.otherwise('/main/home');

	$stateProvider
		.state('main', {
			abstract: true,
			url: '/main',
			templateUrl: '../views/main/main.html',
			controller: 'MainCtrl',
			controllerAs: 'ctrl'
		})
		.state('main.home', {
			url: '/home',
			templateUrl: '../views/home/home.html'
		})
		.state('main.crm', {
			url: '/crm',
			templateUrl: '../views/crm/crmDashboard.html',
			data: {
				module: 'CRM',
				tools: [{ name: 'search' }]
			}
		})
		.state('main.customer', {
			url: '/crm/customer',
			templateUrl: '../views/crm/customer/customerMain.html',
			controller: 'CustomerCtrl',
			controllerAs: 'ctrl',
			data: {
				module: 'Customers',
				tools: [{ name: 'search' }]
			}
		});

	$httpProvider.interceptors.push('apiInterceptor');
});

app.run(function($rootScope, $state) {
	$rootScope.$state = $state;
});