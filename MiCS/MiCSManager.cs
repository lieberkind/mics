﻿using Roslyn.Compilers.CSharp;
using Roslyn.Compilers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using MiCS.Mappers;
using ScriptSharp.Generator;
using MiCS.Builders;
using ScriptSharp;
using ScriptSharp.ScriptModel;


namespace MiCS
{
    public class MiCSManager
    {
        private string _source;
        public static SyntaxTree Tree
        {
            get
            {
                return Instance._tree;
            }
        }
        private SyntaxTree _tree;

        public static SyntaxTree MixedSideTree
        {
            get
            {
                if (Instance._mixedSideTree == null)
                    throw new Exception("MixedSide tree is null!");
                return Instance._mixedSideTree;
            }
        }
        private SyntaxTree _mixedSideTree;


        private Compilation _mixedSideCompilation;
        private Compilation _compilation;

        public static CompilationUnitSyntax MixedSideCompilationUnit
        {
            get
            {
                return Instance._mixedSideCompilationUnit;
            }
        }
        private CompilationUnitSyntax _mixedSideCompilationUnit;

        public static SemanticModel MixedSideSemanticModel
        {
            get
            {
                return Instance._mixedSideSemanticModel;
            }
        }
        private SemanticModel _mixedSideSemanticModel;

        public static SemanticModel SemanticModel
        {
            get
            {
                return Instance._semanticModel;
            }
        }
        private SemanticModel _semanticModel;


        public static CompilationUnitSyntax CompilationUnit
        {
            get
            {
                return Instance._compilationUnit;
            }
        }
        private CompilationUnitSyntax _compilationUnit;

        public static void Initiate(string source)
        {
            var micsManager = new MiCSManager(source);
        }

        public MiCSManager(string source)
        {
            _source = source;
            _tree = SyntaxTree.ParseText(source);
            _compilationUnit = _tree.GetRoot();
            _mixedSideCompilationUnit = GetMixedSideCompilationUnit(_compilationUnit);
            _mixedSideTree = _tree.WithChangedText(_mixedSideCompilationUnit.GetText());

            // Todo: Investigate maybe...
            /*
             * Building the semantic model...
             * Not sure MetadataFileReference is the correct MetadataReference to use.
             * See "Roslyn Overview section 4" and (deprecated example) http://social.msdn.microsoft.com/Forums/en-US/roslyn/thread/d82de2da-50eb-4701-b806-aa3ee24f74c2/
             * 
             * Not sure if any other assemblies (other than mscorlib) are required?
             */
            var mscorlib = new MetadataFileReference(typeof(object).Assembly.Location);

            _compilation = Compilation.Create("MixedSideCompilation", syntaxTrees: new[] { _tree }, references: new[] { mscorlib });
            _semanticModel = _compilation.GetSemanticModel(_tree);

            _mixedSideCompilation = Compilation.Create("MixedSideCompilation", syntaxTrees: new[] { _mixedSideTree }, references: new[] { mscorlib });
            _mixedSideSemanticModel = _mixedSideCompilation.GetSemanticModel(_mixedSideTree);

            MiCSManager._Instance = this;
        }

        // Todo: Should probably ensure that Instance is singleton!
        private static MiCSManager Instance
        {
            get
            {
                if (MiCSManager._Instance == null) throw new Exception("MiCSManager is not instantiated!");
                return _Instance;
            }
        }
        private static MiCSManager _Instance;

        public MiCSManager()
        {

        }

        // Todo: Script should be build from more than one file.
        public void BuildScript(string filePath, ScriptManager scriptManager, Page page)
        {
            /*
             * Roslyn AST creation, manipulation and validation.
             */
            _source = File.ReadAllText(filePath);
            _tree = SyntaxTree.ParseText(_source);
            var mixedSideCompilationUnit = GetMixedSideCompilationUnit(_tree.GetRoot());
            
            // Todo: Ensure that no mixed side (or client side) code makes calls to (or utilize) server side code only.

            /*
             * Map from Roslyn (C#) to ScriptSharp (JavaScript) AST.
             */
            var scriptSharpAST = MapCompilationUnit(mixedSideCompilationUnit);
            // Todo: Maybe wrap MiCS code in its own namespace.

            /*
             * Generate script code and register with ASP.NET ScriptManager.
             */
            var scriptText = GenerateScriptText(scriptSharpAST);
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "MiCSGeneratedScript", scriptText, true);
        }

        /// <summary>
        /// Returns a new compilation unit that only contains
        /// the mixed side syntax nodes (e.i. the mixed side AST).
        /// </summary>
        private CompilationUnitSyntax GetMixedSideCompilationUnit(CompilationUnitSyntax root)
        {
            // Todo: Not sure nested namespaces are supported currently!

            CompilationUnitSyntax mixedSideCompilationUnit = null;
            var mappedNamespaces = new List<MemberDeclarationSyntax>();
            foreach (NamespaceDeclarationSyntax rootMember in root.Members.Where(m => m.Kind == SyntaxKind.NamespaceDeclaration))
            {
                var namespaceDeclaration = (NamespaceDeclarationSyntax)rootMember;
                var mappedClasses = new List<MemberDeclarationSyntax>();
                foreach (var namespaceMember in namespaceDeclaration.DescendantNodes().Where(m => m.Kind == SyntaxKind.ClassDeclaration))
                {
                    var classDeclaration = (ClassDeclarationSyntax)namespaceMember;
                    var mappedMethods = new List<MemberDeclarationSyntax>();
                    foreach (var methodMember in classDeclaration.DescendantNodes().Where(m => m.Kind == SyntaxKind.MethodDeclaration))
                    {
                        var methodDeclaration = (MethodDeclarationSyntax)methodMember;
                        if (methodDeclaration.IsMixedSide())
                            mappedMethods.Add(methodDeclaration); // Todo: Seems like the Add function doesn't work?
                    }

                    if (mappedMethods.Count > 0)
                        mappedClasses.Add(classDeclaration.WithMembers(Syntax.List(mappedMethods.ToArray())));
                }
                if (mappedClasses.Count > 0)
                    mappedNamespaces.Add(namespaceDeclaration.WithMembers(Syntax.List(mappedClasses.ToArray())));
            }
            if (mappedNamespaces.Count > 0)
                mixedSideCompilationUnit = root.WithMembers(Syntax.List(mappedNamespaces.ToArray()));


            if (mixedSideCompilationUnit == null)
                throw new NoMixedOrClientSideException("No MixedSide or ClientSide attributed code was found!");

            return mixedSideCompilationUnit;
        }

        /// <summary>
        /// Map the mixed side Roslyn AST to ScriptSharp AST.
        /// </summary>
        private List<ScriptSharp.ScriptModel.NamespaceSymbol> MapCompilationUnit(CompilationUnitSyntax root)
        {
            var scriptSharpAST = new List<ScriptSharp.ScriptModel.NamespaceSymbol>();
            foreach (var roslynNamespace in root.Members)
            {
                if (roslynNamespace is NamespaceDeclarationSyntax)
                {
                    scriptSharpAST.Add(NamespaceBuilder.Build((NamespaceDeclarationSyntax)roslynNamespace));
                }
            }
            return scriptSharpAST;
        }

        /// <summary>
        /// Generate script string from ScriptSharp AST.
        /// </summary>
        private string GenerateScriptText(List<ScriptSharp.ScriptModel.NamespaceSymbol> scriptSharpAST)
        {
            var stringWriter = new StringWriter();
            var writer = new ScriptTextWriter(stringWriter);
            var options = new CompilerOptions();
            var generator = new ScriptGenerator(writer, options, null);

            foreach (var scriptSharpNamespace in scriptSharpAST)
            {
                foreach (var scriptSharpClass in scriptSharpNamespace.Types)
                {
                    TypeGenerator.GenerateClass(generator, (ClassSymbol)scriptSharpClass);
                }
            }

            return stringWriter.ToString();
        }
    }

    [Serializable]
    public class NoMixedOrClientSideException : Exception
    {
        public NoMixedOrClientSideException() { }
        public NoMixedOrClientSideException(string message) : base(message) { }
        public NoMixedOrClientSideException(string message, Exception inner) : base(message, inner) { }
        protected NoMixedOrClientSideException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
