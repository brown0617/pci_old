'use strict';

function HomeWorkOrderCtrl(workOrderData, $filter) {
	var self = this;
	self.workOrders = {};

	workOrderData.getAllActive()
		.then(function(results) {
			self.allWorkOrders = results.data;
			self.workOrders = self.allWorkOrders;
		});

	self.gridConfig = {
		data: 'workOrder.workOrders',
		multiSelect: false,
		columnDefs: [
			{
				field: 'OrderItem.Order.Property.Name',
				displayName: 'Property Name',
				width: '40%'
			},
			{
				field: 'OrderItem.Service.Name',
				displayName: 'Service',
				cellTemplate:
					'<a ui-sref="pci.workOrderDetail({id: row.entity.Id})">{{ COL_FIELD }}</a>'
			},
			{ field: 'OrderItem.ServiceDeadline', displayName: 'Deadline' },
			{ field: 'OrderItem.ScheduledStart', displayName: 'Scheduled Start' },
			{ field: 'OrderItem.ScheduledCompletion', displayName: 'Scheduled Completion' }
		]
	};

	this.filterGridData = function() {
		self.workOrders = $filter('filter')(self.allWorkOrders, self.searchText, undefined);
	};
}

HomeWorkOrderCtrl.$inject = ['workOrderData', '$filter'];
app.controller('HomeWorkOrderCtrl', HomeWorkOrderCtrl);