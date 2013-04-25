﻿using MiCS.Validators;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    class ScriptSharpTypeManager
    {
        #region Region: Construction and Properties

        private Dictionary<string, Dictionary<string, List<string>>> coreTypeMembers;

        public MiCSCoreMapping CoreMapping
        {
            get;
            private set;
        }

        public ScriptSharpTypeManager()
        {
            var tree = SyntaxTree.ParseText(ScriptSharp.TextSources.CoreLib.Text);
            CompilationUnit = tree.GetRoot();

            var coreTypeCollector = new Collector(CompilationUnit);

            coreTypeCollector.Collect();
            coreTypeMembers = coreTypeCollector.Members;

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

        public static ScriptSharpTypeManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScriptSharpTypeManager();

                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        private static ScriptSharpTypeManager instance;

        #endregion


        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        public bool IsSupportedCoreType(string namespaceName, string typeName)
        {
            return ToCoreScriptType(namespaceName, typeName) != null;
        }

        /// <summary>
        /// Returns true if this is a core type that can be mapped
        /// to a script core type.
        /// </summary>
        public bool IsSupportedCoreType(TypeSymbol typeSymbol)
        {
            if (typeSymbol is ArrayTypeSymbol)
                return IsSupportedCoreType(typeSymbol.BaseType.ContainingNamespace.FullName(), "Array");
            else
                return IsSupportedCoreType(typeSymbol.ContainingNamespace.FullName(), typeSymbol.Name);
        }
        ///// <summary>
        ///// Returns true if this is a core type that can be mapped
        ///// to a script core type.
        ///// </summary>
        //// Todo: Move to MiCSManager
        //public static bool IsSupportedCoreType(SimpleNameSyntax simpleName)
        //{
        //    return IsSupportedCoreType(TypeManager.GetTypeSymbol(simpleName));
        //}


        /// <summary>
        /// Returns true if the specified type is a core script type.
        /// </summary>
        public bool IsCoreScriptType(string namespaceName, string typeName)
        {
            return ToCoreScriptType(namespaceName, typeName) != null;
        }

        /// <summary>
        /// Returns true if the specified type is a core script type..
        /// </summary>
        public bool IsCoreScriptType(TypeSymbol typeSymbol)
        {
            var namespaceName = typeSymbol.ContainingNamespace.FullName();
            var typeName = typeSymbol.Name;
            return IsCoreScriptType(namespaceName, typeName);
        }

        /// <summary>
        /// Returns true if the specified type is a core script type..
        /// </summary>
        public bool IsCoreScriptType(SimpleNameSyntax simpleName)
        {
            
            return IsCoreScriptType((ExpressionSyntax)simpleName);
        }

        /// <summary>
        /// Returns true if the specified expression is of core script type..
        /// </summary>
        private bool IsCoreScriptType(ExpressionSyntax expression)
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
        private TypeSymbol ToCoreScriptType(string namespaceName, string typeName)
        {
            var typeMappings = CoreMapping.Where(t => t.NamespaceName.Equals(namespaceName) && t.Name.Equals(typeName));

            if (typeMappings.Count() == 0)
                return null;

            return GetCoreScriptTypeFromModel(typeMappings.First());
        }

        /// <summary>
        /// Returns the equivalent script core type according
        /// to the specified C# type (if it is a core type and
        /// it is supported by the mapping specification).
        /// </summary>
        public TypeSymbol ToCoreScriptType(TypeSymbol typeSymbol)
        {
            if (typeSymbol is ArrayTypeSymbol)
                return ToCoreScriptType(typeSymbol.BaseType.ContainingNamespace.FullName(), "Array");
            else
                return ToCoreScriptType(typeSymbol.ContainingNamespace.FullName(), typeSymbol.Name);
        }

        public NamespaceSymbol GetCoreScriptTypeNamespace(TypeSymbol typeSymbol)
        {
            return ToCoreScriptType(typeSymbol).ContainingNamespace;
        }

        /// <summary>
        /// Retrieves the specified script core type symbol from the 
        /// ScriptSharp core types semantic model.
        /// </summary>
        private TypeSymbol GetCoreScriptTypeFromModel(string namespaceName, string name, bool throwExceptionOnError = true)
        {
            var namespaces = CompilationUnit.DescendantNodes().Where(n => n.Kind == SyntaxKind.NamespaceDeclaration);
            foreach (var @namespace in namespaces)
            {
                var members = @namespace.DescendantNodes().Where(n => n.Kind == SyntaxKind.ClassDeclaration || n.Kind == SyntaxKind.StructDeclaration);
                foreach (var node in members)
                {

                    var member = ((TypeDeclarationSyntax)node);
                    var memberName = member.Identifier.ValueText;

                    if (memberName.Equals(name))
                    {
                        return SemanticModel.GetDeclaredSymbol(member);
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
        private TypeSymbol GetCoreScriptTypeFromModel(MiCSCoreTypeMapping typeMapping)
        {
            return GetCoreScriptTypeFromModel(typeMapping.NamespaceNameScript, typeMapping.NameScript);
        }

    }
}
