'use strict';

function CustomerDtlCtrl($stateParams, previousState, $q, customerData, propertyData) {
	var self = this;

	var promises = [
		customerData.get($stateParams.id),
		propertyData.getByCustomerId($stateParams.id)
	];

	$q.all(promises).then(function(results) {
		self.customer = results[0].data;
		self.properties = results[1].data;
	});

	self.gridProperties = {
		data: 'customerDtl.properties',
		multiSelect: false,
		columnDefs: [
			{ field: 'Name', displayName: 'Name' },
			{ field: 'AddressStreet1', displayName: 'Street Address' },
			{ field: 'AddressCity', displayName: 'City' },
			{ field: 'AddressState', displayName: 'State' },
			{ field: 'AddressZip', displayName: 'Zip' },
			{ name: 'edit', displayName: '', cellTemplate: '<a class="btn" ui-sref="pci.customerDetail({id: row.entity.Id})"><i class="fa fa-pencil-square-o"></i></a>' }
		]
	};

	this.cancel = function() {
		return customerData.get($stateParams.id);
	};

	this.save = function() {
		return customerData.save(self.customer);
	};

	this.previousState = previousState;
}

CustomerDtlCtrl.$inject = ['$stateParams', 'previousState', '$q', 'customerData', 'propertyData'];
angular
	.module('PCI')
	.controller('CustomerDtlCtrl', CustomerDtlCtrl);