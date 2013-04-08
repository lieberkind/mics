using MiCS;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCSTests.TestUtils
{
    public static class Parse
    {

        public static CompilationUnitSyntax CompilationUnit(string source)
        {
            //var syntaxTree = SyntaxTree.ParseText(source);
            MiCSManager.Initiate(source);
            var syntaxTree = MiCSManager.Tree;
            if (!Syntax.IsCompleteSubmission(syntaxTree))
                throw new Exception("Source submission failed!");
            return syntaxTree.GetRoot();
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
            var classes = Classes(@"class TestClass{ " + methodsSource + @" }");
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
