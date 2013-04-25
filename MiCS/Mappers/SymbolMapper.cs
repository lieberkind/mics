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

            var ssReturnType = returnType.Map();
            var ssMethodName = methodDeclaration.Identifier.ValueText;

            var ssMethod = new SS.MethodSymbol(ssMethodName, ssParentClass, ssReturnType);

            foreach (var parameter in methodDeclaration.ParameterList.Parameters)
                ssMethod.AddParameter(parameter.Map(ssMethod, TypeManager.GetTypeSymbol(parameter.Type).Map()));

            var implementationStatements = new List<SS.Statement>();
            foreach (var statement in methodDeclaration.Body.Statements)
            {
                implementationStatements.Add(StatementBuilder.Build(statement, ssParentClass, ssMethod));
            }

            /*
             * Leaving the second parameter (SymbolScope scope) as null as 
             * the generated script code is still valid and therefore doesn't
             * prevent us from showing prove of concept. If the SymbolScope
             * parameter is set to null this is gracefully handled (see 
             * ScriptSharp.ScriptCompiler.cs line 71).
             */
            var sI = new SS.SymbolImplementation(implementationStatements, null, "this");
            ssMethod.AddImplementation(sI);
            
            return ssMethod;
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
            return new SS.NamespaceSymbol(@namespace.NameText(), null);
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
        // Todo: Maybe try and clean the type symbol mapper extension method...
        /// <summary>
        /// Returns mapped ScriptSharp TypeSymbol object.
        /// </summary>
        /// <param name="typeSymbol">Roslyn type AST node.</param>
        static internal SS.TypeSymbol Map(this TypeSymbol typeSymbol)
        {
            #region Region: Possibly relevant ScriptSharpCode
                //            /// <summary>
                ///// This maps C# intrinsic types (managed types that have an equivalent
                ///// C# keyword)
                ///// </summary>
                //public TypeSymbol ResolveIntrinsicType(IntrinsicType type) {
                //    string mappedTypeName = null;
                //    string mappedNamespace = null;

                //    switch (type) {
                //        case IntrinsicType.Object:
                //            mappedTypeName = "Object";
                //            break;
                //        case IntrinsicType.Boolean:
                //            mappedTypeName = "Boolean";
                //            break;
                //        case IntrinsicType.String:
                //            mappedTypeName = "String";
                //            break;
                //        case IntrinsicType.Integer:
                //            mappedTypeName = "Int32";
                //            break;
                //        case IntrinsicType.UnsignedInteger:
                //            mappedTypeName = "UInt32";
                //            break;
                //        case IntrinsicType.Long:
                //            mappedTypeName = "Int64";
                //            break;
                //        case IntrinsicType.UnsignedLong:
                //            mappedTypeName = "UInt64";
                //            break;
                //        case IntrinsicType.Short:
                //            mappedTypeName = "Int16";
                //            break;
                //        case IntrinsicType.UnsignedShort:
                //            mappedTypeName = "UInt16";
                //            break;
                //        case IntrinsicType.Byte:
                //            mappedTypeName = "Byte";
                //            break;
                //        case IntrinsicType.SignedByte:
                //            mappedTypeName = "SByte";
                //            break;
                //        case IntrinsicType.Single:
                //            mappedTypeName = "Single";
                //            break;
                //        case IntrinsicType.Date:
                //            mappedTypeName = "Date";
                //            break;
                //        case IntrinsicType.Decimal:
                //            mappedTypeName = "Decimal";
                //            break;
                //        case IntrinsicType.Double:
                //            mappedTypeName = "Double";
                //            break;
                //        case IntrinsicType.Delegate:
                //            mappedTypeName = "Delegate";
                //            break;
                //        case IntrinsicType.Function:
                //            mappedTypeName = "Function";
                //            break;
                //        case IntrinsicType.Void:
                //            mappedTypeName = "Void";
                //            break;
                //        case IntrinsicType.Array:
                //            mappedTypeName = "Array";
                //            break;
                //        case IntrinsicType.Dictionary:
                //            mappedTypeName = "Dictionary";
                //            mappedNamespace = "System.Collections";
                //            break;
                //        case IntrinsicType.GenericList:
                //            mappedTypeName = "List`1";
                //            mappedNamespace = "System.Collections.Generic";
                //            break;
                //        case IntrinsicType.GenericDictionary:
                //            mappedTypeName = "Dictionary`2";
                //            mappedNamespace = "System.Collections.Generic";
                //            break;
                //        case IntrinsicType.Type:
                //            mappedTypeName = "Type";
                //            break;
                //        case IntrinsicType.IEnumerator:
                //            mappedTypeName = "IEnumerator";
                //            mappedNamespace = "System.Collections";
                //            break;
                //        case IntrinsicType.Enum:
                //            mappedTypeName = "Enum";
                //            break;
                //        case IntrinsicType.Exception:
                //            mappedTypeName = "Exception";
                //            break;
                //        case IntrinsicType.Script:
                //            mappedTypeName = "Script";
                //            break;
                //        case IntrinsicType.Number:
                //            mappedTypeName = "Number";
                //            break;
                //        case IntrinsicType.Arguments:
                //            mappedTypeName = "Arguments";
                //            break;
                //        case IntrinsicType.Nullable:
                //            mappedTypeName = "Nullable`1";
                //            break;
                //        default:
                //            Debug.Fail("Unmapped intrinsic type " + type);
                //            break;
                //    }
                //}



            //        private Expression ProcessLiteralNode(LiteralNode node) {
            //LiteralToken token = (LiteralToken)node.Token;

            //string systemTypeName = null;

            //switch (token.LiteralType) {
            //    case LiteralTokenType.Null:
            //        systemTypeName = "Object";
            //        break;
            //    case LiteralTokenType.Boolean:
            //        systemTypeName = "Boolean";
            //        break;
            //    case LiteralTokenType.Char:
            //        systemTypeName = "Char";
            //        break;
            //    case LiteralTokenType.String:
            //        systemTypeName = "String";
            //        break;
            //    case LiteralTokenType.Int:
            //        systemTypeName = "Int32";
            //        break;
            //    case LiteralTokenType.UInt:
            //        systemTypeName = "UInt32";
            //        break;
            //    case LiteralTokenType.Long:
            //        systemTypeName = "Int64";
            //        break;
            //    case LiteralTokenType.ULong:
            //        systemTypeName = "UInt64";
            //        break;
            //    case LiteralTokenType.Float:
            //        systemTypeName = "Single";
            //        break;
            //    case LiteralTokenType.Double:
            //        systemTypeName = "Double";
            //        break;
            //    case LiteralTokenType.Decimal:
            //        systemTypeName = "Decimal";
            //        break;
            //    default:
            //        Debug.Fail("Unknown Literal Token Type: " + token.LiteralType);
            //        break;
            //}
            //        }
            #endregion

            if (typeSymbol is ErrorTypeSymbol)
                throw new Exception("Not possible to map error type!");


            string mappedTypeName = null;
            string namespaceName = null;
            string mappedNamespaceName;

            SS.ClassSymbol ssType = null;

            var isArray = false;
            if (typeSymbol is ArrayTypeSymbol)
            {
                mappedTypeName = "Array";
                namespaceName = typeSymbol.BaseType.ContainingNamespace.FullName();
                isArray = true;
            }
            else
            {
                namespaceName = typeSymbol.ContainingNamespace.FullName();
            }

            var isSupportedClientSideType =
                MiCSManager.ClientSideMembers.ContainsKey(namespaceName) &&
                MiCSManager.ClientSideMembers[namespaceName].ContainsKey(typeSymbol.Name);

            var isSupportedMixedSideType =
                MiCSManager.MixedSideMembers.ContainsKey(namespaceName) &&
                MiCSManager.MixedSideMembers[namespaceName].ContainsKey(typeSymbol.Name);

            var isSupportedCoreType = typeSymbol.IsSupportedCoreType();

            var isSupportedType = 
                isSupportedClientSideType || 
                isSupportedMixedSideType || 
                isSupportedCoreType;

            if(!isSupportedType)
                throw new NotSupportedException("TypeSymbol type is currently not supported.");

            mappedTypeName = typeSymbol.TypeScriptName();
            mappedNamespaceName = namespaceName;
            if (isSupportedCoreType)
                mappedNamespaceName = ScriptSharpTypeManager.GetCoreScriptTypeNamespace(typeSymbol).FullName(); // Todo: Not sure if this is required but seems more correct to apply the actual namespace.

            // Todo: Parent namespace should preferably not be a new namspace object.
            ssType = new SS.ClassSymbol(mappedTypeName, new SS.NamespaceSymbol(namespaceName, null));

            if (isArray)
            {
                SS.SymbolSet symbolSet = new SS.SymbolSet();
                return symbolSet.CreateArrayTypeSymbol(((ArrayTypeSymbol)typeSymbol).ElementType.Map());
            }

            if (!typeSymbol.IsUserType())
                ssType.SetIgnoreNamespace();

            if (ssType == null)
            {
                // Todo: Parent namespace should preferably not be a new namspace object.
                ssType = new SS.ClassSymbol(mappedTypeName, new SS.NamespaceSymbol(namespaceName, null));
            }
            return ssType;
        }

    }
}
