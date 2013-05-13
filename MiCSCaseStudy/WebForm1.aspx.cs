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
    public partial class WebForm1 : MiCSPage
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
            form1.Controls.Add(new Label() { Text = "Registration", CssClass = "h2" });
            form1.Controls.Add(NameBox);
            form1.Controls.Add(new Label() { Text = "Choose delivery method", CssClass = "h4" });
            CheckBoxGroup.Controls.Add(SnailMailCheck);
            CheckBoxGroup.Controls.Add(EmailCheck);
            form1.Controls.Add(CheckBoxGroup);
            form1.Controls.Add(AddressBox);
            form1.Controls.Add(ZipcodeBox);
            form1.Controls.Add(EmailBox);
            form1.Controls.Add(new Label() { Text = "And/or", CssClass = "h4" });
            form1.Controls.Add(PhoneBox);
            form1.Controls.Add(SubmitButton);

            SubmitButton.OnClientClick(OnClickAction);

            SubmitButton.Click += SubmitButton_Click;
        }

        void SubmitButton_Click(object sender, EventArgs e)
        {
            Validator v = new Validator();

            var isPhoneValid = v.IsPhoneValid(PhoneBox.Text);
            var isNameValid = v.IsNameValid(NameBox.Text);
            var isDeliveryMethodsValid = v.IsDeliveryMethodsValid(new bool[] { SnailMailCheck.Checked, EmailCheck.Checked }, AddressBox.Text, ZipcodeBox.Text, EmailBox.Text);

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

            var isValid = v.IsNameValid(name) && v.IsPhoneValid(phone) && v.IsDeliveryMethodsValid(deliveryMethods, address, zipcode, email);

            if (!isValid)
                Window.Alert("The form is not valid!");

            return isValid;
        }

    }


}
