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
        public void Namespace_MemberTest()
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
        public void Namespace_MultipleTest()
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
            var ssSymbolSet = Parse.NamespacesToSymbolSet(source);

        }

        [TestMethod]
        public void Class_EmptyMemberTest()
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
        public void Class_MemberTest()
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
        public void Class_MemberDeclarationPredefinedReturnTypeTest()
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
        public void Class_MemberDeclarationCustomReturnTypeTest()
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



    }
}
