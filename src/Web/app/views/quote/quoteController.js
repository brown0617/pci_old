'use strict';

function QuoteCtrl(quoteData, $filter) {
	var self = this;
	self.quotes = {};

	quoteData.getAll().then(function(results) {
		self.allQuotes = results.data;
		self.quotes = self.allQuotes;
	});

	self.gridConfig = {
		data: 'quote.quotes',
		multiSelect: false,
		columnDefs: [
			{ field: 'PropertyName', displayName: 'Property Name' },
			{ field: 'CustomerName', displayName: 'Customer Name' },
			{ field: 'Title', displayName: 'Title' },
			{ field: 'ContractYear', displayName: 'Year' },
			{ field: 'TypeDesc', displayName: 'Type' },
			{ field: 'SeasonDesc', displayName: 'Season' },
			{ name: 'edit', displayName: '', cellTemplate: '<a class="btn" ui-sref="pci.customerDetail({id: row.entity.Id})"><i class="fa fa-pencil-square-o"></i></a>' }
		]
	};

	this.filterGridData = function() {
		self.quotes = $filter('filter')(self.allQuotes, self.searchText, undefined);
	};
}

QuoteCtrl.$inject = ['quoteData', '$filter'];
app.controller('QuoteCtrl', QuoteCtrl);