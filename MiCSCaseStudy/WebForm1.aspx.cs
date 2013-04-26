using MiCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Html;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace MiCSCaseStudy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        TextBox NameBox = new TextBox() { ID = "name", Text = "Name" };
        Panel CheckBoxGroup = new Panel();
        CheckBox SnailMailCheck = new CheckBox() { ID = "dmSnailmail", Text = "Snail Mail" };
        CheckBox EmailCheck = new CheckBox() { ID = "dmEmail", Text = "E-Mail" };
        TextBox AddressBox = new TextBox() { ID = "address", Text = "Address" };
        TextBox ZipcodeBox = new TextBox() { ID = "zipcode", Text = "Zip Code" };
        TextBox EmailBox = new TextBox() { ID = "email", Text = "E-mail" };
        TextBox PhoneBox = new TextBox() { ID = "phone", Text = "Phone" };
        Button SubmitButton = new Button() { Text = "Register" };

        protected void Page_Init(object sender, EventArgs e)
        {
           
            var scriptManager = new ScriptManager();
            form1.Controls.Add(scriptManager);

            var filePath = @"C:\Users\L520\Documents\Visual Studio 2012\Projects\MiCS\MiCSCaseStudy\WebForm1.aspx.cs";
            var source = File.ReadAllText(filePath);

            MiCSManager.Initiate(source);
            MiCSManager.BuildScript(scriptManager, this);

            form1.Add(NameBox);
            CheckBoxGroup.Controls.Add(SnailMailCheck);
            CheckBoxGroup.Controls.Add(EmailCheck);
            form1.Controls.Add(CheckBoxGroup);
            form1.Add(AddressBox);
            form1.Add(ZipcodeBox);
            form1.Add(EmailBox);
            form1.Add(PhoneBox);
            form1.Add(SubmitButton);

            SubmitButton.OnClientClick(OnClickAction);

            SubmitButton.Click += SubmitButton_Click;
        }

        void SubmitButton_Click(object sender, EventArgs e)
        {
            Validator v = new Validator();

            var isPhoneValid = v.isPhoneValid(PhoneBox.Text);
            var isNameValid = v.isNameValid(NameBox.Text);
            var isDeliveryMethodsValid = v.isDeliveryMethodsValid(new bool[] { SnailMailCheck.Checked, EmailCheck.Checked }, AddressBox.Text, ZipcodeBox.Text, EmailBox.Text);

            Color color;
            if (isPhoneValid && isNameValid && isDeliveryMethodsValid)
                color = Color.Green;
            else
                color = Color.Red;
            
            NameBox.BackColor = color;
            AddressBox.BackColor = color;
            ZipcodeBox.BackColor = color;
            EmailBox.BackColor = color;
            PhoneBox.BackColor = color;
        }

        [ClientSide]
        private bool OnClickAction()
        {
            Validator v = new Validator();

            bool[] deliveryMethods = new bool[2];
            
            CheckBoxElement snailmailCheckBox = (CheckBoxElement)Document.GetElementById("dmSnailmail");
            deliveryMethods[0] = snailmailCheckBox.Checked;

            CheckBoxElement emailCheckBok = (CheckBoxElement)Document.GetElementById("dmEmail");
            deliveryMethods[1] = emailCheckBok.Checked;

            InputElement nameField = (InputElement)Document.GetElementById("name");
            string name = nameField.Value;

            InputElement addressField = (InputElement)Document.GetElementById("address");
            string address = addressField.Value;
            
            InputElement zipcodeField = (InputElement)Document.GetElementById("zipcode");
            string zipcode = zipcodeField.Value;
            
            InputElement emailField = (InputElement)Document.GetElementById("email");
            string email = emailField.Value;

            InputElement phoneField = (InputElement)Document.GetElementById("phone");
            string phone = phoneField.Value;

            return v.isNameValid(name) && v.isPhoneValid(phone) && v.isDeliveryMethodsValid(deliveryMethods, address, zipcode, email);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

    class Validator
    {
        public bool isFormValid()
        {
            throw new NotImplementedException();
        }

        [MixedSide]
        public bool isNameValid(string name)
        {
            Regex rx = new Regex(@"^([A-z]+ [A-z]+)( [A-z]+)*$");
            return rx.IsMatch(name) && name.Length > 5 && name.Length < 128;
        }

        [MixedSide]
        public bool isAddressValid(string address, string zipcode)
        {
            var addressRegEx = new Regex("^[A-z]+ [0-9]+(, [0-9]+ (SAL|TH|TV))?$");
            var isAddressFormatValid = addressRegEx.IsMatch(address);

            var zipCodeRegEx = new Regex("^[1-9][0-9][0-9][0-9]$");
            var isZipCodeFormatValid = zipCodeRegEx.IsMatch(zipcode);

            return isAddressFormatValid && isZipCodeFormatValid;
        }

        [MixedSide]
        public bool isEmailValid(string email)
        {
            var emailRegEx = new Regex("^[A-z0-9._%+-]+@[A-z0-9.-]+.[A-z]{2,4}$");
            var isEmailFormatValid = emailRegEx.IsMatch(email);
            return isEmailFormatValid;
        }

        [MixedSide]
        public bool isDeliveryMethodsValid(bool[] deliveryMethods, string address, string zipcode, string email)
        {
            var snailMailChecked = deliveryMethods[0];
            var emailChecked = deliveryMethods[1];

            if (!snailMailChecked && !emailChecked)
            {
                return false;
            }

            if (snailMailChecked && emailChecked)
            {
                return isEmailValid(email) && isAddressValid(address, zipcode);
            }

            else if (snailMailChecked)
            {
                return isAddressValid(address, zipcode);
            }

            else
            {
                return isEmailValid(email);
            }
        }

        [MixedSide]
        public bool isPhoneValid(string phoneNumber)
        {
            var phoneNumberRegEx = new Regex("^([1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9])$");
            var isPhoneFormatValid = phoneNumberRegEx.IsMatch(phoneNumber);

            return isPhoneFormatValid;
        }

    }

    class User
    {

        [MixedSide]
        int CalculateBMI()
        {
            return 0;
        }
    }
}

//using System.Text.RegularExpressions;
//namespace TestNamespace {
//class TestClass {
//[MixedSide]
//public void ImARegEx() {
//RegExp regEx = new RegExp("imapattern"); 
//}
//}
//}