using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiCS
{

    public static class WebControlExtensions
    {
        /// <summary>
        /// Registers a [ClientSide] function to be called on Button client click event.
        /// </summary>
        /// <param name="func">Method with [ClientSide] attribute to be executed on Button client click event.</param>
        public static void OnClientClick(this Button button, Func<bool> func)
        {
            var methodName = func.Method.Name;
            var className = func.Method.DeclaringType.Name;
            var namespaceName = func.Method.DeclaringType.Namespace;

            #region Region: Method name correction

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

            #endregion

            var scriptText = "var obj = new " + namespaceName + "$" + className + "(); return obj." + methodName + "();";
            button.OnClientClick = scriptText;
        }
    }
}
