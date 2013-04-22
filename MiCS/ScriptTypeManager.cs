using MiCS.Validators;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Html;
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

            var mixedSideCollector = new Collector(CompilationUnit, new List<string>() { "MixedSide" });
            var clientSideCollector = new Collector(CompilationUnit, new List<string>() { "ClientSide" });

            builtInCollector.Collect();
            mixedSideCollector.Collect();
            clientSideCollector.Collect();

            MixedSideMembers = mixedSideCollector.Members;
            ClientSideMembers = clientSideCollector.Members;
            ClientSideMembers.AddRange(builtInCollector.Members);

            //Date d = new Date();
            //var rx = new System.Text.RegularExpressions.Regex("owdjwo");

            // Todo: Write about references in report maybe... how to handle references in a more generic manner.
            var mscorlib = new MetadataFileReference(typeof(String).Assembly.Location);
            var systemTextRegularExpression = new MetadataFileReference(typeof(System.Text.RegularExpressions.Regex).Assembly.Location);

            var compilation = Compilation.Create("Compilation", syntaxTrees: new[] { tree }, references: new[] { mscorlib, systemTextRegularExpression });
            SemanticModel = compilation.GetSemanticModel(tree);

            Instance = this;
        }

        public static ScriptTypeManager Instance
        {
            get;
            private set;
        }




        /// <summary>
        /// Returns true if the specified type declaration is
        /// a user defined type.
        /// </summary>
        public static bool IsUserType(ClassDeclarationSyntax classDeclaration)
        {
            foreach (var member in classDeclaration.Members)
            {
                if (member is MethodDeclarationSyntax)
                {
                    var declaration = ((MethodDeclarationSyntax)member);
                    if (declaration.HasAttribute("MixedSide") ||
                        declaration.HasAttribute("ClientSide"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Returns true if the specified type is
        /// a user defined type.
        /// </summary>
        public static bool IsUserType(TypeSymbol typeSymbol)
        {
            if (typeSymbol.DeclaringSyntaxNodes.Count != 1)
                return false;

            var declaration = typeSymbol.DeclaringSyntaxNodes[0];
            if (declaration is ClassDeclarationSyntax)
            {
                var @class = (ClassDeclarationSyntax)declaration;
                return @class.IsUserType();
            }

            return false;
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a DOM type from the ScriptSharp namespace System.Html.
        /// </summary>
        public static bool IsDOMType(ClassDeclarationSyntax classDeclaration)
        {
            return classDeclaration.HasAttribute("ScriptName");
        }
        /// <summary>
        /// Returns true if the specified type is
        /// a DOM type from the ScriptSharp namespace System.Html.
        /// </summary>
        public static bool IsDOMType(TypeSymbol typeSymbol)
        {
            if (typeSymbol.DeclaringSyntaxNodes.Count != 1)
                return false;

            var declaration = typeSymbol.DeclaringSyntaxNodes[0];
            if (declaration is ClassDeclarationSyntax)
            {
                var @class = (ClassDeclarationSyntax)declaration;
                return @class.IsDOMType();
            }
            else
                throw new NotSupportedException();
        }
    }
}
