﻿'use strict';

function QuoteItemModalCtrl($q, $uibModalInstance, serviceData, optionData, materialData, itemModel) {
	var self = this;

	var promises = [serviceData.getAll(), optionData.getServiceFrequencyOptions(), optionData.getMonthOptions(), optionData.getBillingMethodOptions()];
	$q.all(promises).then(function(results) {
		self.services = results[0].data;
		self.serviceFrequencyOptions = results[1].data;
		self.monthOptions = results[2].data;
		self.billingMethodOptions = results[3].data;
		self.item = itemModel.item;
		self.quoteType = itemModel.quoteType;
	});

	self.serviceChanged = function() {
		self.item.ServiceId = self.item.Service.Id;
		self.item.Description = self.item.Service.Description;
		this.updatePrice();
	}

	// 
	self.updatePrice = function() {
		self.item.ServiceUnitPrice = self.item.Service.Price;
		self.item.ServiceUnitCost = self.item.Service.Cost;
		this.calcServiceTotal();
	};

	// calculate labor cost & price
	self.calcServiceTotal = function() {
		self.item.ServiceCost = self.item.ServiceUnitCost * self.item.ServiceQuantity;
		self.item.ServicePrice = self.item.ServiceUnitPrice * self.item.ServiceQuantity;
	};

	// property for handling service deadline date picker
	Object.defineProperties(self,
		{
			serviceDeadlineOpen: {
				open: false,
				set: function(value) {
					this.open = value;
				},
				get: function() {
					return this.open;
				}
			}
		}
	);

	// aynch call to get materials list
	self.getMaterialsByName = function (name) {
		return materialData.getMaterialsByName(name);
	};

	self.materialSelected = function (material) {
		self.item.MaterialUnitCost = material.Cost;
		self.item.MaterialUnitPrice = material.Price;
		self.calcMaterialTotal();
	}

	// calculate material cost & price
	self.calcMaterialTotal = function () {
		self.item.MaterialCost = self.item.MaterialUnitCost * self.item.MaterialQuantity;
		self.item.MaterialPrice = self.item.MaterialUnitPrice * self.item.MaterialQuantity;
	};

	this.save = function() {
		$uibModalInstance.close(self.item);
	};

	this.cancel = function() {
		$uibModalInstance.dismiss();
	};
};

QuoteItemModalCtrl.$inject = ['$q', '$uibModalInstance', 'serviceData', 'optionData', 'materialData', 'itemModel'];
app.controller('QuoteItemModalCtrl', QuoteItemModalCtrl);