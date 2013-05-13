using MiCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MiCSCaseStudy
{
    class Validator
    {

        [MixedSide]
        public bool IsNameValid(string name)
        {
            Regex rx = new Regex(@"^([A-z]+ [A-z]+)( [A-z]+)*$");
            return rx.IsMatch(name) && name.Length > 5 && name.Length < 128;
        }

        [MixedSide]
        public bool IsAddressValid(string address, string zipcode)
        {
            var addressRegEx = new Regex("^[A-z]+ [0-9]+(, [0-9]+ (SAL|TH|TV))?$");
            var isAddressFormatValid = addressRegEx.IsMatch(address);

            var zipCodeRegEx = new Regex("^[1-9][0-9][0-9][0-9]$");
            var isZipCodeFormatValid = zipCodeRegEx.IsMatch(zipcode);

            return isAddressFormatValid && isZipCodeFormatValid;
        }

        [MixedSide]
        public bool IsEmailValid(string email)
        {
            var emailRegEx = new Regex("^[A-z0-9._%+-]+@[A-z0-9.-]+.[A-z]{2,4}$");
            var isEmailFormatValid = emailRegEx.IsMatch(email);
            return isEmailFormatValid;
        }

        [MixedSide]
        public bool IsDeliveryMethodsValid(bool[] deliveryMethods, string address, string zipcode, string email)
        {
            var snailMailChecked = deliveryMethods[0];
            var emailChecked = deliveryMethods[1];

            if (!snailMailChecked && !emailChecked)
            {
                return false;
            }

            if (snailMailChecked && emailChecked)
            {
                return IsEmailValid(email) && IsAddressValid(address, zipcode);
            }

            else if (snailMailChecked)
            {
                return IsAddressValid(address, zipcode);
            }

            else
            {
                return IsEmailValid(email);
            }
        }

        [MixedSide]
        public bool IsPhoneValid(string phoneNumber)
        {
            var phoneNumberRegEx = new Regex("^([1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9])$");
            var isPhoneFormatValid = phoneNumberRegEx.IsMatch(phoneNumber);

            return isPhoneFormatValid;
        }
    }
}