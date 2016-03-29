'use strict';

function saveRegistration() {
	return {
		restrict: 'A',
		require: ['^saveManager', '^ngController'],
		link: function($scope, element, attrs, controllers) {
			var saveManager = controllers[0],
				myController = controllers[1];

			// This is the Save/Cancel object that will be registered with the saveManager. 
			var saveCancelObject = {
				save: function() {
					return myController.save.apply(myController, arguments);
				},

				saveComplete: function() {
					if (myController.saveComplete) {
						myController.saveComplete.apply(myController, arguments);
					}
				},

				cancel: function() {
					return myController.cancel.apply(myController, arguments);
				},

				getPreviousState: function() {
					return myController.getPreviousState.apply(myController, arguments);
				}
			};

			// Register the saveCancelObject and unregister it on $destroy. 
			saveManager.register(saveCancelObject);
			$scope.$on('$destroy', function() {
				saveManager.unregister(saveCancelObject);
			});
		}
	};
}

app.directive('saveRegistration', saveRegistration);