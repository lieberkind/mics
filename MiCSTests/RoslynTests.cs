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
using MiCS;
namespace MiCSTests
{
    [TestClass]
    public class RoslynTests
    {

        [TestMethod]
        public void HasAttributeTest()
        {
            var namespaces = Parse.Namespaces(@"
            namespace TestNameSpace
            { 
                [ScriptIgnoreNamespace]
                [ScriptImport]
                class TestClass 
                { 
                    [MixedSide]
                    void f() { } 
                } 
            }");

            var @namespace = (NamespaceDeclarationSyntax)namespaces.First();
            var @class = (ClassDeclarationSyntax)@namespace.Members[0];

            Assert.IsTrue(@class.HasAttribute("ScriptImport"));
        }

        [TestMethod]
        public void RoslynNamespaceTest()
        {
            var namespaces = Parse.Namespaces(@"namespace TestNameSpace{ class TestClass { [MixedSide]
                                                void f() { } } }");
            Assert.IsTrue(namespaces.Count() > 0);

            var @namespace = (NamespaceDeclarationSyntax)namespaces.First();
            Assert.IsTrue(((IdentifierNameSyntax)@namespace.Name).Identifier.ValueText.Equals("TestNameSpace"));
        }

        [TestMethod]
        public void RoslynClassTest()
        {
            var classes = Parse.Classes(@"class TestClass{ [MixedSide]
                                                void f() { } }");
            Assert.IsTrue(classes.Count() == 1);

            var cl = (ClassDeclarationSyntax)classes.First();
            Assert.IsTrue(cl.Identifier.Value.Equals("TestClass"));
        }

        [TestMethod]
        public void RoslynMethodTest()
       { 
            var methods = Parse.Methods(@"void [MixedSide]
                                                TestMethod() { }");
            Assert.IsTrue(methods.Count() == 1);

            var method = (MethodDeclarationSyntax)methods.First();
            Assert.IsTrue(method.Identifier.Value.Equals("TestMethod"));
        }





        #region Region: Statement Tests



        private SyntaxNode ParseStatement(string statement)
        {
            var statements = Parse.Statements(statement);
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
