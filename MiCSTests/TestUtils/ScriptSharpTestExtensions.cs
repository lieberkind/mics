using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS = ScriptSharp.ScriptModel;

namespace MiCSTests.TestUtils
{
    public static class ScriptSharpTestExtensions
    {
        public static List<SS.MethodSymbol> Methods(this SS.ClassSymbol ssClass)
        {
            var ssMethods = new List<SS.MethodSymbol>();
            foreach (var member in ssClass.Members.Where(m => m is SS.MethodSymbol))
            {
                ssMethods.Add((SS.MethodSymbol)member);
            }
            return ssMethods;
        }

        public static List<SS.Statement> Statements(this SS.MethodSymbol ssMethod)
        {
            return ssMethod.Implementation.Statements.ToList();
        }
    }
}
