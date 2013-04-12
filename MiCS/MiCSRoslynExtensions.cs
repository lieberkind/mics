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
            try
            {
                if (roslynNamespace.Name is IdentifierNameSyntax)
                    return ((IdentifierNameSyntax)roslynNamespace.Name).Identifier.ValueText;
                else if (roslynNamespace.Name is QualifiedNameSyntax)
                {
                    return ((QualifiedNameSyntax)roslynNamespace.Name).ToString();
                }
                else
                    throw new NotSupportedException();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        //public static string NameText(this SimpleNameSyntax simpleName)
        //{
        //    return simpleName.ri + qualifiedName.Ri
        //}

        public static string NameText(this AttributeSyntax attribute)
        {
            return ((IdentifierNameSyntax)attribute.Name).Identifier.ValueText;
        }

        public static bool hasAttribute(this MethodDeclarationSyntax methodDeclaration, string attributeName)
        {
            if (methodDeclaration.AttributeLists.Any())
            {
                foreach (var attList in methodDeclaration.AttributeLists)
                {
                    foreach (AttributeSyntax att in attList.Attributes)
                    {
                        if (att.NameText().Equals(attributeName))
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
