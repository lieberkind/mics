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
    public class ExpressionTests
    {

        [TestMethod]
        public void ConditionalExpression_Test()
        {
            var expression = Parse.Expression("2 > 1 ? 10 : 0");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.ConditionalExpression);

            var ssConditionalExpression = (SS.ConditionalExpression)ssExpression;

            Assert.IsTrue(ssConditionalExpression.Condition is SS.BinaryExpression);
            Assert.IsTrue(ssConditionalExpression.TrueValue is SS.LiteralExpression);
            Assert.IsTrue(ssConditionalExpression.FalseValue is SS.LiteralExpression);
        }

        [TestMethod]
        public void ObjectCreationExpression_Test()
        {
            var source = @"
                public class Person
                {
                    public Person() { }

                    [MixedSide]
                    public void TestFunction(string name, string name2, string name3)
                    {
                        Person p = new Person();
                    }
                }";

            var ssClass = Parse.ClassesToSS(source).First();
            var ssStatement = (SS.VariableDeclarationStatement)ssClass.Methods().First().Statements().First();
            var ssNewExpression = (SS.NewExpression)ssStatement.Variables.ElementAt(0).Value;

            Assert.AreEqual(ssNewExpression.EvaluatedType.GeneratedName, "Person");
        }

        [TestMethod]
        public void UnaryExpression_MinusTest()
        {
            var expression = Parse.Expression(@"-1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(expression is PrefixUnaryExpressionSyntax);
            Assert.IsTrue(ssExpression is SS.UnaryExpression);

            var unaryExpression = (PrefixUnaryExpressionSyntax)expression;
            var ssUnaryExpression = (SS.UnaryExpression)ssExpression;

            Assert.IsTrue(unaryExpression.OperatorToken.Kind == SyntaxKind.MinusToken);
            Assert.IsTrue(ssUnaryExpression.Operator == SS.Operator.Minus);
            Assert.IsTrue(ssUnaryExpression.Operand is SS.LiteralExpression);

            var ssLiteral = (SS.LiteralExpression)ssUnaryExpression.Operand;
            Assert.AreEqual(ssLiteral.Value, 1);
        }

        [TestMethod]
        public void ParenthesizedExpression_Test()
        {
            var parenthesizedExpression = (ParenthesizedExpressionSyntax)Parse.Expression(@"(-1)");
            var expression = parenthesizedExpression.Expression;
            var ssExpression = ExpressionBuilder.BuildExpression(parenthesizedExpression);

            Assert.IsTrue(expression is PrefixUnaryExpressionSyntax);
            Assert.IsTrue(ssExpression is SS.UnaryExpression);

            var unaryExpression = (PrefixUnaryExpressionSyntax)expression;
            var ssUnaryExpression = (SS.UnaryExpression)ssExpression;
            Assert.IsTrue(ssUnaryExpression.Parenthesized);

            Assert.IsTrue(unaryExpression.OperatorToken.Kind == SyntaxKind.MinusToken);
            Assert.IsTrue(ssUnaryExpression.Operator == SS.Operator.Minus);
            Assert.IsTrue(ssUnaryExpression.Operand is SS.LiteralExpression);

            var ssLiteral = (SS.LiteralExpression)ssUnaryExpression.Operand;
            Assert.AreEqual(ssLiteral.Value, 1);

        }

        [TestMethod]
        public void UnaryExpression_LogicalNotTest()
        {
            var expression = Parse.Expression(@"!true");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(expression is PrefixUnaryExpressionSyntax);
            Assert.IsTrue(ssExpression is SS.UnaryExpression);

            var unaryExpression = (PrefixUnaryExpressionSyntax)expression;
            var ssUnaryExpression = (SS.UnaryExpression)ssExpression;

            Assert.IsTrue(unaryExpression.OperatorToken.Kind == SyntaxKind.ExclamationToken);
            Assert.IsTrue(ssUnaryExpression.Operator == SS.Operator.LogicalNot);
            Assert.IsTrue(ssUnaryExpression.Operand is SS.LiteralExpression);

            var ssLiteral = (SS.LiteralExpression)ssUnaryExpression.Operand;
            Assert.AreEqual(ssLiteral.Value, true);
        }

        [TestMethod]
        public void FieldExpression_Test()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    int f() { var s = ""hello""; return s.Length; }
                } 
            }";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var ssMethod = (SS.MethodSymbol)ssNamespace.Types.ElementAt(0).Members.ElementAt(0);
            var ssReturnStatement = (SS.ReturnStatement)ssMethod.Implementation.Statements.ElementAt(1);

            Assert.IsTrue(ssReturnStatement.Value is SS.FieldExpression);

            var ssFieldExpression = (SS.FieldExpression)ssReturnStatement.Value;
            Assert.IsTrue(ssFieldExpression.Field.AssociatedType.Name.Equals("Int32"));
        }

        #region Region: Invocation Expression

        [TestMethod]
        public void InvocationExpression_ExplicitThisTest()
        {
            var source = @"
                class TestClass { 
                    [MixedSide]
                    int f() { return 3; }

                    [MixedSide]
                    int g() { return this.f(); }
                }";
            var ssClass = Parse.ClassesToSS(source).First();
            var ssReturnStatement = (SS.ReturnStatement)ssClass.Methods().ElementAt(1).Statements().First();
            var ssInvocation = (SS.MethodExpression)ssReturnStatement.Value;
            var ssMethodSymbol = (SS.MethodSymbol)ssInvocation.Method;

            Assert.AreEqual(ssMethodSymbol.AssociatedType.Name, "Int32");
            Assert.AreEqual(ssMethodSymbol.Name, "f");
        }

        [TestMethod]
        public void InvocationExpression_Test()
        {
            var source = @"
                class TestClass { 
                    [MixedSide]
                    int f() { return 3; }

                    [MixedSide]
                    int g() { return f(); }
                }";
            var ssClass = Parse.ClassesToSS(source).First();
            var ssReturnStatement = (SS.ReturnStatement)ssClass.Methods().ElementAt(1).Statements().First();
            var ssInvocation = (SS.MethodExpression)ssReturnStatement.Value;
            var ssMethodSymbol = (SS.MethodSymbol)ssInvocation.Method;

            Assert.AreEqual(ssMethodSymbol.AssociatedType.Name, "Int32");
            Assert.AreEqual(ssMethodSymbol.Name, "f");
        }

        [TestMethod]
        public void InvocationExpression_StaticTest()
        {
            var source = @"
                class TestClass { 
                    [MixedSide]
                    int f() { return MyType.f(); }
                } 

                class MyType { 
                    [MixedSide]
                    public static int f() { return 1; }
                }";
            var ssClass = Parse.ClassesToSS(source).First();
            var ssReturnStatement = (SS.ReturnStatement)ssClass.Methods().First().Statements().First();
            var ssInvocation = (SS.MethodExpression)ssReturnStatement.Value;
            var ssMethodSymbol = (SS.MethodSymbol)ssInvocation.Method;

            Assert.AreEqual(ssInvocation.ObjectReference.EvaluatedType.Name, "MyType");
            Assert.AreEqual(ssMethodSymbol.AssociatedType.Name, "Int32");
            Assert.AreEqual(ssMethodSymbol.Name, "f");
        }

        #endregion

        #region Region: Binary Expression Tests

        [TestMethod]
        public void BinaryExpression_AssignmentTest()
        {
            var source = @"string i = ""foo""; i = ""hello"";";
            var ssStatement = Parse.StatementsToSS(source).ElementAt(1);

            Assert.IsTrue(ssStatement is SS.ExpressionStatement);

            var ssBinaryExpression = (SS.BinaryExpression)((SS.ExpressionStatement)ssStatement).Expression;

            Assert.IsTrue(ssBinaryExpression.Operator == SS.Operator.Equals);
            Assert.IsTrue(ssBinaryExpression.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinaryExpression.LeftOperand is SS.LocalExpression);
            Assert.IsTrue(((SS.LocalExpression)ssBinaryExpression.LeftOperand).Symbol is SS.VariableSymbol);
        }

        [TestMethod]
        public void BinaryExpression_PlusTest()
        {
            var expression = Parse.Expression("1 + 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary= (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.Plus);
        }

        [TestMethod]
        public void BinaryExpression_MinusTest()
        {
            var expression = Parse.Expression("1 - 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary= (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.Minus);
        }

        [TestMethod]
        public void BinaryExpression_MultiplyTest()
        {
            var expression = Parse.Expression("1 * 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary= (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.Multiply);
        }

        [TestMethod]
        public void BinaryExpression_DivideTest()
        {
            var expression = Parse.Expression("1 / 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary= (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.Divide);
        }

        [TestMethod]
        public void BinaryExpression_ModulusTest()
        {
            var expression = Parse.Expression("1 % 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary= (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.Mod);
        }

        [TestMethod]
        public void BinaryExpression_RelationalEqualsTest()
        {
            var expression = Parse.Expression("1 == 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary = (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.EqualEqual);
        }

        [TestMethod]
        public void BinaryExpression_RelationalNotEqualsTest()
        {
            var expression = Parse.Expression("1 != 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary = (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.NotEqual);
        }

        [TestMethod]
        public void BinaryExpression_RelationalGreaterThanTest()
        {
            var expression = Parse.Expression("1 > 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary = (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.Greater);
        }

        [TestMethod]
        public void BinaryExpression_RelationalLessThanTest()
        {
            var expression = Parse.Expression("1 < 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary = (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.Less);
        }

        [TestMethod]
        public void BinaryExpression_RelationalGreaterThanEqualsTest()
        {
            var expression = Parse.Expression("1 >= 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary= (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.GreaterEqual);
        }

        [TestMethod]
        public void BinaryExpression_RelationalLessThanEqualsTest()
        {
            var expression = Parse.Expression("1 <= 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary= (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.LessEqual);
        }

        [TestMethod]
        public void BinaryExpression_LogicalAndTest()
        {
            var expression = Parse.Expression("true && -1 > 6");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary= (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.BinaryExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.LogicalAnd);
        }

        [TestMethod]
        public void BinaryExpression_LogicalOrTest()
        {
            var expression = Parse.Expression("true || 1 > -1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary= (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(ssBinary.RightOperand is SS.BinaryExpression);
            Assert.IsTrue(ssBinary.Operator == SS.Operator.LogicalOr);
        }

        [TestMethod]
        public void BinaryExpression_NestedTest()
        {
            var expression = Parse.Expression("1 + 1 + 1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(ssExpression is SS.BinaryExpression);

            var ssBinary = (SS.BinaryExpression)ssExpression;

            Assert.IsTrue(ssBinary.LeftOperand is SS.BinaryExpression);
        }



        #endregion

        #region Region: Literal Expression

        [TestMethod]
        public void LiteralExpression_StringTest()
        {
            var expression = Parse.Expression(@"""foo""");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(expression is LiteralExpressionSyntax);
            Assert.IsTrue(ssExpression is SS.LiteralExpression);

            var literal = (LiteralExpressionSyntax)expression;
            var ssLiteral = (SS.LiteralExpression)ssExpression;

            Assert.AreEqual(literal.Token.ValueText, ssLiteral.Value);
            Assert.AreEqual(ssLiteral.Value, "foo");
            Assert.AreEqual(ssLiteral.EvaluatedType.FullName, "System.String");
        }

        [TestMethod]
        public void LiteralExpression_IntTest()
        {
            var expression = Parse.Expression(@"1");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(expression is LiteralExpressionSyntax);
            Assert.IsTrue(ssExpression is SS.LiteralExpression);

            var literal = (LiteralExpressionSyntax)expression;
            var ssLiteral = (SS.LiteralExpression)ssExpression;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssLiteral.Value, 1);
            Assert.AreEqual(ssLiteral.EvaluatedType.FullName, "System.Int32");
        }

        [TestMethod]
        public void LiteralExpression_TrueTest()
        {
            var expression = Parse.Expression(@"true");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(expression is LiteralExpressionSyntax);
            Assert.IsTrue(ssExpression is SS.LiteralExpression);

            var literal = (LiteralExpressionSyntax)expression;
            var ssLiteral = (SS.LiteralExpression)ssExpression;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssLiteral.Value, true);
            Assert.AreEqual(ssLiteral.EvaluatedType.FullName, "System.Boolean");
        }

        [TestMethod]
        public void LiteralExpression_FalseTest()
        {
            var expression = Parse.Expression(@"false");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(expression is LiteralExpressionSyntax);
            Assert.IsTrue(ssExpression is SS.LiteralExpression);

            var literal = (LiteralExpressionSyntax)expression;
            var ssLiteral = (SS.LiteralExpression)ssExpression;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssLiteral.Value, false);
            Assert.AreEqual(ssLiteral.EvaluatedType.FullName, "System.Boolean");
        }

        [TestMethod]
        public void LiteralExpression_NullTest()
        {
            var expression = Parse.Expression(@"null");
            var ssExpression = ExpressionBuilder.BuildExpression(expression);

            Assert.IsTrue(expression is LiteralExpressionSyntax);
            Assert.IsTrue(ssExpression is SS.LiteralExpression);

            var literal = (LiteralExpressionSyntax)expression;
            var ssLiteral = (SS.LiteralExpression)ssExpression;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssLiteral.Value, null);
            Assert.AreEqual(ssLiteral.EvaluatedType.FullName, "System.Object");
        }


        #endregion
    }
}
