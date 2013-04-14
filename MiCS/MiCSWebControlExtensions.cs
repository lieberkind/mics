using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiCS
{
    public static class MiCSWebControlExtensions
    {
        public static void MiCSOnClientClick(this Button button, Action action)
        {
            var scriptText = MiCSManager.GenerateScriptText(action);
            button.OnClientClick = scriptText;
        }

        public static void MiCSOnLoad(this Page page, Action action)
        {
            var scriptText = MiCSManager.GenerateScriptText(action);
            var uniqueScriptKey = Guid.NewGuid().ToString().Replace("-", "");
            page.ClientScript.RegisterStartupScript(page.GetType(), uniqueScriptKey, scriptText); 
        }
    }
}
