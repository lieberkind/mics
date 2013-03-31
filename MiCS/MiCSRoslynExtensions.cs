using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    // Todo: Consider if these methods are hacks to stuff we are doing wrong or should do differently?
    /// <summary>
    /// Roslyn extension methods to help improve readability of code.
    /// </summary>
    public static class MiCSRoslynExtensions
    {
        public static string NameText(this NamespaceDeclarationSyntax roslynNamespace)
        {
            return ((IdentifierNameSyntax)roslynNamespace.Name).Identifier.ValueText;
        }
    }
}
