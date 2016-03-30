'use strict';

function CustomerDtlCtrl($stateParams, $q, $filter, previousState, customerData, propertyData) {
	var self = this;

	self.previousState = previousState;
	self.searchText = '';

	var promises = [
		customerData.get($stateParams.id),
		propertyData.getByCustomerId($stateParams.id)
	];

	$q.all(promises).then(function(results) {
		self.customer = results[0].data;
		self.allProperties = results[1].data;
		self.properties = self.allProperties;
	});

	self.gridConfig = {
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

	this.getPreviousState = function() {
		return $q.when(self.previousState);
	};

	this.filterGridData = function () {
		self.properties = $filter('filter')(self.allProperties, self.searchText, undefined);
	};
}

CustomerDtlCtrl.$inject = ['$stateParams', '$q', '$filter', 'previousState', 'customerData', 'propertyData'];
app.controller('CustomerDtlCtrl', CustomerDtlCtrl);