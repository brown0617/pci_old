'use strict';

function QuoteDtlCtrl($stateParams, $q, $filter, $uibModal, previousState, optionData, quoteData) {
	var self = this;
	self.previousState = previousState;

	function getModalInstance(itemModel) {
		return $uibModal.open({
			templateUrl: '../views/quote/quoteItemModal.html',
			backdrop: 'static',
			size: 'lg',
			controller: 'QuoteItemModalCtrl',
			controllerAs: 'ctrl',
			resolve: {
				itemModel: function() {
					return itemModel;
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
		var laborPrice = 0, materialsPrice = 0, laborCost = 0, materialsCost = 0;
		_.each(self.quoteItems, function(item) {
				laborPrice += item.ServicePrice || 0;
				laborCost += item.ServiceCost || 0;
				materialsPrice += item.MaterialPrice || 0;
				materialsCost += item.MaterialCost || 0;
			}
		);

		// cost
		self.quote.TotalCostLabor = laborCost;
		self.quote.TotalCostMaterials = materialsCost;
		self.quote.TotalCost = laborCost + materialsCost;

		// price
		var preTaxTotal = laborPrice + materialsPrice;
		var salesTax = self.quote.Taxable ? preTaxTotal * self.quote.SalesTaxRate : 0;

		self.quote.TotalPriceLabor = laborPrice;
		self.quote.TotalPriceMaterials = materialsPrice;
		self.quote.TotalPricePretax = preTaxTotal;
		self.quote.SalesTaxAmount = salesTax;
		self.quote.TotalPrice = preTaxTotal + salesTax;
	};

	self.editItem = function(item) {
		var itemToEdit = { quoteType: self.quote.Type, item: angular.copy(item) };
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


	self.addItem = function() {
		var itemToAdd = { quoteType: self.quote.Type, item: { QuoteId: self.quote.Id } };
		var modalInstance = getModalInstance(itemToAdd);
		modalInstance.result.then(function(newItem) {
			// add item to model
			self.quoteItems.push(newItem);

			// recalculate totals
			self.calculateTotals();

			// set form dirty
			self.form.$setDirty();
		});
	};

	self.deleteItem = function(item) {
		var index = self.quoteItems.indexOf(item);
		self.quoteItems[index].DeletedOn = new Date();

		self.quoteItems = $filter('filter')(self.quote.Items, { DeletedOn: null });

		// recalculate totals
		self.calculateTotals();

		// set form dirty
		self.form.$setDirty();
	};

	self.gridConfig = {
		data: 'quoteDtl.quoteItems',
		multiSelect: false,
		columnDefs: [
			{ field: 'Service.Name', displayName: 'Service', width: '20%' },
			{ field: 'Description', displayName: 'Description' },
			{ field: 'ServiceQuantity', displayName: 'Man Hours', width: '10%', cellClass: 'text-right' },
			{
				name: 'edit',
				displayName: '',
				cellTemplate: '<a class="btn btn-sm" ng-click="grid.appScope.quoteDtl.editItem(row.entity)"><i class="fa fa-pencil-square-o"></i></a><a class="btn btn-sm" ng-click="grid.appScope.quoteDtl.deleteItem(row.entity)"><i class="fa fa-trash-o"></i></a>',
				width: '7%',
				enableSorting: false,
				enableHiding: false
			}
		]
	};

	this.cancel = function() {

		return quoteData.get($stateParams.id).then(function(response) {
			self.quote = response.data;
			self.quoteItems = $filter('orderBy')(self.quote.Items, 'Service.Name');
		});
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