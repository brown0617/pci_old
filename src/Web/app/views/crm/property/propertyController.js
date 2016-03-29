'use strict';

function PropertyCtrl(propertyData) {
	var self = this;
	self.properties = {};

	propertyData.getAll().then(function(results) {
		self.properties = results.data;
	});

	self.gridProperties = {
		data: 'property.properties',
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
}

PropertyCtrl.$inject = ['propertyData'];
app.controller('PropertyCtrl', PropertyCtrl);