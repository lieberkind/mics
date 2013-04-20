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

        public static NamespaceDeclarationSyntax ParentNamespace(this TypeSymbol typeSymbol)
        {
            if (typeSymbol.DeclaringSyntaxNodes.Count != 1)
                throw new NotSupportedException();

            var declaration = typeSymbol.DeclaringSyntaxNodes[0];
            if (declaration is ClassDeclarationSyntax)
            {
                var @class = (ClassDeclarationSyntax)declaration;
                return @class.ParentNamespace();
            }
            else
                throw new NotSupportedException();
        }
        public static NamespaceDeclarationSyntax ParentNamespace(this SyntaxNode node)
        {
            if (node is NamespaceDeclarationSyntax)
                return (NamespaceDeclarationSyntax)node;
            else if (node.Parent == null)
                throw new Exception("No parent namespace was found!");
            else
                return node.Parent.ParentNamespace();
        }


        public static string FullName(this NamespaceSymbol @namespace)
        {
            var fullName = @namespace.FullNameRecursive();
            return fullName; // Remove leading dot.
        }
        private static string FullNameRecursive(this NamespaceSymbol @namespace)
        {
            // Containing namespace name is empty string when it is the global namespace.
            if (@namespace.ContainingNamespace == null || String.IsNullOrEmpty(@namespace.ContainingNamespace.Name))
                return @namespace.Name;
            else
                return @namespace.ContainingNamespace.FullNameRecursive() + "." + @namespace.Name;
        }

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
            return ScriptTypeManager.IsUserType(classDeclaration);
        }
        /// <summary>
        /// Returns true if the specified type is
        /// a user defined type.
        /// </summary>
        public static bool IsUserType(this TypeSymbol typeSymbol)
        {
            return ScriptTypeManager.IsUserType(typeSymbol);
        }

        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        public static bool IsSupportedCoreType(this TypeSymbol typeSymbol)
        {
            return CoreTypeManager.IsSupportedCoreType(typeSymbol);
        }
        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        public static bool IsSupportedCoreType(this SimpleNameSyntax simpleName)
        {
            return CoreTypeManager.IsSupportedCoreType(simpleName);
        }

        /// <summary>
        /// Returns the script name defined by ScriptSharp if the 
        /// type is a built in JavaScript/DOM type (or a supported 
        /// core type). Otherwise the original type name is return.
        /// </summary>
        public static string ScriptName(this TypeSymbol typeSymbol)
        {
            if (typeSymbol.IsUserType())
                return typeSymbol.Name;

            if (typeSymbol.IsSupportedCoreType())
                return CoreTypeManager.ToCoreScriptType(typeSymbol).Name;

            return typeSymbol.Name;
        }
        /// <summary>
        /// Returns the script name defined by ScriptSharp if the 
        /// type is a built in JavaScript/DOM type (or a supported 
        /// core type). Otherwise the original type name is return.
        /// </summary>
        public static string ScriptName(this SimpleNameSyntax identifierName)
        {
            var type = TypeSymbolGetter.GetTypeSymbol(identifierName);
            return type.ScriptName();
        }


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
            foreach (AttributeSyntax att in attributeList.Attributes)
            {
                if (att.GetName().Equals(attributeName))
                {
                    return true;
                }
            }
            return false;
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




    }
}
