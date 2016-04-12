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
				item: function() {
					return item;
				}
			}
		});
	};

	var promises = [
		quoteData.get($stateParams.id),
		optionData.getBillingDayOptions(),
		optionData.getMonthOptions(),
		optionData.getQuoteStatusOptions(),
		optionData.getQuoteTypeOptions(),
		optionData.getSeasonOptions()
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

	self.setIncrease = function() {
		self.quote.AnnualIncreasePercentage = self.quote.ContractTermYears <= 1 ? 0 : self.quote.AnnualIncreasePercentage;
	};

	self.calculateTotals = function() {
		var labor = 0, materials = 0;
		_.each(self.quoteItems, function(item) {
				labor += item.ServicePrice || 0;
				materials += item.MaterialPrice || 0;
			}
		);
		var preTaxTotal = labor + materials;
		var salesTax = self.quote.Taxable ? preTaxTotal * self.quote.SalesTaxRate : 0;

		self.quote.TotalAmountLabor = labor;
		self.quote.TotalAmountMaterials = materials;
		self.quote.TotalAmountPretax = preTaxTotal;
		self.quote.SalesTaxAmount = salesTax;
		self.quote.TotalAmount = preTaxTotal + salesTax;
	};
	self.editItem = function(item) {
		var itemToEdit = angular.copy(item);
		var modalInstance = getModalInstance(itemToEdit);
		modalInstance.result.then(function(editedItem) {
			// update item in model
			var index = self.quoteItems.indexOf(item);
			self.quoteItems[index] = editedItem;

			// recalculate totals
			self.calculateTotals();

			// set form dirty
			self.form.$setDirty();
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
		self.quote.items = self.quoteItems;
		return quoteData.save(self.quote);
	};

	this.getPreviousState = function() {
		return $q.when(self.previousState);
	};

}

QuoteDtlCtrl.$inject = ['$stateParams', '$q', '$filter', '$uibModal', 'previousState', 'optionData', 'quoteData'];
app.controller('QuoteDtlCtrl', QuoteDtlCtrl);