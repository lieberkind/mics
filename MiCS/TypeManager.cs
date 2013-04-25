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

        private TypeManager(CSharpTypeManager cSharpTypeManager, ScriptSharpTypeManager scriptSharpTypeManager)
        {
            this.cSharpTypeManager = cSharpTypeManager;
            this.scriptSharpTypeManager = scriptSharpTypeManager;
        }

        private TypeManager(SyntaxTree userTree)
        {
            this.cSharpTypeManager = new CSharpTypeManager(userTree);
            this.scriptSharpTypeManager = new ScriptSharpTypeManager();
        }

        // Todo: Find out what to do with this
        public static void Initiate(CSharpTypeManager scriptTypeManager, ScriptSharpTypeManager coreTypeManager)
        {
            //if (instance == null)            
            instance = new TypeManager(scriptTypeManager, coreTypeManager);
        }

        public static void Initiate(SyntaxTree userTree)
        {
            instance = new TypeManager(userTree);
        }

        #region CSharpTypeManager functionality
        public static TypeSymbol GetReturnType(SimpleNameSyntax simpleName)
        {
            return instance.cSharpTypeManager.GetReturnType(simpleName);
        }

        public static bool IsUserType(ClassDeclarationSyntax classDeclaration)
        {
            return instance.cSharpTypeManager.IsUserType(classDeclaration);
        }

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

        public static bool IsDOMType(TypeSymbol typeSymbol)
        {
            return instance.cSharpTypeManager.IsDOMType(typeSymbol);
        }

        public static bool IsMixedSideMethod(string namespaceName, string typeName, string memberName)
        {
            return instance.cSharpTypeManager.IsMixedSideMethod(namespaceName, typeName, memberName);
        }

        public static bool IsClientSideMethod(string namespaceName, string typeName, string memberName)
        {
            return instance.cSharpTypeManager.IsClientSideMethod(namespaceName, typeName, memberName);
        }

        public static bool IsMixedSideType(string namespaceName, string typeName)
        {
            return instance.cSharpTypeManager.IsMixedSideType(namespaceName, typeName);
        }

        public static bool IsClientSideType(string namespaceName, string typeName)
        { 
            return instance.cSharpTypeManager.IsClientSideType(namespaceName, typeName);
        }

        public static TypeSymbol GetTypeSymbol(SyntaxNode node)
        {
            return instance.cSharpTypeManager.GetTypeSymbol(node);
        }

        public static TypeSymbol GetTypeSymbol(ExpressionSyntax expression)
        {
            return instance.cSharpTypeManager.GetTypeSymbol(expression);
        }

        public static SymbolInfo GetSymbolInfo(SimpleNameSyntax simpleName)
        { 
            return instance.cSharpTypeManager.GetSymbolInfo(simpleName);
        }
        #endregion

        #region ScriptSharpTypeManager

        // Todo: This really needs untangling
        public static void VerifyCorrectUseOfSupportedCoreType(InvocationExpressionSyntax invocation)
        {
            if (invocation.Expression is MemberAccessExpressionSyntax)
            {
                var memberAccess = (MemberAccessExpressionSyntax)invocation.Expression;
                var objectReference = (IdentifierNameSyntax)memberAccess.Expression;
                var objectType = GetTypeSymbol(objectReference);
                var namespaceName = objectType.ContainingNamespace.FullName();

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

        // Todo: Add documentation
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




        #endregion
    }
}
