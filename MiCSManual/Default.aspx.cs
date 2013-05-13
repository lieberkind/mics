using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiCS;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Html;

namespace MiCSManual
{
    public partial class Default : MiCSPage
    {
        
        Button button;
        CheckBox checkBox;
        TextBox emailBox;

        protected void Page_Load(object sender, EventArgs e)
        {
            emailBox = new TextBox() { ID = "EmailBox", Text = "E-mail" };
            checkBox = new CheckBox() { ID = "CheckBox", Text = "I accept the terms."};
            button = new Button() { Text = "Submit" };

            form1.Controls.Add(emailBox);
            form1.Controls.Add(checkBox);
            form1.Controls.Add(button);

            button.Click += button_Click;
            button.OnClientClick(button_ClientClick);
        }

        void button_Click(object sender, EventArgs e)
        {
            if (isEmailValid(emailBox.Text) && checkBox.Checked)
                form1.Controls.Add(new Label() { Text = "Registration Complete." });
            else
                form1.Controls.Add(new Label() { Text = "Server Side Registration Failed!" });
        }

        [ClientSide]
        bool button_ClientClick()
        {
            var emailField = (InputElement)Document.GetElementById("EmailBox");
            var termsCheckBox = (CheckBoxElement)Document.GetElementById("CheckBox");

            if (!isEmailValid(emailField.Value))
            {
                Window.Alert("Invalid E-mail!");
                return false;
            }

            if (!termsCheckBox.Checked)
            {
                Window.Alert("Accepting terms are required!");
                return false;
            }

            return true;
        }

        [MixedSide]
        bool isEmailValid(string email)
        {
            var emailRegex = new Regex("^[A-z0-9._%+-]+@[A-z0-9.-]+.[A-z]{2,4}$");
            // Todo: Investigate the type of exception that is thrown when using emailBox.Text instead of email here...
            return emailRegex.IsMatch(email);
        }
    }
}