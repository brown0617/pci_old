'use strict';

function mainHeader($rootScope, $window, $state, $timeout, dialogService) {
	return {
		restrict: 'E',
		require: ['^form', '^saveManager'],
		scope: false,
		templateUrl: '../main/header/header.html',
		link: function($scope, element, attrs, controllers) {
			var form = controllers[0],
				saveManager = controllers[1];

			$rootScope.form = form;

			_.defaults($scope, {
				form: form,
				allowBack: form.previousState != undefined,

				// Saves the current form 
				save: function() {
					$scope.saveSucceeded = false;
					if ($scope.form.$invalid || $scope.form.$pending) {
						//$scope.systemSettingsValidation.showAllErrors = true;
						return -1;
					}

					return saveManager.save().then(function() {
						//$scope.systemSettingsValidation.showAllErrors = false;
						form.$setPristine();
						$scope.saveSucceeded = true;
						$timeout(function() { $scope.saveSucceeded = false; }, 2000);
					}).catch(function() {
						//dialogService.saveFailedConfirmation();
					});
				},

				// Cancel the changes made to the current form 
				cancel: function() {
					$scope.form.$invalid = false;
					return saveManager.cancel().then(function() {
						//$scope.systemSettingsValidation.showAllErrors = false;
						form.$setPristine();
					});
				},

				// Opens the "Confirm Cancel Changes" dialog.
				confirmCancel: function() {
					dialogService.message = 'Are you sure that you want to Cancel your changes?';
					dialogService.title = 'Confirm Cancel';
					dialogService.btnYesNo = true;
					dialogService.open().then(function(response) {
						if (response === 'Yes') {
							$scope.cancel();
						}
					});
				},

				// Go back to previous page
				back: function() {
					$scope.form.$invalid = false;
					return saveManager.back().then(function () {
						//$scope.systemSettingsValidation.showAllErrors = false;
						form.$setPristine();
					});
				}
			});
		}
	};
}

mainHeader.$inject = ['$rootScope', '$window', '$state', '$timeout', 'dialogService'];
app.directive('mainHeader', mainHeader);