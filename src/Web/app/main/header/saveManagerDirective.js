'use strict';

function saveManager($q) {
	return {
		restrict: 'A',
		require: ['saveManager'],
		controller: [
			function() {

				var controls = [];

				/** 
				 * @xrs method  
				 * @name save 
				 * 
				 * @description - Save the registered controls 
				 */
				this.save = function() {
					var deferred = this.fire('save');
					deferred.then(function() {
						controls.forEach(function(control) {
							if (control.saveComplete) {
								control.saveComplete(control, null);
							}
						});
					},
						function(error) {
							throw error;
						}
					);
					return deferred;
				};

				/** 
				 * @xrs method  
				 * @name cancel 
				 * 
				 * @description - Cancel the save controls 
				 */
				this.cancel = function() {
					var deferred = this.fire('cancel');
					return deferred;
				};

				/** 
				 * @xrs method  
				 * @name fire 
				 * 
				 * @description - Fire off a specific call on all registered controls 
				 */
				this.fire = function(event, args) {
					var promises = [];

					controls.forEach(function(control) {
						if (event === 'save') {
							promises.push(control[event]
								? control[event].call(control, args)
								: null);

						} else {
							promises.push($q.when(control[event]
								? control[event].call(control, args)
								: null));
						}

					});

					return $q.all(promises);
				};

				/** 
				 * @xrs method  
				 * @name register 
				 * 
				 * @description - Register a savable control ie. { save, cancel, onSave } 
				 */
				this.register = function(control) {
					controls.push(control);
					return _.bind(function() { this.unregister(control); }, this);
				};

				/** 
				 * @xrs method  
				 * @name unregister 
				 * 
				 * @description - Unregister a registered control 
				 */
				this.unregister = function(control) {
					controls.splice(controls.indexOf(control), 1);
				};
			}
		]
	};
}

saveManager.$inject = ['$q'];
app.directive('saveManager', saveManager);