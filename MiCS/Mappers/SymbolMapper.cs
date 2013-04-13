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

    public static class Symbols
    {
        static internal SS.ParameterSymbol Map(this ParameterSyntax p)
        {
            return new SS.ParameterSymbol(p.Identifier.ValueText, null, null, SS.ParameterMode.InOut);
        }

        static internal SS.MethodSymbol Map(this MethodDeclarationSyntax methodDeclaration, 
            SS.ClassSymbol parentClassReference, 
            SS.NamespaceSymbol parentNamespaceReference)
        {
            // Todo: Should this random namespace symbol be used here or should the actual namespace be applied!
            // Namespace is apparently only required for return type! Should it then be the namespase of the return type?
            //var parentNamespace = new ScriptSharp.ScriptModel.NamespaceSymbol("ns", null);

            var returnTypeStr = "";
            
            if (methodDeclaration.ReturnType is IdentifierNameSyntax)      // Custom complex types.
                returnTypeStr = ((IdentifierNameSyntax)methodDeclaration.ReturnType).Identifier.ValueText;
            else if (methodDeclaration.ReturnType is PredefinedTypeSyntax) // Predefined types like void and string
                returnTypeStr = ((PredefinedTypeSyntax)methodDeclaration.ReturnType).Keyword.ValueText;
            else
                throw new NotSupportedException("Method declaration return type is currently not supported.");

            var returnType = new SS.ClassSymbol(returnTypeStr, parentNamespaceReference);
            var name = methodDeclaration.Identifier.ValueText;

            var method = new SS.MethodSymbol(name, parentClassReference, returnType);

            var implementationStatements = new List<SS.Statement>();
            foreach (var roslynStatement in methodDeclaration.Body.Statements)
            {
                implementationStatements.Add(StatementBuilder.Build(roslynStatement, parentClassReference));
            }
            var sI = new SS.SymbolImplementation(implementationStatements, null, "symbolImplementationThisIdentifier_" + method.GeneratedName);
            method.AddImplementation(sI);
            return method;

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


        static internal SS.VariableSymbol Map(this VariableDeclaratorSyntax variable, TypeInfo typeInfo)
        {
            //var typeInfo = MiCSManager.SemanticModel.GetTypeInfo(variable.Parent);

            // Todo: Pass parent value and not null.
            return new SS.VariableSymbol(variable.Identifier.ValueText, null, typeInfo.Type.Map());
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


            string mappedTypeName = null;
            string mappedNamespace = null;


            // Todo: Not sure the typeSymbol.Name property is the right one to use? Thinking that using the fullname (System.String and not just String) would be better.
            switch (typeSymbol.Name)
            {
                // Todo: Where is float? IntrinsicType.Number?
                //case IntrinsicType.Object:
                //    mappedTypeName = "Object";
                //    break;
                case "Boolean":
                    mappedTypeName = "Boolean";
                    break;
                case "String":
                    mappedTypeName = "String";
                    break;
                case "Int32":
                    mappedTypeName = "Int32";
                    break;
                case "UInt32":
                    mappedTypeName = "UInt32";
                    break;
                case "Int64":
                    mappedTypeName = "Int64";
                    break;
                case "UInt64":
                    mappedTypeName = "UInt64";
                    break;
                case "Int16":
                    mappedTypeName = "Int16";
                    break;
                case "UInt16":
                    mappedTypeName = "UInt16";
                    break;
                //case IntrinsicType.Byte:
                //    mappedTypeName = "Byte";
                //    break;
                //case IntrinsicType.SignedByte:
                //    mappedTypeName = "SByte";
                //    break;
                //case IntrinsicType.Single:
                //    mappedTypeName = "Single";
                //    break;
                //case IntrinsicType.Date:
                //    mappedTypeName = "Date";
                //    break;
                case "Decimal":
                    mappedTypeName = "Decimal";
                    break;
                case "Double":
                    mappedTypeName = "Double";
                    break;
                //case IntrinsicType.Delegate:
                //    mappedTypeName = "Delegate";
                //    break;
                //case IntrinsicType.Function:
                //    mappedTypeName = "Function";
                //    break;
                //case IntrinsicType.Void:
                //    mappedTypeName = "Void";
                //    break;
                //case IntrinsicType.Array:
                //    mappedTypeName = "Array";
                //    break;
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
                    // Todo: Test if there will be a problem with custom and unsupported built-in types?
                    var isSupportedType =
                           MiCSManager.ClientSideMembers.ContainsKey(typeSymbol.Name)
                        || MiCSManager.MixedSideMembers.ContainsKey(typeSymbol.Name);

                    if(!isSupportedType)
                        throw new NotSupportedException("TypeSymbol type is currently not supported.");

                    mappedTypeName = typeSymbol.ScriptName();
                    break;
            }

            // Todo: Parent namespace should probably not be a dummy namspace.
            return new SS.ClassSymbol(mappedTypeName, new SS.NamespaceSymbol("ns", null));
        }

    }
}
