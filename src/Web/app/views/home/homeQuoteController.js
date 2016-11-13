'use strict';

function HomeQuoteCtrl($state, $stateParams, $filter, $uibModal, quoteData) {
	var self = this;
	self.quotes = {};

	quoteData.getAllActive().then(function (results) {
		self.allRows = results.data;
		self.visibleRows = self.allRows;
	});

	self.gridConfig = {
		data: 'quote.visibleRows',
		multiSelect: false,
		columnDefs: [
			{
				field: 'PropertyName',
				displayName: 'Property Name',
				cellTemplate:
					'<a ui-sref="pci.quoteDetail({id: row.entity.Id})">{{ COL_FIELD }}</a>',
				width: '40%'
			},
			{
				field: 'Title', displayName: 'Title',
				width: '25%'
			},
			{
				field: 'TypeDesc', displayName: 'Type',
				width: '20%'
			},
			{
				field: 'SeasonDesc', displayName: 'Season',
				width: '15%'
			}
		]
	};

	self.filterGridData = function() {
		var filtered = self.allRows;
		var parsedSearchText = self.searchText.split(' ');
		_.each(parsedSearchText,
			function(searchText) {
				filtered = $filter('filter')(filtered, searchText, undefined);
			});
		self.visibleRows = filtered;
	};

	self.add = function () {
		quoteData.new()
			.then(function (quoteToAdd) {
				var modalInstance = $uibModal.open({
					templateUrl: '../views/quote/quoteAddModal.html',
					backdrop: 'static',
					size: 'lg',
					controller: 'QuoteAddModalCtrl',
					controllerAs: 'ctrl',
					resolve: {
						quoteToAdd: function () {
							return quoteToAdd.data;
						}
					}
				});

				modalInstance.result.then(function (newQuote) {
					quoteData.save(newQuote)
						.then(function (quote) {
							$state.go('pci.quoteDetail', { id: quote.data.Id });
						});
				});
			});
	};

}

HomeQuoteCtrl.$inject = ['$state', '$stateParams', '$filter', '$uibModal', 'quoteData'];
app.controller('HomeQuoteCtrl', HomeQuoteCtrl);