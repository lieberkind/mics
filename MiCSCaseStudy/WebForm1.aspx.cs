using MiCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiCSCaseStudy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var scriptManager = new ScriptManager();
            form1.Controls.Add(scriptManager);
            var MiCSGen = new MiCSManager();
            MiCSGen.BuildScript(@"C:\Users\L520\Documents\Visual Studio 2012\Projects\mics\MiCSCaseStudy\WebForm1.aspx.cs", scriptManager, this);
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
	        return false;
        }
    }
}