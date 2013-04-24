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
    /// <summary>
    /// Summary description for MapTests
    /// </summary>
    [TestClass]
    public class MappingTests
    {

//        [TestMethod]
//        public void MiCSManagerTest()
//        {
//            var source = @"
//            namespace TestNamespace { 
//                class TestClass { 
//                    void f() { }
//                } 
//                class TestClass2 { 
//                    [MixedSide]
//                    void f() { }
//                } 
//            }";

//            MiCSManager.Initiate(source);
//            Assert.IsTrue(((NamespaceDeclarationSyntax)MiCSManager.CompilationUnit.Members[0]).Members.Count == 1);
//        }

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
        public void NewArrayWithElementsCanBeMapped()
        {
            var source = @"
            using System;
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() 
                    {
                        string[] strings = new string[2] { ""Tomas"", ""Asger""};
                    }
                } 
            }";

            var ssStmt = Parse.StatementToSS(@"string[] strings = new string[2] { ""Tomas"", ""Asger""};");
        }

        [TestMethod]
        public void NewEmptyArrayCanBeMapped()
        {
            var source = @"
            using System;
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() 
                    {
                        string[] strings = new string[2];
                    }
                } 
            }";

            //Parse.NamespacesToSymbolSet(source);
            var st = SyntaxTree.ParseText(source);

            var m = (LocalDeclarationStatementSyntax)st.GetRoot().DescendantNodes().Where(n => n is LocalDeclarationStatementSyntax).First();

            //throw new NotImplementedException();
        }

        [TestMethod]
        public void ArrayElementAccessCanBeMapped()
        {
            var source = @"
            using System;
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    string f() 
                    {
                        string[] strings = new string[2] { ""Tomas"", ""Asger""};
                        return strings[1];
                    }
                } 
            }";

            Parse.NamespacesToSymbolSet(source);
        }

        [TestMethod]
        public void NewElementsCanBeAddedToArray()
        {
            var source = @"
            using System;
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() 
                    {
                        string[] strings = new string[2];
                        strings[0] = ""Asger"";
                        strings[1] = ""Tomas"";
                    }
                } 
            }";

            Parse.NamespacesToSymbolSet(source);
        }

        [TestMethod]
        public void ForLoopCanBeMapped()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() 
                    { 
                        int count = 0;
                        int i;
                        for(i = 0; i < 10; i = i + 1)
                        {
                            count = count + i;
                        }
                    }
                } 
            }";

            var st = SyntaxTree.ParseText(source);

            var forStmt = (ForStatementSyntax)st.GetRoot().DescendantNodes().Where(n => n is ForStatementSyntax).First();

            var ssForStmt = (SS.ForStatement)StatementBuilder.Build(forStmt, null, null);

            Assert.IsNotNull(ssForStmt.Initializers);
            Assert.IsNotNull(ssForStmt.Body);
            Assert.IsNotNull(ssForStmt.Condition);
            Assert.IsNotNull(ssForStmt.Increments);
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
                    void f() { int i = 0; }
                } 
            }";

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
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();

            var method = (MethodDeclarationSyntax)member.Members.First();
            var ssMethod = (ScriptSharp.ScriptModel.MethodSymbol)ssMember.Members.First();

            var returnTypeName = ((PredefinedTypeSyntax)method.ReturnType).Keyword.ValueText;
            Assert.AreEqual(returnTypeName, ssMethod.AssociatedType.Name.ToLower());

            var statement = method.Body.Statements.First();
            var ssStatement = ssMethod.Implementation.Statements.First();

            Assert.IsTrue(statement is LocalDeclarationStatementSyntax);
            Assert.IsTrue(ssStatement is SS.VariableDeclarationStatement);
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
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();

            var method = (MethodDeclarationSyntax)member.Members.First();
            var ssMethod = (ScriptSharp.ScriptModel.MethodSymbol)ssMember.Members.First();

            var returnTypeName = ((IdentifierNameSyntax)method.ReturnType).Identifier.ValueText;
            Assert.AreEqual(returnTypeName, ssMethod.AssociatedType.Name);

        }

        [TestMethod]
        public void MultipleNamespaceTest()
        {
            var source = @"
            using System.Html;
            namespace TestNamespace1 { 
                class TestClass1 { 
                    [MixedSide]
                    void g() { Document.HasFocus(); }
                }
            }
            namespace TestNamespace2 { 
                class TestClass2 { 
                    [MixedSide]
                    void f() { Document.HasFocus(); }
                }
            }
            ";

            var namespaces = Parse.Namespaces(source);

        }

        [TestMethod]
        public void DuplicateFunctionsInDifferentClassesTest()
        {
            var source = @"
            namespace TestNamespace1 { 
                class TestClass1 { 
                    [MixedSide]
                    void f() { int i; }
                }

                class TestClass2 { 
                    [MixedSide]
                    void f() { int i; }
                }
            }
            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

        }

        [TestMethod]
        public void DuplicateClassesInDifferentNamespacesTest()
        {
            var source = @"
            namespace TestNamespace1 { 
                class TestClass1 { 
                    [MixedSide]
                    void f() { int i; }
                }
            }
            namespace TestNamespace2 {
                class TestClass1 { 
                    [MixedSide]
                    void g() { int i; }
                }
            }
            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

        }

        [TestMethod]
        public void BuiltInTranslationTest()
        {
            var source = @"
            using System.Html;

            namespace TestNamespace { 
                class TestClass { 
                    [ClientSide]
                    void f() { Document.HasFocus(); }
                }
            }

            namespace TestNameSpace.Nested.NestedAgain.Andagain {
                class Lol {
                    [MixedSide]
                    void lol() { }
                }
            }

            ";
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var ssClass = (SS.ClassSymbol)ssNamespace.Types.ElementAt(0);
        }

        [TestMethod]
        public void BuiltInTranslationTest2()
        {
            var source = @"
            using System.Html;

            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() { Document.HasFocus(); }
                }
            }

            namespace TestNameSpace.Nested.NestedAgain.Andagain {
                class Lol {
                    [MixedSide]
                    void lol() { }
                }
            }

            ";
            SyntaxTree st = SyntaxTree.ParseText(source);

            MiCSManager.Initiate(st);

            var c = new Collector(st.GetRoot());
            c.Collect();

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var ssClass = (SS.ClassSymbol)ssNamespace.Types.ElementAt(0);
        }

        [TestMethod]
        public void BuiltInElementTranslationTest()
        {
            var source = @"
            using System.Html;
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    public void f() { Element e = new Element(); var e2 = Document.GetElementById(""ewjde""); }
                }

                class Person
                {
                    TestClass g()
                    {
                    
                    }
                }
            }";
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void BuiltInTestTranslationTest()
        {
            var source = @"Element e = new Element(); var e2 = Document.GetElementById(""ewjde"");";
            var ssStatement = Parse.StatementsToSS(source);
        }


                   




        #region Region: Statement Test




        #region Region: Testing TypeSymbol Mapping

        [TestMethod]
        public void TypeSymbolDeclarationTest()
        {
            string source = @"int i;";
            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementsToSS(source).First();
            var statement = (LocalDeclarationStatementSyntax)Parse.Statement(source);

            var @type = TypeSymbolGetter.GetTypeSymbol(statement.Declaration.Type);
            Assert.IsTrue(@type is NamedTypeSymbol);
            Assert.IsTrue(@type.Name.Equals("Int32"));

            var ssVariable = ssStatement.Variables.ElementAt(0);
            Assert.IsTrue(ssVariable.ValueType.Name.Equals("Int32"));

        }

        [TestMethod]
        public void TypeSymbolInvocationTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    int f() { return 3; }

                    [MixedSide]
                    int g() { return f(); }
                } 

            }";
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var invocation = (InvocationExpressionSyntax)@namespace.DescendantNodes().Where(n => n.Kind == SyntaxKind.InvocationExpression).First();

            var type = TypeSymbolGetter.GetReturnType((SimpleNameSyntax)invocation.Expression);
          
            Assert.AreEqual(type.Name, "Int32");

        }

        [TestMethod]
        public void TypeSymbolFieldTest()
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

            var memberAccess = (MemberAccessExpressionSyntax)@namespace.DescendantNodes().Where(n => n.Kind == SyntaxKind.MemberAccessExpression).First();

            var ssMethod = (SS.MethodSymbol)ssNamespace.Types.ElementAt(0).Members.ElementAt(0);
            var ssReturnStatement = (SS.ReturnStatement)ssMethod.Implementation.Statements.ElementAt(1);
            Assert.IsTrue(ssReturnStatement.Value is SS.FieldExpression);

            var ssFieldExpression = (SS.FieldExpression)ssReturnStatement.Value;
            Assert.IsTrue(ssFieldExpression.Field.AssociatedType.Name.Equals("Int32"));
        }

        [TestMethod]
        public void StatementVariableDeclarationStringAssignmentTest()
        {
            var source = @"string i = ""foo"";";
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
        public void StatementVariableDeclarationIntAssignmentTest()
        {
            var source = @"int i = -1;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)((PrefixUnaryExpressionSyntax)declaration.Variables.First().Initializer.Value).Operand;
            var ssLiteral = (SS.LiteralExpression)((SS.UnaryExpression)ssDeclaration.Variables.First().Value).Operand;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationUIntAssignmentTest()
        {
            var source = @"uint i = 0U;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationBooleanAssignmentTest()
        {
            var source = @"int b = true;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationLongAssignmentTest()
        {
            var source = @"long l = 1L;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void StatementVariableDeclarationULongAssignmentTest()
        {
            var source = @"ulong l = 1UL;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationDecimalAssignmentTest()
        {
            var source = @"decimal d = 1.0m;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationDoubleAssignmentTest()
        {
            var source = @"double d = 1.0d;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)declaration.Variables.First().Initializer.Value;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationShortAssignmentTest()
        {
            var source = @"short s = (short)1;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)((CastExpressionSyntax)declaration.Variables.First().Initializer.Value).Expression;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        [TestMethod]
        public void StatementVariableDeclarationUShortAssignmentTest()
        {
            var source = @"ushort s = (ushort)1;";
            var statement = Parse.Statement(source);
            var ssStatement = Parse.StatementToSS(source);

            var declaration = ((LocalDeclarationStatementSyntax)statement).Declaration;
            var ssDeclaration = (SS.VariableDeclarationStatement)ssStatement;

            var literal = (LiteralExpressionSyntax)((CastExpressionSyntax)declaration.Variables.First().Initializer.Value).Expression;
            var ssLiteral = (SS.LiteralExpression)ssDeclaration.Variables.First().Value;

            Assert.AreEqual(literal.Token.Value, ssLiteral.Value);
        }

        #endregion





        #endregion


    }
}
