using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{

    /// <summary>
    /// Roslyn extension methods to help improve readability of code.
    /// </summary>
    public static class RoslynExtensions
    {

        /// <summary>
        /// Returns the full namespace name (recursively from nested namespaces).
        /// </summary>
        public static string GetFullName(this NamespaceSymbol @namespace)
        {
            // Containing namespace name is empty string when it is the global namespace.
            if (@namespace.ContainingNamespace == null || String.IsNullOrEmpty(@namespace.ContainingNamespace.Name))
                return @namespace.Name;
            else
                return @namespace.ContainingNamespace.GetFullName() + "." + @namespace.Name;
        }

        /// <summary>
        /// Returns the full namespace name (recursively from nested namespaces).
        /// </summary>
        public static string GetFullName(this NamespaceDeclarationSyntax @namespace)
        {
            return @namespace.Name.GetName();
        }

        /// <summary>
        /// Returns true if the specified type declaration is
        /// a user defined type.
        /// </summary>
        public static bool IsUserType(this ClassDeclarationSyntax classDeclaration)
        {
            return TypeManager.IsUserType(classDeclaration);
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a user defined type.
        /// </summary>
        public static bool IsUserType(this TypeSymbol typeSymbol)
        {
            return TypeManager.IsUserType(typeSymbol);
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a DOM type from the ScriptSharp namespace System.Html.
        /// </summary>
        public static bool IsDOMType(this ClassDeclarationSyntax classDeclaration)
        {
            return TypeManager.IsDOMType(classDeclaration);
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a DOM type from the ScriptSharp namespace System.Html.
        /// </summary>
        public static bool IsDOMType(this TypeSymbol typeSymbol)
        {
            return TypeManager.IsDOMType(typeSymbol);
        }

        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        public static bool IsSupportedCoreType(this TypeSymbol typeSymbol)
        {
            return TypeManager.IsSupportedCoreType(typeSymbol);
        }

        /// <summary>
        /// Returns the script name defined by ScriptSharp if the 
        /// type is a built in JavaScript/DOM type (or a supported 
        /// core type). Otherwise the original type name is return.
        /// </summary>
        public static string GetTypeScriptName(this TypeSymbol typeSymbol)
        {
            return TypeManager.GetTypeScriptName(typeSymbol);
        }

        /// <summary>
        /// Returns the script name of a SimpleNameSyntax. Checks
        /// if the SimpleNameSyntax is static reference to a DOM
        /// type and if it is returns the DOM type's script name.
        /// </summary>
        public static string GetScriptName(this SimpleNameSyntax simpleName)
        {
            return TypeManager.GetScriptName(simpleName);
        }

        #region Region: Get attributes and names of SyntaxNodes

        public static bool HasAttribute(this MethodDeclarationSyntax methodDeclaration, string attributeName)
        {
            if (methodDeclaration.AttributeLists.Any())
            {
                foreach (var attList in methodDeclaration.AttributeLists)
                {
                    if (attList.HasAttribute(attributeName))
                        return true;
                }
            }
            return false;
        }

        public static bool HasAttribute(this ClassDeclarationSyntax classDeclaration, string attributeName)
        {
            if (classDeclaration.AttributeLists.Any())
            {
                foreach (var attList in classDeclaration.AttributeLists)
                {
                    if (attList.HasAttribute(attributeName))
                        return true;
                }
            }
            return false;
        }

        private static bool HasAttribute(this AttributeListSyntax attributeList, string attributeName)
        {
            return attributeList.GetAttributesWithName(attributeName).Count() > 0;
        }

        private static IEnumerable<AttributeSyntax> GetAttributesWithName(this AttributeListSyntax attributeList, string attributeName)
        {
            return attributeList.Attributes.Where(a => a.GetName().Equals(attributeName));
        }

        internal static AttributeSyntax GetAttribute(this TypeSymbol typeSymbol, string attributeName)
        {
            if (typeSymbol.DeclaringSyntaxNodes.Count != 1)
                throw new NotSupportedException();

            if (!(typeSymbol.DeclaringSyntaxNodes[0] is ClassDeclarationSyntax))
                throw new NotSupportedException();

            var @class = ((ClassDeclarationSyntax)typeSymbol.DeclaringSyntaxNodes[0]);
            foreach (var attList in @class.AttributeLists)
            {
                var attributes = attList.GetAttributesWithName(attributeName);
                if (attributes.Count() > 0)
                {
                    return attributes.First();
                }
            }

            return null;
        }

        private static string GetName(this NameSyntax nameSyntax)
        {
            if (nameSyntax is IdentifierNameSyntax)
            {
                return ((IdentifierNameSyntax)nameSyntax).Identifier.ValueText;
            }
            else if (nameSyntax is QualifiedNameSyntax)
            {
                var qualifiedName = (QualifiedNameSyntax)nameSyntax;

                return GetName(qualifiedName.Left) + qualifiedName.DotToken.ValueText + GetName(qualifiedName.Right);
            }
            else
            {
                throw new NotSupportedException("The namesyntax is not supported");
            }
        }

        private static string GetName(this AttributeSyntax attribute)
        {
            return attribute.Name.GetName();
        }

        #endregion

    }
}
