'use strict';

function saveManagerModal($rootScope, $q) {

	return {
		restrict: 'E',
		replace: true,
		transclude: true,
		require: ['^saveManager', '^form'],
		templateUrl: '../main/header/saveManagerModal.html',
		link: function(scope, element, attrs, controllers) {

			var saveManager = controllers[0];
			var form = controllers[1];

			var openModalDeferred;

			function hideModal() {
				scope.$parent.$root.modalBackdrop = false;
				element.addClass('ng-hide');
			}

			// Init step to hide the modal. 
			hideModal();

			_.extend(scope, {

				// Open the Save/Cancel modal. 
				openModal: function() {
					openModalDeferred = $q.defer();
					// TODO: This seems messy - why can't we just show and hide a modal as necessary? 
					scope.$parent.$root.modalBackdrop = true;
					element.removeClass('ng-hide');
					return openModalDeferred.promise;
				},

				// Close the Save/Cancel modal and return a promise that resolves to 'Closed'
				closeModal: function() {
					hideModal();
					openModalDeferred.resolve('Closed');
				},

				// Close the modal, set pristine on the form and return a promise
				saveModal: function() {
					if (form.$invalid || form.$pending) {
						hideModal();
						return openModalDeferred.resolve('ErrorInPage');
					}

					saveManager.save().then(function() {
						hideModal();
						$rootScope.form.$setPristine();

						// in certain situations this is not being reset 
						if (scope.mainForm && scope.mainForm.$dirty) {
							scope.mainForm.$setPristine();
						}

						openModalDeferred.resolve('SaveSuccess');
					}).catch(function(result) {
						//$log.error(result);
						hideModal();
						//confirmationModalService.saveFailedConfirmation();
						openModalDeferred.resolve('SaveFailure');
					});
				},

				// Close the modal, set pristine on the form and return a promise that resolves to 'CancelSuccess'
				cancelModal: function() {
					// Enable navigation without saving changes. 
					hideModal();
					$rootScope.form.$setPristine();

					// in certain situations this is not being reset 
					if (scope.mainForm && scope.mainForm.$dirty) {
						scope.mainForm.$setPristine();
					}

					openModalDeferred.resolve('CancelSuccess');
				}
			});
		}
	};
}

saveManagerModal.$inject = ['$rootScope', '$q'];
app.directive('saveManagerModal', saveManagerModal);