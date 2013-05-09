using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using ScriptSharp.Generator;
using MiCS.Builders;
using ScriptSharp;
using SS = ScriptSharp.ScriptModel;
using MiCS.Validators;

namespace MiCS
{
    public class MiCSManager
    {
        #region Region: Constructor & Fields

        public static bool UserTreeIsValid
        {
            get { return Instance.userTreeIsValid; }
        }
        private bool userTreeIsValid;

        private static MiCSManager Instance
        {
            get
            {
                if (MiCSManager.instance == null)
                    throw new Exception("MiCSManager is not instantiated!");

                return instance;
            }
        }
        private static MiCSManager instance;

        public static void Initiate(string source)
        {
            MiCSManager.instance = new MiCSManager(source);
        }

        public static void Initiate(SyntaxTree tree)
        {
            MiCSManager.instance = new MiCSManager(tree);
        }

        private MiCSManager(string source)
            : this(SyntaxTree.ParseText(source))
        {

        }

        private MiCSManager(SyntaxTree userTree)
        {
            if (!Syntax.IsCompleteSubmission(userTree))
                throw new Exception("Source submission failed!");

            TypeManager.Initiate(userTree);

            userTreeIsValid = this.validate();
        }


        #endregion

        /// <summary>
        /// Returns true if the Mixed Side Principle is not violated.
        /// </summary>
        private bool validate()
        {
            var mixedSideMembers = TypeManager.MixedSideMembers;
            var clientSideMembers = TypeManager.ClientSideMembers;

            var mixedSideValidator = new Validator(TypeManager.CompilationUnit, mixedSideMembers, "MixedSide");
            
            var clientSideValidator = new Validator(TypeManager.CompilationUnit, clientSideMembers, "ClientSide");
            clientSideValidator.AddToMembers(mixedSideMembers);

            mixedSideValidator.Validate();
            clientSideValidator.Validate();

            return mixedSideValidator.IsValid && clientSideValidator.IsValid;
        }

        /// <summary>
        /// Build scripts from the [MixedSide] and [ClientSide]
        /// annotated methods.
        /// </summary>
        /// <param name="scriptManager">The page's ScriptManager instance.</param>
        /// <param name="page">The page object</param>
        public static void BuildScript(ScriptManager scriptManager, Page page)
        {
            // Map from Roslyn (C#) to ScriptSharp (JavaScript) AST.
            var scriptSharpAST = Instance.MapCompilationUnit(TypeManager.CompilationUnit);

             // Generate script code and register with ASP.NET ScriptManager.
            var scriptText = Instance.GenerateScriptText(scriptSharpAST);
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "MiCSGeneratedScript", scriptText, true);
        }

        /// <summary>
        /// Returns a list of ScriptSharp namespaces representing the JavaScript (user types) AST.
        /// </summary>
        private List<SS.NamespaceSymbol> MapCompilationUnit(CompilationUnitSyntax root)
        {
            var ssNamespaces = new List<SS.NamespaceSymbol>();
            foreach (var roslynNamespace in root.Members)
            {
                if (roslynNamespace is NamespaceDeclarationSyntax)
                {
                    ssNamespaces.Add(NamespaceBuilder.Build((NamespaceDeclarationSyntax)roslynNamespace));
                }
            }
            return ssNamespaces;
        }

        /// <summary>
        /// Generate script string from ScriptSharp symbols using
        /// the ScriptSharp TypeGenerater.
        /// </summary>
        private string GenerateScriptText(List<SS.NamespaceSymbol> ssNamespaces)
        {
            var stringWriter = new StringWriter();
            var writer = new ScriptTextWriter(stringWriter);
            var options = new CompilerOptions();
            var generator = new ScriptGenerator(writer, options, null);

            foreach (var scriptSharpNamespace in ssNamespaces)
            {
                foreach (var scriptSharpClass in scriptSharpNamespace.Types)
                {
                    TypeGenerator.GenerateClass(generator, (SS.ClassSymbol)scriptSharpClass);
                }
            }

            return stringWriter.ToString();
        }
    }
}
