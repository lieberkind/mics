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
            var filePath = @"C:\Users\L520\Documents\visual studio 2012\Projects\MiCS\MiCSCaseStudy\WebForm1.aspx.cs";
            MiCSManager.Initiate(File.ReadAllText(filePath));
            var MiCSGen = new MiCSManager();
            //MiCSGen.BuildScript(@"C:\Users\Tomas Lieberkind\documents\visual studio 2012\Projects\MiCS\MiCSCaseStudy\WebForm1.aspx.cs", scriptManager, this);
            MiCSGen.BuildScript(filePath, scriptManager, this);
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