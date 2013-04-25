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
                // Todo: Wat?
                throw;
            }
            
        }

        public static NamespaceDeclarationSyntax ParentNamespace(this TypeSymbol typeSymbol)
        {
            //if (typeSymbol.ContainingNamespace.DeclaringSyntaxNodes.Count > 1)
            //    throw new NotSupportedException();

            //return (NamespaceDeclarationSyntax)typeSymbol.ContainingNamespace.DeclaringSyntaxNodes[0];


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
        public static string TypeScriptName(this TypeSymbol typeSymbol)
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
        public static string TypeScriptName(this SimpleNameSyntax simpleName)
        {
            var type = TypeSymbolGetter.GetTypeSymbol(simpleName);
            return type.TypeScriptName();
        }

        /// <summary>
        /// Returns the script name of a SimpleNameSyntax. Checks
        /// if the SimpleNameSyntax is static reference to a DOM
        /// type and if it is returns the DOM type's script name.
        /// </summary>
        public static string ScriptName(this SimpleNameSyntax simpleName)
        {

            var symbol = TypeSymbolGetter.GetTypeSymbol(simpleName);
            if (symbol.IsDOMType())
            {
                var typeName = symbol.Name;
                var referenceName = simpleName.Identifier.ValueText;

                // Check if simpleName is static reference to DOM type.
                if (referenceName.Equals(typeName))
                {
                    // Is static reference (e.g. Document.GetElementById(...)).
                    return symbol.GetScriptNameAttributeValue();
                }
            }

            var ts = ScriptTypeManager.Instance.SemanticModel.GetSymbolInfo(simpleName).Symbol;
            if (ts is MethodSymbol)
            {
                var method = (MethodSymbol)ts;
                var methodName = method.Name;
                var containingTypeName = method.ContainingType.Name;
                var containingNamespaceName = method.ContainingNamespace.FullName();
                if (method.ContainingType.IsSupportedCoreType())
                {
                    var coreMappings = CoreTypeManager.Instance.CoreMapping.Where(n => 
                        n.NamespaceName.Equals(containingNamespaceName) &&
                        n.Name.Equals(containingTypeName) &&
                        (n.Members.Where(m => m.Name.Equals(methodName))).Count() > 0);

                    if (coreMappings.Count() == 1)
                    {
                        return coreMappings.First().Members.Find(m => m.Name.Equals(methodName)).NameScript;
                    }
                }
            }

            // Script and server side name are the same.
            return simpleName.Identifier.ValueText;
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
            return attributeList.GetAttributesWithName(attributeName).Count() > 0;
        }

        private static IEnumerable<AttributeSyntax> GetAttributesWithName(this AttributeListSyntax attributeList, string attributeName)
        {
            return attributeList.Attributes.Where(a => a.GetName().Equals(attributeName));
        }
        private static AttributeSyntax GetAttribute(this TypeSymbol typeSymbol, string attributeName)
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
        /// <summary>
        /// Retrieve the ScriptSharp defined script name from class attribute
        /// if any exist (otherwise original TypeSymbol name is returned).
        /// </summary>
        private static string GetScriptNameAttributeValue(this TypeSymbol typeSymbol)
        {
            if (!typeSymbol.IsDOMType())
                throw new Exception("It is only possible to get the attribute ScriptName value from a DOM type.");

            var attribute = typeSymbol.GetAttribute("ScriptName");
            if (attribute != null)
            {
                var @value = attribute.ArgumentList.Arguments.First().Expression;
                if (@value is LiteralExpressionSyntax)
                    return ((LiteralExpressionSyntax)@value).Token.ValueText;
                else
                    throw new Exception();
            }

            return typeSymbol.Name;
        }

        //public static bool IsDOMType(this ClassDeclarationSyntax @class)
        //{
        //    return @class.HasAttribute("ScriptName") || @class.HasAttribute("ScriptImport");
        //}

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
