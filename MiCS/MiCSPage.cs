using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiCS
{
    public class MiCSPage : System.Web.UI.Page
    {
        private string rootPath;

        public MiCSPage()
        {
            rootPath = Server.MapPath("/");
        }

        protected override void OnInit(EventArgs e)
        {
            // Ensure a ScriptManager is added to the Page.
            // http://blogs.prexens.com/pages/post.aspx?ID=3
            if (Page != null && ScriptManager.GetCurrent(Page) == null)
            {
                Page.Form.Controls.AddAt(0, new ScriptManager());
            }

            base.OnInit(e);
        }


        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            // Todo: Improvement - Every file in solution should be in its own SyntaxTree.
            var source = @"
                using MiCS;
                using System.Html;
                using System.Text.RegularExpressions;
            ";

            foreach (string file in Directory.EnumerateFiles(rootPath, "*.cs", SearchOption.AllDirectories))
            {
                source += File.ReadAllText(file);
            }

            MiCSManager.Initiate(source);
            MiCSManager.BuildScript(ScriptManager.GetCurrent(this), this);
        }

    }
}
