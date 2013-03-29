using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MiCSCaseStudy
{
    public partial class Default : System.Web.UI.Page
    {
        TextBox NameBox = new TextBox() { ID = "name", Text = "Name" };
        Panel CheckBoxGroup = new Panel();
        CheckBox SnailMailCheck = new CheckBox() { Text = "Snail Mail" };
        CheckBox EmailCheck = new CheckBox() { Text = "E-Mail" };
        TextBox AddressBox = new TextBox() { ID = "address", Text = "Address" };
        TextBox ZipcodeBox = new TextBox() { ID = "zipcode", Text = "Zip Code" };
        TextBox EmailBox = new TextBox() { ID = "email", Text = "E-mail" };
        TextBox PhoneBox = new TextBox() { ID = "phone", Text = "Phone"  };
        Button SubmitButton = new Button() { Text = "Register" };

        protected void Page_Init(object sender, EventArgs e)
        {
            form1.Controls.Add(new ScriptManager());
            form1.Add(NameBox);
            CheckBoxGroup.Controls.Add(SnailMailCheck);
            CheckBoxGroup.Controls.Add(EmailCheck);
            form1.Controls.Add(CheckBoxGroup);
            form1.Add(AddressBox);
            form1.Add(ZipcodeBox);
            form1.Add(EmailBox);
            form1.Add(PhoneBox);
            form1.Add(SubmitButton);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptInclude(Page, Page.GetType(), "ValidationCode", "javascript.js");
            SubmitButton.OnClientClick = "alert(isNameValid('Test Name'));";
        }

    }


    public static class ExtensionMethods
    {
        public static void Add(this HtmlForm form, Control control)
        {
            var p = new Panel();
            p.Controls.Add(control);
            form.Controls.Add(p);
        }
    }
}