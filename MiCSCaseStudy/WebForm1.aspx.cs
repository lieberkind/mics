﻿using MiCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Html;
using System.IO;
using System.Text.RegularExpressions;

namespace MiCSCaseStudy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var scriptManager = new ScriptManager();
            form1.Controls.Add(scriptManager);

            var filePath = @"C:\Users\L520\Documents\Visual Studio 2012\Projects\mics\MiCSCaseStudy\WebForm1.aspx.cs";
            var source = File.ReadAllText(filePath);

            MiCSManager.Initiate(source);
            MiCSManager.BuildScript(scriptManager, this);


            var message = "Implemented!";
            var button = new Button() { Text = "Test Button" };
            form1.Controls.Add(button);

            button.MiCSOnClientClick(() =>
            {
                Window.Alert(MiCSValue.String(message));
            });
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

    class Validator
    {
        [MixedSide]
        bool isNameValid(string name)
        {
            Regex rx = new Regex(@"/^([A-z]+ [A-z]+)( [A-z]+)*$/");
            return rx.IsMatch(name);
            //return false;
        }

        [MixedSide]
        bool isNameInvalid(string name)
        {
            String s = "foo";
            int i = s.Length;
            i = s.IndexOf('o');
            Regex rx = new Regex(@"/^([A-z]+ [A-z]+)( [A-z]+)*$/");
            User usr = new User();
            Validator v = new Validator();
            v.isNameValid("tomas");

            Document.HasFocus();
            bool b = Document.HasFocus();
            Element e = Document.GetElementById("test");

            if (2 < 3)
            {
                return true;
            }
            else
            {
                return false;
            }
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