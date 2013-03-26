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

        private ExpressionSyntax ParseExpression(string expr)
        {
            var statements = ParseStatements("var expr = " + expr + ";");
            var nodes = statements.Where(n => n.Kind == SyntaxKind.EqualsValueClause);
            if (nodes.Count() != 1) throw new Exception();
            var node = (EqualsValueClauseSyntax)nodes.First();
            return node.Value;
        }

        #region Region: Literal Expressions

        [TestMethod]
        public void RoslynIntLiteralExpressionTest()
        {
            Assert.IsTrue(ParseExpression(@"1").Kind == SyntaxKind.NumericLiteralExpression);
        }

        [TestMethod]
        public void RoslynDoubleLiteralExpressionTest()
        {
            Assert.IsTrue(ParseExpression(@"1.1").Kind == SyntaxKind.NumericLiteralExpression);
        }

        [TestMethod]
        public void RoslynStringLiteralExpressionTest()
        {
            Assert.IsTrue(ParseExpression(@"""hello""").Kind == SyntaxKind.StringLiteralExpression);
        }

        [TestMethod]
        public void RoslynTrueLiteralExpressionTest()
        {
            Assert.IsTrue(ParseExpression(@"true").Kind == SyntaxKind.TrueLiteralExpression);
        }

        [TestMethod]
        public void RoslynNullLiteralExpressionTest()
        {
            Assert.IsTrue(ParseExpression(@"null").Kind == SyntaxKind.NullLiteralExpression);
        }

        #endregion




        #endregion


    }
}
