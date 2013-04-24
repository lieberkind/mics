using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using MiCS;
using MiCS.Mappers;
using MiCSTests.TestUtils;
using MiCS.Builders;
using MiCS.Validators;

namespace MiCSTests
{
    [TestClass]
    public class StatementTests
    {

        [TestMethod]
        public void StatementVariableDeclarationTest()
        {
            var source = @"string i;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(statement is LocalDeclarationStatementSyntax);
            Assert.IsTrue(ssStatement is SS.VariableDeclarationStatement);

            var RosDeclaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var SSDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            Assert.IsTrue(RosDeclaration is VariableDeclarationSyntax);
            Assert.AreEqual(RosDeclaration.Variables.Count, SSDeclaration.Variables.Count);
            Assert.IsTrue(SSDeclaration.Variables.Count == 1);

            var RosName = RosDeclaration.Variables.First().Identifier.ValueText;
            var SSName = SSDeclaration.Variables.First().Name;

            Assert.AreEqual(RosName, SSName);
        }

        [TestMethod]
        public void StatementVariableVarDeclarationAssignmentTest()
        {
            var source = @"var i = ""foo"";";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(statement is LocalDeclarationStatementSyntax);
            Assert.IsTrue(ssStatement is SS.VariableDeclarationStatement);

            var RosDeclaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var SSDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            Assert.IsTrue(RosDeclaration is VariableDeclarationSyntax);
            Assert.AreEqual(RosDeclaration.Variables.Count, SSDeclaration.Variables.Count);
            Assert.IsTrue(SSDeclaration.Variables.Count == 1);

            var RosVal = RosDeclaration.Variables.First().Initializer.Value;
            var SSVal = SSDeclaration.Variables.First().Value;

            Assert.IsTrue(RosVal is LiteralExpressionSyntax);
            Assert.IsTrue(SSVal is SS.LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosVal;
            var SSLiteral = (SS.LiteralExpression)SSVal;

            Assert.AreEqual(RosLiteral.Token.ValueText, SSLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableAssignmentTest()
        {
            var source = @"string i = ""foo""; i = ""hello"";";
            var statement = Parse.Statements(source).ElementAt(1);
            var ssStatement = Parse.StatementsToSS(source).ElementAt(1);

            Assert.IsTrue(ssStatement is SS.ExpressionStatement);

            var SSExpr = ((SS.ExpressionStatement)ssStatement).Expression;

            Assert.IsTrue(SSExpr is SS.BinaryExpression);
        }

        [TestMethod]
        public void StatementAritmethicAssignmentTest()
        {
            var source = @"string i = 1 + 1;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);
        }

        [TestMethod]
        public void IfElseStatementTest()
        {
            var source = @"if (true) { int i; } else { int i; }";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(ssStatement is SS.IfElseStatement);

            var SSIfElseStmt = (SS.IfElseStatement)ssStatement;

            Assert.IsTrue(SSIfElseStmt.Condition is SS.LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is SS.BlockStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement is SS.BlockStatement);
        }

        [TestMethod]
        public void IfStatementTest()
        {
            var source = @"if (true) { int i; } ";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(ssStatement is SS.IfElseStatement);

            var SSIfElseStmt = (SS.IfElseStatement)ssStatement;

            Assert.IsTrue(SSIfElseStmt.Condition is SS.LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is SS.BlockStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement == null);
        }

        [TestMethod]
        public void IfElseStatementExplicitNoBlockTest()
        {
            var source = @"
                if (true)
                    int i;
                else
                    int i;
                ";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(ssStatement is SS.IfElseStatement);

            var SSIfElseStmt = (SS.IfElseStatement)ssStatement;

            Assert.IsTrue(SSIfElseStmt.Condition is SS.LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is SS.VariableDeclarationStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement is SS.VariableDeclarationStatement);
        }

        [TestMethod]
        public void IfStatementNoBlockTest()
        {
            var source = @"if (true) int i;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(ssStatement is SS.IfElseStatement);

            var SSIfElseStmt = (SS.IfElseStatement)ssStatement;

            Assert.IsTrue(SSIfElseStmt.Condition is SS.LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is SS.VariableDeclarationStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement == null);
        }

        [TestMethod]
        public void ReturnStatementTest()
        {
            var source = @"return 12;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(ssStatement is SS.ReturnStatement);

            var returnStmt = (SS.ReturnStatement)ssStatement;

            Assert.IsTrue(returnStmt.Value is SS.LiteralExpression);
        }

        [TestMethod]
        public void StatementExpressionInvocationTest()
        {
            var source = @"
            namespace ns {
                class Foo {
                    [MixedSide]
                    public void f()
                    {
                        int i;
                    }

                    [MixedSide]
                    public void g()
                    { 
                        f(); 
                    }
                }
            }";
            var roslynNamespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();

            var scriptSharpNamespace = NamespaceBuilder.Build(roslynNamespace);
            var foo = (SS.ClassSymbol)scriptSharpNamespace.Types.First();
            var g = (ScriptSharp.ScriptModel.MethodSymbol)foo.Members.ElementAt(1);
            var expressionStatement = (SS.ExpressionStatement)g.Implementation.Statements.First();
            var expression = ((SS.MethodExpression)expressionStatement.Expression);
            var invocationTarget = expression.Method;

            Assert.IsTrue(expression.ObjectReference is SS.ThisExpression);
            Assert.IsTrue(expression.Parameters.Count == 0);
            Assert.IsTrue(invocationTarget.Name.Equals("f"));

            /*
             * The property InvocationTarget.Implementation has an 
             * assertion that fails when _implementation is null.
             * the invocation target implementation is null here as 
             * this is not a method declaration but and invocation. 
             * Not sure how to verify this with an Assert?
             */

        }

    }
}
