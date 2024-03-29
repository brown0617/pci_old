﻿'use strict';

var app = angular.module('PCI',
[
	'angular-storage',
	'ui.router',
	'ui.bootstrap',
	'ui.grid',
	'ui.grid.edit',
	'ui.select',
	'ngSanitize'
]);

app.config(function($stateProvider, $urlRouterProvider, $httpProvider) {
	$urlRouterProvider.otherwise('/pci/home');

	$stateProvider
		.state('pci',
		{
			abstract: true,
			url: '/pci',
			templateUrl: '../main/main.html',
			controller: 'MainCtrl',
			controllerAs: 'main'
		})
		.state('pci.home',
			{
				url: '/home',
				views: {
					'': {
						templateUrl: '../views/home/home.html',
						controller: 'HomeCtrl',
						controllerAs: 'home'
					},
					'quotes@pci.home': {
						templateUrl: '../views/quote/quote.html',
						controller: 'HomeQuoteCtrl',
						controllerAs: 'quote'
					},
					'workOrders@pci.home': {
						templateUrl: '../views/workOrder/workOrder.html',
						controller: 'HomeWorkOrderCtrl',
						controllerAs: 'workOrder'
					}
				}
			}
		)
		.state('pci.crm',
		{
			url: '/crm',
			templateUrl: '../views/crmDashboard.html',
			data: {
				module: 'CRM',
				tools: [{ name: 'search' }]
			}
		})
		.state('pci.customer',
		{
			url: '/customer',
			templateUrl: '../views/customer/customer.html',
			controller: 'CustomerCtrl',
			controllerAs: 'customer',
			data: {
				module: 'Customers',
				tools: [{ name: 'search' }]
			}
		})
		.state('pci.customerDetail',
		{
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
		})
		.state('pci.quote',
		{
			url: '/quote',
			templateUrl: '../views/quote/quote.html',
			controller: 'QuoteCtrl',
			controllerAs: 'quote',
			data: {
				module: 'Quotes',
				tools: [{ name: 'search' }]
			}
		})
		.state('pci.quoteDetail',
		{
			url: '/quote/detail/:id',
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
		})
		.state('pci.property',
		{
			url: '/property/:id',
			views: {
				'': {
					templateUrl: '../views/property/property.html',
					controller: 'PropertyCtrl',
					controllerAs: 'property'
				},
				'quotes@pci.property': {
					templateUrl: '../views/quote/quote.html',
					controller: 'QuoteCtrl',
					controllerAs: 'quote'
				}
			},
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
			onEnter: [
				'$state', '$stateParams', function($state, $stateParams) {
					if ($state.transition && !$stateParams.id) {
						$state.go('pci.propertySearch');
					}
				}
			]
		})
		.state('pci.propertySearch',
		{
			url: '/propertySearch',
			templateUrl: '../views/property/propertySearch.html',

			controller: 'PropertySearchCtrl',
			controllerAs: 'propertySearch',
			data: {
				module: 'Property Search',
				tools: [{ name: 'search' }]
			}
		})
		.state('pci.workOrder',
		{
			url: '/workOrder',
			templateUrl: '../views/workOrder/workOrder.html',
			controller: 'WorkOrderCtrl',
			controllerAs: 'workOrder',
			data: {
				module: 'Work Orders',
				tools: [{ name: 'search' }]
			}
		})
		.state('pci.workOrderDetail',
		{
			url: '/workOrder/detail/:id',
			templateUrl: '../views/workOrder/workOrderDetail.html',
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
			controller: 'WorkOrderDtlCtrl',
			controllerAs: 'workOrderDtl'
		});

	$httpProvider.interceptors.push('apiInterceptor');
});

app.run(function($rootScope, $state) {
	$rootScope.$state = $state;
});