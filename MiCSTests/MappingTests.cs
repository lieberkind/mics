﻿using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using MiCS;
using MiCS.Extensions;
using MiCSTests.TestUtils;

namespace MiCSTests
{
    /// <summary>
    /// Summary description for MapTests
    /// </summary>
    [TestClass]
    public class MappingTests
    {

        [TestMethod]
        public void NamespaceEmptyTest()
        {
            var RosNamespace = (NamespaceDeclarationSyntax)Parse.Namespaces(@"namespace TestNamespace{ }").First();
            var SSNamespace = RosNamespace.Map();

            Assert.AreEqual(RosNamespace.Name.ToString(), SSNamespace.Name);
        }

        [TestMethod]
        public void NamespaceMemberTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() { }
                } 
            }";
            var RosNamespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var SSNamespace = RosNamespace.Map();

            var RosMember = (ClassDeclarationSyntax)RosNamespace.Members.First();
            var SSMember = (ClassSymbol)SSNamespace.Types.First();
            Assert.AreEqual(RosNamespace.Members.Count, SSNamespace.Types.Count);
            Assert.AreEqual(RosMember.Identifier.ValueText, SSMember.Name);
        }

        [TestMethod]
        public void ClassMemberEmptyTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() { }
                } 
            }";
            var RosNamespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var SSNamespace = RosNamespace.Map();

            var RosMember = (ClassDeclarationSyntax)RosNamespace.Members.First();
            var SSMember = (ClassSymbol)SSNamespace.Types.First();

            var RosMethod = (MethodDeclarationSyntax)RosMember.Members.First();
            var SSMethod = (ScriptSharp.ScriptModel.MethodSymbol)SSMember.Members.First();

            Assert.AreEqual(RosMethod.Identifier.ValueText, SSMethod.Name);
            Assert.AreEqual(RosMethod.Body.Statements.Count, SSMethod.Implementation.Statements.Count);
        }

        [TestMethod]
        public void ClassMemberTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() { int i; }
                } 
            }";
            var RosNamespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var SSNamespace = RosNamespace.Map();

            var RosMember = (ClassDeclarationSyntax)RosNamespace.Members.First();
            var SSMember = (ClassSymbol)SSNamespace.Types.First();

            var RosMethod = (MethodDeclarationSyntax)RosMember.Members.First();
            var SSMethod = (ScriptSharp.ScriptModel.MethodSymbol)SSMember.Members.First();

            var RosStmt = RosMethod.Body.Statements.First();
            var SSStmt = SSMethod.Implementation.Statements.First();

            Assert.IsTrue(RosStmt is LocalDeclarationStatementSyntax);
            Assert.IsTrue(SSStmt is VariableDeclarationStatement);
        }



        #region Region: Statement Test


        [TestMethod]
        public void StatementVariableDeclarationTest()
        {
            var RosStmt = Parse.Statement("string i;");
            var SSStmt = RosStmt.Map();

            Assert.IsTrue(RosStmt is LocalDeclarationStatementSyntax);
            Assert.IsTrue(SSStmt is VariableDeclarationStatement);

            var RosDeclaration = ((LocalDeclarationStatementSyntax)RosStmt).Declaration;
            var SSDeclaration = (VariableDeclarationStatement)SSStmt;

            Assert.IsTrue(RosDeclaration is VariableDeclarationSyntax);
            Assert.AreEqual(RosDeclaration.Variables.Count, SSDeclaration.Variables.Count);
            Assert.IsTrue(SSDeclaration.Variables.Count == 1);

            var RosName = RosDeclaration.Variables.First().Identifier.ValueText;
            var SSName = SSDeclaration.Variables.First().Name;

            Assert.AreEqual(RosName, SSName);
        }

        [TestMethod]
        public void StatementVariableDeclarationAssignmentTest()
        {
            var RosStmt = Parse.Statement(@"string i = ""foo"";");
            var SSStmt = RosStmt.Map();

            Assert.IsTrue(RosStmt is LocalDeclarationStatementSyntax);
            Assert.IsTrue(SSStmt is VariableDeclarationStatement);

            var RosDeclaration = ((LocalDeclarationStatementSyntax)RosStmt).Declaration;
            var SSDeclaration = (VariableDeclarationStatement)SSStmt;

            Assert.IsTrue(RosDeclaration is VariableDeclarationSyntax);
            Assert.AreEqual(RosDeclaration.Variables.Count, SSDeclaration.Variables.Count);
            Assert.IsTrue(SSDeclaration.Variables.Count == 1);

            var RosVal = RosDeclaration.Variables.First().Initializer.Value;
            var SSVal = SSDeclaration.Variables.First().Value;

            Assert.IsTrue(RosVal is LiteralExpressionSyntax);
            Assert.IsTrue(SSVal is LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosVal;
            var SSLiteral = (LiteralExpression)SSVal;

            Assert.AreEqual(RosLiteral.Token.ValueText, SSLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableAssignmentTest()
        {
            var RosStmt = Parse.Statements(@"string i = ""foo""; i = ""hello"";").ElementAt(1);
            var SSStmt = RosStmt.Map();

            Assert.IsTrue(SSStmt is ExpressionStatement);

            var SSExpr = ((ExpressionStatement)SSStmt).Expression;

            Assert.IsTrue(SSExpr is BinaryExpression);
        }

        [TestMethod]
        public void StatementAritmethicAssignmentTest()
        {
            var RosStmt = Parse.Statement(@"string i = 1 + 1;");
            var SSStmt = RosStmt.Map();
        }

        [TestMethod]
        public void IfElseStatementTest()
        {
            var RosStmt = Parse.Statement(@"if (true) { int i; } else { int i; }");
            var SSStmt = RosStmt.Map();

            Assert.IsTrue(SSStmt is IfElseStatement);

            var SSIfElseStmt = (IfElseStatement)SSStmt;

            Assert.IsTrue(SSIfElseStmt.Condition is LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is BlockStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement is BlockStatement);
        }

        [TestMethod]
        public void IfStatementTest()
        {
            var RosStmt = Parse.Statement(@"if (true) { int i; } ");
            var SSStmt = RosStmt.Map();

            Assert.IsTrue(SSStmt is IfElseStatement);

            var SSIfElseStmt = (IfElseStatement)SSStmt;

            Assert.IsTrue(SSIfElseStmt.Condition is LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is BlockStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement == null);
        }

        [TestMethod]
        public void IfElseStatementNoBlockTest()
        {
            var RosStmt = Parse.Statement(@"
                if (true)
                    int i;
                else
                    int i;
                ");

            var SSStmt = RosStmt.Map();

            Assert.IsTrue(SSStmt is IfElseStatement);

            var SSIfElseStmt = (IfElseStatement)SSStmt;

            Assert.IsTrue(SSIfElseStmt.Condition is LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is VariableDeclarationStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement is VariableDeclarationStatement);
        }

        [TestMethod]
        public void IfStatementNoBlockTest()
        {
            var RosStmt = Parse.Statement(@"if (true) int i;");
            var SSStmt = RosStmt.Map();

            Assert.IsTrue(SSStmt is IfElseStatement);

            var SSIfElseStmt = (IfElseStatement)SSStmt;

            Assert.IsTrue(SSIfElseStmt.Condition is LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is VariableDeclarationStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement == null);
        }

        [TestMethod]
        public void ReturnStatementTest()
        {
            var RosStmt = Parse.Statement(@"return 12;");
            var SSStmt = RosStmt.Map();

            Assert.IsTrue(SSStmt is ReturnStatement);

            var returnStmt = (ReturnStatement)SSStmt;

            Assert.IsTrue(returnStmt.Value is LiteralExpression);
        }


        // Todo: Add more statements tests...

        #endregion

        #region Region: Expression Tests

        [TestMethod]
        public void ConditionalExpressionTest()
        {
            var RosExpr = Parse.Expression("2 > 1 ? 10 : 0");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is ConditionalExpression);

            var cExpr = (ConditionalExpression)SSExpr;

            Assert.IsTrue(cExpr.Condition is BinaryExpression);
            Assert.IsTrue(cExpr.TrueValue is LiteralExpression);
            Assert.IsTrue(cExpr.FalseValue is LiteralExpression);
        }

        [TestMethod]
        public void ObjectCreationExpressionSyntaxTest()
        {
            var source = @"
                public class Person
                {

                    public Person()
                    {
                    }

                    [MixedSide]
                    public void TestFunction(string name, string name2, string name3)
                    {
                        Person p = new Person(""Tomas"");
                    }
                }";

            var tree = SyntaxTree.ParseText(source);
            var root = tree.GetRoot();

            Assert.IsTrue(Syntax.IsCompleteSubmission(tree));

            var oces = root.DescendantNodes().Where(n => n.Kind == SyntaxKind.ObjectCreationExpression);

            Assert.IsTrue(oces.Count() == 1);

            // Todo: Fuckly hack! This should be changed!
            var SSNe = ((ObjectCreationExpressionSyntax)oces.First()).Map(new ClassSymbol("Person", new ScriptSharp.ScriptModel.NamespaceSymbol("dummyNamespace", null)));
            Assert.IsTrue(SSNe.EvaluatedType.GeneratedName == "Person");
        }

        [TestMethod]
        public void ExpressionUnaryExpressionTest()
        {
            var RosExpr = Parse.Expression(@"-1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(RosExpr is PrefixUnaryExpressionSyntax);
            Assert.IsTrue(SSExpr is UnaryExpression);

            var RosUnaryExpr = (PrefixUnaryExpressionSyntax)RosExpr;
            var SSUnaryExpression = (UnaryExpression)SSExpr;

            Assert.IsTrue(RosUnaryExpr.OperatorToken.Kind == SyntaxKind.MinusToken);
            Assert.IsTrue(SSUnaryExpression.Operator == Operator.Minus);
            Assert.IsTrue(SSUnaryExpression.Operand is LiteralExpression);

            var SSLiteral = (LiteralExpression)SSUnaryExpression.Operand;
            Assert.AreEqual(SSLiteral.Value, 1);
        }


        #region Region: Literal Expression


        [TestMethod]
        public void ExpressionStringLiteralTest()
        {
            var RosExpr = Parse.Expression(@"""foo""");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.ValueText, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, "foo");
        }

        [TestMethod]
        public void ExpressionIntLiteralTest()
        {
            var RosExpr = Parse.Expression(@"1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.Value, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, 1);
        }

        [TestMethod]
        public void ExpressionTrueLiteralTest()
        {
            var RosExpr = Parse.Expression(@"true");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.Value, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, true);
        }

        [TestMethod]
        public void ExpressionFalseLiteralTest()
        {
            var RosExpr = Parse.Expression(@"false");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.Value, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, false);
        }

        [TestMethod]
        public void ExpressionNullLiteralTest()
        {
            var RosExpr = Parse.Expression(@"null");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.Value, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, null);
        }


        #endregion

        #region Region: Binary Expression Tests

        [TestMethod]
        public void ExpressionAssignmentTest()
        {
            var RosStmt = Parse.Statements(@"string i = ""foo""; i = ""hello"";").ElementAt(1);
            var SSStmt = RosStmt.Map();

            Assert.IsTrue(SSStmt is ExpressionStatement);

            var SSExpr = (BinaryExpression)((ExpressionStatement)SSStmt).Expression;

            Assert.IsTrue(SSExpr.Operator == Operator.Equals);
            Assert.IsTrue(SSExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSExpr.LeftOperand is LocalExpression);
            Assert.IsTrue(((LocalExpression)SSExpr.LeftOperand).Symbol is VariableSymbol);
        }


        [TestMethod]
        public void ExpressionPlusBinaryTest()
        {
            var RosExpr = Parse.Expression("1 + 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.Plus);
        }

        [TestMethod]
        public void ExpressionMinusBinaryTest()
        {
            var RosExpr = Parse.Expression("1 - 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.Minus);
        }

        [TestMethod]
        public void ExpressionMultiplyBinaryTest()
        {
            var RosExpr = Parse.Expression("1 * 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.Multiply);
        }

        [TestMethod]
        public void ExpressionDivideBinaryTest()
        {
            var RosExpr = Parse.Expression("1 / 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.Divide);
        }

        [TestMethod]
        public void ExpressionModulusBinaryTest()
        {
            var RosExpr = Parse.Expression("1 % 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.Mod);
        }


        [TestMethod]
        public void ExpressionRelationalEqualsTest()
        {
            var RosExpr = Parse.Expression("1 == 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.EqualEqual);
        }

        [TestMethod]
        public void ExpressionRelationalNotEqualsTest()
        {
            var RosExpr = Parse.Expression("1 != 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.NotEqual);
        }

        [TestMethod]
        public void ExpressionRelationalGreaterTest()
        {
            var RosExpr = Parse.Expression("1 > 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.Greater);
        }

        [TestMethod]
        public void ExpressionRelationalLessTest()
        {
            var RosExpr = Parse.Expression("1 < 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.Less);
        }

        [TestMethod]
        public void ExpressionRelationalGreaterEqualsTest()
        {
            var RosExpr = Parse.Expression("1 >= 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.GreaterEqual);
        }

        [TestMethod]
        public void ExpressionRelationalLessEqualsTest()
        {
            var RosExpr = Parse.Expression("1 <= 1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.LessEqual);
        }

        [TestMethod]
        public void ExpressionLogicalAndTest()
        {
            var RosExpr = Parse.Expression("true && -1 > 6");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is BinaryExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.LogicalAnd);
        }

        [TestMethod]
        public void ExpressionLogicalOrTest()
        {
            var RosExpr = Parse.Expression("true || 1 > -1");
            var SSExpr = RosExpr.Map();

            Assert.IsTrue(SSExpr is BinaryExpression);

            var SSBinaryExpr = (BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is BinaryExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == Operator.LogicalOr);
        }






        [TestMethod]
        public void ExpressionNestedBinaryTest()
        {
            var RosExpr = Parse.Expression("1 + 1 + 1");
            var SSExpr = RosExpr.Map();
        }

        #endregion


        // Todo: Add more expression tests...

        #endregion



    }
}
