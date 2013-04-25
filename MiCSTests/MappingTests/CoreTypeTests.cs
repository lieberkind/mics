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
    public class CoreTypeTests
    {

        [TestMethod]
        public void Regex_CanBeMappedTest()
        {
            var source = @"
                using System.Text.RegularExpressions;
                
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void f() {
                            Regex rx = new Regex(""imapattern""); 
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void Regex_RegexMethodCanBeMapped()
        {
            var source = @"
                using System.Text.RegularExpressions;
                
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void ImARegEx() {
                            Regex rx = new Regex(""regexpattern""); 
                            var b = rx.IsMatch(""targetstring"");
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void String_StringPropertyCanBeMappedTest()
        {
            var source = @"
                using System;
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void ImARegEx() {
                            String s = ""foo"";
                            int i = s.Length;
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void String_MethodCanBeMappedTest()
        {
            var source = @"
                using System;
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void f() {
                            String s = ""foo"";
                            int j = s.IndexOf('o');
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void Array_NewWithElementsTest()
        {
            var source = @"string[] strings = new string[2] { ""Tomas"", ""Asger""};";
            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);

            var ssLiteral = (SS.LiteralExpression)ssStatement.Variables.ElementAt(0).Value;

            Assert.AreEqual("System.Array", ssLiteral.EvaluatedType.FullName);
            Assert.IsTrue(ssLiteral.EvaluatedType.IsArray);

            Assert.IsTrue(ssLiteral.Value is SS.Expression[]);
        }

        [TestMethod]
        public void Array_NewEmptyArrayTest()
        {
            var source = @"string[] strings = new string[2];";
            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariable = ssStatement.Variables.ElementAt(0);

            Assert.AreEqual(ssVariable.ValueType.FullName, "System.Array");

            var ssNewExpression = (SS.NewExpression)ssVariable.Value;

            Assert.AreEqual(ssNewExpression.EvaluatedType.FullName, "System.Array");
        }

        [TestMethod]
        public void Array_ElementGetAccessTest()
        {
            var source = @"
                [MixedSide]
                int f() {
                        string[] strings = new string[2] { ""Tomas"", ""Asger""};
                        return strings[1];
                }";

            var ssReturnStatement = (SS.ReturnStatement)Parse.MethodsToSS(source).First().Statements().ElementAt(1);

            Assert.IsTrue(ssReturnStatement.Value is SS.IndexerExpression);
            Assert.AreEqual("System.String", ssReturnStatement.Value.EvaluatedType.FullName);

            var ssIndexerExpression = (SS.IndexerExpression)ssReturnStatement.Value;

            Assert.AreEqual("System.String", ssIndexerExpression.Indexer.AssociatedType.FullName);
            Assert.AreEqual("System.Int32", ssIndexerExpression.Indices.ElementAt(0).EvaluatedType.FullName);

            // Todo: Fix...
        }

        [TestMethod]
        public void Array_ElementsSetAccessTest()
        {
            var source = @"
                        string[] strings = new string[2];
                        strings[0] = ""Asger"";
                        strings[1] = ""Tomas"";";

            var ssStatements = Parse.StatementsToSS(source);

            // Todo: Make assertions...
        }


        #region Region: Special Core Type Tests

        [TestMethod]
        public void CoreType_VoidTest()
        {
            var source = @"
                        [MixedSide]
                        public void f() {
                           int i = 0; 
                        }";
            var ssMethod = Parse.MethodsToSS(source).First();
            Assert.AreEqual(ssMethod.AssociatedType.FullName, "System.Void");
        }

        // Todo: Fix maybe...
        //[TestMethod]
        //public void CoreType_NullObjectLiteralTest()
        //{
        //    var source = @"return null;";
        //    var statement = Parse.Statement(source);
        //    var ssStatement = Parse.StatementToSS(source);

        //    Assert.IsTrue(ssStatement is SS.ReturnStatement);

        //    var returnStmt = (SS.ReturnStatement)ssStatement;

        //    Assert.IsTrue(returnStmt.Value is SS.LiteralExpression);
        //}

        [TestMethod]
        [ExpectedException(typeof(MemberNotMappedException))]
        public void CoreType_BooleanMemberFailTest()
        {
            var source = @"bool b = true; b.GetType();";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementsToSS(source).First();
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;
            
        }

        [TestMethod]
        public void CoreType_BooleanTest()
        {
            var source = @"Boolean b = true;";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.Boolean");
        }

        [TestMethod]
        public void CoreType_NewBooleanTest()
        {
            var source = @"bool b = new Boolean();";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.Boolean");
        }

        [TestMethod]
        [ExpectedException(typeof(MemberSignatureArgumentTypeNotMappedException))]
        public void CoreType_StringMemberFailTest()
        {
            var source = @"string s = ""foo""; s.IndexOf(""foo"")";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.String");
        }

        [TestMethod]
        public void CoreType_StringMemberFieldTest()
        {
            var source = @"string s = ""foo""; s.Length;";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.String");
        }

        [TestMethod]
        [ExpectedException(typeof(MemberSignatureNotMappedException))]
        public void CoreType_StringMemberFailArgumentCountTest()
        {
            var source = @"string s = ""foo""; s.IndexOf('f', 2)";
            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;
        }

        [TestMethod]
        public void CoreType_StringMemberTest()
        {
            var source = @"string s = ""foo""; s.IndexOf('f')";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.String");
        }

        [TestMethod]
        public void CoreType_StringNewTest()
        {
            var source = @"var s = new String(""foo"");";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.String");
        }

        #endregion

        #region Region: Types

        // Todo: Add Array tests

        [TestMethod]
        public void TypeSymbol_StringTest()
        {
            var source = @"string i = ""foo"";";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            Assert.IsTrue(statement is LocalDeclarationStatementSyntax);
            Assert.IsTrue(ssStatement is SS.VariableDeclarationStatement);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            Assert.IsTrue(declaration is VariableDeclarationSyntax);
            Assert.AreEqual(declaration.Variables.Count, ssDeclaration.Variables.Count);
            Assert.IsTrue(ssDeclaration.Variables.Count == 1);

            var ssVariable = ssDeclaration.Variables.First();

            Assert.AreEqual(ssVariable.ValueType.FullName, "System.String");
        }

        [TestMethod]
        public void TypeSymbol_IntTest()
        {
            var source = @"int i = -1;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)((PrefixUnaryExpressionSyntax)declaration.Variables.First().Initializer.Value).Operand;
            var ssLiteral = (SS.LiteralExpression)((SS.UnaryExpression)ssDeclaration.Variables.First().Value).Operand;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssDeclaration.Variables.ElementAt(0).ValueType.FullName, "System.Int32");
        }

        [TestMethod]
        public void TypeSymbol_UIntTest()
        {
            var source = @"uint i = 0U;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssDeclaration.Variables.ElementAt(0).ValueType.FullName, "System.UInt32");
        }

        [TestMethod]
        public void TypeSymbol_BooleanTest()
        {
            var source = @"bool b = true;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssDeclaration.Variables.ElementAt(0).ValueType.FullName, "System.Boolean");
        }

        [TestMethod]
        public void TypeSymbol_LongTest()
        {
            var source = @"long l = 1L;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssDeclaration.Variables.ElementAt(0).ValueType.FullName, "System.Int64");
        }

        [TestMethod]
        public void TypeSymbol_ULongTest()
        {
            var source = @"ulong l = 1UL;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssDeclaration.Variables.ElementAt(0).ValueType.FullName, "System.UInt64");
        }

        [TestMethod]
        public void TypeSymbol_DecimalTest()
        {
            var source = @"decimal d = 1.0m;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssDeclaration.Variables.ElementAt(0).ValueType.FullName, "System.Decimal");
        }

        [TestMethod]
        public void TypeSymbol_DoubleTest()
        {
            var source = @"double d = 1.0d;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssDeclaration.Variables.ElementAt(0).ValueType.FullName, "System.Double");
        }

        [TestMethod]
        public void TypeSymbol_ShortTest()
        {
            var source = @"short s = (short)1;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)((CastExpressionSyntax)declaration.Variables.First().Initializer.Value).Expression;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssDeclaration.Variables.ElementAt(0).ValueType.FullName, "System.Int16");
        }

        [TestMethod]
        public void TypeSymbol_UShortTest()
        {
            var source = @"ushort s = (ushort)1;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)((CastExpressionSyntax)declaration.Variables.First().Initializer.Value).Expression;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
            Assert.AreEqual(ssDeclaration.Variables.ElementAt(0).ValueType.FullName, "System.UInt16");
        }

        #endregion


    }
}
