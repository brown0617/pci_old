'use strict';

function saveManagerModal($rootScope, $q, dialogService) {

	return {
		restrict: 'E',
		replace: true,
		transclude: true,
		require: ['^saveManager'],
		templateUrl: '../main/header/saveManagerModal.html',
		link: function(scope, element, attrs, controllers) {

			var saveManager = controllers[0];

			// Open the Save/Cancel modal. 
			scope.openModal = function() {
				var deferred = $q.defer();

				dialogService.message = 'Do you want to save the changes before continuing?';
				dialogService.title = 'Unsaved Changes';
				dialogService.btnYesNo = true;
				dialogService.btnCancel = true;

				dialogService.open().then(function(response) {
					switch (response) {
					case 'Yes':
						saveManager.save()
							.then(function() {
								$rootScope.form.$setPristine();

								// in certain situations this is not being reset 
								if (scope.mainForm && scope.mainForm.$dirty) {
									scope.mainForm.$setPristine();
								}

								deferred.resolve('SaveSuccess');
							}, (function() {
								deferred.resolve('SaveFailure');
							}));
						break;
					case 'No':
						$rootScope.form.$setPristine();

						if (scope.mainForm && scope.mainForm.$dirty) {
							scope.mainForm.$setPristine();
						}

						deferred.resolve('CancelSuccess');
						break;
					case 'Cancel':
						deferred.resolve('Closed');
						break;
					default:
					}

				});

				return deferred.promise;
			};
		}
	};
}

saveManagerModal.$inject = ['$rootScope', '$q', 'dialogService'];
app.directive('saveManagerModal', saveManagerModal);