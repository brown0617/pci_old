'use strict';

function CustomerDtlCtrl($stateParams, previousState, $q, customerData, propertyData) {
	var self = this;

	self.previousState = previousState;

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

	this.getPreviousState = function () {
		return $q.when(self.previousState);
	};
}

CustomerDtlCtrl.$inject = ['$stateParams', 'previousState', '$q', 'customerData', 'propertyData'];
app.controller('CustomerDtlCtrl', CustomerDtlCtrl);