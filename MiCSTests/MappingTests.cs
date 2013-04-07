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
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(@"namespace TestNamespace{ }").First();
            var ssNamespace = @namespace.Map();

            Assert.AreEqual(@namespace.Name.ToString(), ssNamespace.Name);
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
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();
            Assert.AreEqual(@namespace.Members.Count, ssNamespace.Types.Count);
            Assert.AreEqual(member.Identifier.ValueText, ssMember.Name);
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
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();

            var method = (MethodDeclarationSyntax)member.Members.First();
            var ssMethod = (ScriptSharp.ScriptModel.MethodSymbol)ssMember.Members.First();

            Assert.AreEqual(method.Identifier.ValueText, ssMethod.Name);
            Assert.AreEqual(method.Body.Statements.Count, ssMethod.Implementation.Statements.Count);
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
            MiCSManager.Initiate(source);
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();

            var method = (MethodDeclarationSyntax)member.Members.First();
            var ssMethod = (ScriptSharp.ScriptModel.MethodSymbol)ssMember.Members.First();

            var statement = method.Body.Statements.First();
            var ssStatement = ssMethod.Implementation.Statements.First();

            Assert.IsTrue(statement is LocalDeclarationStatementSyntax);
            Assert.IsTrue(ssStatement is SS.VariableDeclarationStatement);
        }

        [TestMethod]
        public void ClassMemberDeclarationPredefinedReturnTypeTest()
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
            var SSMember = (SS.ClassSymbol)SSNamespace.Types.First();

            var RosMethod = (MethodDeclarationSyntax)RosMember.Members.First();
            var SSMethod = (ScriptSharp.ScriptModel.MethodSymbol)SSMember.Members.First();

            var RosReturnTypeName = ((PredefinedTypeSyntax)RosMethod.ReturnType).Keyword.ValueText;
            Assert.AreEqual(RosReturnTypeName, SSMethod.AssociatedType.Name);

            var RosStmt = RosMethod.Body.Statements.First();
            var SSStmt = SSMethod.Implementation.Statements.First();

            Assert.IsTrue(RosStmt is LocalDeclarationStatementSyntax);
            Assert.IsTrue(SSStmt is SS.VariableDeclarationStatement);
        }

        [TestMethod]
        public void ClassMemberDeclarationCustomReturnTypeTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    MyType f() { return new MyType(); }
                } 

                class MyType { 
                    [MixedSide]
                    void f() { int i; }
                } 
            }";
            var RosNamespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var SSNamespace = NamespaceBuilder.Build(RosNamespace);

            var RosMember = (ClassDeclarationSyntax)RosNamespace.Members.First();
            var SSMember = (SS.ClassSymbol)SSNamespace.Types.First();

            var RosMethod = (MethodDeclarationSyntax)RosMember.Members.First();
            var SSMethod = (ScriptSharp.ScriptModel.MethodSymbol)SSMember.Members.First();

            var RosReturnTypeName = ((IdentifierNameSyntax)RosMethod.ReturnType).Identifier.ValueText;
            Assert.AreEqual(RosReturnTypeName, SSMethod.AssociatedType.Name);

        }


        #region Region: Statement Test


        [TestMethod]
        public void StatementVariableDeclarationTest()
        {
            var RosStmt = Parse.Statement("string i;");
            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(RosStmt is LocalDeclarationStatementSyntax);
            Assert.IsTrue(SSStmt is SS.VariableDeclarationStatement);

            var RosDeclaration = ((LocalDeclarationStatementSyntax)RosStmt).Declaration;
            var SSDeclaration = (SS.VariableDeclarationStatement)SSStmt;

            Assert.IsTrue(RosDeclaration is VariableDeclarationSyntax);
            Assert.AreEqual(RosDeclaration.Variables.Count, SSDeclaration.Variables.Count);
            Assert.IsTrue(SSDeclaration.Variables.Count == 1);

            var RosName = RosDeclaration.Variables.First().Identifier.ValueText;
            var SSName = SSDeclaration.Variables.First().Name;

            Assert.AreEqual(RosName, SSName);
        }

        #region Region: Testing TypeSymbol Mapping

        [TestMethod]
        public void StatementVariableDeclarationStringAssignmentTest()
        {

            var RosStmt = Parse.Statement(@"string i = ""foo"";");
            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(RosStmt is LocalDeclarationStatementSyntax);
            Assert.IsTrue(SSStmt is SS.VariableDeclarationStatement);

            var RosDeclaration = ((LocalDeclarationStatementSyntax)RosStmt).Declaration;
            var SSDeclaration = (SS.VariableDeclarationStatement)SSStmt;

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
        public void StatementVariableDeclarationIntAssignmentTest()
        {

            var statement = Parse.Statement(@"int i = -1;");
            var ssStatement = StatementBuilder.Build(statement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)((PrefixUnaryExpressionSyntax)declaration.Variables.First().Initializer.Value).Operand;
            var ssLiteral = (SS.LiteralExpression)((SS.UnaryExpression)ssDeclaration.Variables.First().Value).Operand;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationUIntAssignmentTest()
        {

            var statement = Parse.Statement(@"uint i = 0U;");
            var ssStatement = StatementBuilder.Build(statement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationBooleanAssignmentTest()
        {

            var statement = Parse.Statement(@"int b = true;");
            var ssStatement = StatementBuilder.Build(statement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationLongAssignmentTest()
        {

            var statement = Parse.Statement(@"long l = 1L;");
            var ssStatement = StatementBuilder.Build(statement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationULongAssignmentTest()
        {

            var statement = Parse.Statement(@"ulong l = 1UL;");
            var ssStatement = StatementBuilder.Build(statement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationDecimalAssignmentTest()
        {
            var statement = Parse.Statement(@"decimal d = 1.0m;");
            var ssStatement = StatementBuilder.Build(statement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationDoubleAssignmentTest()
        {
            var statement = Parse.Statement(@"double d = 1.0d;");
            var ssStatement = StatementBuilder.Build(statement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationShortAssignmentTest()
        {
            var statement = Parse.Statement(@"short s = (short)1;");
            var ssStatement = StatementBuilder.Build(statement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)((CastExpressionSyntax)declaration.Variables.First().Initializer.Value).Expression;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationUShortAssignmentTest()
        {
            var statement = Parse.Statement(@"ushort s = (ushort)1;");
            var ssStatement = StatementBuilder.Build(statement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)((CastExpressionSyntax)declaration.Variables.First().Initializer.Value).Expression;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        #endregion



        [TestMethod]
        public void StatementVariableVarDeclarationAssignmentTest()
        {
            var RosStmt = Parse.Statement(@"var i = ""foo"";");
            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(RosStmt is LocalDeclarationStatementSyntax);
            Assert.IsTrue(SSStmt is SS.VariableDeclarationStatement);

            var RosDeclaration = ((LocalDeclarationStatementSyntax)RosStmt).Declaration;
            var SSDeclaration = (SS.VariableDeclarationStatement)SSStmt;

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
            var RosStmt = Parse.Statements(@"string i = ""foo""; i = ""hello"";").ElementAt(1);
            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(SSStmt is SS.ExpressionStatement);

            var SSExpr = ((SS.ExpressionStatement)SSStmt).Expression;

            Assert.IsTrue(SSExpr is SS.BinaryExpression);
        }

        [TestMethod]
        public void StatementAritmethicAssignmentTest()
        {
            var RosStmt = Parse.Statement(@"string i = 1 + 1;");
            var SSStmt = StatementBuilder.Build(RosStmt);
        }

        [TestMethod]
        public void IfElseStatementTest()
        {
            var RosStmt = Parse.Statement(@"if (true) { int i; } else { int i; }");
            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(SSStmt is SS.IfElseStatement);

            var SSIfElseStmt = (SS.IfElseStatement)SSStmt;

            Assert.IsTrue(SSIfElseStmt.Condition is SS.LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is SS.BlockStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement is SS.BlockStatement);
        }

        [TestMethod]
        public void IfStatementTest()
        {
            var RosStmt = Parse.Statement(@"if (true) { int i; } ");
            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(SSStmt is SS.IfElseStatement);

            var SSIfElseStmt = (SS.IfElseStatement)SSStmt;

            Assert.IsTrue(SSIfElseStmt.Condition is SS.LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is SS.BlockStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement == null);
        }

        [TestMethod]
        public void IfElseStatementExplicitNoBlockTest()
        {
            var RosStmt = Parse.Statement(@"
                if (true)
                    int i;
                else
                    int i;
                ");

            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(SSStmt is SS.IfElseStatement);

            var SSIfElseStmt = (SS.IfElseStatement)SSStmt;

            Assert.IsTrue(SSIfElseStmt.Condition is SS.LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is SS.VariableDeclarationStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement is SS.VariableDeclarationStatement);
        }

        [TestMethod]
        public void IfStatementNoBlockTest()
        {
            var RosStmt = Parse.Statement(@"if (true) int i;");
            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(SSStmt is SS.IfElseStatement);

            var SSIfElseStmt = (SS.IfElseStatement)SSStmt;

            Assert.IsTrue(SSIfElseStmt.Condition is SS.LiteralExpression);
            Assert.IsTrue(SSIfElseStmt.IfStatement is SS.VariableDeclarationStatement);
            Assert.IsTrue(SSIfElseStmt.ElseStatement == null);
        }

        [TestMethod]
        public void ReturnStatementTest()
        {
            var RosStmt = Parse.Statement(@"return 12;");
            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(SSStmt is SS.ReturnStatement);

            var returnStmt = (SS.ReturnStatement)SSStmt;

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
        // Todo: Static member invocation and regular member invocation (none local/this invocation).

        // Todo: Add more statements tests...

        #endregion

        #region Region: Expression Tests

        [TestMethod]
        public void ConditionalExpressionTest()
        {
            var roslynExpression = Parse.Expression("2 > 1 ? 10 : 0");
            var scriptSharpExpression = ExpressionBuilder.Build(roslynExpression);

            Assert.IsTrue(scriptSharpExpression is SS.ConditionalExpression);

            var cExpr = (SS.ConditionalExpression)scriptSharpExpression;

            Assert.IsTrue(cExpr.Condition is SS.BinaryExpression);
            Assert.IsTrue(cExpr.TrueValue is SS.LiteralExpression);
            Assert.IsTrue(cExpr.FalseValue is SS.LiteralExpression);
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
                        Person p = new Person();
                    }
                }";

            var tree = SyntaxTree.ParseText(source);
            var root = tree.GetRoot();

            Assert.IsTrue(Syntax.IsCompleteSubmission(tree));

            var oces = root.DescendantNodes().Where(n => n.Kind == SyntaxKind.ObjectCreationExpression);

            Assert.IsTrue(oces.Count() == 1);

            // Todo: Fuckly hack! This should be changed!
            var SSNe = ((ObjectCreationExpressionSyntax)oces.First()).Map(new SS.ClassSymbol("Person", new ScriptSharp.ScriptModel.NamespaceSymbol("dummyNamespace", null)));
            Assert.IsTrue(SSNe.EvaluatedType.GeneratedName == "Person");
        }

        [TestMethod]
        public void ExpressionUnaryExpressionTest()
        {
            var RosExpr = Parse.Expression(@"-1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(RosExpr is PrefixUnaryExpressionSyntax);
            Assert.IsTrue(SSExpr is SS.UnaryExpression);

            var RosUnaryExpr = (PrefixUnaryExpressionSyntax)RosExpr;
            var SSUnaryExpression = (SS.UnaryExpression)SSExpr;

            Assert.IsTrue(RosUnaryExpr.OperatorToken.Kind == SyntaxKind.MinusToken);
            Assert.IsTrue(SSUnaryExpression.Operator == SS.Operator.Minus);
            Assert.IsTrue(SSUnaryExpression.Operand is SS.LiteralExpression);

            var SSLiteral = (SS.LiteralExpression)SSUnaryExpression.Operand;
            Assert.AreEqual(SSLiteral.Value, 1);
        }


        #region Region: Literal Expression


        [TestMethod]
        public void ExpressionStringLiteralTest()
        {
            var RosExpr = Parse.Expression(@"""foo""");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is SS.LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (SS.LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.ValueText, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, "foo");
        }

        [TestMethod]
        public void ExpressionIntLiteralTest()
        {
            var RosExpr = Parse.Expression(@"1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is SS.LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (SS.LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.Value, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, 1);
        }

        [TestMethod]
        public void ExpressionTrueLiteralTest()
        {
            var RosExpr = Parse.Expression(@"true");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is SS.LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (SS.LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.Value, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, true);
        }

        [TestMethod]
        public void ExpressionFalseLiteralTest()
        {
            var RosExpr = Parse.Expression(@"false");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is SS.LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (SS.LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.Value, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, false);
        }

        [TestMethod]
        public void ExpressionNullLiteralTest()
        {
            var RosExpr = Parse.Expression(@"null");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(RosExpr is LiteralExpressionSyntax);
            Assert.IsTrue(SSExpr is SS.LiteralExpression);

            var RosLiteral = (LiteralExpressionSyntax)RosExpr;
            var SSLiteral = (SS.LiteralExpression)SSExpr;

            Assert.AreEqual(RosLiteral.Token.Value, SSLiteral.Value);
            Assert.AreEqual(SSLiteral.Value, null);
        }


        #endregion

        #region Region: Binary Expression Tests

        [TestMethod]
        public void ExpressionAssignmentTest()
        {
            var RosStmt = Parse.Statements(@"string i = ""foo""; i = ""hello"";").ElementAt(1);
            var SSStmt = StatementBuilder.Build(RosStmt);

            Assert.IsTrue(SSStmt is SS.ExpressionStatement);

            var SSExpr = (SS.BinaryExpression)((SS.ExpressionStatement)SSStmt).Expression;

            Assert.IsTrue(SSExpr.Operator == SS.Operator.Equals);
            Assert.IsTrue(SSExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSExpr.LeftOperand is SS.LocalExpression);
            Assert.IsTrue(((SS.LocalExpression)SSExpr.LeftOperand).Symbol is SS.VariableSymbol);
        }


        [TestMethod]
        public void ExpressionPlusBinaryTest()
        {
            var RosExpr = Parse.Expression("1 + 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.Plus);
        }

        [TestMethod]
        public void ExpressionMinusBinaryTest()
        {
            var RosExpr = Parse.Expression("1 - 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.Minus);
        }

        [TestMethod]
        public void ExpressionMultiplyBinaryTest()
        {
            var RosExpr = Parse.Expression("1 * 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.Multiply);
        }

        [TestMethod]
        public void ExpressionDivideBinaryTest()
        {
            var RosExpr = Parse.Expression("1 / 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.Divide);
        }

        [TestMethod]
        public void ExpressionModulusBinaryTest()
        {
            var RosExpr = Parse.Expression("1 % 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.Mod);
        }


        [TestMethod]
        public void ExpressionRelationalEqualsTest()
        {
            var RosExpr = Parse.Expression("1 == 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.EqualEqual);
        }

        [TestMethod]
        public void ExpressionRelationalNotEqualsTest()
        {
            var RosExpr = Parse.Expression("1 != 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.NotEqual);
        }

        [TestMethod]
        public void ExpressionRelationalGreaterTest()
        {
            var RosExpr = Parse.Expression("1 > 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.Greater);
        }

        [TestMethod]
        public void ExpressionRelationalLessTest()
        {
            var RosExpr = Parse.Expression("1 < 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.Less);
        }

        [TestMethod]
        public void ExpressionRelationalGreaterEqualsTest()
        {
            var RosExpr = Parse.Expression("1 >= 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.GreaterEqual);
        }

        [TestMethod]
        public void ExpressionRelationalLessEqualsTest()
        {
            var RosExpr = Parse.Expression("1 <= 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.LessEqual);
        }

        [TestMethod]
        public void ExpressionLogicalAndTest()
        {
            var RosExpr = Parse.Expression("true && -1 > 6");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.BinaryExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.LogicalAnd);
        }

        [TestMethod]
        public void ExpressionLogicalOrTest()
        {
            var RosExpr = Parse.Expression("true || 1 > -1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);

            Assert.IsTrue(SSExpr is SS.BinaryExpression);

            var SSBinaryExpr = (SS.BinaryExpression)SSExpr;

            Assert.IsTrue(SSBinaryExpr.LeftOperand is SS.LiteralExpression);
            Assert.IsTrue(SSBinaryExpr.RightOperand is SS.BinaryExpression);
            Assert.IsTrue(SSBinaryExpr.Operator == SS.Operator.LogicalOr);
        }






        [TestMethod]
        public void ExpressionNestedBinaryTest()
        {
            var RosExpr = Parse.Expression("1 + 1 + 1");
            var SSExpr = ExpressionBuilder.Build(RosExpr);
        }

        #endregion


        // Todo: Add more expression tests...

        #endregion



    }
}
