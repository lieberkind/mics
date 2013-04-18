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
    class ScriptTypeManager
    {
        public SemanticModel SemanticModel
        {
            get;
            private set;
        }

        public Dictionary<string, Dictionary<string, List<string>>> MixedSideMembers
        {
            get;
            private set;
        }

        public Dictionary<string, Dictionary<string, List<string>>> ClientSideMembers
        {
            get;
            private set;
        }

        public CompilationUnitSyntax CompilationUnit
        {
            get;
            private set;
        }

        public ScriptTypeManager(SyntaxTree userTree)
        {
            var tree = SyntaxTree.ParseText(userTree.GetText() + ScriptSharp.TextSources.Web.Text);

            // Collects all members from ScriptSharp.Web
            var builtInCollector = new Collector(SyntaxTree.ParseText(ScriptSharp.TextSources.Web.Text).GetRoot());

            CompilationUnit = tree.GetRoot();

            var mixedSideCollector = new Collector(CompilationUnit, "MixedSide");
            var clientSideCollector = new Collector(CompilationUnit, "ClientSide");

            builtInCollector.Collect();
            mixedSideCollector.Collect();
            clientSideCollector.Collect();

            MixedSideMembers = mixedSideCollector.Members;
            ClientSideMembers = clientSideCollector.Members;
            ClientSideMembers.AddRange(builtInCollector.Members);

            var mscorlib = new MetadataFileReference(typeof(object).Assembly.Location);

            var compilation = Compilation.Create("Compilation", syntaxTrees: new[] { tree }, references: new[] { mscorlib });
            SemanticModel = compilation.GetSemanticModel(tree);

            Instance = this;
        }

        public static ScriptTypeManager Instance
        {
            get;
            private set;
        }
    }
}
