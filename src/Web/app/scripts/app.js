'use strict';

var app = angular.module('PCI', [
	'angular-storage',
	'ui.router',
	'ui.bootstrap',
	'ui.grid',
	'ui.grid.edit'
]);

app.config(function($stateProvider, $urlRouterProvider, $httpProvider) {
	$urlRouterProvider.otherwise('/pci/customer');

	$stateProvider
		.state('pci', {
			abstract: true,
			url: '/pci',
			templateUrl: '../main/main.html',
			controller: 'MainCtrl',
			controllerAs: 'main'
		}).state('pci.home', {
			url: '/home',
			templateUrl: '../views/home/home.html'
		}).state('pci.crm', {
			url: '/crm',
			templateUrl: '../views/crmDashboard.html',
			data: {
				module: 'CRM',
				tools: [{ name: 'search' }]
			}
		}).state('pci.customer', {
			url: '/customer',
			templateUrl: '../views/customer/customer.html',
			controller: 'CustomerCtrl',
			controllerAs: 'customer',
			data: {
				module: 'Customers',
				tools: [{ name: 'search' }]
			}
		}).state('pci.customerDetail', {
			url: '/customer/detail/:id',
			templateUrl: '../views/customer/customerDetail.html',
			resolve: {
				previousState: [
					'$state',
					function($state) {
						var currentStateData = {
							Name: $state.current.name,
							Params: $state.params,
							URL: $state.href($state.current.name, $state.params)
						};
						return currentStateData;
					}
				]
			},
			controller: 'CustomerDtlCtrl',
			controllerAs: 'customerDtl',
			data: {
				module: 'Customer Detail',
				tools: [{ name: 'search' }]
			}
		}).state('pci.quote', {
			url: '/quote',
			templateUrl: '../views/quote/quote.html',
			controller: 'QuoteCtrl',
			controllerAs: 'quote',
			data: {
				module: 'Quotes',
				tools: [{ name: 'search' }]
			}
		}).state('pci.quoteDetail', {
			url: '/quote/detail:id',
			templateUrl: '../views/quote/quoteDetail.html',
			resolve: {
				previousState: [
					'$state',
					function($state) {
						var currentStateData = {
							Name: $state.current.name,
							Params: $state.params,
							URL: $state.href($state.current.name, $state.params)
						};
						return currentStateData;
					}
				]
			},
			controller: 'QuoteDtlCtrl',
			controllerAs: 'quoteDtl',
			data: {
				module: 'Quote',
				tools: [{ name: 'search' }]
			}
		}).state('pci.property', {
			url: '/property',
			templateUrl: '../views/property/property.html',
			controller: 'PropertyCtrl',
			controllerAs: 'property',
			data: {
				module: 'Properties',
				tools: [{ name: 'search' }]
			}
		}).state('pci.propertyDetail', {
			url: '/property/detail/:id',
			templateUrl: '../views/property/propertyDetail.html',
			resolve: {
				previousState: [
					'$state',
					function ($state) {
						var currentStateData = {
							Name: $state.current.name,
							Params: $state.params,
							URL: $state.href($state.current.name, $state.params)
						};
						return currentStateData;
					}
				]
			},
			controller: 'PropertyDtlCtrl',
			controllerAs: 'propertyDtl',
			data: {
				module: 'Property Detail',
				tools: [{ name: 'search' }]
			}
		});

	$httpProvider.interceptors.push('apiInterceptor');
});

app.run(function($rootScope, $state) {
	$rootScope.$state = $state;
});