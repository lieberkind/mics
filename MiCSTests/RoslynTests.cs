﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptSharp.Generator;
using System.IO;
using System.Linq;
using ScriptSharp;
using ScriptSharp.ScriptModel;
using Roslyn.Compilers.CSharp;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MiCSTests.TestUtils;
namespace MiCSTests
{
    [TestClass]
    public class RoslynTests
    {

        [TestMethod]
        public void RoslynNamespaceTest()
        {
            var namespaces = Parse.Namespaces(@"namespace TestNameSpace{ }");
            Assert.IsTrue(namespaces.Count() == 1);

            var ns = (NamespaceDeclarationSyntax)namespaces.First();
            Assert.IsTrue(((IdentifierNameSyntax)ns.Name).Identifier.Value.Equals("TestNameSpace"));
        }

        [TestMethod]
        public void RoslynClassTest()
        {
            var classes = Parse.Classes(@"class TestClass{ }");
            Assert.IsTrue(classes.Count() == 1);

            var cl = (ClassDeclarationSyntax)classes.First();
            Assert.IsTrue(cl.Identifier.Value.Equals("TestClass"));
        }

        [TestMethod]
        public void RoslynMethodTest()
       { 
            var methods = Parse.Methods(@"void TestMethod() { }");
            Assert.IsTrue(methods.Count() == 1);

            var method = (MethodDeclarationSyntax)methods.First();
            Assert.IsTrue(method.Identifier.Value.Equals("TestMethod"));
        }

        //[TestMethod]
        //public void RoslynSyntaxListTest()
        //{
        //    Syntax.List(
        //    var list = new SyntaxList<MethodDeclarationSyntax>();
        //    var mD = Syntax.MethodDeclaration(Syntax.IdentifierName("typeIdenifierName"), "name");
        //    //list.Add(mD);
        //    var l = new List<MethodDeclarationSyntax>();
        //    l.Add(mD);
        //    var clist = list.Concat<MethodDeclarationSyntax>(mD);
        //    Assert.IsTrue(list.Count == 0);

        //}



        #region Region: Statement Tests

        private IEnumerable<SyntaxNode> ParseStatements(string statements)
        {
            var methods = Parse.Methods(@"void TestMethod() { " + statements + " }");
            var method = (MethodDeclarationSyntax)methods.First();
            return method.Body.DescendantNodes();
        }

        private SyntaxNode ParseStatement(string statement)
        {
            var statements = ParseStatements(statement);
            if (statements.Count() != 1) throw new Exception();
            return statements.First();
        }

        [TestMethod]
        public void RoslynEmptyStatementTest()
        {
            Assert.IsTrue(ParseStatement(";").Kind == SyntaxKind.EmptyStatement);
        }

        #endregion

        #region Region: Expression Tests

        #region Region: Literal Expressions

        [TestMethod]
        public void RoslynIntLiteralExpressionTest()
        {
            Assert.IsTrue(Parse.Expression(@"1").Kind == SyntaxKind.NumericLiteralExpression);
        }

        [TestMethod]
        public void RoslynDoubleLiteralExpressionTest()
        {
            Assert.IsTrue(Parse.Expression(@"1.1").Kind == SyntaxKind.NumericLiteralExpression);
        }

        [TestMethod]
        public void RoslynStringLiteralExpressionTest()
        {
            Assert.IsTrue(Parse.Expression(@"""hello""").Kind == SyntaxKind.StringLiteralExpression);
        }

        [TestMethod]
        public void RoslynTrueLiteralExpressionTest()
        {
            Assert.IsTrue(Parse.Expression(@"true").Kind == SyntaxKind.TrueLiteralExpression);
        }

        [TestMethod]
        public void RoslynNullLiteralExpressionTest()
        {
            Assert.IsTrue(Parse.Expression(@"null").Kind == SyntaxKind.NullLiteralExpression);
        }

        #endregion




        #endregion


    }
}
