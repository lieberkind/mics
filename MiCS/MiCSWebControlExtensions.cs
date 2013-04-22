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
        public static void OnClientClick(this Button button, Action action)
        {
            var methodName = action.Method.Name;
            var className = action.Method.DeclaringType.Name;
            var namespaceName = action.Method.DeclaringType.Namespace;

            var scriptText = "var obj = new " + namespaceName + "$" + className + "(); obj." + methodName + "(); return false;";
            button.OnClientClick = scriptText;
        }
    }
}
