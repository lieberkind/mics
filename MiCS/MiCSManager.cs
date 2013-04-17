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
using SS = ScriptSharp.ScriptModel;
using MiCS.Validators;


namespace MiCS
{
    public class MiCSManager
    {
        private ScriptTypeManager scriptTypeManager;
        private CoreTypeManager coreTypeManager;
        private TypeSymbolGetter typeSymbolGetter;
        private static MiCSManager instance;


        public static TypeSymbolGetter TypeSymbolGetter 
        {
            get { return Instance.typeSymbolGetter; } 
        }

        public static Dictionary<string, Dictionary<string, List<string>>> MixedSideMembers
        {
            get { return Instance.scriptTypeManager.MixedSideMembers; }
        }

        public static Dictionary<string, Dictionary<string, List<string>>> ClientSideMembers
        {
            get { return Instance.scriptTypeManager.ClientSideMembers; }
        }

        public static Dictionary<string, Dictionary<string, List<string>>> CoreTypeMembers
        {
            get { return Instance.coreTypeManager.CoreTypeMembers;  }
        }

        public static SemanticModel ScriptTypeSemanticModel
        {
            get { return Instance.scriptTypeManager.SemanticModel; }
        }

        public static SemanticModel CoreTypeSemanticModel
        {
            get { return Instance.coreTypeManager.SemanticModel; }
        }

        // Todo: Should probably ensure that Instance is singleton!
        private static MiCSManager Instance
        {
            get
            {
                if (MiCSManager.instance == null) throw new Exception("MiCSManager is not instantiated!");
                return instance;
            }
        }


        //public static SemanticModel SemanticModel
        //{
        //    get { return Instance.scriptTypeManager.SemanticModel; }
        //}


        public static void Initiate(string source)
        {
            var micsManager = new MiCSManager(source);
        }

        public static void Initiate(SyntaxTree tree)
        {
            var micsManager = new MiCSManager(tree);
        }


        public MiCSManager(string source) : this(SyntaxTree.ParseText(source))
        { 
            
        }

        public MiCSManager(SyntaxTree userTree)
        {
            this.scriptTypeManager = new ScriptTypeManager(userTree);
            this.coreTypeManager = new CoreTypeManager();

            this.typeSymbolGetter = new TypeSymbolGetter(scriptTypeManager, coreTypeManager);

            MiCSManager.instance = this;
        }

        // Todo: Script should be build from more than one file.
        public static void BuildScript(ScriptManager scriptManager, Page page)
        {
            /*
             * Map from Roslyn (C#) to ScriptSharp (JavaScript) AST.
             */
            var scriptSharpAST = Instance.MapCompilationUnit(CompilationUnit);
            // Todo: Maybe wrap MiCS code in its own namespace.

            /*
             * Generate script code and register with ASP.NET ScriptManager.
             */
            var scriptText = Instance.GenerateScriptText(scriptSharpAST);
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "MiCSGeneratedScript", scriptText, true);
        }

        /// <summary>
        /// Map the mixed side Roslyn AST to ScriptSharp AST.
        /// </summary>
        private List<SS.NamespaceSymbol> MapCompilationUnit(CompilationUnitSyntax root)
        {
            var scriptSharpAST = new List<SS.NamespaceSymbol>();
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
        private string GenerateScriptText(List<SS.NamespaceSymbol> scriptSharpAST)
        {
            var stringWriter = new StringWriter();
            var writer = new ScriptTextWriter(stringWriter);
            var options = new CompilerOptions();
            var generator = new ScriptGenerator(writer, options, null);

            foreach (var scriptSharpNamespace in scriptSharpAST)
            {
                foreach (var scriptSharpClass in scriptSharpNamespace.Types)
                {
                    TypeGenerator.GenerateClass(generator, (SS.ClassSymbol)scriptSharpClass);
                }
            }

            return stringWriter.ToString();
        }

        private string GenerateScriptText(SS.SymbolSet symbols)
        {
            var stringWriter = new StringWriter();
            var writer = new ScriptTextWriter(stringWriter);
            var options = new CompilerOptions();
            var generator = new ScriptGenerator(writer, options, symbols);
            generator.GenerateScript(symbols);
            return stringWriter.ToString();
        }

        public static string GenerateScriptText(Action action)
        {
            return "alert('Not Implemented!');";
        }
    }
}
