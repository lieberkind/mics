window.onload = function() {

	var form = document.getElementById("form");

	form.onsubmit = function(e) {
		e.preventDefault();

		// Fields
		var name 	= this.name.value;
		var address = this.address.value;
		var zipcode = this.zipcode.value;
		var phone 	= this.phone.value;
		var email   = this.email.value;
		var cbs 	= [];

		var cbs = document.getElementsByName("deliveryMethod");
		for(var i = 0; i < cbs.length; i++) {
			if(cbs[i].value === "snailMail") {
				cbs['snailMail'] = cbs[i].checked;
			}
			if(cbs[i].value === "email") {
				cbs['email'] = cbs[i].checked;
			}
		}

		if(isFormValid(name, cbs, address, zipcode, email, phone)) {
			alert("Form is valid!");
		} else {
			alert("Error: not valid.");
		}
	}

}


function isFormValid(name, deliveryMethods, address, zipcode, email, phone) {
	return isNameValid(name) && isDeliveryMethodValid(deliveryMethods, address, zipcode, email) && isPhoneValid(phone);
}

/*
*  Checks is a name is valid according to the following rules
*  
*  Required: 	True
*  Format: 		First name [Middle names] Last name
*  Max length: 	128 
*/
function isNameValid(name) {
	var nameRegEx 			= /^([A-z]+ [A-z]+)( [A-z]+)*$/;
	var isNameFormatValid 	= nameRegEx.test(name);

	return name.length <= 128 && isNameFormatValid;
}

function isAddressValid(address, zipcode) {
	var addressRegEx 			= /^[A-z]+ [0-9]+(, [0-9]+ (SAL|TH|TV))?$/;
	var isAddressFormatValid 	= addressRegEx.test(address);

	var zipCodeRegEx 			= /^[1-9][0-9][0-9][0-9]$/;
	var isZipCodeFormatValid 	= zipCodeRegEx.test(zipcode);

	return isAddressFormatValid && isZipCodeFormatValid;
}

function isEmailValid(email) {
	var emailRegEx 			= /^[A-z0-9._%+-]+@[A-z0-9.-]+.[A-z]{2,4}$/;
	var isEmailFormatValid 	= emailRegEx.test(email);

	return isEmailFormatValid;
}

function isDeliveryMethodValid(deliveryMethods, address, zipcode, email) {
	var snailMailChecked 	= deliveryMethods['snailMail'];
	var emailChecked 		= deliveryMethods['email'];

	if((!snailMailChecked && !emailChecked)) {
		return false;
	}

	if(snailMailChecked && emailChecked) {
		return isEmailValid(email) && isAddressValid(address, zipcode);
	} else if(snailMailChecked) {
		return isAddressValid(address, zipcode);
	} else {
		return isEmailValid(email);
	}
}

function isPhoneValid(phoneNumber) {
	var phoneNumberRegEx = /^([1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9])?$/;
	var isPhoneFormatValid = phoneNumberRegEx.test(phoneNumber);

	return isPhoneFormatValid;
}