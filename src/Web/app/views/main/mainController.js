'use strict';

function MainCtrl($rootScope) {

	this.userName = 'Example user';
	this.helloText = 'Welcome to PCI';
	this.descriptionText = 'It is an application skeleton for a typical AngularJS web app. You can use it to quickly bootstrap your angular webapp projects and dev environment for these projects.';

	$rootScope.$on('Error', function (event, data) {
		this.errorMessage = data;
	});
};

angular
	.module('PCI')
	.controller('MainCtrl', MainCtrl)