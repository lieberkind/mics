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

        public static string NameText(this AttributeSyntax attribute)
        {
            return ((IdentifierNameSyntax)attribute.Name).Identifier.ValueText;
        }

        public static bool IsMixedSide(this MethodDeclarationSyntax methodDeclaration)
        {
            if (methodDeclaration.AttributeLists.Any())
            {
                foreach (var attList in methodDeclaration.AttributeLists)
                {
                    foreach (AttributeSyntax att in attList.Attributes)
                    {
                        if (att.NameText().Equals("MixedSide"))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
