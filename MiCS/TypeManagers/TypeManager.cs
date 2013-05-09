using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    class TypeManager
    {
        #region Region: Fields, properties and construction

        private CSharpTypeManager cSharpTypeManager;
        private ScriptSharpTypeManager scriptSharpTypeManager;

        private static TypeManager instance;

        public static MiCSCoreMapping CoreMapping
        {
            get { return instance.scriptSharpTypeManager.CoreMapping; }
        }

        public static Dictionary<string, Dictionary<string, List<string>>> MixedSideMembers
        {
            get { return instance.cSharpTypeManager.MixedSideMembers;  }
        }

        public static Dictionary<string, Dictionary<string, List<string>>> ClientSideMembers
        {
            get { return instance.cSharpTypeManager.ClientSideMembers; }
        }

        public static CompilationUnitSyntax CompilationUnit
        {
            get { return instance.cSharpTypeManager.CompilationUnit; }
        }

        private  TypeManager(SyntaxTree userTree)
        {
            this.cSharpTypeManager = new CSharpTypeManager(userTree);
            this.scriptSharpTypeManager = new ScriptSharpTypeManager();
        }

        public static void Initiate(SyntaxTree userTree)
        {
            instance = new TypeManager(userTree);
        }

        #endregion

        #region Region: CSharpTypeManager functionality

        /// <summary>
        /// Gets the return type from the specified name
        /// </summary>
        public static TypeSymbol GetReturnType(SimpleNameSyntax simpleName)
        {
            return instance.cSharpTypeManager.GetReturnType(simpleName);
        }

        /// <summary>
        /// Returns true if the specified type declaration is
        /// a user defined type.
        /// </summary>
        public static bool IsUserType(ClassDeclarationSyntax classDeclaration)
        {
            return instance.cSharpTypeManager.IsUserType(classDeclaration);
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a user defined type.
        /// </summary>
        public static bool IsUserType(TypeSymbol typeSymbol)
        {
            return instance.cSharpTypeManager.IsUserType(typeSymbol);
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a DOM type from the ScriptSharp namespace System.Html.
        /// </summary>
        public static bool IsDOMType(ClassDeclarationSyntax classDeclaration)
        {
            return classDeclaration.HasAttribute("ScriptName") || classDeclaration.HasAttribute("ScriptImport");
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a DOM type from the ScriptSharp namespace System.Html.
        /// </summary>
        public static bool IsDOMType(TypeSymbol typeSymbol)
        {
            return instance.cSharpTypeManager.IsDOMType(typeSymbol);
        }

        /// <summary>
        /// Determines if a member is mixed side from a namespace name, a type name and a member name
        /// </summary>
        /// <param name="namespaceName">Namespace name</param>
        /// <param name="typeName">Type name</param>
        /// <param name="memberName">Member name</param>
        /// <returns>Returns true if member is mixed side, false otherwise</returns>
        public static bool IsMixedSideMethod(string namespaceName, string typeName, string memberName)
        {
            return instance.cSharpTypeManager.IsMixedSideMethod(namespaceName, typeName, memberName);
        }

        /// <summary>
        /// Determines if a member is client side from a namespace name, a type name and a member name
        /// </summary>
        /// <param name="namespaceName">Namespaace name</param>
        /// <param name="typeName">Type name</param>
        /// <param name="memberName">Member name</param>
        /// <returns>Returns true if member is client side, false otherwise</returns>
        public static bool IsClientSideMethod(string namespaceName, string typeName, string memberName)
        {
            return instance.cSharpTypeManager.IsClientSideMethod(namespaceName, typeName, memberName);
        }

        /// <summary>
        /// Determines if a type is mixed side from a namespace name and a type name
        /// </summary>
        /// <param name="namespaceName">Namespace name</param>
        /// <param name="typeName">Type name</param>
        /// <returns>True if type is mixed side, false otherwise</returns>
        public static bool IsMixedSideType(string namespaceName, string typeName)
        {
            return instance.cSharpTypeManager.IsMixedSideType(namespaceName, typeName);
        }

        /// <summary>
        /// Determines if a type is client side from a namespace name and a type name
        /// </summary>
        /// <param name="namespaceName">Namespace name</param>
        /// <param name="typeName">Type name</param>
        /// <returns>True if type is client side, false otherwise</returns>
        public static bool IsClientSideType(string namespaceName, string typeName)
        { 
            return instance.cSharpTypeManager.IsClientSideType(namespaceName, typeName);
        }

        /// <summary>
        /// Gets a type symbol from the given syntax node
        /// </summary>
        public static TypeSymbol GetTypeSymbol(SyntaxNode node)
        {
            return instance.cSharpTypeManager.GetTypeSymbol(node);
        }

        /// <summary>
        /// Gets a type symbol from the given expression 
        /// </summary>
        public static TypeSymbol GetTypeSymbol(ExpressionSyntax expression)
        {
            return instance.cSharpTypeManager.GetTypeSymbol(expression);
        }

        /// <summary>
        /// Gets symbol info from the specified name
        /// </summary>
        public static SymbolInfo GetSymbolInfo(SimpleNameSyntax simpleName)
        { 
            return instance.cSharpTypeManager.GetSymbolInfo(simpleName);
        }

        #endregion

        #region Region: ScriptSharpTypeManager functionality

        // Todo: This really needs untangling
        public static void VerifyCorrectUseOfSupportedCoreType(InvocationExpressionSyntax invocation)
        {
            if (invocation.Expression is MemberAccessExpressionSyntax)
            {
                var memberAccess = (MemberAccessExpressionSyntax)invocation.Expression;
                var objectReference = (IdentifierNameSyntax)memberAccess.Expression;
                var objectType = GetTypeSymbol(objectReference);
                var namespaceName = objectType.ContainingNamespace.GetFullName();

                if (objectType.IsSupportedCoreType())
                {
                    var mappings = CoreMapping.Where(t => t.NamespaceName.Equals(namespaceName) && t.Name.Equals(objectType.Name));
                    if (mappings.Count() == 1)
                    {
                        var typeMapping = mappings.First();
                        var memberName = memberAccess.Name.Identifier.ValueText;
                        var memberReturnType = GetTypeSymbol(memberAccess.Name);

                        // Verify member use.
                        var memberMappings = typeMapping.Members.Where(m => m.Name.Equals(memberName) && m.ReturnType.Name.Equals(memberReturnType.Name));
                        if (memberMappings.Count() == 1)
                        {
                            // Verify arguments count and types.
                            var memberMapping = memberMappings.First();
                            var memberArguments = invocation.ArgumentList.Arguments;
                            if (memberMapping.Arguments.Count == memberArguments.Count)
                            {
                                for (int i = 0; i < memberMapping.Arguments.Count; i++)
                                {
                                    var argumentType = TypeManager.GetTypeSymbol(memberArguments[i]);
                                    var argumentTypeMapping = memberMapping.Arguments[i];

                                    if (!argumentTypeMapping.Name.Equals(argumentType.Name))
                                        throw new MemberSignatureArgumentTypeNotMappedException("Argument with index '" + i + "' with type '" + argumentType.Name + "' on member: '" + namespaceName + "." + objectType.Name + "." + memberName + "' is not correctly mapped.");
                                }
                            }
                            else
                                throw new MemberSignatureNotMappedException("Arguments on member: '" + namespaceName + "." + objectType.Name + "." + memberName + "' are not correctly mapped.");
                        }
                        else
                            throw new MemberNotMappedException("Member: '" + namespaceName + "." + objectType.Name + "." + memberName + "' is not currently (or correctly) mapped.");
                    }
                    else
                        throw new TypeNotMappedException("Type: '" + namespaceName + "." + objectType.Name + "' is not currently (or correctly) mapped.");
                }
            }
        }

        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        public static bool IsSupportedCoreType(string namespaceName, string typeName)
        {
            return instance.scriptSharpTypeManager.IsSupportedCoreType(namespaceName, typeName);
        }

        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        public static bool IsSupportedCoreType(TypeSymbol typeSymbol)
        {
            return instance.scriptSharpTypeManager.IsSupportedCoreType(typeSymbol);
        }

        /// <summary>
        /// Returns true if the specified type is a core script type..
        /// </summary>
        public static bool IsCoreScriptType(TypeSymbol typeSymbol)
        {
            return instance.scriptSharpTypeManager.IsCoreScriptType(typeSymbol);
        }

        /// <summary>
        /// Returns true if the specified type is a core script type..
        /// </summary>
        public static bool IsCoreScriptType(SimpleNameSyntax simpleName)
        {
            return instance.scriptSharpTypeManager.IsCoreScriptType(simpleName);
        }

        /// <summary>
        /// Returns the equivalent script core type according
        /// to the specified C# type (if it is a core type and
        /// it is supported by the mapping specification).
        /// </summary>
        public static TypeSymbol ToCoreScriptType(TypeSymbol typeSymbol)
        {
            return instance.scriptSharpTypeManager.ToCoreScriptType(typeSymbol);
        }

        /// <summary>
        /// Retrieve the TypeSymbols containing NamespaceSymbol from 
        /// the ScriptSharp core type semanic model.
        /// </summary>
        public static NamespaceSymbol GetCoreScriptTypeNamespace(TypeSymbol typeSymbol)
        {
            return instance.scriptSharpTypeManager.GetCoreScriptTypeNamespace(typeSymbol);
        }

        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        public static bool IsSupportedCoreType(SimpleNameSyntax simpleName)
        {
            var typeSymbol = GetTypeSymbol(simpleName);
            return instance.scriptSharpTypeManager.IsSupportedCoreType(typeSymbol);
        }

        /// <summary>
        /// Returns the script name defined by ScriptSharp if the 
        /// type is a built in JavaScript/DOM type (or a supported 
        /// core type). Otherwise the original type name is return.
        /// </summary>
        public static string GetTypeScriptName(TypeSymbol typeSymbol)
        {
            if (typeSymbol.IsUserType())
                return typeSymbol.Name;

            if (typeSymbol.IsSupportedCoreType())
                return ToCoreScriptType(typeSymbol).Name;

            return typeSymbol.Name;
        }

        /// <summary>
        /// Returns the script name of a SimpleNameSyntax. Checks
        /// if the SimpleNameSyntax is static reference to a DOM
        /// type and if it is returns the DOM type's script name.
        /// </summary>
        public static string GetScriptName(SimpleNameSyntax simpleName)
        {

            var symbol = TypeManager.GetTypeSymbol(simpleName);
            if (symbol.IsDOMType())
            {
                var typeName = symbol.Name;
                var referenceName = simpleName.Identifier.ValueText;

                // Check if simpleName is static reference to DOM type.
                if (referenceName.Equals(typeName))
                {
                    // Is static reference (e.g. Document.GetElementById(...)).
                    return TypeManager.GetScriptNameAttributeValue(symbol);
                }
            }

            // Core type script name mapping (E.g Regex.IsMatch(...) to RegExp.test(...)).
            var ts = TypeManager.GetSymbolInfo(simpleName).Symbol;
            if (ts is MethodSymbol)
            {
                var method = (MethodSymbol)ts;
                var methodName = method.Name;
                var containingTypeName = method.ContainingType.Name;
                var containingNamespaceName = method.ContainingNamespace.GetFullName();
                if (method.ContainingType.IsSupportedCoreType())
                {
                    var coreMappings = TypeManager.CoreMapping.Where(n =>
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

        /// <summary>
        /// Retrieve the ScriptSharp defined script name from class attribute
        /// if any exist (otherwise original TypeSymbol name is returned).
        /// </summary>
        private static string GetScriptNameAttributeValue(TypeSymbol typeSymbol)
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


        #endregion


    }
}
