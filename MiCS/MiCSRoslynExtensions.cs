﻿using Roslyn.Compilers.CSharp;
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

        public static bool IsUserType(this TypeSymbol typeSymbol)
        {
            if (typeSymbol.DeclaringSyntaxNodes.Count != 1)
                throw new NotSupportedException();

            var declaration = typeSymbol.DeclaringSyntaxNodes[0];
            if (declaration is ClassDeclarationSyntax)
            {
                var @class = (ClassDeclarationSyntax)declaration;
                return @class.IsUserType();
            }

            return false;
        }

        public static NamespaceDeclarationSyntax ParentNamespace(this TypeSymbol typeSymbol)
        {
            if (typeSymbol.ContainingNamespace.DeclaringSyntaxNodes.Count > 1)
                throw new NotSupportedException();

            return (NamespaceDeclarationSyntax)typeSymbol.ContainingNamespace.DeclaringSyntaxNodes[0];


            //if (typeSymbol.DeclaringSyntaxNodes.Count != 1)
            //    throw new NotSupportedException();

            //var declaration = typeSymbol.DeclaringSyntaxNodes[0];
            //if (declaration is ClassDeclarationSyntax)
            //{
            //    var @class = (ClassDeclarationSyntax)declaration;
            //    return @class.ParentNamespace();
            //}
            //else
            //    throw new NotSupportedException();
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

        /// <summary>
        /// Returns the script name defined by ScriptSharp if the type is a built
        /// in JavaScript/DOM type. Otherwise the original type name is return.
        /// IdentifierNameSyntax script name can be different from the defined name
        /// e.g. in the Document.HasFocus() call where Document is translated to 
        /// document.HasFocus() (lower case "d"). The method name (HasFocus) is 
        /// translated internally by ScriptSharp (to hasFocus).
        /// </summary>
        public static string ScriptName(this IdentifierNameSyntax identifierName)
        {
            var nameText = identifierName.Identifier.ValueText;

            // Todo: Check if both core types and script types has special script names
            var symbol = MiCSManager.ScriptTypeSemanticModel.GetSymbolInfo(identifierName).Symbol;
            if (symbol == null)
                throw new Exception();
            var declaration = symbol.DeclaringSyntaxNodes[0];
            if (declaration is ClassDeclarationSyntax)
            {
                var @class = (ClassDeclarationSyntax)declaration;
                if (!@class.IsUserType())
                {
                    // Check if static reference to type.
                    if (@class.Identifier.ValueText.Equals(nameText))
                    {
                        var @type = TypeSymbolGetter.GetTypeSymbol(identifierName);
                        return @type.ScriptName();
                    }
                }
            }
            return nameText;
        }

        /// <summary>
        /// Returns the script name defined by ScriptSharp if the type is a built
        /// in JavaScript/DOM type. Otherwise the original type name is return.
        /// </summary>
        public static string ScriptName(this TypeSymbol typeSymbol)
        {
            if (!typeSymbol.IsUserType())
            {
                var declaration = typeSymbol.DeclaringSyntaxNodes[0];
                if (declaration is ClassDeclarationSyntax)
                {
                    var @class = (ClassDeclarationSyntax)declaration;
                    if (@class.HasAttribute("ScriptName"))
                    {
                        var argList = @class.GetAttribute("ScriptName").ArgumentList;
                        if (argList.Arguments.Count != 1)
                            throw new NotSupportedException();

                        var argExpression = argList.Arguments[0].Expression;
                        if (!(argExpression is LiteralExpressionSyntax))
                            throw new NotSupportedException();

                        return ((LiteralExpressionSyntax)argExpression).Token.ValueText;
                    }
                    else if(@class.HasAttribute("ScriptImport"))
                    {
                        return @class.Identifier.ValueText;
                    }
                    else
                        throw new NotSupportedException();
                }
                else
                    throw new NotSupportedException();
            }
            else
            {
                return typeSymbol.Name;
            }
        }

        public static string GetName(this AttributeSyntax attribute)
        {
            return attribute.Name.GetName();
        }

        public static bool IsUserType(this ClassDeclarationSyntax classDeclaration)
        {
            foreach (var member in classDeclaration.Members)
            {
                if (member is MethodDeclarationSyntax)
                {
                    var declaration = ((MethodDeclarationSyntax)member);
                    if (declaration.HasAttribute("MixedSide") ||
                        declaration.HasAttribute("ClientSide"))
                    {
                        return true;
                    }
                }
            }
            return false;
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

        public static AttributeSyntax GetAttribute(this ClassDeclarationSyntax classDeclaration, string attributeName)
        {
            if (classDeclaration.AttributeLists.Any())
            {
                foreach (var attList in classDeclaration.AttributeLists)
                {
                    foreach (AttributeSyntax att in attList.Attributes)
                    {
                        if (att.GetName().Equals(attributeName))
                        {
                            return att;
                        }
                    }
                }
            }
            return null;
        }

        public static string GetFullName(this NamespaceDeclarationSyntax @namespace)
        {
            return @namespace.Name.GetName();
        }

        public static string GetName(this NameSyntax nameSyntax)
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

        public static bool HasAttribute(this AttributeListSyntax attributeList, string attributeName)
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
    }
}
