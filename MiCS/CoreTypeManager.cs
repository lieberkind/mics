using MiCS.Validators;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    class CoreTypeManager
    {
        #region Region: Construction and Properties

        public Dictionary<string, Dictionary<string, List<string>>> CoreTypeMembers;

        public MiCSCoreMapping CoreMapping
        {
            get;
            private set;
        }

        public CoreTypeManager()
        {
            var tree = SyntaxTree.ParseText(ScriptSharp.TextSources.CoreLib.Text);
            CompilationUnit = tree.GetRoot();

            var coreTypeCollector = new Collector(CompilationUnit);

            coreTypeCollector.Collect();
            CoreTypeMembers = coreTypeCollector.Members;

            CoreMapping = MiCSCoreMapping.Instance;

            var compilation = Compilation.Create("Compilation", syntaxTrees: new[] { tree });
            SemanticModel = compilation.GetSemanticModel(tree);
        }

        public CompilationUnitSyntax CompilationUnit
        {
            get;
            private set;
        }

        public SemanticModel SemanticModel
        {
            get;
            private set;
        }

        public static CoreTypeManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new CoreTypeManager();

                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        private static CoreTypeManager instance;

        #endregion

        public static void VerifyCorrectUseOfSupportedCoreType(InvocationExpressionSyntax invocation)
        {
            if (invocation.Expression is MemberAccessExpressionSyntax)
            {
                var memberAccess = (MemberAccessExpressionSyntax)invocation.Expression;
                var objectReference = (IdentifierNameSyntax)memberAccess.Expression;
                var objectType = TypeSymbolGetter.GetTypeSymbol(objectReference);
                var namespaceName = objectType.ContainingNamespace.FullName();

                if (objectType.IsSupportedCoreType())
                {
                    var mappings = Instance.CoreMapping.Where(t => t.NamespaceName.Equals(namespaceName) && t.Name.Equals(objectType.Name));
                    if (mappings.Count() == 1)
                    {
                        var typeMapping = mappings.First();
                        var memberName = memberAccess.Name.Identifier.ValueText;
                        var memberReturnType = TypeSymbolGetter.GetTypeSymbol(memberAccess.Name);

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
			                        var argumentType = TypeSymbolGetter.GetTypeSymbol(memberArguments[i]);
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
            return ToCoreScriptType(namespaceName, typeName) != null;
        }
        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        public static bool IsSupportedCoreType(TypeSymbol typeSymbol)
        {
            if (typeSymbol is ArrayTypeSymbol)
                return IsSupportedCoreType(typeSymbol.BaseType.ContainingNamespace.FullName(), "Array");
            else
                return IsSupportedCoreType(typeSymbol.ContainingNamespace.FullName(), typeSymbol.Name);
        }
        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        // Todo: Move to MiCSManager
        public static bool IsSupportedCoreType(SimpleNameSyntax simpleName)
        {
            return IsSupportedCoreType(TypeSymbolGetter.GetTypeSymbol(simpleName));
        }


        /// <summary>
        /// Returns true if the specified type is a core script type.
        /// </summary>
        public static bool IsCoreScriptType(string namespaceName, string typeName)
        {
            return ToCoreScriptType(namespaceName, typeName) != null;
        }
        /// <summary>
        /// Returns true if the specified type is a core script type..
        /// </summary>
        public static bool IsCoreScriptType(TypeSymbol typeSymbol)
        {
            var namespaceName = typeSymbol.ContainingNamespace.FullName();
            var typeName = typeSymbol.Name;
            return IsCoreScriptType(namespaceName, typeName);
        }
        /// <summary>
        /// Returns true if the specified type is a core script type..
        /// </summary>
        public static bool IsCoreScriptType(SimpleNameSyntax simpleName)
        {
            
            return IsCoreScriptType((ExpressionSyntax)simpleName);
        }
        /// <summary>
        /// Returns true if the specified expression is of core script type..
        /// </summary>
        private static bool IsCoreScriptType(ExpressionSyntax expression)
        {
            var coreType = Instance.SemanticModel.GetTypeInfo(expression).Type;
            if (coreType.IsSupportedCoreType())
                return true;
            else
                return false;
        }


        /// <summary>
        /// Returns the equivalent script core type according
        /// to the specified C# type (if it is a core type and
        /// it is supported by the mapping specification).
        /// </summary>
        private static TypeSymbol ToCoreScriptType(string namespaceName, string typeName)
        {
            var typeMappings = Instance.CoreMapping.Where(t => t.NamespaceName.Equals(namespaceName) && t.Name.Equals(typeName));

            if (typeMappings.Count() == 0)
                return null;

            return GetCoreScriptTypeFromModel(typeMappings.First());
        }
        /// <summary>
        /// Returns the equivalent script core type according
        /// to the specified C# type (if it is a core type and
        /// it is supported by the mapping specification).
        /// </summary>
        public static TypeSymbol ToCoreScriptType(TypeSymbol typeSymbol)
        {
            if (typeSymbol is ArrayTypeSymbol)
                return ToCoreScriptType(typeSymbol.BaseType.ContainingNamespace.FullName(), "Array");
            else
                return ToCoreScriptType(typeSymbol.ContainingNamespace.FullName(), typeSymbol.Name);
        }

        public static NamespaceSymbol GetCoreScriptTypeNamespace(TypeSymbol typeSymbol)
        {
            return ToCoreScriptType(typeSymbol).ContainingNamespace;
        }

        /// <summary>
        /// Retrieves the specified script core type symbol from the 
        /// ScriptSharp core types semantic model.
        /// </summary>
        private static TypeSymbol GetCoreScriptTypeFromModel(string namespaceName, string name, bool throwExceptionOnError = true)
        {
            var namespaces = Instance.CompilationUnit.DescendantNodes().Where(n => n.Kind == SyntaxKind.NamespaceDeclaration);
            foreach (var @namespace in namespaces)
            {
                var members = @namespace.DescendantNodes().Where(n => n.Kind == SyntaxKind.ClassDeclaration || n.Kind == SyntaxKind.StructDeclaration);
                foreach (var node in members)
                {

                    var member = ((TypeDeclarationSyntax)node);
                    var memberName = member.Identifier.ValueText;

                    if (memberName.Equals(name))
                    {
                        return Instance.SemanticModel.GetDeclaredSymbol(member);
                    }
                }
            }

            if (throwExceptionOnError)
                throw new NotSupportedException("Core Type does not exist");
            else
                return null;
        }
        /// <summary>
        /// Retrieves the specified script core type symbol from the 
        /// ScriptSharp core types semantic model.
        /// </summary>
        private static TypeSymbol GetCoreScriptTypeFromModel(MiCSCoreTypeMapping typeMapping)
        {
            return GetCoreScriptTypeFromModel(typeMapping.NamespaceNameScript, typeMapping.NameScript);
        }

    }

    // Todo: Should probably be moved away from CoreTypeManager
    [Serializable]
    public class MemberSignatureArgumentTypeNotMappedException : Exception
    {
        public MemberSignatureArgumentTypeNotMappedException() { }
        public MemberSignatureArgumentTypeNotMappedException(string message) : base(message) { }
        public MemberSignatureArgumentTypeNotMappedException(string message, Exception inner) : base(message, inner) { }
        protected MemberSignatureArgumentTypeNotMappedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class MemberSignatureNotMappedException : Exception
    {
        public MemberSignatureNotMappedException() { }
        public MemberSignatureNotMappedException(string message) : base(message) { }
        public MemberSignatureNotMappedException(string message, Exception inner) : base(message, inner) { }
        protected MemberSignatureNotMappedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class MemberNotMappedException : Exception
    {
        public MemberNotMappedException() { }
        public MemberNotMappedException(string message) : base(message) { }
        public MemberNotMappedException(string message, Exception inner) : base(message, inner) { }
        protected MemberNotMappedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class TypeNotMappedException : Exception
    {
        public TypeNotMappedException() { }
        public TypeNotMappedException(string message) : base(message) { }
        public TypeNotMappedException(string message, Exception inner) : base(message, inner) { }
        protected TypeNotMappedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
