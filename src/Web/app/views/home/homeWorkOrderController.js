﻿'use strict';

function HomeWorkOrderCtrl(workOrderData, $filter) {
	var self = this;
	self.workOrders = {};

	workOrderData.getAllActive().then(function(results) {
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

HomeWorkOrderCtrl.$inject = ['workOrderData', '$filter'];
app.controller('HomeWorkOrderCtrl', HomeWorkOrderCtrl);