'use strict';

function CustomerCtrl(customerData) {
	var self = this;

	customerData.getAll().then(function(result) {
		self.customers = result.data;
	});

	self.gridCustomers = {
		data: 'customer.customers',
		multiSelect: false,
		columnDefs: [
			{ field: 'Name', displayName: 'Name' },
			{ field: 'BillingAddressStreet1', displayName: 'Address 1' },
			{ field: 'BillingAddressStreet2', displayName: 'Address 2' },
			{ field: 'BillingAddressCity', displayName: 'City' },
			{ field: 'BillingAddressState', displayName: 'State' },
			{ field: 'BillingAddressZip', displayName: 'Zip' },
			{ name: 'edit', displayName: '', cellTemplate: '<a class="btn" ui-sref="pci.customerDetail({id: row.entity.Id})"><i class="fa fa-pencil-square-o"></i></a>' }
		]
	};
}

CustomerCtrl.$inject = ['customerData'];
app.controller('CustomerCtrl', CustomerCtrl);