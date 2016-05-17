'use strict';

function WorkOrderCtrl(workOrderData, $filter) {
	var self = this;
	self.workOrders = {};

	workOrderData.getAll().then(function(results) {
		self.allWorkOrders = results.data;
		self.workOrders = self.allWorkOrders;
	});

	self.gridConfig = {
		data: 'workOrder.workOrders',
		multiSelect: false,
		columnDefs: [
			{ field: 'Name', displayName: 'Name' },
			{ field: 'AddressStreet1', displayName: 'Street Address' },
			{ field: 'AddressCity', displayName: 'City' },
			{ field: 'AddressState', displayName: 'State' },
			{ field: 'AddressZip', displayName: 'Zip' },
			{ name: 'edit', displayName: '', cellTemplate: '<a class="btn" ui-sref="pci.workOrderDetail({id: row.entity.Id})"><i class="fa fa-pencil-square-o"></i></a>' }
		]
	};

	this.filterGridData = function () {
		self.workOrders = $filter('filter')(self.allWorkOrders, self.searchText, undefined);
	};
}

WorkOrderCtrl.$inject = ['workOrderData', '$filter'];
app.controller('WorkOrderCtrl', WorkOrderCtrl);