using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptSharp.Generator;
using System.IO;
using System.Linq;
using ScriptSharp;
using ScriptSharp.ScriptModel;
using Roslyn.Compilers.CSharp;
using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace MiCSTests
{
    [TestClass]
    public class RoslynTests
    {
        private CompilationUnitSyntax GetCompilationUnit(string source)
        {
            var syntaxTree = SyntaxTree.ParseText(source);
            return syntaxTree.GetRoot();
        }



        [TestMethod]
        public void RoslynNamespaceTest()
        {
            var cUnit = GetCompilationUnit(@"namespace TestNameSpace{ }");
            var namespaces = cUnit.Members.Where(m => m.Kind == SyntaxKind.NamespaceDeclaration);
            Assert.IsTrue(namespaces.Count() == 1);

            var ns = (NamespaceDeclarationSyntax)namespaces.First();
            Assert.IsTrue(((IdentifierNameSyntax)ns.Name).Identifier.Value.Equals("TestNameSpace"));
        }

        [TestMethod]
        public void RoslynClassTest()
        {
            var cUnit = GetCompilationUnit(@"class TestClass{ }");
            var classes = cUnit.Members.Where(m => m.Kind == SyntaxKind.ClassDeclaration);
            Assert.IsTrue(classes.Count() == 1);

            var cl = (ClassDeclarationSyntax)classes.First();
            Assert.IsTrue(cl.Identifier.Value.Equals("TestClass"));
        }

        [TestMethod]
        public void RoslynMethodTest()
        {
            var cUnit = GetCompilationUnit(@"void TestMethod() { }");
            var methods = cUnit.Members.Where(m => m.Kind == SyntaxKind.MethodDeclaration);
            Assert.IsTrue(methods.Count() == 1);

            var method = (MethodDeclarationSyntax)methods.First();
            Assert.IsTrue(method.Identifier.Value.Equals("TestMethod"));
        }

        #region Region: Statement Tests

        private IEnumerable<SyntaxNode> MethodBodyDescendantNodes(string statements)
        {
            var cUnit = GetCompilationUnit(@"void TestMethod() { " + statements + " }");
            var methods = cUnit.Members.Where(m => m.Kind == SyntaxKind.MethodDeclaration);
            var method = (MethodDeclarationSyntax)methods.First();
            return method.Body.DescendantNodes();
        }

        [TestMethod]
        public void RoslynEmptyStatementTest()
        {
            var nodes = MethodBodyDescendantNodes(";");
            var count = nodes.Where(n => n.Kind == SyntaxKind.EmptyStatement).Count();
            Assert.IsTrue(count == 1);
        }

        #endregion


    }
}
