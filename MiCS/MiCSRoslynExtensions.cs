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

        public static bool IsScriptType(this TypeSymbol typeSymbol)
        {
            if (typeSymbol.DeclaringSyntaxNodes.Count != 1)
                throw new NotSupportedException();

            var declaration = typeSymbol.DeclaringSyntaxNodes[0];
            if (declaration is ClassDeclarationSyntax)
            {
                var @class = (ClassDeclarationSyntax)declaration;
                return @class.IsScriptType();
            }

            return false;
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
            if (node.Parent is NamespaceDeclarationSyntax)
                return (NamespaceDeclarationSyntax)node;
            else if (node.Parent == null)
                throw new Exception("No parent namespace was found!");
            else
                return node.Parent.ParentNamespace();
        }

        //public static MethodDeclarationSyntax

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

            var symbol = MiCSManager.MixedSideSemanticModel.GetSymbolInfo(identifierName).Symbol;
            var declaration = symbol.DeclaringSyntaxNodes[0];
            if (declaration is ClassDeclarationSyntax)
            {
                var @class = (ClassDeclarationSyntax)declaration;
                if (@class.IsScriptType())
                {
                    // Check if static reference to type.
                    if (@class.Identifier.ValueText.Equals(nameText))
                    {
                        var @type = MiCSManager.MixedSideSemanticModel.GetTypeInfo(identifierName).Type;
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
            if (typeSymbol.IsScriptType())
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

        public static string NameText(this AttributeSyntax attribute)
        {
            return ((IdentifierNameSyntax)attribute.Name).Identifier.ValueText;
        }

        public static bool IsScriptType(this ClassDeclarationSyntax classDeclaration)
        {
            return classDeclaration.HasAttribute("ScriptImport");
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
                        if (att.NameText().Equals(attributeName))
                        {
                            return att;
                        }
                    }
                }
            }
            return null;
        }

        public static bool HasAttribute(this AttributeListSyntax attributeList, string attributeName)
        {
            foreach (AttributeSyntax att in attributeList.Attributes)
            {
                if (att.NameText().Equals(attributeName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
