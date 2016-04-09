'use strict';

function QuoteDtlCtrl($stateParams, $q, $filter, $uibModal, previousState, optionData, quoteData) {
	var self = this;

	self.previousState = previousState;

	function getModalInstance(item) {
		return $uibModal.open({
			templateUrl: '../views/quote/quoteItemModal.html',
			backdrop: 'static',
			size: 'lg',
			controller: 'QuoteItemModalCtrl',
			controllerAs: 'ctrl',
			resolve: {
				item: function () {
					return item;
				}
			}
		});
	};

	var promises = [
		quoteData.get($stateParams.id),
		optionData.get('BillingDay'),
		optionData.get('Month'),
		optionData.get('QuoteStatus'),
		optionData.get('QuoteType'),
		optionData.get('Season')
	];

	$q.all(promises).then(function(results) {
		self.quote = results[0].data;
		self.quoteItems = $filter('orderBy')(self.quote.Items, 'Service.Name');
		self.billingDays = results[1].data;
		self.months = results[2].data;
		self.statuses = results[3].data;
		self.types = results[4].data;
		self.seasons = results[5].data;
	});

	self.editItem = function (item) {
		var modalInstance = getModalInstance(item);
		modalInstance.result.then(function (updatedItem) {
			// add contact
			//personData.save(addedContact);
		});
	};

	self.gridConfig = {
		data: 'quoteDtl.quoteItems',
		multiSelect: false,
		columnDefs: [
			{ field: 'Service.Name', displayName: 'Service', width: '20%' },
			{ field: 'Description', displayName: 'Description' },
			{ field: 'ServiceQuantity', displayName: 'Man Hours', width: '10%', cellClass: 'text-right' },
			{ name: 'edit', displayName: '', cellTemplate: '<a class="btn" ng-click="grid.appScope.quoteDtl.editItem(row.entity)"><i class="fa fa-pencil-square-o"></i></a>', width: '5%' }
		]
	};

	this.cancel = function() {
		return quoteData.get($stateParams.id);
	};

	this.save = function() {
		return quoteData.save(self.quote);
	};

	this.getPreviousState = function() {
		return $q.when(self.previousState);
	};

}

QuoteDtlCtrl.$inject = ['$stateParams', '$q', '$filter', '$uibModal', 'previousState', 'optionData', 'quoteData'];
app.controller('QuoteDtlCtrl', QuoteDtlCtrl);