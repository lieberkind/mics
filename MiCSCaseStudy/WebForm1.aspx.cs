using MiCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Html;
using System.IO;

namespace MiCSCaseStudy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var scriptManager = new ScriptManager();
            form1.Controls.Add(scriptManager);
            var filePath = @"C:\Users\L520\Documents\Visual Studio 2012\Projects\mics\MiCSCaseStudy\WebForm1.aspx.cs";
            var filePathBuiltInType2 = @"C:\Users\L520\Documents\Visual Studio 2012\Projects\mics\MiCS\ScriptSharp\Web\Html\Element.cs";
            var filePathBuiltInType = @"C:\Users\L520\Documents\Visual Studio 2012\Projects\mics\MiCS\ScriptSharp\Web\Html\Document.cs";


            var source = File.ReadAllText(filePath);
            source += File.ReadAllText(filePathBuiltInType);
            source += File.ReadAllText(filePathBuiltInType2);


            MiCSManager.Initiate(source);
            MiCSManager.BuildScript(scriptManager, this);
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
            //var child = Document.GetElementById("hdjwhd").ChildNodes[0];

            return false;
        }

        [MixedSide]
        bool isNameInvalid(string name)
        {
            Validator v = new Validator();
            v.isNameValid("tomas");

            Document.HasFocus();
            bool b = Document.HasFocus();
            var b2 = Document.HasFocus();
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
}