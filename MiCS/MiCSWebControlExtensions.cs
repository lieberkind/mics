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

            // Todo: maybe add to report.
            /*
             * Manual method declaration name correction!
             * 
             * ScriptSharp script generator changes all method names to start
             * with a lower case letter. We manually handle this here by changing
             * the first letter to lower case. This however leaves the possibility
             * of name collisions in some situation. If one declares two methods 
             * where the only difference is in the method names are a lower and 
             * upper case first letter (E.g. myMethod() and MyMethod()) name 
             * collisions will happen. We have ignored this problem and therefore 
             * mention it here.
             */
            methodName = Char.ToLower(methodName[0]).ToString() + methodName.Substring(1);

            var scriptText = "var obj = new " + namespaceName + "$" + className + "(); obj." + methodName + "(); return false;";
            button.OnClientClick = scriptText;
        }
    }
}
