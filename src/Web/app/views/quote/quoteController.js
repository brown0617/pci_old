'use strict';

function QuoteCtrl($filter, quoteData) {
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
			{ field: 'ContractYear', displayName: 'Year' },
			{ field: 'TypeDesc', displayName: 'Type' },
			{ field: 'SeasonDesc', displayName: 'Season' },
			{ name: 'edit', displayName: '', cellTemplate: '<a class="btn" ui-sref="pci.quoteDetail({id: row.entity.Id})"><i class="fa fa-pencil-square-o"></i></a>' }
		]
	};

	this.filterGridData = function() {
		var filtered = self.allRows;
		var parsedSearchText = self.searchText.split(' ');
		_.each(parsedSearchText, function(searchText) {
			filtered = $filter('filter')(filtered, searchText, undefined);
		});
		self.visibleRows = filtered;
	};
}

QuoteCtrl.$inject = ['$filter','quoteData', ];
app.controller('QuoteCtrl', QuoteCtrl);