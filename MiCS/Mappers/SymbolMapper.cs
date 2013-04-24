﻿using MiCS.Builders;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS = ScriptSharp.ScriptModel;

namespace MiCS.Mappers
{

    public static class Symbols
    {
        static internal SS.ParameterSymbol Map(this ParameterSyntax p, SS.MemberSymbol ssParent, SS.TypeSymbol valueType)
        {
            return new SS.ParameterSymbol(p.Identifier.ValueText, ssParent, valueType, SS.ParameterMode.InOut);
        }

        static internal SS.MethodSymbol Map(this MethodDeclarationSyntax methodDeclaration, SS.ClassSymbol ssParentClass, SS.NamespaceSymbol ssParentNamespace)
        {
            var ssReturnType = TypeSymbolGetter.GetTypeSymbol(methodDeclaration.ReturnType).Map();
            var ssMethodName = methodDeclaration.Identifier.ValueText;

            var ssMethod = new SS.MethodSymbol(ssMethodName, ssParentClass, ssReturnType);

            foreach (var parameter in methodDeclaration.ParameterList.Parameters)
                ssMethod.AddParameter(parameter.Map(ssMethod, TypeSymbolGetter.GetTypeSymbol(parameter.Type).Map()));

            var implementationStatements = new List<SS.Statement>();
            foreach (var statement in methodDeclaration.Body.Statements)
            {
                implementationStatements.Add(StatementBuilder.Build(statement, ssParentClass, ssMethod));
            }

            // Todo: Consider if this is done right...
            var sI = new SS.SymbolImplementation(implementationStatements, null, "symbolImplementationThisIdentifier_" + ssMethod.GeneratedName);
            ssMethod.AddImplementation(sI);
            
            return ssMethod;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roslynClass"></param>
        /// <param name="ssParentNamespace">
        /// Parent namespace should be provided so its
        /// not required to map the parent namespace in
        /// in the ClassDeclarationSyntax.Map(...)
        /// function as this creates cyclic mapping in
        /// n infinte loop.
        /// </param>
        /// <returns></returns>
        static internal SS.ClassSymbol Map(this ClassDeclarationSyntax @class, SS.NamespaceSymbol ssParentNamespace)
        {
            if (ssParentNamespace == null)
                throw new Exception("Parent namespace reference is required by ScriptSharp infrastructure.");
            
            return new SS.ClassSymbol(@class.Identifier.ValueText, ssParentNamespace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        static internal SS.NamespaceSymbol Map(this NamespaceDeclarationSyntax @namespace)
        {
            return new SS.NamespaceSymbol(@namespace.NameText(), null);
        }

        static internal SS.VariableSymbol Map(this VariableDeclaratorSyntax variable, SS.MemberSymbol ssParentMember, SS.TypeSymbol ssType)
        {
            if (ssParentMember == null)
                throw new Exception("Variable parent member cannot be null.");

            return new SS.VariableSymbol(variable.Identifier.ValueText, ssParentMember, ssType);
        }

        // Todo: Is this the correct place to map VariableDeclarationSyntax?
        static internal SS.VariableDeclarationStatement Map(this VariableDeclarationSyntax variableDeclaration)
        {
            return new SS.VariableDeclarationStatement();
        }

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

            // Todo: Not sure the typeSymbol.Name property is the right one to use? Thinking that using the fullname (System.String and not just String) would be better.
            switch (typeSymbol.Name)
            {
                // Todo: Where is float? IntrinsicType.Number?
                //case IntrinsicType.Object:
                //    mappedTypeName = "Object";
                //    break;
                //case "Boolean":
                //    mappedTypeName = "Boolean";
                //    break;
                //case "String":
                //    mappedTypeName = "String";
                //    break;
                //case "Int32":
                //    mappedTypeName = "Int32";
                //    break;
                //case "UInt32":
                //    mappedTypeName = "UInt32";
                //    break;
                //case "Int64":
                //    mappedTypeName = "Int64";
                //    break;
                //case "UInt64":
                //    mappedTypeName = "UInt64";
                //    break;
                //case "Int16":
                //    mappedTypeName = "Int16";
                //    break;
                //case "UInt16":
                //    mappedTypeName = "UInt16";
                //    break;
                case "Decimal":
                    mappedTypeName = "Decimal";
                    break;
                case "Double":
                    mappedTypeName = "Double";
                    break;
                case "Void":
                    mappedTypeName = "Void";
                    break;
                case "Array":
                    mappedTypeName = "Array";
                    namespaceName = typeSymbol.BaseType.ContainingNamespace.FullName();
                    break;
                //case IntrinsicType.Dictionary:
                //    mappedTypeName = "Dictionary";
                //    mappedNamespace = "System.Collections";
                //    break;
                //case IntrinsicType.GenericList:
                //    mappedTypeName = "List`1";
                //    mappedNamespace = "System.Collections.Generic";
                //    break;
                //case IntrinsicType.GenericDictionary:
                //    mappedTypeName = "Dictionary`2";
                //    mappedNamespace = "System.Collections.Generic";
                //    break;
                //case IntrinsicType.Type:
                //    mappedTypeName = "Type";
                //    break;
                //case IntrinsicType.IEnumerator:
                //    mappedTypeName = "IEnumerator";
                //    mappedNamespace = "System.Collections";
                //    break;
                //case IntrinsicType.Enum:
                //    mappedTypeName = "Enum";
                //    break;
                //case IntrinsicType.Exception:
                //    mappedTypeName = "Exception";
                //    break;
                //case IntrinsicType.Script:
                //    mappedTypeName = "Script";
                //    break;
                //case IntrinsicType.Number:
                //    mappedTypeName = "Number";
                //    break;
                //case IntrinsicType.Arguments:
                //    mappedTypeName = "Arguments";
                //    break;
                //case IntrinsicType.Nullable:
                //    mappedTypeName = "Nullable`1";
                //    break;
                default:
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
                        mappedNamespaceName = CoreTypeManager.GetCoreScriptTypeNamespace(typeSymbol).FullName(); // Todo: Not sure if this is required but seems more correct to apply the actual namespace.

                    
                    ssType = new SS.ClassSymbol(mappedTypeName, new SS.NamespaceSymbol(namespaceName, null));

                    if (isArray)
                    {
                        SS.SymbolSet symbolSet = new SS.SymbolSet();

                        var shit = symbolSet.CreateArrayTypeSymbol(((ArrayTypeSymbol)typeSymbol).ElementType.Map());

                        return shit;
                        //var lolarrayTypeSymbol = (ArrayTypeSymbol)typeSymbol;

                        //var ssElementSymbel = lolarrayTypeSymbol.ElementType.Map();
                            
                        //var _systemNamespace = new SS.NamespaceSymbol("System", null);
                        //SS.TypeSymbol arrayTypeSymbol = new SS.ClassSymbol("Array", _systemNamespace);
                        //    //(SS.TypeSymbol)((SS.ISymbolTable)_systemNamespace).FindSymbol("Array", null, SS.SymbolFilter.Types);                        //Debug.Assert(arrayTypeSymbol != null);

                        //ssType = new SS.ClassSymbol("Array", _systemNamespace);
                        //foreach (SS.MemberSymbol memberSymbol in arrayTypeSymbol.Members)
                        //{
                        //    ssType.AddMember(memberSymbol);
                        //}

                        //SS.IndexerSymbol indexerSymbol = new SS.IndexerSymbol(ssType, null,
                        //                                                SS.MemberVisibility.Public);
                        //indexerSymbol.SetScriptIndexer();
                        //ssType.AddMember(indexerSymbol);
                        //ssType.SetIgnoreNamespace();
                        //ssType.SetArray();



                        //var i = new SS.IndexerSymbol(ssType, null);
                        //i.SetScriptIndexer();
                        //ssType.AddMember(i);
                        //ssType.SetIgnoreNamespace();
                        //ssType.SetArray();
                        
                    }

                    if (!typeSymbol.IsUserType())
                        ssType.SetIgnoreNamespace();

                    break;
            }


            
            if (ssType == null)
            {
                // Todo: Parent namespace should preferably not be a new namspace object.
                ssType = new SS.ClassSymbol(mappedTypeName, new SS.NamespaceSymbol(namespaceName, null));
            }
            return ssType;
        }

        static internal  SS.IndexerSymbol Map(this BracketedArgumentListSyntax bracketedArgumentList, SS.TypeSymbol ssParentType, SS.TypeSymbol ssPropertyType)
        {
            return new SS.IndexerSymbol(ssParentType, ssPropertyType);
        }
    }


}
