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
        public Dictionary<string, Dictionary<string, List<string>>> CoreTypeMembers;

        public CoreTypeManager()
        {
            var tree = SyntaxTree.ParseText(ScriptSharp.TextSources.CoreLib.Text);
            CompilationUnit = tree.GetRoot();

            var coreTypeCollector = new Collector(CompilationUnit);
            
            coreTypeCollector.Collect();
            CoreTypeMembers = coreTypeCollector.Members;
            
            var mscorlib = new MetadataFileReference(typeof(object).Assembly.Location);

            var compilation = Compilation.Create("Compilation", syntaxTrees: new[] { tree }, references: new[] { mscorlib });
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

        public static TypeSymbol GetTypeByName(string namespaceName, string typeName, bool throwExceptionOnError = true)
        {
            var namespaces = Instance.CompilationUnit.DescendantNodes().Where(n => n.Kind == SyntaxKind.NamespaceDeclaration);
            foreach (var @namespace in namespaces)
            {

                var classes = @namespace.DescendantNodes().Where(n => n.Kind == SyntaxKind.ClassDeclaration);

                foreach (var node in classes)
                {
                    var @class = ((ClassDeclarationSyntax)node);
                    var className = @class.Identifier.ValueText;

                    if (className.Equals(typeName))
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
        public static TypeSymbol GetTypeByName(string typeName)
        {
            /*
             * Assumes primary ScriptSharp core type namespace.
             */
            return GetTypeByName("System", typeName);
        }



        public static bool IsCoreType(string namespaceName, string typeName)
        {
            var foundInScriptSharpCoreSource = GetTypeByName(namespaceName, typeName, false) != null;
            if (foundInScriptSharpCoreSource)
            {
                return true;
            }
            else
            {
                // Manually supported core types.
                if (namespaceName.Equals("System.Text.RegularExpressions"))
                {
                    if (typeName.Equals("Regex"))
                    {
                        return true;
                    }
                }
                return false;

            }
        }
        public static bool IsCoreType(TypeSymbol typeSymbol)
        {
            var namespaceName = typeSymbol.ContainingNamespace.FullName();
            var typeName = typeSymbol.Name;
            return IsCoreType(namespaceName, typeName);
        }
        public static bool IsCoreType(IdentifierNameSyntax identifierName)
        {
            return IsCoreType(TypeSymbolGetter.GetTypeSymbol(identifierName));
        }

        // Todo: Remember to check for correct type, order and number of arguments.
        public static string GetCoreTypeMemberScriptName(string namespaceName, string typeName, string memberName, bool throwExceptionOnUnsupported = true)
        {
            if (namespaceName.Equals("System"))
            {
                if (typeName.Equals("String"))
                {
                    if (memberName.Equals("Length"))
                    {
                        return "length";
                    }
                    if (memberName.Equals("IndexOf"))
                    {
                        return "indexOf";
                    }
                }

            }
            if (namespaceName.Equals("System.Text.RegularExpressions"))
            {
                if (typeName.Equals("Regex"))
                {
                    if (memberName.Equals("IsMatch"))
                    {
                        return "test";
                    }
                }

            }
            if (throwExceptionOnUnsupported)
                throw new NotSupportedException("Unsupported use of core type.");
            else
                return memberName;
        }
        public static string GetCoreTypeMemberScriptName(TypeSymbol coreType, string memberName)
        {
            var namespaceName = coreType.ContainingNamespace.FullName();
            var typeName = coreType.Name;
            return GetCoreTypeMemberScriptName(namespaceName, typeName, memberName);
        }
        public static string GetCoreTypeMemberScriptName(IdentifierNameSyntax identifierName, string memberName)
        {
            return GetCoreTypeMemberScriptName(TypeSymbolGetter.GetTypeSymbol(identifierName), memberName);
        }
    }
}
