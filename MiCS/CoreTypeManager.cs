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

        private MiCSCoreMapping coreMapping;


        public CoreTypeManager()
        {
            var tree = SyntaxTree.ParseText(ScriptSharp.TextSources.CoreLib.Text);
            CompilationUnit = tree.GetRoot();

            var coreTypeCollector = new Collector(CompilationUnit);

            coreTypeCollector.Collect();
            CoreTypeMembers = coreTypeCollector.Members;

            coreMapping = MiCSCoreMapping.Instance;

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
            return IsSupportedCoreType(typeSymbol.ContainingNamespace.FullName(), typeSymbol.Name);
        }
        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
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
            var typeMappings = Instance.coreMapping.Where(t => t.NamespaceName.Equals(namespaceName) && t.Name.Equals(typeName));

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
            return ToCoreScriptType(typeSymbol.ContainingNamespace.FullName(), typeSymbol.Name);
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
                var classes = @namespace.DescendantNodes().Where(n => n.Kind == SyntaxKind.ClassDeclaration);
                foreach (var node in classes)
                {
                    var @class = ((ClassDeclarationSyntax)node);
                    var className = @class.Identifier.ValueText;

                    if (className.Equals(name))
                    {
                        return Instance.SemanticModel.GetDeclaredSymbol(@class);
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
}
