'use strict';

function contactService($uibModal, personData) {

	function getModalInstance(contact) {
		return $uibModal.open({
			templateUrl: '../services/contact/contactServiceModal.html',
			backdrop: 'static',
			controller: 'ContactServiceModalCtrl',
			controllerAs: 'ctrl',
			resolve: {
				contact: function() {
					return contact;
				}
			}
		});
	};

	function addContact() {
		personData.new().then(function(contact) {
			var modalInstance = getModalInstance(contact.data);
			modalInstance.result.then(function(addedContact) {
				// add contact
				personData.save(addedContact);
			});
		});
	};

	function editContact(id) {
		personData.get(id).then(function(contact) {
			var modalInstance = getModalInstance(contact.data);
			modalInstance.result.then(function(updatedContact) {
				// update contact
				personData.save(updatedContact);
			});
		});
	};

	return { add: addContact, edit: editContact };
};

contactService.$inject = ['$uibModal', 'personData'];
app.service('contactService', contactService);