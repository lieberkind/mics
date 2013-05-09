using MiCS.Builders;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS = ScriptSharp.ScriptModel;

namespace MiCS.Mappers
{
    /// <summary>
    /// Class with extension mapping methods that are used to map
    /// from Roslyn AST nodes to ScriptSharp symbols
    /// </summary>
    internal static class SymbolMapper
    {
        /// <summary>
        /// Returns a mapped ScriptSharp ParameterSymbol. Used in methods and constructors.
        /// </summary>
        /// <param name="parameter">Roslyn parameter AST node</param>
        /// <param name="ssParent">The parent ScriptSharp MethodSymbol where the parameter is used.</param>
        /// <param name="ssValueType">The ScriptSharp type of the parameter.</param>
        /// <remarks>
        /// ParameterMode (or parameter modifier settings) is set to "In". 
        /// If the other parameterModes are needed support this needs to be implemented.
        /// We have left this setting as unchangeable as it is not that relevant for our
        /// case study.
        /// </remarks>
        static internal SS.ParameterSymbol Map(this ParameterSyntax parameter, SS.MemberSymbol ssParent, SS.TypeSymbol ssValueType)
        {
            return new SS.ParameterSymbol(parameter.Identifier.ValueText, ssParent, ssValueType, SS.ParameterMode.In);
        }

        /// <summary>
        /// Returns a mapped ScriptSharp MethodSymbol that represents a method declaration.
        /// </summary>
        /// <param name="methodDeclaration">Roslyn method declaration AST node.</param>
        /// <param name="ssParentClass">The ScriptSharp class containing this method</param>
        /// <param name="ssParentNamespace">The ScriptSharp namespace containing this method</param>
        static internal SS.MethodSymbol Map(this MethodDeclarationSyntax methodDeclaration, SS.ClassSymbol ssParentClass, SS.NamespaceSymbol ssParentNamespace)
        {
            var returnType = TypeManager.GetTypeSymbol(methodDeclaration.ReturnType);
            // Todo: Move return type mapping to builder class.
            var ssReturnType = returnType.Map();
            var ssMethodName = methodDeclaration.Identifier.ValueText;

            return new SS.MethodSymbol(ssMethodName, ssParentClass, ssReturnType);
        }

        /// <summary>
        /// Returns a mapped ScriptSharp ClassSymbol representing a class declaration.
        /// </summary>
        /// <param name="class">Roslyn class declaration AST node</param>
        /// <param name="ssParentNamespace">Parent ScriptSharp namespace containing this class</param>
        static internal SS.ClassSymbol Map(this ClassDeclarationSyntax @class, SS.NamespaceSymbol ssParentNamespace)
        {
            if (ssParentNamespace == null)
                throw new Exception("Parent namespace reference is required by ScriptSharp infrastructure.");
            
            return new SS.ClassSymbol(@class.Identifier.ValueText, ssParentNamespace);
        }

        /// <summary>
        /// Returns mapped ScriptShar NamespaceSymbol.
        /// </summary>
        /// <param name="namespace">Roslyn namespace declaration AST node</param>
        static internal SS.NamespaceSymbol Map(this NamespaceDeclarationSyntax @namespace)
        {
            return new SS.NamespaceSymbol(@namespace.GetFullName(), null);
        }

        /// <summary>
        /// Returns mapped ScriptSharp VariableSymbol e.g. representing the variable
        /// in a VariableDeclarationStatement.
        /// </summary>
        /// <param name="variable">Roslyn VariableDeclarator AST node.</param>
        /// <param name="ssParentMember">The parent ScriptSharp MethodSymbol.</param>
        /// <param name="ssType">The ScriptSharp type of the variable.</param>
        /// <returns></returns>
        static internal SS.VariableSymbol Map(this VariableDeclaratorSyntax variable, SS.MemberSymbol ssParentMember, SS.TypeSymbol ssType)
        {
            if (ssParentMember == null)
                throw new Exception("Variable parent member cannot be null.");

            return new SS.VariableSymbol(variable.Identifier.ValueText, ssParentMember, ssType);
        }

        /// <summary>
        /// Returns mapped ScriptSharp IndexerSymbol e.g. used for accessing Array elements.
        /// </summary>
        /// <param name="bracketedArgumentList">Roslyn bracketed argument list AST node</param>
        /// <param name="ssParentType">Parent ScriptSharp indexer enabled type (e.g Array).</param>
        /// <param name="ssPropertyType">ScriptSharp type of the indexer argument.</param>
        static internal SS.IndexerSymbol Map(this BracketedArgumentListSyntax bracketedArgumentList, SS.TypeSymbol ssParentType, SS.TypeSymbol ssPropertyType)
        {
            return new SS.IndexerSymbol(ssParentType, ssPropertyType);
        }

        /// <summary>
        /// Returns mapped ScriptSharp TypeSymbol object.
        /// </summary>
        /// <param name="typeSymbol">Roslyn type AST node. Attention! if null is provided as this parameter it is always mapped to System.Object.</param>
        static internal SS.TypeSymbol Map(this TypeSymbol typeSymbol)
        {
            if (typeSymbol is ErrorTypeSymbol)
                throw new Exception("Not possible to map error type!");

            SS.ClassSymbol ssType = null;

            ssType = TryMapToNullType(typeSymbol);
            if (ssType != null)
                return ssType;

            ssType = TryMapToArrayType(typeSymbol);
            if (ssType != null)
                return ssType;

            ssType = TryMapToSupportedCoreType(typeSymbol);
            if (ssType != null)
                return ssType;


            var mappedTypeName = typeSymbol.GetTypeScriptName();
            var namespaceName = typeSymbol.ContainingNamespace.GetFullName();

            var isClientSideType = TypeManager.IsClientSideType(namespaceName, typeSymbol.Name);
            var isMixedSideType = TypeManager.IsMixedSideType(namespaceName, typeSymbol.Name);

            if (isClientSideType || isMixedSideType)
            {
                ssType = new SS.ClassSymbol(mappedTypeName, new SS.NamespaceSymbol(namespaceName, null));

                if (!typeSymbol.IsUserType())
                    ssType.SetIgnoreNamespace();

                return ssType;
            }
            else
                throw new NotSupportedException("TypeSymbol type is currently not supported.");

        }

        /// <summary>
        /// Mapping null as ScriptSharp (and EcmaScript specification)
        /// null -> Object. The only time when typeSymbol has the value
        /// null is when the type of a NullLiteral is requested. This
        /// however is only guaranteed if the TypeSymbolWalker.GetTypeSymbol(...)
        /// method is used to retreive the typeSymbol. The TypeSymbolWalker
        /// only returns null on null literals and otherwise throw an
        /// Exception if a type is not found.
        /// </summary>
        private static SS.ClassSymbol TryMapToNullType(TypeSymbol typeSymbol)
        {
            if (typeSymbol == null)
                return new SS.ClassSymbol("Object", new SS.NamespaceSymbol("System", null));
            else
                return null;

        }

        /// <summary>
        /// Map to Array if specified typeSymbol is of ArrayTypeSymbol.
        /// The ScriptSharp array type is created using the SymbolSet
        /// factory method.
        /// </summary>
        private static SS.ClassSymbol TryMapToArrayType(TypeSymbol typeSymbol)
        {
            if (typeSymbol is ArrayTypeSymbol)
            {
                SS.SymbolSet symbolSet = new SS.SymbolSet();
                return (SS.ClassSymbol)symbolSet.CreateArrayTypeSymbol(((ArrayTypeSymbol)typeSymbol).ElementType.Map());
            }
            else
                return null;
        }

        /// <summary>
        /// Map to supported ScriptSharp core type if the
        /// specified type symbol is a supported C# core type.
        /// </summary>
        private static SS.ClassSymbol TryMapToSupportedCoreType(TypeSymbol typeSymbol)
        {
            if (typeSymbol.IsSupportedCoreType())
            {
                var mappedTypeName = typeSymbol.GetTypeScriptName();
                var mappedNamespaceName = TypeManager.GetCoreScriptTypeNamespace(typeSymbol).GetFullName();

                // Todo: Parent namespace should preferably not be a new namspace object.
                var ssType = new SS.ClassSymbol(mappedTypeName, new SS.NamespaceSymbol(mappedNamespaceName, null));
                ssType.SetIgnoreNamespace();
                return ssType;
            }
            else
                return null;
        }
    }
}
