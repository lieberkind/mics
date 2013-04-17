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

        public static TypeSymbol GetTypeByName(string name)
        {
            var classes = Instance.CompilationUnit.DescendantNodes().Where(n => n.Kind == SyntaxKind.ClassDeclaration);

            foreach (var node in classes)
            {
                var @class = ((ClassDeclarationSyntax)node);
                var className = @class.Identifier.ValueText;

                if (className.Equals(name))
                {
                    return Instance.SemanticModel.GetDeclaredSymbol(@class);
                }
            }

            throw new NotSupportedException("Core Type does not exist");
        }
    }
}
