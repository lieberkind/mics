using MiCS;
using MiCS.Builders;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS = ScriptSharp.ScriptModel;

namespace MiCSTests.TestUtils
{
    public static class Parse
    {

        public static SS.SymbolSet NamespacesToSymbolSet(string source)
        {
            var ssSymbolSet = new SS.SymbolSet();
            source = @"using System.Html;" + source;
            foreach (var @namespace in Namespaces(source))
                ssSymbolSet.Namespaces.Add(NamespaceBuilder.Build((NamespaceDeclarationSyntax)@namespace));

            return ssSymbolSet;
        }

        public static SS.SymbolSet ClassesToSymbolSet(string source)
        {
            source = "namespace TestNamespace { " + source + " }";
            return NamespacesToSymbolSet(source);
        }

        public static SS.SymbolSet MethodsToSymbolSet(string source)
        {
            source = "class TestClass { " + source + " }";
            return ClassesToSymbolSet(source);
        }

        public static SS.SymbolSet StatementsToSymbolSet(string source)
        {
            source = @"[MixedSide]
                        void TestMethod() { " + source + " }";
            return MethodsToSymbolSet(source);
        }

        public static SS.SymbolSet ExpressionToSymbolSet(string source)
        {
            source = source + "; ";
            return StatementsToSymbolSet(source);
        }

        public static List<SS.Statement> StatementsToSS(string source)
        {
            var ssSymblSet = StatementsToSymbolSet(source);
            var ssMethod = (SS.MethodSymbol)ssSymblSet.Namespaces.ElementAt(2).Types.ElementAt(0).Members.ElementAt(0);
            return ssMethod.Implementation.Statements.ToList();
        }

        public static SS.Statement StatementToSS(string source)
        {
            return StatementsToSS(source).First();
        }


        public static CompilationUnitSyntax CompilationUnit(string source)
        {
            //var syntaxTree = SyntaxTree.ParseText(source);
            MiCSManager.Initiate(source);
            return ScriptTypeManager.Instance.CompilationUnit;
        }

        public static IEnumerable<SyntaxNode> Namespaces(string source)
        {
            var cUnit = CompilationUnit(source);
            var namespaces = cUnit.Members.Where(m => m.Kind == SyntaxKind.NamespaceDeclaration);
            return namespaces;
        }

        public static IEnumerable<SyntaxNode> Namespaces(CompilationUnitSyntax root)
        {
            var namespaces = root.Members.Where(m => m.Kind == SyntaxKind.NamespaceDeclaration);
            return namespaces;
        }

        public static IEnumerable<SyntaxNode> Classes(string classSource)
        {
            var cUnit = CompilationUnit(@"namespace TestNameSpace{ " + classSource + @" }");

            var namespaces = cUnit.Members.Where(m => m.Kind == SyntaxKind.NamespaceDeclaration);
            return namespaces.First().DescendantNodes().Where(m => m.Kind == SyntaxKind.ClassDeclaration);
        }

        public static IEnumerable<SyntaxNode> Methods(string methodsSource)
        {
            var classes = Parse.Classes(@"class TestClass{ " + methodsSource + @" }");
            var methods = classes.First().DescendantNodes().Where(m => m.Kind == SyntaxKind.MethodDeclaration);
            return methods;
        }

        public static IEnumerable<StatementSyntax> Statements(string source)
        {
            var method = (MethodDeclarationSyntax)Parse.Methods(@" 
            [MixedSide]
            void f() { " + source + " };").First();
            return method.Body.Statements;
        }

        public static StatementSyntax Statement(string source)
        {
            var stmts = Parse.Statements(source);
            if (stmts.Count() != 1) throw new Exception("The provided source is not exactly one statement.");
            return stmts.First();
        }

        public static ExpressionSyntax Expression(string source)
        {
            var RosStmt = Parse.Statement(@"var expr = " + source + ";");
            var RosExpr = ((LocalDeclarationStatementSyntax)RosStmt).Declaration.Variables.First().Initializer.Value;
            return RosExpr;
        }

    }
}
