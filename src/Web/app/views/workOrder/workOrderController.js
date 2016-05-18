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
			{ field: 'OrderItem.Order.Property.Name', displayName: 'Property Name' },
			{ field: 'OrderItem.Service.Name', displayName: 'Service' },
			{ field: 'OrderItem.ServiceDeadline', displayName: 'Deadline' },
			{ field: 'OrderItem.ScheduledStart', displayName: 'Scheduled Start' },
			{ field: 'OrderItem.ScheduledCompletion', displayName: 'Scheduled Completion' },
			{ name: 'edit', displayName: '', width: '8%', cellTemplate: '<a class="btn btn-sm" ui-sref="pci.workOrderDetail({id: row.entity.Id})"><i class="fa fa-pencil-square-o"></i></a><a class="btn btn-sm" ui-sref="pci.workOrderDetail({id: row.entity.Id})"><i class="fa fa-clock-o"></i></a>' }
		]
	};

	this.filterGridData = function () {
		self.workOrders = $filter('filter')(self.allWorkOrders, self.searchText, undefined);
	};
}

WorkOrderCtrl.$inject = ['workOrderData', '$filter'];
app.controller('WorkOrderCtrl', WorkOrderCtrl);