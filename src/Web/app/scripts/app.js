﻿'use strict';

var app = angular.module('PCI', [
	'angular-storage',
	'ui.router',
	'ui.bootstrap'
]);

app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
	$urlRouterProvider.otherwise('/pci/home');

	$stateProvider
		.state('pci', {
			abstract: true,
			url: '/pci',
			templateUrl: '../views/main/main.html',
			controller: 'MainCtrl',
			controllerAs: 'main'
		})
		.state('pci.home', {
			url: '/home',
			templateUrl: '../views/home/home.html'
		})
		.state('pci.crm', {
			url: '/crm',
			templateUrl: '../views/crm/crmDashboard.html',
			data: {
				module: 'CRM',
				tools: [{ name: 'search' }]
			}
		})
		.state('pci.customer', {
			url: '/crm/customer',
			templateUrl: '../views/crm/customer/customerMain.html',
			controller: 'CustomerCtrl',
			controllerAs: 'customer',
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