using Roslyn.Compilers.CSharp;
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
        private bool userTreeIsValid;
        private TypeManager typeManager;

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

        public static bool UserTreeIsValid
        {
            get { return Instance.userTreeIsValid; }
        }

        // Todo: Should probably ensure that Instance is singleton!
        private static MiCSManager Instance
        {
            get
            {
                if (MiCSManager.instance == null) 
                    throw new Exception("MiCSManager is not instantiated!");

                return instance;
            }
        }

        // Todo: How do these work? This seems overly quirky...
        public static void Initiate(string source)
        {
            var micsManager = new MiCSManager(source);
        }

        public static void Initiate(SyntaxTree tree)
        {
            var micsManager = new MiCSManager(tree);
        }


        private MiCSManager(string source) : this(SyntaxTree.ParseText(source))
        {

        }

        private MiCSManager(SyntaxTree userTree)
        {
            if (!Syntax.IsCompleteSubmission(userTree))
                throw new Exception("Source submission failed!");

            this.scriptTypeManager = new ScriptTypeManager(userTree);
            this.coreTypeManager = new CoreTypeManager();

            TypeManager.Initiate(scriptTypeManager, coreTypeManager);

            this.typeSymbolGetter = new TypeSymbolGetter();

            userTreeIsValid = this.validate();

            // Todo: Move this elsewhere - probably to BuildScript method
            //if (!userTreeIsValid)
            //    throw new Exception("The submitted source code is not valid");

            MiCSManager.instance = this;
        }

        #region TypeSymbol functionality
        public static TypeSymbol GetTypeSymbol(SyntaxNode node)
        {
            Instance.typeSymbolGetter.Visit(node);
            return Instance.typeSymbolGetter.TypeSymbol;
        }
        #endregion


        private bool validate()
        {
            var mixedSideMembers = scriptTypeManager.MixedSideMembers;
            var clientSideMembers = scriptTypeManager.ClientSideMembers;

            var mixedSideValidator = new Validator(scriptTypeManager.CompilationUnit, mixedSideMembers, "MixedSide");
            
            var clientSideValidator = new Validator(scriptTypeManager.CompilationUnit, clientSideMembers, "ClientSide");
            clientSideValidator.AddToMembers(mixedSideMembers);

            mixedSideValidator.Validate();
            clientSideValidator.Validate();

            return mixedSideValidator.IsValid && clientSideValidator.IsValid;
        }

        // Todo: Script should be build from more than one file.
        public static void BuildScript(ScriptManager scriptManager, Page page)
        {
            /*
             * Map from Roslyn (C#) to ScriptSharp (JavaScript) AST.
             */
            var scriptSharpAST = Instance.MapCompilationUnit(Instance.scriptTypeManager.CompilationUnit);
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

        //public static TypeSymbol GetTypeSymbol(SyntaxNode node)
        //{
        //    return Instance.scriptTypeManager.GetTypeSymbol(node);
        //}

    }
}
