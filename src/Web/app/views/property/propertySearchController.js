'use strict';

function PropertySearchCtrl(propertyData, $filter) {
	var self = this;
	self.properties = {};

	propertyData.getAll().then(function(results) {
		self.allProperties = results.data;
		self.properties = self.allProperties;
	});

	self.gridConfig = {
		data: 'propertySearch.properties',
		multiSelect: false,
		columnDefs: [
			{ field: 'Name', displayName: '', cellTemplate: '<a ui-sref="pci.property({id: row.entity.Id})">{{ row.entity.Name }}</a>' },
			{ field: 'AddressStreet1', displayName: 'Street Address' },
			{ field: 'AddressCity', displayName: 'City' },
			{ field: 'AddressState', displayName: 'State' },
			{ field: 'AddressZip', displayName: 'Zip' }
		]
	};

	this.filterGridData = function () {
		self.properties = $filter('filter')(self.allProperties, self.searchText, undefined);
	};
}

PropertySearchCtrl.$inject = ['propertyData', '$filter'];
app.controller('PropertySearchCtrl', PropertySearchCtrl);