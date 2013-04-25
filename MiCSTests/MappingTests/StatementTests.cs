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
        public void VariableDeclarationStatement_DeclarationTest()
        {
            var source = @"string i;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(statement is LocalDeclarationStatementSyntax);
            Assert.IsTrue(ssStatement is SS.VariableDeclarationStatement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            Assert.IsTrue(declaration is VariableDeclarationSyntax);
            Assert.AreEqual(declaration.Variables.Count, ssDeclaration.Variables.Count);
            Assert.IsTrue(ssDeclaration.Variables.Count == 1);

            var name = declaration.Variables.First().Identifier.ValueText;
            var ssName = ssDeclaration.Variables.First().Name;

            Assert.AreEqual(name, ssName);
        }

        [TestMethod]
        public void VariableDeclarationStatement_VarDeclarationTest()
        {
            var source = @"var i = ""foo"";";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(statement is LocalDeclarationStatementSyntax);
            Assert.IsTrue(ssStatement is SS.VariableDeclarationStatement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            Assert.IsTrue(declaration is VariableDeclarationSyntax);
            Assert.AreEqual(declaration.Variables.Count, ssDeclaration.Variables.Count);
            Assert.IsTrue(ssDeclaration.Variables.Count == 1);

            var @value = declaration.Variables.First().Initializer.Value;
            var ssValue = ssDeclaration.Variables.First().Value;

            Assert.IsTrue(@value is LiteralExpressionSyntax);
            Assert.IsTrue(ssValue is SS.LiteralExpression);

            var literal = (LiteralExpressionSyntax)@value;
            var ssLiteral = (SS.LiteralExpression)ssValue;

            Assert.AreEqual(literal.Token.ValueText, ssLiteral.Value);
        }

        [TestMethod]
        public void ExpressionStatement_AssignmentTest()
        {
            var source = @"string i = ""foo""; i = ""hello"";";
            var statement = Parse.Statements(source).ElementAt(1);
            var ssStatement = Parse.StatementsToSS(source).ElementAt(1);

            Assert.IsTrue(ssStatement is SS.ExpressionStatement);

            var SSExpr = ((SS.ExpressionStatement)ssStatement).Expression;

            Assert.IsTrue(SSExpr is SS.BinaryExpression);
        }

        [TestMethod]
        public void VariableDeclarationStatement_AritmethicAssignmentTest()
        {
            var source = @"string i = 1 + 1;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);
        }

        [TestMethod]
        public void IfElseStatement_Test()
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
        public void IfStatement_Test()
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
        public void IfElseStatement_NoBlockTest()
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
        public void IfStatement_NoBlockTest()
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
        public void ReturnStatement_Test()
        {
            var source = @"return 12;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(ssStatement is SS.ReturnStatement);

            var returnStmt = (SS.ReturnStatement)ssStatement;

            Assert.IsTrue(returnStmt.Value is SS.LiteralExpression);
        }

        [TestMethod]
        public void ExpressionStatement_InvocationTest()
        {
            var source = @"
                    [MixedSide]
                    public void f()
                    {
                        int i;
                    }

                    [MixedSide]
                    public void g()
                    { 
                        f(); 
                    }";

            var ssMethod = Parse.MethodsToSS(source).ElementAt(1);
            var ssExpressionStatement = (SS.ExpressionStatement)ssMethod.Implementation.Statements.First();
            var ssExpression = ((SS.MethodExpression)ssExpressionStatement.Expression);
            var ssInvocationTarget = ssExpression.Method;

            Assert.IsTrue(ssExpression.ObjectReference is SS.ThisExpression);
            Assert.IsTrue(ssExpression.Parameters.Count == 0);
            Assert.IsTrue(ssInvocationTarget.Name.Equals("f"));

        }

    }
}
