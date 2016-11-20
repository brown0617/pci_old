'use strict';

function WorkOrderDtlCtrl($stateParams, $filter, dialogService, previousState, workOrderData) {
	var self = this;
	self.previousState = previousState;

	workOrderData.get($stateParams.id)
		.then(function(results) {
			self.workOrder = results.data;
			self.workOrderItems = $filter('orderBy')(self.workOrder.Items, 'Service.Name');
		});

	self.gridConfig = {
		data: 'workOrderDtl.workOrderItems',
		multiSelect: false,
		columnDefs: [
			{ field: 'Service.Name', displayName: 'Service', width: '20%' },
			{ field: 'Description', displayName: 'Description' },
			{ field: 'ServiceQuantity', displayName: 'Man Hours', width: '10%', cellClass: 'text-right' },
			{
				name: 'edit',
				displayName: '',
				cellTemplate:
					'<a class="btn btn-sm" ng-click="grid.appScope.quoteDtl.editItem(row.entity)"><i class="fa fa-pencil-square-o"></i></a><a class="btn btn-sm" ng-click="grid.appScope.quoteDtl.deleteItem(row.entity)"><i class="fa fa-trash-o"></i></a>',
				width: '7%',
				enableSorting: false,
				enableHiding: false
			}
		]
	};

	this.cancel = function() {

		return workOrderData.get($stateParams.id)
			.then(function(response) {
				self.quote = response.data;
				self.quoteItems = $filter('orderBy')(self.quote.Items, 'Service.Name');
			});
	};

	this.save = function() {
		self.workOrder.items = self.workOrderItems;
		return workOrderData.save(self.workOrder);
	};

	this.getPreviousState = function() {
		return $q.when(self.previousState);
	};

}

WorkOrderDtlCtrl.$inject = [
	'$stateParams', '$filter', 'dialogService', 'previousState', 'workOrderData'
];
app.controller('WorkOrderDtlCtrl', WorkOrderDtlCtrl);