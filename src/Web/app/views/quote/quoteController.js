'use strict';

function QuoteCtrl($state, $filter, $uibModal, quoteData) {
	var self = this;
	self.quotes = {};

	quoteData.getAll().then(function(results) {
		self.allRows = results.data;
		self.visibleRows = self.allRows;
	});

	self.gridConfig = {
		data: 'quote.visibleRows',
		multiSelect: false,
		columnDefs: [
			{ field: 'PropertyName', displayName: 'Property Name' },
			{ field: 'CustomerName', displayName: 'Customer Name' },
			{ field: 'Title', displayName: 'Title' },
			{ field: 'ContractYear', displayName: 'Year', width: '7%' },
			{ field: 'TypeDesc', displayName: 'Type', width: '13%' },
			{ field: 'SeasonDesc', displayName: 'Season', width: '10%' },
			{ name: 'edit', displayName: '', cellTemplate: '<a class="btn" ui-sref="pci.quoteDetail({id: row.entity.Id})"><i class="fa fa-pencil-square-o"></i></a>', width: '5%' }
		]
	};

	self.filterGridData = function() {
		var filtered = self.allRows;
		var parsedSearchText = self.searchText.split(' ');
		_.each(parsedSearchText, function(searchText) {
			filtered = $filter('filter')(filtered, searchText, undefined);
		});
		self.visibleRows = filtered;
	};

	self.add = function() {
		quoteData.new().then(function (quoteToAdd) {
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

			modalInstance.result.then(function(newQuote) {
				quoteData.save(newQuote).then(function(quote) {
					$state.transition(pci.quoteDetail, { id: quote.id });
				});
			});
		});
	}
}

QuoteCtrl.$inject = ['$state', '$filter', '$uibModal', 'quoteData',];
app.controller('QuoteCtrl', QuoteCtrl);